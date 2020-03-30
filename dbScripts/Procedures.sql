USE booking


--DROP FUNCTION dbo.CalculateDistance
GO
CREATE FUNCTION CalculateDistance (@RouteId int)
RETURNS double precision
AS
BEGIN

DECLARE @Distance double precision

SET @Distance = (SELECT SUM(Destination)
	FROM RouteStation AS rost
INNER JOIN RouteStation AS roonst
ON rost.RouteId = roonst.RouteId AND rost.OrderValue + 1 = roonst.OrderValue
INNER JOIN DestinationMap AS map
ON map.FromStationId = rost.StationId AND map.ToStationId = roonst.StationId 
WHERE rost.RouteId = @RouteId);

RETURN @Distance
END;
GO

SELECT dbo.CalculateDistance(1)
--DROP FUNCTION dbo.ConvertHoursToTime
GO
Create Function ConvertHoursToTime( @RouteId int, @Speed double precision)
RETURNS varchar(50)
AS
BEGIN

RETURN convert(varchar, convert(int,floor(dbo.CalculateDistance(@RouteId)/@Speed)))+':'+
	replicate('0',(2 - len(convert (varchar, convert(int,(dbo.CalculateDistance(@RouteId)/@Speed - floor(dbo.CalculateDistance(@RouteId)/@Speed)) * 60.0)))))
	+convert (varchar, convert(int,(dbo.CalculateDistance(@RouteId)/@Speed - floor(dbo.CalculateDistance(@RouteId)/@Speed)) * 60.0))+':00';

END;
GO

--DROP PROCEDURE dbo.GetTravelDescription
GO
CREATE  PROCEDURE GetTravelDescription @DepartureDate datetime, @FromStation varchar(25), @ToStation varchar(25)
AS

CREATE table  #TempRoute(

	RouteId int
)

INSERT INTO #TempRoute
SELECT rost.RouteId
	FROM RouteStation AS rost
INNER JOIN RouteStation AS roonst
ON rost.RouteId = roonst.RouteId  AND rost.OrderValue < roonst.OrderValue
INNER JOIN Station AS st1
ON rost.StationId = st1.StationId
INNER JOIN Station AS st2
ON roonst.StationId = st2.StationId
WHERE st2.StationId!= st1.StationId
AND st1.Name LIKE CONCAT('%', @FromStation, '%')
AND st2.Name LIKE CONCAT('%',@ToStation,'%')

DECLARE @RouteId int;
SET @RouteId = (SELECT RouteId FROM Route
WHERE Description LIKE CONCAT(@FromStation, '-', @ToStation));

--Виводжу загальну інформацію про потяги
SELECT @RouteId AS RouteId, Description, tr.TrainId ,
	tr.Name AS TrainName,
	tr.DepartureTime,
	tr.DepartureTime + CAST((dbo.ConvertHoursToTime(@RouteId, trtype.Speed )) AS datetime) AS ArrivalTime,
	 dbo.ConvertHoursToTime (@RouteId, trtype.Speed)AS Duration
	FROM Route AS r
INNER JOIN Train AS tr
ON tr.RouteId = r.RouteId
INNER JOIN TrainType AS trtype
ON tr.TrainTypeId = trtype.TrainTypeId
INNER JOIN #TempRoute AS tempr
ON r.RouteId = tempr.RouteId
WHERE tr.DepartureTime >=@DepartureDate AND CONVERT(date, tr.DepartureTime) = CONVERT(date, @DepartureDate) 

CREATE table  #TempAllSeats(

	TrainId int,
	Name varchar(Max),
	CountOfSeats int
)

CREATE table #ReservedSeats(

	TrainId int,
	Name varchar(Max),
	CountOfSeats int
)

INSERT INTO #TempAllSeats
SELECT 
		tr.TrainId AS TrainId, cartype.Name AS Name, SUM(car.CountOfSeats) AS CountOfSeats
	FROM Train AS tr
INNER JOIN Route as r
ON tr.RouteId = r.RouteId
INNER JOIN Carriage AS car
ON car.TrainId = tr.TrainId
INNER JOIN CarriageType AS cartype
ON car.CarriageTypeId = cartype.CarriageTypeId
INNER JOIN #TempRoute AS tempr
ON r.RouteId = tempr.RouteId
WHERE tr.DepartureTime >=@DepartureDate AND CONVERT(date, tr.DepartureTime) = CONVERT(date, @DepartureDate) 
GROUP BY  tr.TrainId, cartype.Name

INSERT INTO #ReservedSeats
SELECT  tr.TrainId, cartype.Name, COUNT(t.SeatId) AS ReservedSeats  FROM Ticket as t
INNER JOIN ReservedSeat as seat
ON t.SeatId = seat.SeatId
INNER JOIN Carriage AS car
ON car.CarriageId = seat.CarriageId
INNER JOIN CarriageType AS cartype
ON car.CarriageTypeId = cartype.CarriageTypeId
INNER JOIN Train AS tr
ON tr.TrainId = car.TrainId
INNER JOIN Route AS r
ON r.RouteId = tr.RouteId
INNER JOIN #TempRoute AS tempr
ON r.RouteId = tempr.RouteId
WHERE tr.DepartureTime >=@DepartureDate AND CONVERT(date, tr.DepartureTime) = CONVERT(date, @DepartureDate) 
GROUP BY  tr.TrainId, cartype.Name 

--кількість вільних місць по типу вагона
SELECT allseat.TrainId, allseat.Name, CASE WHEN (allseat.TrainId = resseat.TrainId AND allseat.Name = resseat.Name)  THEN
(allseat.CountOfSeats - resseat.CountOfSeats) ELSE allseat.CountOfSeats END AS FreeSeats 
FROM #TempAllSeats AS allseat
LEFT JOIN #ReservedSeats AS resseat
ON allseat.TrainId = resseat.TrainId AND allseat.Name = resseat.Name;


Drop Table #TempAllSeats
Drop Table #ReservedSeats
Drop Table #TempRoute
GO

EXEC dbo.GetTravelDescription @DepartureDate = '04.25.2020 09:00:00', @FromStation ='Київ', @ToStation = 'Рівне'


DROP PROCEDURE dbo.GetTrainInfo
GO
CREATE PROCEDURE GetTrainInfo @TrainId int
AS

CREATE TABLE #TempResSeatsInfo(
		CarriageId int,
		CountOfReservedSeats int,
		NameType varchar(50)
);

INSERT INTO #TempResSeatsInfo
	SELECT car.CarriageId,Count(seat.Number) AS CountSeat, cartype.Name AS NameType FROM Ticket as t
	INNER JOIN ReservedSeat as seat
	ON t.SeatId = seat.SeatId
	INNER JOIN Carriage AS car
	ON car.CarriageId = seat.CarriageId
	INNER JOIN CarriageType AS cartype
	ON car.CarriageTypeId = cartype.CarriageTypeId
	INNER JOIN Train AS tr
	ON tr.TrainId = car.TrainId
	INNER JOIN Route AS r
	ON r.RouteId = tr.RouteId
	WHERE tr.TrainId = @TrainId
	Group By car.CarriageId, cartype.Name;

SELECT tr.TrainId AS TrainId, car.CarriageId, car.Number, cartype.Name AS Name, car.CountOfSeats AS CountOfSeats,
CASE WHEN (car.CarriageId = inf.CarriageId)
THEN car.CountOfSeats - inf.CountOfReservedSeats
ELSE car.CountOfSeats END AS FreeSeats
FROM Train AS tr
INNER JOIN Route as r
ON tr.RouteId = r.RouteId
INNER JOIN Carriage AS car
ON car.TrainId = tr.TrainId
INNER JOIN CarriageType AS cartype
ON car.CarriageTypeId = cartype.CarriageTypeId
LEFT JOIN #TempResSeatsInfo as inf
ON inf.CarriageId = car.CarriageId
WHERE tr.TrainId=@TrainId;

SELECT car.CarriageId, seat.Number AS Number FROM Ticket as t
	INNER JOIN ReservedSeat as seat
	ON t.SeatId = seat.SeatId
	INNER JOIN Carriage AS car
	ON car.CarriageId = seat.CarriageId
	INNER JOIN Train AS tr
	ON tr.TrainId = car.TrainId
	WHERE tr.TrainId = @TrainId;

DROP Table #TempResSeatsInfo;

GO

EXEC dbo.GetTrainInfo @TrainId = 2


DROP PROCEDURE dbo.AddPerson
GO
CREATE PROCEDURE AddPerson @Name varchar(50), @Surname varchar(50), @Patronymic varchar(50), @DateOfBirth date, 
@Email varchar(50), @Phone varchar(50)
AS

IF ((SELECT COUNT(*) FROM Person WHERE Name = @Name AND Surname = @Surname AND Patronymic = @Patronymic AND DateOfBirth =@DateOfBirth )>0 )
BEGIN
UPDATE Person
SET Email = @Email ,Phone = @Phone
WHERE Name = @Name AND Surname = @Surname AND @Patronymic = Patronymic AND DateOfBirth = @DateOfBirth
SELECT CAST(PersonId as int) FROM Person WHERE Name = @Name AND Surname = @Surname AND @Patronymic = Patronymic AND DateOfBirth = @DateOfBirth;
END
ELSE
BEGIN
INSERT INTO Person(Name, Surname,Patronymic , DateOfBirth, Email, Phone)
VALUES (@Name, @Surname, @Patronymic, @DateOfBirth, @Email, @Phone);
SELECT CAST(SCOPE_IDENTITY() as int)
END
GO

EXEC dbo.AddPerson @Name = 'Олена', @Surname = 'Патріюк', @Patronymic='Андріївна',
@DateOfBirth= '10.03.1999',@Email = 'pun02@gmail.com', @Phone='+3806458666'; 

