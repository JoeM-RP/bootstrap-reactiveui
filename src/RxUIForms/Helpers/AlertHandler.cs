using System;
using System.Reactive.Disposables;
using RxUIForms.Models;
using Xamarin.Forms;

namespace RxUIForms.Helpers
{
    public static class AlertHandler
    {
        /// <summary>
        /// Registers the error handler with no retry action
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="disposable">Disposable.</param>
        public static void RegisterAlertHandler(Page page, CompositeDisposable disposable)
        {
            Interactions.Alerts.RegisterHandler(async (interaction) =>
            {
                var alert = interaction.Input;

                await page.DisplayAlert(alert.Title, alert.Body, "OK");

                // We need to set output before returning
                interaction.SetOutput(true);
            }).DisposeWith(disposable);
        }
    }
}
