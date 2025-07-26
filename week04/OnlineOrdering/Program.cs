using System;
using System.Collections.Generic;
using System.Text;

class Product
{
    private string _name;
    private string _id;
    private double _price;
    private int _quantity;

    public Product(string name, string id, double price, int quantity)
    {
        _name = name;
        _id = id;
        _price = price;
        _quantity = quantity;
    }

    public string Name { get { return _name; } }
    public string Id { get { return _id; } }
    public double Price { get { return _price; } }
    public int Quantity { get { return _quantity; } }

    public double GetTotalPrice()
    {
        return _price * _quantity;
    }
}

class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.ToLower() == "usa" || _country.ToLower() == "united states" || _country.ToLower() == "united states of america";
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}

class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string Name { get { return _name; } }
    public Address Address { get { return _address; } }

    public string GetCustomerInfo()
    {
        return $"{_name}\n{_address.GetFullAddress()}";
    }
}

class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (var product in _products)
        {
            total += product.GetTotalPrice();
        }
        total += ShippingCost();
        return total;
    }

    private double ShippingCost()
    {
        return _customer.Address.IsInUSA() ? 5 : 35;
    }

    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        label.AppendLine("Packing Label:");
        foreach (var product in _products)
        {
            label.AppendLine($"Product: {product.Name}, ID: {product.Id}");
        }
        return label.ToString();
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.Name}\n{_customer.Address.GetFullAddress()}";
    }
}

class Program
{
    static void Main()
    {
        // Pedido 1: cliente en EE. UU.
        Address address1 = new Address("123 Maple St", "Springfield", "IL", "USA");
        Customer customer1 = new Customer("John Doe", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "L1001", 1200.00, 1));
        order1.AddProduct(new Product("Mouse", "M2001", 25.50, 2));

        // Pedido 2: cliente fuera de EE. UU.
        Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Jane Smith", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Desk Chair", "D3001", 150.75, 1));
        order2.AddProduct(new Product("Monitor", "M4001", 300.00, 2));
        order2.AddProduct(new Product("Keyboard", "K5001", 45.99, 1));

        // Mostrar resultados pedido 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Order Cost: ${order1.CalculateTotalCost():F2}\n");

        // Mostrar resultados pedido 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Order Cost: ${order2.CalculateTotalCost():F2}");
    }
}
