using MailSender.lib.Models;

namespace MailSender.Data.Stores.InDB
{
    class SendersStoreInDB : StoreInDB<Sender>
    {
        public SendersStoreInDB(MailSenderDB db) : base(db) { }
    }
}
