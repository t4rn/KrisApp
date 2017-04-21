using KrisApp.Common.Extensions;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.Services
{
    public class ContactService : AbstractService, IContactService
    {
        private readonly IContactMessageRepository _contactRepo;

        public ContactService(ILogger log, IContactMessageRepository contactRepo) : base(log)
        {
            _contactRepo = contactRepo;
        }

        /// <summary>
        /// Dodaje kontakt na bazę [WWW.ContactMessage]
        /// </summary>
        public Result AddContactMessage(ContactMessage message)
        {
            Result result = new Result();
            try
            {
                FillMessage(message);

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
        public List<ContactMessage> GetContactMessages()
        {
            return _contactRepo.GetContactMessages()
                .OrderByDescending(x => x.AddDate).ToList();
        }

        /// <summary>
        /// Wypełnia ContactMessage brakującymi danymi
        /// </summary>
        private void FillMessage(ContactMessage message)
        {
            message.IP = NetworkService.GetContextIP();
            message.AddDate = DateTime.Now;
        }
    }
}