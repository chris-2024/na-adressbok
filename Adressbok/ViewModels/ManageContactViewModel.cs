using Adressbok.Interfaces;
using Adressbok.Models;
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

    // Constructor for new contact creation
    public ManageContactViewModel(IContactService contactService) : this(null, contactService) { }

    [RelayCommand]
    async Task SaveContact()
    {
        // Only continue if email is valid
        if (!IsValidEmail(Contact.Email))
        {
            EmailErrorMessage = "Invalid Email.";
            return;
        }

        // Add new contact if _contactId is null otherwise update existing contact
        bool result = _contactId is null ? _contactService.AddContact(Contact) : _contactService.UpdateContact(Contact);

        if (result)
            // Go back to MainPage
            await Shell.Current.Navigation.PopToRootAsync();
        else
            EmailErrorMessage = "Contact with this email already exist.";
    }

    // Command to return to the previous view
    [RelayCommand]
    public async Task Return() => await Shell.Current.GoToAsync("..");

    // Return true if email provided is valid
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        // Regex pattern for basic email validation
        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
    }
}
