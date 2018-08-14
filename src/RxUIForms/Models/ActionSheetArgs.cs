using System;
namespace RxUIForms.Models
{
    public class ActionSheetArgs
    {
        public ActionSheetArgs(string title, string[] options)
        {
            this.Title = title;
            this.Options = options;
        }

        public string Title { get; set; }
        public string Cancel => "Cancel";
        //public string Destroy { get; set; }
        public string[] Options { get; set; }
    }
}