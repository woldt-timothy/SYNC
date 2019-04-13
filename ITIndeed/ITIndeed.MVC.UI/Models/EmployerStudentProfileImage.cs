using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITIndeed.BL;

namespace ITIndeed.MVC.UI.Models
{
    public partial class EmployerStudentProfileImage
    {
        public Employer employer { get; set; }
        public Student student { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}