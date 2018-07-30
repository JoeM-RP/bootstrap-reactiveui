using System;
using System.Collections.Generic;
using ReactiveUI;
using RxUIForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamvvm;

namespace RxUIForms.Views
{
    public partial class HomePage : ContentPage, IBasePageRxUI<HomePageViewModel>
    {
        public HomePage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public HomePageViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get; set; }
    }
}
