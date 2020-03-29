using Booking.Interfaces;
using Booking.ViewModels;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Booking.Services
{
    public class PdfCreator : IPdfCreator
    {
        public void CreatePdf(List<TicketViewModel> ticketInfo)
        {
            foreach (var el in ticketInfo)
            {
                PdfWriter writer = new PdfWriter(String.Format(HostingEnvironment.MapPath("/App_Data/ticket{0}.pdf"), el.SeatId));
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                PdfFont font = PdfFontFactory.CreateFont(HostingEnvironment.MapPath("/App_Data/Alice-Regular.ttf"), PdfEncodings.IDENTITY_H, true);
                document.Add(new Paragraph("Білет").SetFont(font));
                List list = new List().SetSymbolIndent(12).SetListSymbol("\u2022").SetFont(font);
                list.Add(new ListItem(String.Format("ФІО: {0}", el.SNP)));
                list.Add(new ListItem(String.Format("Маршрут: {0}", el.Description)));
                list.Add(new ListItem(String.Format("Час відправлення - час прибуття: {0} - {1}", el.DepartureTime, el.ArrivalTime)));
                list.Add(new ListItem(String.Format("Номер потяга: {0}", el.TrainName)));
                list.Add(new ListItem(String.Format("Номер вагона: {0}", el.CarriageNumber)));
                list.Add(new ListItem(String.Format("Номер місця: {0}", el.SeatNumber)));
                list.Add(new ListItem(String.Format("Ціна: {0}", el.Price)));
                document.Add(list.SetFont(font));
                document.Close();
            }
        }
    }
}