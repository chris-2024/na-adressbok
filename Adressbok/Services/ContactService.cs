using Adressbok.Interfaces;
using Adressbok.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Adressbok.Services;

public class ContactService : IContactService
{
    private readonly IFileService<ContactModel> _fileService;
    private readonly List<ContactModel> _contacts;

    public ContactService(IFileService<ContactModel> fileService, List<ContactModel> contacts)
    {
        // Initialize the ContactService with a file service and a collection of contacts
        _fileService = fileService ?? new FileService<ContactModel>("Adressbok_Kontakter");
        _contacts = contacts ?? new(_fileService.ReadFromFile());

        ContactsUpdated += SaveContactsToFile;
    }

    // Constructor for creating a ContactService without parameters
    public ContactService() : this(null, null)
    {         
        // Optional Contact Seeder for initial data
        ContactsSeeder();
    }

    public event Action ContactsUpdated;

    // Event handler for handling changes in the contact collection and saving changes to a file
    private void SaveContactsToFile() => _fileService.WriteToFile(_contacts.ToList());

    // Retrieve all contacts in the collection
    public List<ContactModel> GetAllContacts() => _contacts;

    public bool AddContact(ContactModel contact)
    {
        if (contact is null || _contacts.Any(c => c.Email == contact.Email)) return false;

        try
        {
            _contacts.Add(contact);
        }
        catch { return false; }

        ContactsUpdated.Invoke();
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

    // Update existing contact
    public bool UpdateContact(ContactModel contactEdit)
    {
        var contactOld = _contacts.FirstOrDefault(c => c.Id == contactEdit.Id);

        // If the contact with the specified ID doesn't exist
        if (contactOld is null) return false;
        
        // Check if the two objects share the same reference
        if (contactEdit != contactOld)
        {
            // Update the contact properties with the new values
            contactOld.FirstName = contactEdit.FirstName;
            contactOld.LastName = contactEdit.LastName;
            contactOld.Email = contactEdit.Email;
            contactOld.PhoneNumber = contactEdit.PhoneNumber;
            contactOld.Address = contactEdit.Address;
        }

        // Invoke contacts updated to save the updated contact information
        ContactsUpdated.Invoke();

        return true;
    }

    public bool RemoveContact(Guid id)
    {
        // Find the contact by the Guid provided
        var contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact is null) return false;

        _contacts.Remove(contact);
        ContactsUpdated.Invoke();

        // Indicate successful removal
        return true;
    }

    public bool RemoveContact(string email)
    {
        // Find the contact by the email address provided
        var contact = _contacts.FirstOrDefault(x => x.Email == email);

        if (contact is null) return false;

        _contacts.Remove(contact);
        ContactsUpdated.Invoke();

        // Indicate successful removal
        return true; 
    }

    private void ContactsSeeder()
    {
        // Exit seeder if contact list is not empty
        if (_contacts.Count > 0) return;

        // Add sample contact data to the collection
        _contacts.Add(new ContactModel() { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", PhoneNumber = "123-456-7890", Address = "123 Main St" });
        _contacts.Add(new ContactModel() { FirstName = "Bob", Email = "bob@example.com", PhoneNumber = "555-123-7890", Address = "789 Oak St" });
        _contacts.Add(new ContactModel() { FirstName = "Sarah", LastName = "Brown", Email = "sarah@example.com", Address = "101 Pine St" });
        _contacts.Add(new ContactModel() { Email = "michael@example.com" });
        _contacts.Add(new ContactModel() { LastName = "David", Email = "david@example.com", PhoneNumber = "555-123-4567" });

        ContactsUpdated.Invoke();
    }
}
