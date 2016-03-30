using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Pendant
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; }
        public string Description { get; set; }
        public Size PendantSize { get; set; }
        public string FileName { get { return Id + ".jpg"; } }

        [NotMapped]
        public HttpPostedFileBase UploadedFile { get; set; }
    }

    public enum Size
    {
        Small,
        Large
    }
}