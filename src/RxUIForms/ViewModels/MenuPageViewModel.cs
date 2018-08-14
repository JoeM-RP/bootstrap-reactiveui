using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using RxUIForms.Models;
using Xamvvm;

namespace RxUIForms.ViewModels
{
    public class MenuPageViewModel : BasePageModelRxUI
    {
        /*
         * Define Fields
         */

        /*
         * Define Properties
         */
        private ObservableCollection<NavigationMenuItem> _menuItems = new ObservableCollection<NavigationMenuItem>();
        public ObservableCollection<NavigationMenuItem> MenuItems
        {
            get { return _menuItems; }
            set { this.RaiseAndSetIfChanged(ref _menuItems, value); }
        }

        private NavigationMenuItem _selectedItem;
        public NavigationMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);
                if (_selectedItem == null)
                    return;
                
                SelectedItem = null;
            }
        }


        /*
         * Define Commands
         */
        public ReactiveCommand<NavigationMenuItem, bool> SelectItemCommand { get; }

        public MenuPageViewModel()
        {
            SelectItemCommand = ReactiveCommand.CreateFromTask<NavigationMenuItem, bool>(async (arg) => await SetDetailPageFromMenu(arg));

            Initialize();
        }

        /*
         * Define Methods
         */
        private async Task<bool> SetDetailPageFromMenu(NavigationMenuItem arg)
        {
            
            var masterDetailPage = this.GetPageFromCache<AppShellViewModel>();

            switch(arg.Key)
            {
                case("Settings"):
                    masterDetailPage.GetPageModel().SetDetail(this.GetPageFromCache<SettingsPageViewModel>());
                    break;
                default:
                    masterDetailPage.GetPageModel().SetDetail(this.GetPageFromCache<HomePageViewModel>());
                    break;
            }

            return true;
        }

        private void Initialize()
        {
            MenuItems.Add(new NavigationMenuItem()
            {
                Key = "Home",
                Title = "Home",
                Image = "home.png"
            });

            MenuItems.Add(new NavigationMenuItem()
            {
                Key = "Settings",
                Title = "Settings",
                Image = "settings.png"
            });

            this.ObservableForProperty(x => x.SelectedItem, skipInitial: true).Select(x => x.Value)
                .Where(_ => SelectedItem != null)
                .Do(_ => Debug.WriteLine("DEBUG: SelectItem"))
                .InvokeCommand(SelectItemCommand);
        }
    }
}
