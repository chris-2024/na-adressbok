using Adressbok.Interfaces;
using Adressbok.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Adressbok.Services;

public class ContactService : IContactService
{
    private readonly IFileService<ContactModel> _fileService;
    private readonly ObservableCollection<ContactModel> _contacts;

    public ContactService(IFileService<ContactModel> fileService, ObservableCollection<ContactModel> contacts)
    {
        // Initialize the ContactService with a file service and a collection of contacts
        _fileService = fileService ?? new FileService<ContactModel>("Adressbok_Kontakter");
        _contacts = contacts ?? new(_fileService.ReadFromFile());

        // Subscribe to the CollectionChanged event to handle changes in the contact collection
        _contacts.CollectionChanged += UpdateContacts;

        // Optional Contact Seeder for initial data
        ContactsSeeder();
    }

    // Constructor for creating a ContactService without parameters
    public ContactService() : this(null, null) { }

    // Event handler for handling changes in the contact collection and saving changes to a file
    private void UpdateContacts(object sender, NotifyCollectionChangedEventArgs e) => _fileService.WriteToFile(_contacts.ToList());

    // Retrieve all contacts in the collection
    public ObservableCollection<ContactModel> GetAllContacts() => _contacts;

    public bool AddContact(ContactModel contact)
    {
        try
        {
            // TODO: trim fields - check if email already exist?
            // Try to add the new contact to the collection
            _contacts.Add(contact);
        }
        catch { return false; } // If an exception occurs while adding a contact

        return true;
    }

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
        var contactOld = _contacts.FirstOrDefault(c => c.Id == contactEdit.Id);

        // If the contact with the specified ID doesn't exist
        if (contactOld is null) return false;

        int index = _contacts.IndexOf(contactOld);

        // See if index is out of range by checking collection count and contact index
        if (index < 0 || index >= _contacts.Count) return false;
            
        _contacts[index] = contactEdit;
        return true;
    }

    public bool RemoveContact(Guid id)
    {
        // Find the contact by the Guid provided
        var contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact is null) return false;

        _contacts.Remove(contact);
        // Indicate successful removal
        return true;
    }

    public bool RemoveContact(string email)
    {
        // Find the contact by the email address provided
        var contact = _contacts.FirstOrDefault(x => x.Email == email);

        if (contact is null) return false;

        _contacts.Remove(contact);
        // Indicate successful removal
        return true; 
    }

    private void ContactsSeeder()
    {
        // Exit seeder if contact list is not empty
        if (_contacts.Count > 0) return;

        // Add sample contact data to the collection
        _contacts.Add(new ContactModel() { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", PhoneNumber = "123-456-7890", Address = "123 Main St" });
        _contacts.Add(new ContactModel() { LastName = "Smith", Email = "alice@example.com", PhoneNumber = "987-654-3210" });
        _contacts.Add(new ContactModel() { FirstName = "Bob", Email = "bob@example.com", PhoneNumber = "555-123-7890", Address = "789 Oak St" });
        _contacts.Add(new ContactModel() { FirstName = "Sarah", LastName = "Brown", Email = "sarah@example.com", Address = "101 Pine St" });
        _contacts.Add(new ContactModel() { Email = "michael@example.com" });
        _contacts.Add(new ContactModel() { FirstName = "Alice", Email = "alice.j@example.com", Address = "123 Elm St" });
        _contacts.Add(new ContactModel() { LastName = "David", Email = "david@example.com", PhoneNumber = "555-123-4567" });
        _contacts.Add(new ContactModel() { FirstName = "Jennifer", Email = "jennifer@example.com" });
        _contacts.Add(new ContactModel() { FirstName = "Michael", LastName = "Johnson", Email = "michael.j@example.com" });
        _contacts.Add(new ContactModel() { FirstName = "Sarah", LastName = "Garcia", Email = "sarah.g@example.com" });
    }
}
