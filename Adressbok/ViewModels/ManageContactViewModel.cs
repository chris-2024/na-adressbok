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

    public ManageContactViewModel(ContactModel contact, IContactService contactService)
    {
        _contactService = contactService;
        _contactId = contact?.Id;
        this.contact = contact ?? new();
    }

    [RelayCommand]
    async Task SaveContact()
    {
        bool result;

        if (string.IsNullOrWhiteSpace(Contact.Email)) return;

        if (_contactId is null)
            // Add new contact
            result = _contactService.AddContact(Contact);
        else
            // Update existing contact
            result = _contactService.UpdateContact(Contact);

        if (result)
        {
            // Go back to MainPage
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
