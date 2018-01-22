namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        [MinLength(UserNameMinLength)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<Course> Trainings { get; set; } = new HashSet<Course>();

        public ICollection<UserCourse> Courses { get; set; } = new HashSet<UserCourse>();

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}
