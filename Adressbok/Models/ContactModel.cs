namespace Adressbok.Models;

public class ContactModel
{
    public ContactModel()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public required string Email { get; set; }
    public int? PhoneNumber { get; set; }
    public string Address { get; set; }
}
