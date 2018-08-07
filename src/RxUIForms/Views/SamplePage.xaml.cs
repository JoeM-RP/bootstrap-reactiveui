using System;
using System.Collections.Generic;
using ReactiveUI;
using RxUIForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamvvm;

namespace RxUIForms.Views
{
    public partial class SamplePage : ContentPage, IBasePageRxUI<SamplePageViewModel>
    {
        public SamplePage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public SamplePageViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get; set; }
    }
}
