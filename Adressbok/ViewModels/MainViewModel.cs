using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Adressbok.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IContactService _contactService;

    public MainViewModel() 
    {
        _contactService = new ContactService();
        Contacts = new(_contactService.GetAllContacts());

        if (Contacts.Count > 0 )
        {
            Contacts.Add(new ContactModel() { FirstName = "Bob", Email = "bob@mail.se" });
            Contacts.Add(new ContactModel() { FirstName = "Bert", Email = "Bert@mail.se" });
        }
    }

    public ObservableCollection<ContactModel> Contacts { get; set; }

}
