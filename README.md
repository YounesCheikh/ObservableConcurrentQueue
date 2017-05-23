ObservableConcurrentQueue
=========================

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
```
// Create new instance
var observableConcurrentQueue = new ObservableConcurrentQueue();
// Subscribe the Handler to the event ContentChanged
observableConcurrentQueue.ContentChanged += OnObservableConcurrentQueueContentChanged;
```

### Example of handling method: 
```
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

### Event Args
The EventArgs object sent by the event contains 2 properties:

#### NotifyConcurrentQueueChangedAction Action:

* *Enqueue*: If a new item has been enqueued.
* *Dequeue*: an item has been dequeued.
* *Peek*: an item has been peeked.
* *Empty*: The last element in the queue has been dequeued and the queue is empty.

#### T ChangedItem:
The item which the changes applied on. can be null if the notification action is *NotifyConcurrentQueueChangedAction.Empty*.
