﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee
    {
        public int Id { get; set; }

        [Display(Name = "Document")]
        public int Document { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "the field {0} can contain {1} characters length.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "the field {0} can contain {1} characters length.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public string? FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public string FullNameWithDocument { get { return $"{FirstName} {LastName} {Document}"; } }

        public User User { get; set; }
    }
}
