using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using Moq;

namespace Adressbok.Tests.UnitTests
{
    public class ContactManagementTests
    {
        private readonly Mock<IFileService<ContactModel>> mockFileService;
        private readonly List<ContactModel> contacts;
        private readonly IContactService contactService;

        public ContactManagementTests()
        {
            mockFileService = new Mock<IFileService<ContactModel>>();
            contacts = new List<ContactModel>();
            contactService = new ContactService(mockFileService.Object, contacts);
        }

        [Fact]
        public void AddContact_Should_ReturnTrueIfContactAddedToList()
        {
            // Arrange
            var newContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };

            // Act
            bool result = contactService.AddContact(newContact);

            // Assert
            Assert.True(result);
            Assert.Single(contacts); // Make sure only one contact was added to the List.
        }

        [Fact]
        public void GetContact_Should_ReturnTrueIfRetrievedCorrectContactById()
        {
            // Arrange
            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            var retrievedContact = contactService.GetContact(existingContact.Id);

            // Assert
            Assert.NotNull(retrievedContact);
            Assert.Equal(existingContact.Id, retrievedContact.Id);
        }

        [Fact]
        public void UpdateContact_Should_ReturnTrueIfContactUpdated()
        {
            // Arrange
            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };

            contacts.Add(existingContact);

            var updatedContact = new ContactModel(existingContact.Id)
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated@example.com"
            };

            // Act
            bool result = contactService.UpdateContact(updatedContact);

            // Assert
            Assert.True(result);
            var updatedContactInCollection = contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
            Assert.NotNull(updatedContactInCollection);
        }

        [Fact]
        public void RemoveContactById_Should_ReturnTrueIfCorrectContactRemoved()
        {
            // Arrange
            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            bool result = contactService.RemoveContact(existingContact.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(contacts); // Make sure the contact was removed from the List.
        }

        [Fact]
        public void RemoveContactByEmail_Should_ReturnTrueIfCorrectContactRemoved()
        {
            // Arrange
            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            bool result = contactService.RemoveContact(existingContact.Email);

            // Assert
            Assert.True(result);
            Assert.Empty(contacts); // Make sure the contact was removed from the List.
        }
    }
}

