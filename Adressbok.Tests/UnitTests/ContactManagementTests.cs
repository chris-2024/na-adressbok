using Adressbok.Interfaces;
using Adressbok.Models;
using Adressbok.Services;
using Moq;
using System.Collections.ObjectModel;

namespace Adressbok.Tests.UnitTests
{
    public class ContactManagementTests
    {
        [Fact]
        public void AddContact_Should_AddContact()
        {
            // Arrange
            var mockFileService = new Mock<IFileService<ContactModel>>();
            var contacts = new ObservableCollection<ContactModel>();
            var mockContactService = new ContactService(mockFileService.Object, contacts);

            var newContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };

            // Act
            bool result = mockContactService.AddContact(newContact);

            // Assert
            Assert.True(result);
            Assert.Single(contacts); // Make sure the contact was added to the collection.
        }

        [Fact]
        public void GetContact_Should_ReturnContactById()
        {
            // Arrange
            var mockFileService = new Mock<IFileService<ContactModel>>();
            var contacts = new ObservableCollection<ContactModel>();
            var mockContactService = new ContactService(mockFileService.Object, contacts);

            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            var retrievedContact = mockContactService.GetContact(existingContact.Id);

            // Assert
            Assert.NotNull(retrievedContact);
            Assert.Equal(existingContact.Id, retrievedContact.Id);
        }

        [Fact]
        public void UpdateContact_Should_UpdateContact()
        {
            // Arrange
            var mockFileService = new Mock<IFileService<ContactModel>>();
            var contacts = new ObservableCollection<ContactModel>();
            var mockContactService = new ContactService(mockFileService.Object, contacts);

            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            var updatedContact = new ContactModel
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated@example.com"
            };

            // Act
            bool result = mockContactService.UpdateContact(updatedContact);

            // Assert
            Assert.True(result);
            var updatedContactInCollection = contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
            Assert.NotNull(updatedContactInCollection);
            Assert.Equal("UpdatedFirstName", updatedContactInCollection.FirstName);
            Assert.Equal("UpdatedLastName", updatedContactInCollection.LastName);
            Assert.Equal("updated@example.com", updatedContactInCollection.Email);
        }

        [Fact]
        public void RemoveContact_Should_RemoveContactById()
        {
            // Arrange
            var mockFileService = new Mock<IFileService<ContactModel>>();
            var contacts = new ObservableCollection<ContactModel>();
            var mockContactService = new ContactService(mockFileService.Object, contacts);

            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            bool result = mockContactService.RemoveContact(existingContact.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(contacts); // Make sure the contact was removed from the collection.
        }

        [Fact]
        public void RemoveContact_Should_RemoveContactByEmail()
        {
            // Arrange
            var mockFileService = new Mock<IFileService<ContactModel>>();
            var contacts = new ObservableCollection<ContactModel>();
            var mockContactService = new ContactService(mockFileService.Object, contacts);

            var existingContact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };
            contacts.Add(existingContact);

            // Act
            bool result = mockContactService.RemoveContact(existingContact.Email);

            // Assert
            Assert.True(result);
            Assert.Empty(contacts); // Make sure the contact was removed from the collection.
        }
    }
}

