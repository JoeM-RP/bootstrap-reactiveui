using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using ReactiveUI;
using RxUIForms.Helpers;
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

        CompositeDisposable disposables = new CompositeDisposable();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ErrorHandler.RegisterErrorHandlerWithRetry(this, disposables);
        }
    }
}
