using System;
using ReactiveUI;
using RxUIForms.ViewModels;
using Xamarin.Forms;
using Xamvvm;

namespace RxUIForms.Views
{
    public class AppShell : MasterDetailPage, IBasePageRxUI<AppShellViewModel>
    {
        public AppShell()
        {
            Master = this.GetPageFromCache<MenuPageViewModel>() as Page;
            Detail = new NavigationPage(this.GetPageFromCache<HomePageViewModel>() as Page)
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#394A76"),
                Title = "Home Page"
            };
        }

        public AppShellViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get; set; }
    }
}
