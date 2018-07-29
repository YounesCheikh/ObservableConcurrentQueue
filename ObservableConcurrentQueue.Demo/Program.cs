// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------
namespace System.Collections.Concurrent.Demo
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The program.
    /// </summary>
    public class Program
    {
        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            var observableConcurrentQueue = new ObservableConcurrentQueue<int>();
            observableConcurrentQueue.ContentChanged += OnObservableConcurrentQueueContentChanged;
            var task = new Task(
                () =>
                    {
                        Console.WriteLine("Enqueue elements...");
                        for (int i = 1; i <= 20; i++)
                        {
                            observableConcurrentQueue.Enqueue(i);
                            Thread.Sleep(100);
                        }

                        int item;

                        Console.WriteLine("Peek & Dequeue 5 elements...");
                        for (int i = 0; i < 5; i++)
                        {
                            observableConcurrentQueue.TryPeek(out item);
                            Thread.Sleep(300);
                            observableConcurrentQueue.TryDequeue(out item);
                            Thread.Sleep(300);
                        }

                        observableConcurrentQueue.TryPeek(out item);
                        Thread.Sleep(300);

                        Console.WriteLine("Dequeue all elements...");
                        while (observableConcurrentQueue.TryDequeue(out item))
                        {
                            Thread.Sleep(300);
                        }
                    });
            task.Start();
            Console.WriteLine("End. Press any key to exit...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// The observable concurrent queue on changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void OnObservableConcurrentQueueContentChanged(
            object sender, 
            NotifyConcurrentQueueChangedEventArgs<int> args)
        {
            if (args.Action == NotifyConcurrentQueueChangedAction.Enqueue)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"New Item added: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Dequeue)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"New Item deleted: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Peek)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Item peeked: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Empty)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Queue is empty");
            }

            Console.ResetColor();
        }

        #endregion
    }
}