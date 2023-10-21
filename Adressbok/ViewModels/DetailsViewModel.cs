using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Adressbok.ViewModels;

public partial class DetailsViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ContactModel contact;

    public DetailsViewModel(ContactModel contact, IContactService contactService)
    {
        _contactService = contactService;
        this.contact = contact;
    }

    // Command to remove this contact
    [RelayCommand]
    public async Task RemoveContact()
    {
        _contactService.RemoveContact(Contact.Id);
        await Return();
    }

    // Command to navigate to the update contact view
    [RelayCommand]
    public async Task GoToUpdateContact()
    {
        var viewModel = new ManageContactViewModel(Contact, _contactService);
        var manageContactPage = new ManageContactPage(viewModel);
        await Shell.Current.Navigation.PushAsync(manageContactPage);
    }

    // Command to return to the previous view
    [RelayCommand]
    public async Task Return() => await Shell.Current.GoToAsync("..");

}
