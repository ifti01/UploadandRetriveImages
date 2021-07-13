using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UploadRetriveImages.Models
{
    public class Student
    {
        
        public int ID { get; set; }
        
        [DisplayName("Student Name")]
        public string Name { get; set; }
        
        [DisplayName("Student Class")]
        public int Standard { get; set; }
        
        [DisplayName("Choose Image")] 
        public string Imagepath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }

}