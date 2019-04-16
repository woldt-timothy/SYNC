using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITIndeed.BL;
using System.Drawing;
using System.ComponentModel;

namespace ITIndeed.MVC.UI.Models
{
    public class EmployerStudentProfileImage
    {
        public Employer employer { get; set; }
        public Student student { get; set; }

        [DisplayName("Profile Picture")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}