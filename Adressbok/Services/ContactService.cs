using Adressbok.Interfaces;
using Adressbok.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Adressbok.Services;

internal class ContactService : IContactService
{
    private readonly IFileService<ContactModel> _fileService;
    private ObservableCollection<ContactModel> contacts;

    public ContactService()
    {
        _fileService = new FileService<ContactModel>("Contacts");
        contacts = new(_fileService.ReadFromFile());
        contacts.CollectionChanged += UpdateContacts;
    }

    private void UpdateContacts(object sender, NotifyCollectionChangedEventArgs e) => _fileService.WriteToFile(contacts.ToList());

    public bool AddContact(ContactModel contact)
    {
        try
        {
            contacts.Add(contact);
        }
        catch { return false; }

        return true;
    }

    public ObservableCollection<ContactModel> GetAllContacts() => contacts;

    public ContactModel GetContact(Guid id)
    {
        return contacts.FirstOrDefault(c => c.Id == id);
    }

    public ContactModel GetContact(string parameter)
    {
        return contacts.FirstOrDefault(c => (c.FirstName?.Contains(parameter) ?? false) || (c.LastName?.Contains(parameter) ?? false) || c.Email.Contains(parameter));
    }

    public bool UpdateContact(ContactModel contactEdit)
    {
        var contactOld = contacts.FirstOrDefault(c => contactEdit.Id == contactEdit.Id);

        if (contactOld is null) return false;

        int index = contacts.IndexOf(contactOld);

        if (index < 0 || index >= contacts.Count) return false;
            
        contacts[index] = contactEdit;
        return true;
    }

    public bool RemoveContact(Guid id)
    {
        var contact = contacts.FirstOrDefault(x => x.Id == id);

        if (contact is null) return false;

        contacts.Remove(contact);
        return true;
    }

    public bool RemoveContact(string email)
    {
        var contact = contacts.FirstOrDefault(x => x.Email == email);

        if (contact is null) return false;

        contacts.Remove(contact);
        return true;
    }
}
