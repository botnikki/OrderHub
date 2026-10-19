using System.Data.Common;
using OrderHub.Domain;
namespace OrderHub.Services;

public class OrderService
{
    // ЗАДАЧА: объявите три коллекции с точной сигнатурой:
    // List<Order> allOrders — все заказы
    // Queue<Order> pendingQueue — заказы, ожидающие обработки, в порядке поступления
    // Stack<string> undoHistory — история последних действий для отмены
    // подсказка: для каждой коллекции создайте экземпляр прямо в поле,
    // например: private readonly Queue<Order> pendingQueue = new();
    private readonly List<Order> allOrders = new();
    private readonly Queue<Order> pendingQueue = new();
    private readonly Stack<string> undoHistory = new();

    private int _nextOrderId = 1; //Id счётчик

    public Order CreateOrder(string customerName, string
    productName, decimal price)
    {
        // ЗАДАЧА 1: создайте новый заказ (Id счётчик увеличивается на 1)
        Order newOrder = new Order(_nextOrderId, customerName, productName, price, DateTime.Now);
        _nextOrderId++;
        // ЗАДАЧА 2: добавьте заказ в allOrders
        allOrders.Add(newOrder);
        // ЗАДАЧА 3: добавьте тот же заказ в pendingQueue методом Enqueue
        pendingQueue.Enqueue(newOrder);
        // ЗАДАЧА 4: запишите в undoHistory описание действия для возможной отмены
        // подсказка: описание удобно собрать так:
        // undoHistory.Push($"Создан заказ {order.Id} для {customerName}");
        undoHistory.Push($"Создан заказ {newOrder.Id} для {customerName}");
        // ЗАДАЧА 5: верните созданный заказ
        return newOrder;
    }
    public Order? ProcessNext()
    {
        // ЗАДАЧА 1: если pendingQueue пуст — верните null
        // подсказка: проверка emptiness — pendingQueue.Count == 0
        if (pendingQueue.Count == 0)
            return null;
        // ЗАДАЧА 2: извлеките заказ из очереди методом Dequeue (именно он, а не Peek)
        Order processedOrder = pendingQueue.Dequeue();
        // ЗАДАЧА 3: запишите в undoHistory, какой заказ обработан
        undoHistory.Push($"Обработан заказ: #{processedOrder.Id}");
        // ЗАДАЧА 4: выведите в консоль $"Обработан заказ: {order}"
        Console.WriteLine($"Обработан заказ: {processedOrder}");
        // ЗАДАЧА 5: верните обработанный заказ
        return processedOrder;
    }
    public bool UndoLastAction()
    {
        // ЗАДАЧА 1: если undoHistory пуст — верните false
        if (undoHistory.Count == 0)
            return false;
        // ЗАДАЧА 2: извлеките последнее действие методом Pop
        string lastAction = undoHistory.Pop();
        // ЗАДАЧА 3: выведите $"Отменено действие: {action}" и верните true
        Console.WriteLine($"Отменено действие: {lastAction}");
        return true;
    }
    // ЗАДАЧА: допишите три метода-отчёта, используя foreach:
    // PrintAllOrders() — все заказы из allOrders по порядку добавления
    // PrintPendingQueue() — сколько заказов ждёт и какой первый в очереди (Peek, без извлечения)
    // PrintRecentActions() — содержимое undoHistory (обход идёт от последнего к первому)
    public Order? PrintAllOrders()
    {
        foreach (Order order in allOrders)
        {
            Console.WriteLine(order);
        }
        if (allOrders.Count == 0)
            return null;
        else
            return allOrders[allOrders.Count - 1];
    }
    public Order? PrintPendingQueue()
    {
        foreach (Order order in pendingQueue)
        {
            Console.WriteLine(order);
        }
        if (pendingQueue.Count > 0)
        {
            Order firstOrder = pendingQueue.Peek();
            return firstOrder;
        }
        return null;
    }
    public Order? PrintRecentActions()
    {
        foreach (string action in undoHistory)
        {
            Console.WriteLine(action);
        }
        return null;
    }
}