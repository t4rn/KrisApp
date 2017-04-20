using KrisApp.DataModel.Contact;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IContactMessageRepository
    {
        /// <summary>
        /// Dodaje kontakt na bazę [WWW.ContactMessage]
        /// </summary>
        void AddMessage(ContactMessage message);

        /// <summary>
        /// Zwraca wszystkie ContactMessage [WWW.ContactMessage]
        /// </summary>
        /// <returns></returns>
        List<ContactMessage> GetContactMessages();
    }
}
