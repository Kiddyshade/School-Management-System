using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem2.Models
{
    public class MetaDataEnrollment
    {
        [Display(Name ="Student Grade")]
        public Nullable<decimal> Grade { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Student")]
        public int StudentID { get; set; }

        [Display(Name = "Student")]
        public Student Student { get; set; }

        [Display(Name = "Lecturer")]
        public Nullable<int> LecturerId { get; set; }

        [Display(Name = "Lecturer")]
        public Lecturers Lecturers { get; set; }

    }

    [MetadataType(typeof(MetaDataEnrollment))]
    public partial class Enrollment { }
}