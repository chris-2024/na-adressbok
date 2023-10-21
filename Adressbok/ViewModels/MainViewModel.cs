using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using Adressbok.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Adressbok.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ObservableCollection<ContactModel> contacts;

    [ObservableProperty]
    private string contactEmail;

    [ObservableProperty]
    private string removeContactByEmailMessage;

    public MainViewModel(ContactService contactService) 
    {
        _contactService = contactService;
        ResetMainView();
        _contactService.ContactsUpdated += ResetMainView; // Call whenever contacts are updated
    }

    // Refresh the main view by updating fields and reloading contacts
    private void ResetMainView()
    {
        Contacts = new(_contactService.GetAllContacts());
        RemoveContactByEmailMessage = "";
        ContactEmail = "";
    }

    // Command to navigate to the details view for a specific contact
    [RelayCommand]
    public async Task GoToDetails(ContactModel contact)
    {
        var viewModel = new DetailsViewModel(contact, _contactService);
        var detailPage = new DetailsPage(viewModel);
        await Shell.Current.Navigation.PushAsync(detailPage);
    }

    // Command to navigate to the view for adding a new contact
    [RelayCommand]
    public async Task GoToAddContact()
    {
        var viewModel = new ManageContactViewModel(_contactService);
        var manageContactPage = new ManageContactPage(viewModel);
        await Shell.Current.Navigation.PushAsync(manageContactPage);
    }

    // Command to remove a contact by email
    [RelayCommand]
    public void RemoveContactByEmail()
    {
        var result = _contactService.RemoveContact(ContactEmail);

        RemoveContactByEmailMessage = result ? "" : "No contact with that email";
    }
}
