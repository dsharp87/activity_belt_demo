using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_belt_demo.Models
{
    public class User: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID {get; set;}

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<ActivityPlan> MyActivityPlans { get; set; }

        public List<UserActivityPlan> AttendingActivities { get; set; }


        public User() {
            MyActivityPlans = new List<ActivityPlan>();
            AttendingActivities = new List<UserActivityPlan>();
        }


    }
}