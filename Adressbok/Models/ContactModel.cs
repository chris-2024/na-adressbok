namespace Adressbok.Models;

public class ContactModel
{
    public ContactModel(Guid id)
    {
        Id = id != Guid.Empty ? id : Guid.NewGuid();
    }
    
    public ContactModel() : this(Guid.Empty) { }

    public Guid Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
