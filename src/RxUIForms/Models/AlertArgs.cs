using System;
namespace RxUIForms.Models
{
    public class AlertArgs
    {
        public AlertArgs(string title, string body)
        {
            this.Title = title;
            this.Body = body;
        }

        public string Title { get; set; }
        public string Body { get; set; }
    }
}
