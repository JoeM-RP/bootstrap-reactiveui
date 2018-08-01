using System;
using System.Collections.Generic;
using ReactiveUI;
using RxUIForms.ViewModels;
using Xamarin.Forms;
using Xamvvm;

namespace RxUIForms.Views
{
    public partial class MenuPage : ContentPage, IBasePageRxUI<MenuPageViewModel>
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public MenuPageViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get; set; }
    }
}
