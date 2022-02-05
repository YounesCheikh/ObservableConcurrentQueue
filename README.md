ObservableConcurrentQueue
=========================

<img src="img/ObservableConcurrentQueue.png" alt="ObservableConcurrentQueue" width="200"/>
<br /> 

![BUILD](https://github.com/cyounes/ObservableConcurrentQueue/workflows/BUILD/badge.svg)
[![NuGet Badge](https://buildstats.info/nuget/ObservableConcurrentQueue)](https://www.nuget.org/packages/ObservableConcurrentQueue/)

Using System.Collections.Concurrent.ConcurrentQueue with notifications

Get the latest version of source code from [~~Codeplex~~](https://observableconcurrentqueue.codeplex.com/)

Or get it from NUGET: 

``` 
PM> Install-Package ObservableConcurrentQueue

```

# Documentation

if you are not familiar with *ConcurrentQueue*, [Read more about it on MSDN](http://msdn.microsoft.com/en-us/library/dd267265(v=vs.110).aspx)

# Usage
## Syntax
### Create new instance
```Csharp
var observableConcurrentQueue = new ObservableConcurrentQueue();
``` 

#### Note about Thread Safety:
> According to [Mircosoft Documentation](https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1?redirectedfrom=MSDN&view=netcore-3.1#thread-safety) All public and protected members of `ConcurrentQueue<T>` are thread-safe and may be used concurrently from multiple threads. This additional Thread Safe option is just for some customization stuff. 

### Subscribe the Handler to the event ContentChanged
```csharp
observableConcurrentQueue.ContentChanged += OnObservableConcurrentQueueContentChanged;
```

## Example of handling method: 
```csharp
private static void OnObservableConcurrentQueueContentChanged(
 object sender,
 NotifyConcurrentQueueChangedEventArgs args)
 {
      // Item Added
      if (args.Action == NotifyConcurrentQueueChangedAction.Enqueue)
      {
          Console.WriteLine("New Item added: {0}", args.ChangedItem);
      }
 
      // Item deleted
      if (args.Action == NotifyConcurrentQueueChangedAction.Dequeue)
      {
          Console.WriteLine("New Item deleted: {0}", args.ChangedItem);
      }
 
      // Item peeked
      if (args.Action == NotifyConcurrentQueueChangedAction.Peek)
      {
           Console.WriteLine("Item peeked: {0}", args.ChangedItem);
      }
 
      // Queue Empty
      if (args.Action == NotifyConcurrentQueueChangedAction.Empty)
      {
           Console.WriteLine("Queue is empty");
      }
 } 
```

Once the handler is defined, we can start adding, deleting or getting elements from the concurrentQueue, and after each operation an event will be raised and handled by the method above.

## Event Args
The EventArgs object sent by the event contains 2 properties:

### NotifyConcurrentQueueChangedAction Action:

* *Enqueue*: If a new item has been enqueued.
* *Dequeue*: an item has been dequeued.
* *Peek*: an item has been peeked.
* *Empty*: The last element in the queue has been dequeued and the queue is empty.

### T ChangedItem:
The item which the changes applied on. can be null if the notification action is *NotifyConcurrentQueueChangedAction.Empty*.

# Supported Frameworks
## .NET Standard
netstandard1.1
netstandard1.2
netstandard1.3
netstandard1.4
netstandard1.5
netstandard1.6
netstandard2.0
netstandard2.1

## .NET Core
netcoreapp1.0
netcoreapp1.1
netcoreapp2.0
netcoreapp2.1
netcoreapp2.2
netcoreapp3.0
netcoreapp3.1
net5.0
net6.0

## .NET Framework
net40
net45
net451
net452
net46
net461
net462
net47
net471
net472
net48
