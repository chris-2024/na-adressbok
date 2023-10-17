using Adressbok.Interfaces;
using Adressbok.Models;

namespace Adressbok.Services;

partial class ContactService : IContactService
{
    private readonly IFileService<ContactModel> _fileService;
    private readonly List<ContactModel> _contacts;

    public ContactService()
    {
        _fileService = new FileService<ContactModel>("Contacts");
        _contacts = new(_fileService.ReadFromFile());
    }

    private bool UpdateContacts() => _fileService.WriteToFile(_contacts.ToList());

    public bool AddContact(ContactModel contact)
    {
        try
        {
            _contacts.Add(contact);
        }
        catch { return false; }

        return UpdateContacts();
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
        return UpdateContacts();
    }

    public bool RemoveContact(Guid id)
    {
        var contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact is null) return false;

        _contacts.Remove(contact);
        return UpdateContacts();
    }

    public bool RemoveContact(string email)
    {
        var contact = _contacts.FirstOrDefault(x => x.Email == email);

        if (contact is null) return false;

        _contacts.Remove(contact);
        return UpdateContacts();
    }
}
