using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_belt_demo.Models
{
    public class ActivityPlan: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityPlanID {get; set;}

        public string Title { get; set; }
        
        public DateTime StartDate { get; set; }

        public int Duration { get; set; }

        public string DurationType { get; set; }
        
        public string Description { get; set; }

        public int UserID {get; set;}

        public User User {get; set;}

        public List<UserActivityPlan> AttendingUsers {get; set;}

        public ActivityPlan()
        {
            AttendingUsers = new List<UserActivityPlan>();
        }

    }
}