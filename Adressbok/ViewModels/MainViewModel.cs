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
        _contactService.ContactsUpdated += ResetMainView;
    }

    private void ResetMainView()
    {
        Contacts = new(_contactService.GetAllContacts());
        RemoveContactByEmailMessage = "";
        ContactEmail = "";
    }

    [RelayCommand]
    public async Task GoToDetails(ContactModel contact)
    {
        var viewModel = new DetailsViewModel(contact, _contactService);
        var detailPage = new DetailsPage(viewModel);
        await Shell.Current.Navigation.PushAsync(detailPage);
    }

    [RelayCommand]
    public async Task GoToAddContact()
    {
        var viewModel = new ManageContactViewModel(_contactService);
        var manageContactPage = new ManageContactPage(viewModel);
        await Shell.Current.Navigation.PushAsync(manageContactPage);
    }

    [RelayCommand]
    public void RemoveContactByEmail()
    {
        var result = _contactService.RemoveContact(ContactEmail);

        RemoveContactByEmailMessage = result ? "" : "No contact with that email";
    }
}
