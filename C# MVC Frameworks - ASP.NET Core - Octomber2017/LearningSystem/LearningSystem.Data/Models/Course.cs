namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CourseNameMaxLength)]
        [MinLength(CourseNameMinLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        [MinLength(CourseDescriptionMinLength)]
        public string Description { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<UserCourse> Students { get; set; } = new HashSet<UserCourse>();
    }
}
