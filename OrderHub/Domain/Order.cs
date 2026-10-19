namespace OrderHub.Domain;

public class Order
{
    // ЗАДАЧА: добавьте конструктор с параметрами Id, CustomerName, ProductName, Price, CreatedAt
    // ЗАДАЧА: объявите свойства только для чтения: Id, CustomerName, ProductName, Price, CreatedAt
    // подсказка: DateTime создаётся вызовом DateTime.Now, тип decimal — для денежных сумм
    public int Id { get; }
    public string CustomerName { get; }
    public string ProductName { get; }
    public decimal Price { get; }
    public DateTime CreatedAt { get; }

    public Order(int myId, string myCustomerName, string myProductName, decimal myPrice, DateTime myCreatedAt)
    {
        Id = myId;
        CustomerName = myCustomerName;
        ProductName = myProductName;
        Price = myPrice;
        CreatedAt = myCreatedAt;

    }
    // ЗАДАЧА: переопределите ToString() так, чтобы вывод был вида
    // "#12 Иванов — Клавиатура 5 490,00 ₽ (12.03.2026)"
    // подсказка: используйте интерполяцию $"...", форматирование даты — d.ToString("dd.MM.yyyy")
    public override string ToString() {
        return $"#{Id} {CustomerName} — {ProductName} {Price:N2} ₽ ({CreatedAt.ToString("dd.MM.yyyy")})";
    }

}