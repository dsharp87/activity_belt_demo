using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_belt_demo.Models
{
    public class ActivityPlanViewModel
    {

        [Required]
        [MinLength(2)]
        public string Title { get; set; }
        
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime StartDate { get; set; }

        public int Duration { get; set; }

        public string DurationType { get; set; }
        
        [Required]
        [MinLength(10)]
        public string Description { get; set; }

    }

        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                bool valid = false;
                if (value != null && (DateTime)value > DateTime.Now) {
                    valid = true;
                }
                return valid; 
                // return value != null && (DateTime)value > DateTime.Now;
            }
        }  
}