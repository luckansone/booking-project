using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class PersonViewModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Введіть ім'я.")]
        public string Name { get; set; }

        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Введіть прізвище.")]
        public string Surname { get; set; }
        [Display(Name = "По батькові")]
        [Required(ErrorMessage = "Введіть по батькові.")]
        public string Patronymic { get; set; }

        [Display(Name = "Дата народження")]
        [Required(ErrorMessage = "Введіть дату народження.")]
        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Введіть Email.")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Введіть номер телефону.")]
        public string Phone { get; set; }
    }
}