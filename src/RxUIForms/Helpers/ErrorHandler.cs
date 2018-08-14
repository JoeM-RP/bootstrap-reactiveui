using System;
using System.Reactive.Disposables;
using RxUIForms.Models;
using Xamarin.Forms;

namespace RxUIForms.Helpers
{
    public static class ErrorHandler
    {
        /// <summary>
        /// Registers the error handler with retry option dialog
        /// </summary>
        /// <param name="page">Page handling the interaction</param>
        /// <param name="disposable">Composite disposable for the page</param>
        public static void RegisterErrorHandlerWithRetry(Page page, CompositeDisposable disposable)
        {
            Interactions.Errors.RegisterHandler(
               async interaction =>
               {
                   var action = await page.DisplayAlert("Yikes!",
                                                        $"Looks like something went wrong on our end. Retry?",
                                                        "Yes",
                                                        "No");

                   interaction.SetOutput(action ? ErrorRecoveryOption.Retry : ErrorRecoveryOption.Abort);
               }).DisposeWith(disposable);
        }

        /// <summary>
        /// Registers the error handler with no retry action
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="disposable">Disposable.</param>
        public static void RegisterErrorHandler(Page page, CompositeDisposable disposable)
        {
            Interactions.Errors.RegisterHandler(
               async interaction =>
               {
                   await page.DisplayAlert("Yikes!",
                                           $"Looks like something went wrong on our end",
                                           "OK");

                   interaction.SetOutput(ErrorRecoveryOption.Abort);
               }).DisposeWith(disposable);
        }

        /// <summary>
        /// Registers the error handler with no retry action and custom error message
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="disposable">Disposable.</param>
        public static void RegisterErrorHandler(Page page, CompositeDisposable disposable, string message)
        {
            Interactions.Errors.RegisterHandler(
               async interaction =>
               {
                   await page.DisplayAlert("Yikes!",
                                           $"{message}",
                                           "OK");

                   interaction.SetOutput(ErrorRecoveryOption.Abort);
               }).DisposeWith(disposable);
        }
    }
}
