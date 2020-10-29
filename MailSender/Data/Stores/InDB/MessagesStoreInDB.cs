using MailSender.lib.Models;

namespace MailSender.Data.Stores.InDB
{
    class MessagesStoreInDB : StoreInDB<Message>
    {
        public MessagesStoreInDB(MailSenderDB db) : base(db) { }
    }
}
