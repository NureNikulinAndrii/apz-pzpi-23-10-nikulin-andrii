using System;
using System.Collections.Generic;

// Інтерфейс підписника (Спостерігача)
public interface IObserver
{
    void Update(ISubject subject);
}

// Інтерфейс видавця (Суб'єкта)
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}
// Конкретний видавець
public class ConcreteSubject : ISubject
{
    public int State { get; set; } = 0;
    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    // Метод, що імітує зміну стану (бізнес-логіку)
    public void SomeBusinessLogic()
    {
        State = new Random().Next(0, 10);
        Console.WriteLine($"\nСуб'єкт: Мій стан змінився на: {State}");
        Notify();
    }
}

// Конкретний спостерігач
public class ConcreteObserver : IObserver
{
    public void Update(ISubject subject)
    {
        if (subject is ConcreteSubject concreteSubject)
        {
            Console.WriteLine($"Спостерігач: Відреагував на новий стан ({concreteSubject.State}).");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо видавця
        var subject = new ConcreteSubject();

        // Створюємо двох спостерігачів
        var observer1 = new ConcreteObserver();
        var observer2 = new ConcreteObserver();

        // Підписуємо їх на видавця
        subject.Attach(observer1);
        subject.Attach(observer2);

        // Змінюємо стан видавця (спостерігачі відреагують автоматично)
        subject.SomeBusinessLogic();
        subject.SomeBusinessLogic();

        // Відписуємо одного спостерігача
        subject.Detach(observer1);

        // Знову змінюємо стан (тепер відреагує лише observer2)
        subject.SomeBusinessLogic();
    }
}