// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Younes Cheikh
// </Author>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableConcurrentQueue.Demo
{
    public class Program
    {
        static ObservableConcurrentQueue<int> observableConcurrentQueue = new ObservableConcurrentQueue<int>();

        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static async Task Main(string[] args)
        {            
            observableConcurrentQueue.ContentChanged += OnObservableConcurrentQueueContentChanged;
            observableConcurrentQueue.CollectionChanged += OnObservableConcurrentQueueCollectionChanged;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("### Parallel testing for ObservableConcurrentQueue ###");
            Console.ResetColor();
            await TryItAsync();
            observableConcurrentQueue.ContentChanged -= OnObservableConcurrentQueueContentChanged;
            observableConcurrentQueue.CollectionChanged -= OnObservableConcurrentQueueCollectionChanged;
            Console.WriteLine("End. Press any key to exit...");
            Console.ReadKey();
        }

        private static Task TryItAsync()
        {
            return Task.Run(() =>

            {
                Console.WriteLine("Enqueue elements...");
                Parallel.For(1, 20, i => { observableConcurrentQueue.Enqueue(i); });

                int item;

                Console.WriteLine("Peek & Dequeue 5 elements...");
                Parallel.For(0, 5, i =>
                {
                    observableConcurrentQueue.TryPeek(out item);
                    Thread.Sleep(300);
                    observableConcurrentQueue.TryDequeue(out item);
                });

                Thread.Sleep(300);

                observableConcurrentQueue.TryPeek(out item);
                Thread.Sleep(300);

                Console.WriteLine("Dequeue all elements...");

                Parallel.For(1, 20, i =>
                {
                    while (observableConcurrentQueue.TryDequeue(out item))
                    {
                        // NO SLEEP, Force Concurrence
                        // Thread.Sleep(300);
                    }
                });
            }
            );
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
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"[+] New Item added: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Dequeue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[-] New Item deleted: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Peek)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"[O] Item peeked: {args.ChangedItem}");
            }

            if (args.Action == NotifyConcurrentQueueChangedAction.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[ ] Queue is empty");
            }

            Console.ResetColor();
        }

        private static void OnObservableConcurrentQueueCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"[+] Collection Changed [Add]: New Item added: {args.NewItems[0]}");
            }

            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[-] Collection Changed [Remove]: New Item deleted: {args.OldItems[0]}");
            }

            if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[ ] Collection Changed [Reset]: Queue is empty");
            }

            Console.ResetColor();
        }

        #endregion
    }
}
