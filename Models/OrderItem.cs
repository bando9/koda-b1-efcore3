namespace KodaStore.Api.Models;


public sealed class OrderItem
{
    private OrderItem(){}

    public long OrderItemId {get; private set;}

    // FK Property Order
    public long OrderId {get;private set;}
    public Order Order {get; private set;} = null!;
    
    // FK Property Product
    public string ProductCode {get; private set;}
    public Product Product {get; private set;}

    public int Quantity {get; private set;}
    public decimal UnitPrice {get; private set;}
    public decimal Subtotal {get; private set;}


    public OrderItem (Product product, int quantity)
    {
        if(quantity <= 0) throw new ArgumentOutOfRangeException("quantity gabisa kurang dari 0.");
        ProductCode = product.ProductCode;
        Product = product;
        Quantity = quantity;
        UnitPrice=product.Price;
    }
}