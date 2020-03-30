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
            times = new SelectList(InitializeTimeRange());
        }

        private static List<string> InitializeTimeRange()
        {
            List<string> timeRange = new List<string>();

            for(int i = 0; i <= 9; i++)
            {
                timeRange.Add(String.Format("0{0}:00:00", i));
            }

            for(int i = 10; i < 24; i++)
            {
                timeRange.Add(String.Format("{0}:00:00", i));
            }

            return timeRange;
        }

        

      
    }
}
