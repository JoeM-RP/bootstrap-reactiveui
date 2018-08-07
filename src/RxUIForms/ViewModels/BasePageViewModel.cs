using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamvvm;

namespace RxUIForms.ViewModels
{
    public abstract class BasePageViewModel : BasePageModelRxUI, IPageVisibilityChange, INavigationPushed, INavigationPopped
    {
        /*
         * Define Fields
         */
        protected CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        /*
         * Define Properties
         */
        protected ObservableAsPropertyHelper<bool> _busy;
        public bool IsBusy => _busy.Value;

        private string _pageTitle;
        public string Title
        {
            get { return _pageTitle; }
            set { this.RaiseAndSetIfChanged(ref _pageTitle, value); }
        }

        /*
         * Define Commands
         */

        public BasePageViewModel()
        {
            Observable.Merge(this.ThrownExceptions)
                      .Throttle(TimeSpan.FromMilliseconds(250), RxApp.MainThreadScheduler)
                      .Subscribe(async ex => 
            { 
                
            })
                      .DisposeWith(SubscriptionDisposables);
        }

        /*
         * Define Methods
         */

        /// <summary>
        /// Called when the pagve is popped from the navigation stack
        /// </summary>
        public void NavigationPopped()
        {
        }

        /// <summary>
        /// Called when the page is pushed onto the navigation stack
        /// </summary>
        public void NavigationPushed()
        {
        }

        /// <summary>
        /// Called whenever the page is revealed
        /// </summary>
        public void OnAppearing()
        {
        }

        /// <summary>
        /// Called whenever the page is hidden
        /// </summary>
        public void OnDisappearing()
        {
        }
    }
}
