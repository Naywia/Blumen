using Blumen.Models;
using Blumen.Persistence;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [TestMethod]
    public void AddCustomerTest()
    {
        // Arrange
        CustomerRepo customerRepo = new();
        Customer customer = new()
        {
            Name = "TestGuy",
            Address = "TestAddress",
            PhoneNumber = 90909090,
            Email = "TestMail@Test",
            PaymentNumber = 0,
            PaymentNumberType = PaymentNumberType.Ingen,
        };
        
        // Act
        var status = customerRepo.AddItem(customer);
        var dbCustomer = customerRepo.GetCustomer(customer.Name);

        // Assert
        Assert.AreEqual(true, status);
        Assert.AreEqual(customer.Name, dbCustomer.Name);
        Assert.AreEqual(customer.Address, dbCustomer.Address);
        Assert.AreEqual(customer.PhoneNumber, dbCustomer.PhoneNumber);
        Assert.AreEqual(customer.Email, dbCustomer.Email);
        Assert.AreEqual(customer.PaymentNumber, dbCustomer.PaymentNumber);
        Assert.AreEqual(customer.PaymentNumberType, dbCustomer.PaymentNumberType);
        
        // Cleanup
        customerRepo.RemoveItem(dbCustomer);
    }
}