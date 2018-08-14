using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using RxUIForms.Models;
using Xamvvm;

namespace RxUIForms.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        /*
         * Define Fields
         */

        /*
         * Define Properties
         */
        private ObservableCollection<CopyItem> _copyItems = new ObservableCollection<CopyItem>();
        public ObservableCollection<CopyItem> CopyItems
        {
            get { return _copyItems; }
            set { this.RaiseAndSetIfChanged(ref _copyItems, value); }
        }

        private CopyItem _selectedItem;
        public CopyItem SelectedItem
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
        public ReactiveCommand ShowActionSheetCommand { get; }
        public ReactiveCommand ShowAlertCommand { get; }
        public ReactiveCommand<CopyItem, bool> SelectItemCommand { get; }

        public HomePageViewModel()
        {
            this.Title = "Home";

            SelectItemCommand = ReactiveCommand.CreateFromTask<CopyItem, bool>(async (arg) => await Navigate(arg));
            ShowAlertCommand = ReactiveCommand.CreateFromTask(async (arg) => await ShowAlert());
            ShowActionSheetCommand = ReactiveCommand.CreateFromTask(async (arg) => await ShowActionSheet());

            Initialize();
        }

        /*
         * Define Methods
         */
        private async Task ShowAlert()
        {
            var response = await Interactions.Errors.Handle(new NotImplementedException("This is a fake error to demonstrate the alert"));

            if (response == ErrorRecoveryOption.Retry)
            {
                // TODO: If response is true, the user wants to "retry" the action. 
                // Do something here to attempt the action again
            }
            else
            {
                // TODO: If response is false, the user has canceled the attempted action
                // So perform an action here that makes sense; we may want to naviagte to 
                // a previous page or just sit idle
            }
        }

        private async Task ShowActionSheet()
        {
            var response = await Interactions.Actions.Handle(new ActionSheetArgs("Actions", new string[] { "Action1", "Action2" }));

            // TODO: Like with the Error Dialog, we want to perform different actions based on the user selection
            switch (response)
            {
                case ("Action1"):
                case ("Action2"):
                default:
                    var alert = await Interactions.Alerts.Handle(new AlertArgs("I did it!", $"Selected {response}"));
                    break;
            }
        }

        private async Task<bool> Navigate(CopyItem parameter)
        {
            try
            {
                return await this.PushPageAsNewInstanceAsync<SamplePageViewModel>(vm =>
                {
                    vm.Subject = parameter.Title;
                    vm.Copy = parameter.Body;
                });
            }
            catch (Exception ex)
            {
                await Observable.Throw<Unit>(ex);
                return false;
            }
        }

        private void Initialize()
        {
            _copyItems.Add(new CopyItem()
            {
                Title = "About Me",
                Body = "I’m Joe; full-time developer, part-time hobby-jogger, Tsar of awful check-in comments. I like cooking, exploring Chicago, and a good story. I write code sometimes."
            });

            _copyItems.Add(new CopyItem()
            {
                Title = "About ReactiveUI",
                Body = "ReactiveUI is a composable, cross-platform model-view-viewmodel framework for all .NET platforms that is inspired by functional reactive programming which is a paradigm that allows you to express the idea around a feature in one readable place, abstract mutable state away from your user interfaces and improve the testability of your application."
            });

            _copyItems.Add(new CopyItem()
            {
                Title = "About Xamvvm.Forms.RxUI",
                Body = "A simple, fast and lightweight MVVM Framework for Xamarin.Forms with fluent API that makes using ReactiveUI in Xamarin.Forms even easier!"
            });

            _copyItems.Add(new CopyItem()
            {
                Title = "About Shakespeare",
                Body = "William Shakespeare was an English poet, playwright, and actor, widely regarded as the greatest writer in the English language and the world's pre-eminent dramatist. He is often called England's national poet, and the \"Bard of Avon\". His extant works, including collaborations, consist of approximately 38 plays, 154 sonnets, 2 long narrative poems, and a few other verses, some of uncertain authorship. His plays have been translated into every major living language and are performed more often than those of any other playwright."
            });

            this.ObservableForProperty(x => x.SelectedItem, skipInitial: true).Select(x => x.Value)
                .Where(_ => SelectedItem != null)
                .Do(_ => Debug.WriteLine("DEBUG: SelectItem"))
                .InvokeCommand(SelectItemCommand);

            // Error Handling
            Observable.Merge(this.ThrownExceptions, SelectItemCommand.ThrownExceptions, ShowAlertCommand.ThrownExceptions, ShowActionSheetCommand.ThrownExceptions)
                      .Throttle(TimeSpan.FromMilliseconds(250), RxApp.MainThreadScheduler)
                      .Subscribe(async ex =>
            {
                Debug.WriteLine($"[{ex.Source}] Error = {ex}");

                await Interactions.Errors.Handle(ex);

            }).DisposeWith(SubscriptionDisposables);
        }
    }
}
