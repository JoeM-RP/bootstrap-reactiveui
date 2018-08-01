using System;

using Xamarin.Forms;
using Xamvvm;

namespace RxUIForms.ViewModels
{
    public class AppShellViewModel : BasePageModelRxUI
    {
        /// <summary>
        /// Sets the detail page of the Master/Detail View
        /// </summary>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        public void SetDetail<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel
        {
            var masterDetailPage = this.GetCurrentPage() as MasterDetailPage;

            // If we have set a custom Navbar color in AppShell.cs, we need to preserve it
            masterDetailPage.Detail = new NavigationPage(page as Page)
            {
                BarBackgroundColor = Color.FromHex("#87A3BA"),
                BarTextColor = Color.White
            };

            masterDetailPage.IsPresented = false;
        }
    }
}

