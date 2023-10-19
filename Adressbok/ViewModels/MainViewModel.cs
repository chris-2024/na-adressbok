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

    public MainViewModel(ContactService contactService) 
    {
        _contactService = contactService;
        GetAllContacts();
        _contactService.ContactsUpdated += GetAllContacts;
    }

    private void GetAllContacts()
    {
        Contacts = new(_contactService.GetAllContacts());
    }

    [RelayCommand]
    public async Task GoToDetails(ContactModel contact)
    {
        var viewModel = new DetailsViewModel(contact, _contactService);
        var detailPage = new DetailsPage(viewModel);
        await Shell.Current.Navigation.PushAsync(detailPage);
    }

    [RelayCommand]
    public async Task GoToAddContact(ContactModel contact)
    {
        var viewModel = new ManageContactViewModel(contact, _contactService);
        var manageContactPage = new ManageContactPage(viewModel);
        await Shell.Current.Navigation.PushAsync(manageContactPage);
    }
}
