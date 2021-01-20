using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_build.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}