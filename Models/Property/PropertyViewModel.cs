using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace realtor_dotnet_core_mvc.Models.Property
{
    public class PropertyViewModel
    {
        public PropertyLookUp? Property { get; set; }
        public PropertyLandType? LandType { get; set; }
        public PropertyType? PropertyType { get; set; }
        public List<Attachment>? Attachments { get; set; }
    }

    public class Attachment
    {
        public long AttachmentId { get; set; }
        public long PropertyId { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class PropertyUpload
    {
        public IFormFile? File { get; set; }
    }
}





    


