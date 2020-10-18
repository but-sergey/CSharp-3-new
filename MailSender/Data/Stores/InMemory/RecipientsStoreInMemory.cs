using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InMemory
{
    class RecipientsStoreInMemory : IStore<Recipient>
    {
        public Recipient Add(Recipient Item)
        {
            if (TestData.Recipients.Contains(Item)) return Item;

            var id = TestData.Recipients.DefaultIfEmpty().Max(r => r.Id) + 1;
            Item.Id = id;
            TestData.Recipients.Add(Item);
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            TestData.Recipients.Remove(item);
        }

        public IEnumerable<Recipient> GetAll() => TestData.Recipients;

        public Recipient GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);

        public void Update(Recipient Item) { }
    }
}
