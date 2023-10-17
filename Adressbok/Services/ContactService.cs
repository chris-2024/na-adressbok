using Adressbok.Interfaces;
using Adressbok.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Adressbok.Services;

public class ContactService : IContactService
{
    private readonly IFileService<ContactModel> _fileService;
    private readonly ObservableCollection<ContactModel> contacts;

    public ContactService()
    {
        _fileService = new FileService<ContactModel>("Contacts");
        contacts = new(_fileService.ReadFromFile());
        contacts.CollectionChanged += UpdateContacts;
        FillContacs();
    }

    private void UpdateContacts(object sender, NotifyCollectionChangedEventArgs e) => _fileService.WriteToFile(contacts.ToList());

    public ObservableCollection<ContactModel> GetAllContacts() => contacts;

    public bool AddContact(ContactModel contact)
    {
        try
        {
            contacts.Add(contact);
        }
        catch { return false; }

        return true;
    }

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

    private void FillContacs()
    {
        if (contacts.Count > 0) return;

        contacts.Add(new ContactModel() { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", PhoneNumber = "123-456-7890", Address = "123 Main St" });
        contacts.Add(new ContactModel() { LastName = "Smith", Email = "alice@example.com", PhoneNumber = "987-654-3210" });
        contacts.Add(new ContactModel() { FirstName = "Bob", Email = "bob@example.com", PhoneNumber = "555-123-7890", Address = "789 Oak St" });
        contacts.Add(new ContactModel() { FirstName = "Sarah", LastName = "Brown", Email = "sarah@example.com", Address = "101 Pine St" });
        contacts.Add(new ContactModel() { Email = "michael@example.com" });
    }
}
