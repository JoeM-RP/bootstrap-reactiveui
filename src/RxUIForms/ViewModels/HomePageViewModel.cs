using System;
using System.Collections.ObjectModel;
using System.Reactive;
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
        public ReactiveCommand<Unit, bool> ShowActionSheetCommand { get; }
        public ReactiveCommand<Unit, bool> ShowAlertCommand { get; }
        public ReactiveCommand<CopyItem, bool> SelectItemCommand { get; }

        public HomePageViewModel()
        {
            this.Title = "Home";

            SelectItemCommand = ReactiveCommand.CreateFromTask<CopyItem, bool>(async (arg) => await Navigate(arg));

            Initialize();
        }

        /*
         * Define Methods
         */
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
                Title = "About Prism.Forms",
                Body = "Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF, Windows 10 UWP, and Xamarin Forms. Separate releases are available for each platform and those will be developed on independent timelines. Prism provides an implementation of a collection of design patterns that are helpful in writing well-structured and maintainable XAML applications, including MVVM, dependency injection, commands, EventAggregator, and others. "
            });

            _copyItems.Add(new CopyItem()
            {
                Title = "About Shakespeare",
                Body = "William Shakespeare was an English poet, playwright, and actor, widely regarded as the greatest writer in the English language and the world's pre-eminent dramatist. He is often called England's national poet, and the \"Bard of Avon\". His extant works, including collaborations, consist of approximately 38 plays, 154 sonnets, 2 long narrative poems, and a few other verses, some of uncertain authorship. His plays have been translated into every major living language and are performed more often than those of any other playwright."
            });

            //this.WhenAnyValue(x => x.SelectedItem)
                //.Where((arg) => SelectedItem != null)
                //.Do((obj) => System.Diagnostics.Debug.WriteLine("DEBUG: SelectItem"))
                //.InvokeCommand(SelectItemCommand);

            this.ObservableForProperty(x => x.SelectedItem, skipInitial: true).Select(x => x.Value)
                .Where(_ => SelectedItem != null)
                .Do(_ => System.Diagnostics.Debug.WriteLine("DEBUG: SelectItem"))
                .InvokeCommand(SelectItemCommand);
        }
    }
}
