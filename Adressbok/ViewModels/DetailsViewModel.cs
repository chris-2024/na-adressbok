﻿using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace Adressbok.ViewModels;

public partial class DetailsViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ContactModel contact;
    [ObservableProperty]
    private string displayName;

    public DetailsViewModel(ContactModel contact, IContactService contactService)
    {
        _contactService = contactService;
        this.contact = contact;
        displayName = string.Join(" ", new[] { contact.FirstName, contact.LastName }.Where(s => !string.IsNullOrEmpty(s)));
    }

    [RelayCommand]
    public async Task RemoveContact()
    {
        _contactService.RemoveContact(Contact.Id);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task GoToEdit()
    {
        //var viewModel = new AddEditViewModel(contact, _contactService);
        //var editPage = new DetailsPage(viewModel);
        //await Shell.Current.Navigation.PushAsync(editPage);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task Return() => await Shell.Current.GoToAsync("..");

}