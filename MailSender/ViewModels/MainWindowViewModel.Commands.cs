using MailSender.Infrastructure.Commands;
using MailSender.Models;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Commands

        #region CreateNewServerCommand

        private ICommand _CreateNewServerCommand;

        public ICommand CreateNewServerCommand => _CreateNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p) => true;

        private void OnCreateNewServerCommandExecuted(object p)
        {
            MessageBox.Show("Создание нового сервера", "Управление серверами");
        }

        #endregion


        #region EditServerCommand

        private ICommand _EditServerCommand;

        public ICommand EditServerCommand => _EditServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);

        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnEditServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            MessageBox.Show($"Редактирование сервера {server.Address}", "Управление серверами");
        }

        #endregion


        #region DeleteServerCommand

        private ICommand _DeleteServerCommand;

        public ICommand DeleteServerCommand => _DeleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);

        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();

            MessageBox.Show($"Удаление сервера {server.Address}", "Управление серверами");
        }

        #endregion

        #region Command SendMailCommand - Отправка почты

        private ICommand _SendMailCommand;

        public ICommand SendMailCommand => _SendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);

        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMessage is null) return false;
            return true;
        }

        private void OnSendMailCommandExecuted(object p)
        {
            var server = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);

            mail_sender.Send(sender.Address, recipient.Address, message.Subject, message.Body);
        }

        #endregion

        #endregion

    }
}
