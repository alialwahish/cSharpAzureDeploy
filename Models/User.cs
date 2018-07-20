
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace cSharpBelt
{

    public class User
    {


        [Key]
        [Required]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Field Required")]
        [RegularExpression("^[a-zA-Z ]*$"
    , ErrorMessage = "Letters Only!")]
        [MinLength(2)]

        public string First_Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [RegularExpression("^[a-zA-Z ]*$"
    , ErrorMessage = "Letters Only!")]
        [MinLength(2)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [MinLength(8)]
        
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Don't match")]
        public string Confirm_Password { get; set; }


        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public int? ActivitiesId { get; set; }

        public List<Activities> Activities {get;set;}
        
        public int? IntrstsId {get;set;}
        
        public List<Intrsts> Intrsts { get; set; }

        public User (){
            Activities = new List<Activities>();
            Intrsts = new List<Intrsts>();
        }

    }

}