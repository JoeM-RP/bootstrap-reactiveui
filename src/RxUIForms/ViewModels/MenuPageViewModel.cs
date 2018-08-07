using System;
using System.Collections.ObjectModel;
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
            Initialize();
        }

        /*
         * Define Methods
         */
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

            this.ObservableForProperty(x => x.SelectedItem)
                .Subscribe(item => 
            { 
                
            });
        }
    }
}
