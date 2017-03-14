using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RememBeer.MvcClient.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FileTypeAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FileTypeAttribute(string fileExtensions)
        {
            this.AllowedExtensions = fileExtensions.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }

            var fileName = file.FileName;
            return this.AllowedExtensions.Any(y => fileName.EndsWith(y));
        }
    }
}
