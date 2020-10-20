using MailSender.lib.Models;

namespace MailSender.Data.Stores.InDB
{
    class SchedulerTasksStoreInDB : StoreInDB<SchedulerTask>
    {
        public SchedulerTasksStoreInDB(MailSenderDB db) : base(db) { }
    }
}
