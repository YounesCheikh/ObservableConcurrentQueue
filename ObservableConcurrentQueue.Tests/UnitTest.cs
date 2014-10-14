// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="BledSoft">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------
namespace System.Collections.Concurrent.Tests
{
    using System.Collections.Concurrent;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The unit test.
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        #region Fields

        /// <summary>
        ///     Gets or sets a value indicating whether the queue is empty.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the queue instance is empty; otherwise, <c>false</c>.
        /// </value>
        private bool isQueueEmpty = true;

        /// <summary>
        ///     The queue
        /// </summary>
        private ObservableConcurrentQueue<int> queue;

        /// <summary>
        ///     The queue new item.
        /// </summary>
        private int queueAddedItem;

        /// <summary>
        ///     The queue deleted item.
        /// </summary>
        private int queueDeletedItem;

        /// <summary>
        ///     The queue peeked item
        /// </summary>
        private int queuePeekedItem;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            this.queue = new ObservableConcurrentQueue<int>();
            this.queue.ContentChanged += this.OnQueueChanged;
        }

        /// <summary>
        ///     Mains the test.
        /// </summary>
        [TestMethod]
        public void MainTest()
        {
            // Add 2 elements.
            this.EnqueueEventTest();

            // Dequeue 1 element.
            this.DequeueEventTest();

            // Peek 1 element.
            this.PeekEventTest();

            // Dequeue all elements
            // the queue should be empty
            this.EmptyQueueTest();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     De-queue event test.
        /// </summary>
        private void DequeueEventTest()
        {
            int item;
            var result = this.queue.TryDequeue(out item);
            Assert.IsTrue(result);
            Assert.AreEqual(item, this.queueDeletedItem);
            Assert.IsFalse(this.isQueueEmpty);
        }

        /// <summary>
        ///     Empties the queue test.
        /// </summary>
        private void EmptyQueueTest()
        {
            int item;
            while (this.queue.TryDequeue(out item))
            {
                Assert.AreEqual(item, this.queueDeletedItem);
            }

            Assert.IsTrue(this.isQueueEmpty);
        }

        /// <summary>
        ///     Enqueues the event test.
        /// </summary>
        private void EnqueueEventTest()
        {
            const int Item = 11;
            this.queue.Enqueue(Item);
            Assert.AreEqual(Item, this.queueAddedItem);
            Assert.IsFalse(this.isQueueEmpty);

            this.queue.Enqueue(Item + 1);
            Assert.AreEqual(Item + 1, this.queueAddedItem);
            Assert.IsFalse(this.isQueueEmpty);
        }

        /// <summary>
        /// The on queue changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnQueueChanged(object sender, NotifyConcurrentQueueChangedEventArgs<int> args)
        {
            switch (args.Action)
            {
                case NotifyConcurrentQueueChangedAction.Enqueue:
                    {
                        this.queueAddedItem = args.ChangedItem;
                        this.isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Dequeue:
                    {
                        this.queueDeletedItem = args.ChangedItem;
                        this.isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Peek:
                    {
                        this.queuePeekedItem = args.ChangedItem;
                        this.isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Empty:
                    {
                        this.isQueueEmpty = true;
                        break;
                    }
            }
        }

        /// <summary>
        ///     Peeks the event test.
        /// </summary>
        private void PeekEventTest()
        {
            int item;
            var result = this.queue.TryPeek(out item);
            Assert.IsTrue(result);
            Assert.AreEqual(item, this.queuePeekedItem);
            Assert.IsFalse(this.isQueueEmpty);
        }

        #endregion
    }
}