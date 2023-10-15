using Adressbok.Interfaces;
using Adressbok.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbok.Services
{
    internal class ContactService : IContactService
    {
        //private readonly IFileService<ContactModel> _fileService;
        private readonly List<ContactModel> _contacts;

        public ContactService()
        {
            //_fileService = new FileService<ContactModel>("Contacts");
            //_contacts = new(_fileService.ReadFromFile());
            _contacts = new List<ContactModel>() 
            {
                new ContactModel() { FirstName = "Bert", Email = "bert@anka.se" },
                new ContactModel() { FirstName = "Ketchup", Email = "ketchup@tomato.co.uk" },
                new ContactModel() { LastName = "Svanslös", Email = "svans@mail.com" }
            };
        }

        public bool AddContact(ContactModel contact)
        {
            try
            {
                _contacts.Add(contact);
            }
            catch { return false; }

            UpdateContacts();
            return true;
        }

        public List<ContactModel> GetAllContacts() => _contacts;

        public ContactModel GetContact(Guid id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public ContactModel GetContact(string parameter)
        {
            return _contacts.FirstOrDefault(c => (c.FirstName?.Contains(parameter) ?? false) || (c.LastName?.Contains(parameter) ?? false) || c.Email.Contains(parameter));
        }

        public bool UpdateContact(ContactModel contactEdit)
        {
            var contactOld = _contacts.Find(c => contactEdit.Id == contactEdit.Id);

            if (contactOld is null) return false;

            int index = _contacts.IndexOf(contactOld);

            if (index < 0 || index >= _contacts.Count) return false;
                
            _contacts[index] = contactEdit;
            UpdateContacts();
            return true;
        }

        public bool RemoveContact(Guid id)
        {
            var contact = _contacts.FirstOrDefault(x => x.Id == id);

            if (contact is null) return false;

            _contacts.Remove(contact);
            UpdateContacts();
            return true;
        }

        public bool RemoveContact(string email)
        {
            var contact = _contacts.FirstOrDefault(x => x.Email == email);

            if (contact is null) return false;

            _contacts.Remove(contact);
            UpdateContacts();
            return true;
        }

        private void UpdateContacts()
        {
            //_fileService.WriteToFile(_contacts.ToList());
        }
    }
}
