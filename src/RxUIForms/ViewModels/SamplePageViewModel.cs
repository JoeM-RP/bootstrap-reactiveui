using System;
using ReactiveUI;

namespace RxUIForms.ViewModels
{
    public class SamplePageViewModel : BasePageViewModel
    {
        /*
         * Define Fields
         */
        // TODO: this is a good place to define services that will be initialized or injected in the constructor

        /*
         * Define Properites
         */
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { this.RaiseAndSetIfChanged(ref _subject, value); }
        }

        private string _copy;
        public string Copy
        {
            get { return _copy; }
            set { this.RaiseAndSetIfChanged(ref _copy, value); }
        }

        public SamplePageViewModel()
        {
            this.Title = "Sample Page";
        }
    }
}

