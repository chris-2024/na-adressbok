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

        // Initiate get all contacts
        GetContacts();

        // Subscribe to contacts being uppdated and reload contacts
        _contactService.ContactsUpdated += GetContacts;

        // Subscribe to changes to property changed in this MainViewModel
        this.PropertyChanged += (sender, e) =>
        {
            // Update ContactEmail when contacts is changed
            if (e.PropertyName == nameof(Contacts))
                ContactEmail = "";
            // Update message when ContactEmail is changed
            if (e.PropertyName == nameof(ContactEmail))
                RemoveContactByEmailMessage = "";
        };
    }

    private void GetContacts() => Contacts = new(_contactService.GetAllContacts());

    // Command to navigate to the details view for a specific contact
    [RelayCommand]
    public async Task GoToDetails(ContactModel contact)
    {
        var viewModel = new DetailsViewModel(contact, _contactService);
        var detailPage = new DetailsPage(viewModel);
        await Shell.Current.Navigation.PushAsync(detailPage);
        ContactEmail = "";
    }

    // Command to navigate to the view for adding a new contact
    [RelayCommand]
    public async Task GoToAddContact()
    {
        var viewModel = new ManageContactViewModel(_contactService);
        var manageContactPage = new ManageContactPage(viewModel);
        await Shell.Current.Navigation.PushAsync(manageContactPage);
        ContactEmail = "";
    }

    // Command to remove a contact by email
    [RelayCommand]
    public void RemoveContactByEmail()
    {
        var result = _contactService.RemoveContact(ContactEmail);

        RemoveContactByEmailMessage = result ? "" : "No contact with that email";
    }
}
