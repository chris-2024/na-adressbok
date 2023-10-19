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
    private string invalidEmail;

    public ManageContactViewModel(ContactModel contact, IContactService contactService)
    {
        _contactService = contactService;
        _contactId = contact?.Id;
        this.contact = new ContactModel(contact.Id) { FirstName = contact.FirstName, LastName = contact.LastName, Email = contact.Email, PhoneNumber = contact.PhoneNumber, Address = contact.Address };
        invalidEmail = "";
    }

    public ManageContactViewModel(IContactService contactService) : this(new(), contactService) { }

    [RelayCommand]
    async Task SaveContact()
    {
        bool result;

        if (!IsValidEmail(Contact.Email))
        {
            InvalidEmail = "Invalid Email.";
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
            InvalidEmail = "Contact with this email already exist.";
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        // Regex pattern for basic email validation
        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
    }
}
