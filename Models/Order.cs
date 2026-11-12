namespace KodaStore.Api.Models;

public sealed class Order
{
    private Order(){}
    
    public long OrderId {get; private set;}
    public string OrderNumber {get; private set;} = string.Empty;
    public DateTimeOffset OrderDate {get; private set;}
    public string Status {get; private set;} = string.Empty;
    public ICollection<OrderItem> Items {get; private set;} = new List<OrderItem>();


    public Order(string orderNumber)
    {
        if(string.IsNullOrWhiteSpace(orderNumber) || orderNumber.Trim().Length > 32)
        {
            throw new ArgumentException("order number ga boleh kosong dan max 32 karakter", nameof(orderNumber));
        }

        OrderNumber = orderNumber.Trim().ToUpperInvariant();
        OrderDate = DateTimeOffset.UtcNow.AddHours(7);
        Status = "Pending";
    }

    public void AddItem(Product product, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        if (Status != "Pending") throw new InvalidOperationException("Order tidak bisa diubah");
        if(Items.Any(i=>i.ProductCode == product.ProductCode)) throw new ArgumentException("Porduct Code tidak boleh berulan dalam satu order");

        Items.Add(new OrderItem(product,quantity));
    }

    public void Confirm()
    {
        if(Status == "Confirmed") return;

        Status = "Confirmed";
    }



}