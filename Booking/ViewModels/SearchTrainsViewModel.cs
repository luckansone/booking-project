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
        [Required(ErrorMessage = "Введіть дані.")]
        public string FromStation { get; set; }

        
        [Display(Name = "Куди")]
        [Required(ErrorMessage ="Введіть дані.")]
        public string ToStation { get; set; }

        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateOfDeparture { get; set; }

        
        [Display(Name = "Час від")]
        [Required(ErrorMessage = "Введіть дані.")]
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
