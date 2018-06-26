using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_belt_demo.Models
{
    public class UserActivityPlan: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserActivityPlanID {get; set;}

        public int UserID {get; set;}

        public User User {get; set;}

        public int ActivityPlanID {get; set;}

        public ActivityPlan ActivityPlan {get; set;}

    }
}