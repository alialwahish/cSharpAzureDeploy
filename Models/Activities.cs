using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace cSharpBelt{

    public class Activities{

        [Key]
        [Required]
        public int ActivitiesId { get; set; }

        [Required]
        [MinLength(2)]
        public string Title {get;set;}


        [Required]
        public DateTime Time {get;set;}


        [Required]
        public DateTime Date {get;set;}

        [Required]

        public int NumOfPrts {get;set;}


        [Required]
       
        public int  Duration {get;set;}

        [Required]
        public string durType {get;set;}

        [Required]
        [MinLength(10)]
        public string Description {get;set;}
        
        public int UserId {get;set;}


        public string Creator {get;set;}
        public int? IntrstsId {get;set;}

        public List<Intrsts> Intrsts {get;set;}

        public Activities(){

            NumOfPrts =0;
            Intrsts= new List<Intrsts>();
        }




    }






    
}