// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableConcurrentQueue.Demo
{
    public class Program
    {
        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("### TRYING ObservableConcurrentQueue ###");
            Console.ResetColor();
            await TryItAsync();
            Console.WriteLine("End. Press any key to exit...");
            Console.ReadKey();
        }

        private static Task TryItAsync()
        {

            var observableConcurrentQueue = new ObservableConcurrentQueue<int>();
            observableConcurrentQueue.ContentChanged += OnObservableConcurrentQueueContentChanged;
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
