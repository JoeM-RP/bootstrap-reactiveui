using System;
using ReactiveUI;

namespace RxUIForms.Models
{
    public class Interactions
    {
        public static readonly Interaction<Exception, ErrorRecoveryOption> Errors = new Interaction<Exception, ErrorRecoveryOption>();
        public static readonly Interaction<AlertArgs, bool> Alerts = new Interaction<AlertArgs, bool>();
        public static readonly Interaction<ActionSheetArgs, string> Actions = new Interaction<ActionSheetArgs, string>();
    }

    public enum AlertActionOptions
    {
        Ok,
        Cancel
    }

    public enum ErrorRecoveryOption
    {
        Retry,
        Abort
    }
}
