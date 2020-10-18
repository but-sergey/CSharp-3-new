using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MailSender.Data
{
    class MailSenderDbInitilizer
    {
        private readonly MailSenderDB _db;

        public MailSenderDbInitilizer(MailSenderDB db) => _db = db;

        public void Initialize()
        {
            _db.Database.Migrate();

            InitializeRecipients();
            InitializeSenders();
            InitializeMessages();
            InitializeServers();
            
        }

        private void InitializeRecipients()
        {
            if (_db.Recipients.Any()) return;

            _db.Recipients.AddRange(TestData.Recipients);
            _db.SaveChanges();
        }

        private void InitializeSenders()
        {
            if (_db.Senders.Any()) return;

            _db.Senders.AddRange(TestData.Senders);
            _db.SaveChanges();
        }

        private void InitializeMessages()
        {
            if (_db.Messages.Any()) return;

            _db.Messages.AddRange(TestData.Messages);
            _db.SaveChanges();
        }

        private void InitializeServers()
        {
            if (_db.Servers.Any()) return;

            _db.Servers.AddRange(TestData.Servers);
            _db.SaveChanges();
        }

    }
}
