using System.Windows.Controls;

namespace MailSender.Views
{
    /// <summary>
    /// Interaction logic for RecipientEditor.xaml
    /// </summary>
    public partial class RecipientEditor : UserControl
    {
        public RecipientEditor() => InitializeComponent();

        private void OnDataValidationError(object? Sender, ValidationErrorEventArgs E)
        {
            var control = (Control)Sender;
            if (E.Action == ValidationErrorEventAction.Added)
                control.ToolTip = E.Error.ErrorContent.ToString();
            else
                control.ClearValue(ToolTipProperty);
        }
    }
}
