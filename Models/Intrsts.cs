using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace cSharpBelt
{
    public class Intrsts{
        [Key]
        public int IntrstsId {set;get;}

        public int UserId {get;set;}
        public User User {get;set;}

        public int ActivitiesId {get;set;}
        public Activities Activities {get;set;}

        




    }







}