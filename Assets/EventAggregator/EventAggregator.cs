using UniRx;
using System;
using System.Threading.Tasks;
using UniRx.Async;

public class EventAggregator
{
    static IMessageBroker messageBroker;
    static IMessageBroker MessageBroker => messageBroker ??  (messageBroker = new MessageBroker());

    public static void Publish<T>(T message)
    { MessageBroker.Publish(message); }

    public static IObservable<T> OnEvent<T>()
    { return MessageBroker.Receive<T>(); }
}

public class EventAggregatorAsync
{
    static IAsyncMessageBroker messageBroker;
    static IAsyncMessageBroker MessageBroker => messageBroker ??  (messageBroker = new AsyncMessageBroker());

    public static UniTask PublishAsync<T>(T message)
    {
        return MessageBroker.PublishAsync(message).ToUniTask();
    }

    public static IDisposable Subscribe<T>(Func<T, Task> taskGetter)
    {
        return MessageBroker.Subscribe<T>(message => taskGetter.Invoke(message).ToObservable());
    }
}