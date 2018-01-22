using LearningSystem.Data;

namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class CreateCourseViewModel
    {
        [Required]
        [MaxLength(CourseNameMaxLength)]
        [MinLength(CourseNameMinLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        [MinLength(CourseDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}
