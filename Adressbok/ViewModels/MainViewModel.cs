using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Adressbok.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public MainViewModel() 
    {
        _contactService = new ContactService();
        Contacts = new(_contactService.GetAllContacts());
    }

    public ObservableCollection<ContactModel> Contacts { get; set; }
}
