using Base.Domain.Common;
using Base.Domain.Events;

namespace Base.Domain.Entities.Aggregates.Product;

public class Product : BaseEntity<int>
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Image { get; private set; }
    public Rating Rating { get; private set; }

    protected Product() { }

    private Product(string title, decimal price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;
    }

    public void AlterPrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public static Product Create(string title, decimal price, string description, string category, string image, Rating rating)
    {
        var product = new Product(title, price, description, category, image, rating);
        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }
    public void Update(string title, string description, string category, string image)
    {
        Title = title;
        Description = description;
        Category = category;
        Image = image;
    }
}


//public class Product : BaseEntity<int>
//{
//    public string Name { get; private set; }
//    public decimal Price { get; private set; }

//    private Product(string name, decimal price)
//    {
//        Name = name;
//        Price = price;
//    }

//    public void AlterPrice(decimal newPrice)
//    {
//        Price = newPrice;
//    }

//    public static Product Create(string name, decimal price)
//    {
//        var product = new Product(name, price);

//        product.AddDomainEvent(new ProductCreatedEvent(product));

//        return product;
//    }
//}

