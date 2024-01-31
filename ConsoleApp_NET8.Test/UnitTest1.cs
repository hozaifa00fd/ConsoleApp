using Microsoft.VisualStudio.TestPlatform.TestHost;
using ConsoleApp_NET8;

namespace ConsoleApp_NET8.Test
{
    public class AddressBookTests
    {
        [Fact]
        public void AddContact_ShouldAddContactToList()
        {

            // Arrange
            var program = new Program();

            // Act
            program.AddContact("John Doe", "123456789", "john.doe@example.com", "123 Main St");

            // Assert
            Assert.Single(program.addressBook);
        }

        [Fact]
        public void RemoveContact_ShouldRemoveContactFromList()
        {
            // Arrange
            var program = new Program();
            program.AddContact("John Doe", "123456789", "john.doe@example.com", "123 Main St");

            // Act
            program.RemoveContact("john.doe@example.com");

            // Assert
            Assert.Empty(program.addressBook);
        }

        [Fact]
        public void DisplayAllContacts_ShouldDisplayContacts()
        {
            // Arrange
            var program = new Program();
            program.AddContact("John Doe", "123456789", "john.doe@example.com", "123 Main St");

            // Act
            program.DisplayAllContacts();

            // Assert

        }

        [Fact]
        public void DisplayContactDetails_ShouldDisplayDetails()
        {
            // Arrange
            var program = new Program();
            program.AddContact("John Doe", "123456789", "john.doe@example.com", "123 Main St");

            // Act
            program.DisplayContactDetails("John Doe");

            // Assert
           
        }

        
    }

}