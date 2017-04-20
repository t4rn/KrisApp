using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class ContactMessageRepo : BaseDAL, IContactMessageRepository
    {
        public ContactMessageRepo(string cs) : base(cs)
        {
        }

        public void AddMessage(ContactMessage message)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.ContactMessages.Add(message);
                context.SaveChanges();
            }
        }

        public List<ContactMessage> GetContactMessages()
        {
            List<ContactMessage> contactMessages = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                contactMessages = context.ContactMessages.AsNoTracking()
                    .ToList();
            }

            return contactMessages;
        }
    }
}
