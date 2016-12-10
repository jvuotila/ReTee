using System.ComponentModel.DataAnnotations;

namespace ReTeeUmbraco.Models
{
    public class MemberRegistrationFormViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Etunimi")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Sukunimi")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Anna validi sähköpostiosoite")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Sähköpostiosoite")]
        //[StringLength(50, MinimumLength = 3)]
        public string EmailAddress { get; set; }

        [Required]
        //[DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Puhelinnumero")]
        public string PhoneNumber { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [StringLength(10, MinimumLength = 8)]
        [Display(Name = "Syntymäpäivä")]
        public string DateOfBirth { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Katuosoite")]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Postinumero")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Postitoimipaikka")]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Jäsentyyppi")]
        public string MemberType { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Ensisijainen frisbeegolfseurani")]
        public string SupportMemberClub { get; set; }

        [Display(Name = "PDGA-pelaajalisenssi")]
        public bool PDGALicense { get; set; }

        [StringLength(7, MinimumLength = 1)]
        [Display(Name = "PDGA-pelaajalisenssinumero")]
        public string PDGALicenseNumber { get; set; }

        [Display(Name = "PDGA-pelaajakiekko")]
        public bool PDGAMemberDisc { get; set; }

        [Display(Name = "Tilaan PDGA Disc Golfer Magazine -lehden")]
        public bool DiscGolferMagazine { get; set; }

        [Required]
        [Range(13,13, ErrorMessage = "Vastaus ei ole oikein.")]
        [Display(Name = "Paljonko on viisi plus kahdeksan?")]
        public decimal checkCode { get; set; }

        //[Required]
        //[StringLength(2500, MinimumLength = 10)]
        //[DataType(DataType.MultilineText)]
        //public string Message { get; set; }
    }
}