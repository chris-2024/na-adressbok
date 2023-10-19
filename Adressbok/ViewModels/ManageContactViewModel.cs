using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Adressbok.ViewModels;

public partial class ManageContactViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    private readonly Guid? _contactId;

    [ObservableProperty]
    private ContactModel contact;

    [ObservableProperty]
    private string emailErrorMessage;

    public ManageContactViewModel(ContactModel contact, IContactService contactService)
    {
        _contactService = contactService;
        // Sets id if its an existing contact to be updated, null if new contact
        _contactId = contact?.Id; 
        // Create new contactmodel if contact is null, otherwise copy the properties
        this.contact = contact is null ? new() : new ContactModel(contact.Id) { FirstName = contact.FirstName, LastName = contact.LastName, Email = contact.Email, PhoneNumber = contact.PhoneNumber, Address = contact.Address };
        emailErrorMessage = "";
    }

    public ManageContactViewModel(IContactService contactService) : this(null, contactService) { }

    [RelayCommand]
    async Task SaveContact()
    {
        bool result;

        if (!IsValidEmail(Contact.Email))
        {
            EmailErrorMessage = "Invalid Email.";
            return;
        }

        if (_contactId is null)
            // Add new contact
            result = _contactService.AddContact(Contact);
        else
            // Update existing contact
            result = _contactService.UpdateContact(Contact);

        if (result)
            // Go back to MainPage
            await Shell.Current.Navigation.PopToRootAsync();
        else
            EmailErrorMessage = "Contact with this email already exist.";
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        // Regex pattern for basic email validation
        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
    }
}
