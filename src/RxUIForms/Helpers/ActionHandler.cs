using System;
using System.Reactive.Disposables;
using RxUIForms.Models;
using Xamarin.Forms;

namespace RxUIForms.Helpers
{
    public static class ActionHandler
    {
        /// <summary>
        /// Registers the action handler to show an Action Sheet user interaction
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="disposable">Disposable.</param>
        public static void RegisterActionHandler(Page page, CompositeDisposable disposable)
        {
            Interactions.Actions.RegisterHandler(async (interaction) =>
            {
                var sheet = interaction.Input;

                var result = await page.DisplayActionSheet(sheet.Title, sheet.Cancel, null, sheet.Options);

                interaction.SetOutput(result);
            }).DisposeWith(disposable);
        }

        /// <summary>
        /// Registers the action handler to show an Action Sheet user interaction with Destroy option
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="destroy">Destroy.</param>
        /// <param name="disposable">Disposable.</param>
        public static void RegisterActionHandlerWithDestroy(Page page, string destroy, CompositeDisposable disposable)
        {
            Interactions.Actions.RegisterHandler(async (interaction) =>
            {
                var sheet = interaction.Input;

                var result = await page.DisplayActionSheet(sheet.Title, sheet.Cancel, destroy, sheet.Options);

                interaction.SetOutput(result);
            }).DisposeWith(disposable);
        }
    }
}
