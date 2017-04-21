using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Results;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Dodaje kontakt na bazę [WWW.ContactMessage]
        /// </summary>
        Result AddContactMessage(ContactMessage message);

        /// <summary>
        /// Zwraca wszystkie wiadomości
        /// </summary>
        List<ContactMessage> GetContactMessages();
    }
}
