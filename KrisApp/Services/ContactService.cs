using KrisApp.Common.Extensions;
using KrisApp.DataAccess;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Results;
using KrisApp.Models.Admin;
using KrisApp.Models.Me;
using System;
using System.Linq;

namespace KrisApp.Services
{
    public class ContactService : AbstractService
    {
        private readonly IContactMessageRepository _contactRepo;

        public ContactService(KrisLogger log) : base(log)
        {
            _contactRepo = new ContactMessageRepo(Properties.Settings.Default.csDB);
        }

        /// <summary>
        /// Dodaje kontakt na bazę [WWW.ContactMessage]
        /// </summary>
        internal Result AddContactMessage(ContactModel contactModel)
        {
            Result result = new Result();
            try
            {
                ContactMessage message = PrepareMessage(contactModel);

                _contactRepo.AddMessage(message);

                if (message.ID > 0)
                {
                    result.IsOK = true;
                    result.Message = $"Wiadomość wysłana pomyślnie! Otrzymała ID = '{message.ID}'";

                    _log.Debug("[AddContactMessage] Dodano wiadomosc o temacie = '{0}' z IP = '{1}'. Otrzymala ID = '{2}'",
                        message.Subject, message.Content, message.ID);
                }
                else
                {
                    result.Message = "Wystąpił błąd podczas wysyłania wiadomości!";
                }
            }
            catch (Exception ex)
            {
                _log.Error("[AddContactMessage] Ex: {0} StackTrace: {1}",
                    ex.MessageFromInnerEx(), ex.StackTrace);

                result.Message = $"Ex: {ex.MessageFromInnerEx()}";
            }

            return result;
        }

        /// <summary>
        /// Zwraca wszystkie wiadomości
        /// </summary>
        internal ContactsListModel GetContactMessages()
        {
            ContactsListModel model = new ContactsListModel();

            model.ContactMessages = _contactRepo.GetContactMessages()
                .OrderByDescending(x => x.AddDate).ToList();

            return model;
        }

        /// <summary>
        /// Zwraca ContactMessage wypełniony danymi z przekazanego modelu
        /// </summary>
        private ContactMessage PrepareMessage(ContactModel contactModel)
        {
            // TODO: automapper
            ContactMessage message = new ContactMessage();

            message.Author = contactModel.Author;
            message.Subject = contactModel.Subject;
            message.Content = contactModel.Content;
            message.IP = NetworkService.GetContextIP();
            message.AddDate = DateTime.Now;

            return message;
        }
    }
}