using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem2.Models
{
    public class MetaDataStudent
    {
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Date of Enrollment")]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
    }
    [MetadataType(typeof(MetaDataStudent))]
    public partial class Student
    {

    }
}