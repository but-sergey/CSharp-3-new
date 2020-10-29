using MailSender.lib.Models;

namespace MailSender.Data.Stores.InDB
{
    class ServersStoreInDB : StoreInDB<Server>
    {
        public ServersStoreInDB(MailSenderDB db) : base(db) { }
    }
}
