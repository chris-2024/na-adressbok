using Adressbok.Models;
using System.Collections.ObjectModel;

namespace Adressbok.Interfaces;

public interface IContactService
{
    /// <summary>
    /// Event to trigger changes to contacts.
    /// </summary>
    event Action ContactsUpdated;

    /// <summary>
    /// Get all contacts.
    /// </summary>
    List<ContactModel> GetAllContacts();

    /// <summary>
    /// Add New Contact To List.
    /// </summary>
    bool AddContact(ContactModel contact);

    /// <summary>
    /// Get Contact By ID.
    /// </summary>
    ContactModel GetContact(Guid id);
    /// <summary>
    /// Get Contact By Provided Parameter.
    /// </summary>
    ContactModel GetContact(string parameter);

    /// <summary>
    /// Update Existing Contact.
    /// </summary>
    bool UpdateContact(ContactModel contact);

    /// <summary>
    /// Delete Contact By ID.
    /// </summary>
    bool RemoveContact(Guid id);

    /// <summary>
    /// Delete Contact By Email.
    /// </summary>
    bool RemoveContact(string email);
}
