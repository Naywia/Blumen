using Blumen.Models;
using Blumen.Persistence;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    private CustomerRepo customerRepo;
    private ProductTypeRepo productTypeRepo;
    private ProductRepo productRepo;

    [TestInitialize]
    public void Initialize()
    {
        customerRepo = new();
        productTypeRepo = new();
        productRepo = new();
    }

    [TestMethod]
    public void AddCustomerTest()
    {
        // Arrange
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

        Assert.AreEqual(customer.Name, dbCustomer?.Name);
        Assert.AreEqual(customer.Address, dbCustomer?.Address);
        Assert.AreEqual(customer.PhoneNumber, dbCustomer?.PhoneNumber);
        Assert.AreEqual(customer.Email, dbCustomer?.Email);
        Assert.AreEqual(customer.PaymentNumber, dbCustomer?.PaymentNumber);
        Assert.AreEqual(customer.PaymentNumberType, dbCustomer?.PaymentNumberType);

        // Cleanup
        customerRepo.RemoveItem(dbCustomer);
    }

    [TestMethod]
    public void GetCustomerByNameTest()
    {

    }

    [TestMethod]
    public void RemoveCustomerTest()
    {

    }
    
    [TestMethod]
    public void AddProductTest()
    {
        // Arrange
        Product product = new()
        {
            Name = "TestProduct",
            Price = 0.0,
            Description = "Test",
            Quantity = 0,
            Type = productTypeRepo.GetItems()[0],
        };

        // Act
        var status = productRepo.AddItem(product);
        
        var dbProduct = productRepo.GetProduct(product.Name);

        // Assert
        Assert.AreEqual(true, status);

        Assert.AreEqual(product.Name, dbProduct?.Name);
        Assert.AreEqual(product.Price, dbProduct?.Price);
        Assert.AreEqual(product.Description, dbProduct?.Description);
        Assert.AreEqual(product.Quantity, dbProduct?.Quantity);
        //Assert.AreEqual(product.Type, dbProduct?.Type);

        // Cleanup
        productRepo.RemoveItem(dbProduct);
    }
    
    [TestMethod]
    public void GetProductByNameTest()
    {

    }

    [TestMethod]
    public void RemoveProductTest()
    {

    }
    
    [TestCleanup]
    public void Cleanup()
    {
        customerRepo = null;
        productRepo = null;
    }

}