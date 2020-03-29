using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace Booking.WEB.ViewModels
{
    public class SearchTrainsViewModel
    {

     
        [Display(Name ="Звідки")]
        [Required]
        public string FromStation { get; set; }

        [Display(Name = "Куди")]
        [Required]

        public string ToStation { get; set; }

        [Required]
        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfDeparture { get; set; }

        [Display(Name = "Час від")]
        [Required]
        public string TimeOfDeparture { get; set; }

        public SelectList times { get; set; }

        public SearchTrainsViewModel()
        {
            List<string> timeRange = new List<string>
            {
                "12:00:00",
                "15:00:00",
                "18:00:00"
            };

            times = new SelectList(timeRange);
        }

      
    }
}
