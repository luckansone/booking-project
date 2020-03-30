USE master

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'booking')
DROP DATABASE booking

CREATE DATABASE booking

USE booking


CREATE TABLE Person(
	PersonId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(25) NOT NULL,
	Surname varchar(25) NOT NULL, 
	Patronymic varchar(25) NOT NULL,
	DateOfBirth datetime NOT NULL,
	Email varchar(25) NOT NULL unique, 
	Phone varchar(25) NOT NULL
);
 

CREATE TABLE DiscountType(
	DiscountTypeId int IDENTITY(1,1) PRIMARY KEY,
	TypeName varchar(255) NOT NULL
);

GO
CREATE NONCLUSTERED INDEX IX_DiscountType
	ON dbo.DiscountType(TypeName);
GO

CREATE TABLE Discount(
	DiscountId int IDENTITY(1,1) PRIMARY KEY,
	DiscountTypeId int NOT NULL,
	Name varchar(255) NOT NULL,
	Value double precision NOT NULL,
	CONSTRAINT FK_DiscountTypeDiscount FOREIGN KEY (DiscountTypeId)
	REFERENCES DiscountType(DiscountTypeId)
);

CREATE TABLE Point(
	PointId int IDENTITY(1,1) PRIMARY KEY, 
	X double precision NOT NULL,
	Y double precision NOT NULL
);

CREATE TABLE Address(
	AddressId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(255) NOT NULL,
	Number varchar(5) NOT NULL,
	PointId int NOT NULL,
	CONSTRAINT FK_PointAddress FOREIGN KEY (PointId)
	REFERENCES Point(PointId)
);

GO
CREATE NONCLUSTERED INDEX IX_Address_Number
	ON dbo.Address(Number)
GO

CREATE TABLE Station(
	StationId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(25) NOT NULL,
	AddressId int NOT NULL,
	CONSTRAINT FK_AddressStation FOREIGN KEY (AddressId)
	REFERENCES Address(AddressId)
);

GO
CREATE NONCLUSTERED INDEX IX_Station_Name
	ON dbo.Station(Name)
GO

CREATE TABLE DestinationMap(
	Id int IDENTITY(1,1) PRIMARY KEY,
	FromStationId int NOT NULL,
	ToStationId int NOT NULL,
	Destination double precision NOT NULL,
	CONSTRAINT FK_StationDestination1 FOREIGN KEY (FromStationId)
	REFERENCES Station(StationId),
	CONSTRAINT FK_StationDestination2 FOREIGN KEY (ToStationId)
	REFERENCES Station(StationId)
);

CREATE TABLE Route(
	RouteId int IDENTITY(1,1) PRIMARY KEY,
	Description varchar(255)
);

CREATE TABLE RouteStation(
	Id int IDENTITY(1,1) PRIMARY KEY,
	OrderValue int NOT NULL,
	RouteId int NOT NULL,
	StationId int NOT NULL,
	CONSTRAINT FK_StationRouteStation FOREIGN KEY (StationId)
	REFERENCES Station(StationId),
	CONSTRAINT FK_RouteRouteStation FOREIGN KEY (RouteId)
	REFERENCES Route(RouteId)
);

CREATE TABLE TrainType(
	TrainTypeId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(255) NOT NULL,
	Speed double precision NOT NULL
);

GO
CREATE NONCLUSTERED INDEX IX_TrainType_Name
	ON dbo.TrainType(Name)
GO

CREATE TABLE CarriageType(
	CarriageTypeId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(255) NOT NULL
);

GO
CREATE NONCLUSTERED INDEX IX_CarriageType_Name
	ON dbo.CarriageType(Name)
GO

CREATE TABLE Train(
	TrainId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(255) NOT NULL,
	DepartureTime datetime NOT NULL,
	TrainTypeId int NOT NULL,
	RouteId int NOT NULL,
	CONSTRAINT FK_TrainTypeTrain FOREIGN KEY (TrainTypeId)
	REFERENCES TrainType(TrainTypeId),
	CONSTRAINT FK_RouteTrain FOREIGN KEY (RouteId)
	REFERENCES Route(RouteId)
);

CREATE TABLE Carriage(
	CarriageId int IDENTITY(1,1) PRIMARY KEY,
	Number int NOT NULL,
	CountOfSeats int NOT NULL,
	CarriageTypeId int NOT NULL,
	TrainId int NOT NULL,
	CONSTRAINT FK_CarriageTypeCarriage FOREIGN KEY (CarriageTypeId)
	REFERENCES CarriageType(CarriageTypeId),
	CONSTRAINT FK_TrainCarriage FOREIGN KEY (TrainId)
	REFERENCES Train(TrainId)
);

CREATE TABLE ReservedSeat(
	SeatId int IDENTITY(1,1) PRIMARY KEY,
	CarriageId int NOT NULL,
	Number int NOT NULL,
	CONSTRAINT FK_CarriageSeat FOREIGN KEY (CarriageId)
	REFERENCES Carriage(CarriageId)
);


CREATE TABLE Ticket(
	TicketId int IDENTITY(1,1) PRIMARY KEY,
	Price double precision NOT NULL,
	SeatId int NOT NULL,
	RouteId int NOT NULL,
	CONSTRAINT FK_ReservedSeatTicket FOREIGN KEY (SeatId)
	REFERENCES ReservedSeat(SeatId),
	CONSTRAINT FK_RouteTicket FOREIGN KEY (RouteId)
	REFERENCES Route(RouteId)
);

CREATE TABLE "Order"(
	OrderId int IDENTITY(1,1) PRIMARY KEY,
	PersonId int NOT NULL,
	DiscountId int NULL,
	TicketId int NOT NULL,
	CONSTRAINT FK_PersonOrder FOREIGN KEY (PersonId)
	REFERENCES Person(PersonId),
	CONSTRAINT FK_DiscountOrder FOREIGN KEY (DiscountId)
	REFERENCES Discount(DiscountId),
	CONSTRAINT FK_TicketOrder FOREIGN KEY (TicketId)
	REFERENCES Ticket(TicketId)
);





