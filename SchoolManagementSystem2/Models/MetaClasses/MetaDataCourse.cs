using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem2.Models
{
    public class MetaDataCourse
    {
        [StringLength(50)]
        public string Title { get; set; }
        [Range(1,8)]
        public int Credits { get; set; }
    }

    [MetadataType(typeof(MetaDataCourse))]
    public partial class Course
    {

    }
}