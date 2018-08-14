using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Xamvvm;

namespace RxUIForms.ViewModels
{
    public class SettingsPageViewModel : BasePageViewModel
    {
        /*
         * Define Fields
         */

        /*
         * Define Properties
         */
        private string _settingOne;
        public string SettingOne
        {
            get { return _settingOne; }
            set { this.RaiseAndSetIfChanged(ref _settingOne, value); }
        }

        private bool _settingTwo;
        public bool SettingTwo
        {
            get { return _settingTwo; }
            set { this.RaiseAndSetIfChanged(ref _settingTwo, value); }
        }

        private bool _settingThree;
        public bool SettingThree
        {
            get { return _settingThree; }
            set { this.RaiseAndSetIfChanged(ref _settingThree, value); }
        }

        /*
         * Define Commands
         */
        public ReactiveCommand SaveCommand { get; set; }


        public SettingsPageViewModel()
        {
            SaveCommand = ReactiveCommand.CreateFromTask(async () => await SaveAndNavigate());

            Initialize();
        }

        /*
         * Methods
         */
        private void Initialize()
        {
            this.Title = "Settings";

            Observable.Merge(this.SaveCommand.IsExecuting)
                      .ToProperty(this, vm => vm.IsBusy, out _busy);
        }

        private async Task SaveAndNavigate()
        {
            // TODO: do something to save settings here.
            await Task.Delay(2000);
            //\

            // Normally, we'd use the following line to pop the stack:
            // await this.PopPageAsync();

            // However, due to some interesting restrictions in RxUI/Xamvvm, we need to be a bit more 
            // explicit for Master/Detail. TODO: unfortunately, there is no page transition in this case
            // (which actually affects navigation from the menu page also - just less obvious) so I'll 
            // need to look more into that. Let me know if you find the solution!
            var masterDetailPage = this.GetPageFromCache<AppShellViewModel>();
            masterDetailPage.GetPageModel().SetDetail(this.GetPageFromCache<HomePageViewModel>());
        }
    }
}
