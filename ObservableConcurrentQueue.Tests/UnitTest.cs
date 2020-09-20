// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObservableConcurrentQueue.Tests
{
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
        private bool _isQueueEmpty = true;

        /// <summary>
        ///     The queue
        /// </summary>
        private ObservableConcurrentQueue<int> _queue;

        /// <summary>
        ///     The queue new item.
        /// </summary>
        private int _queueAddedItem;

        /// <summary>
        ///     The queue deleted item.
        /// </summary>
        private int _queueDeletedItem;

        /// <summary>
        ///     The queue peeked item
        /// </summary>
        private int _queuePeekedItem;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            _queue = new ObservableConcurrentQueue<int>();
            _queue.ContentChanged += OnQueueChanged;
        }

        /// <summary>
        ///     Mains the test.
        /// </summary>
        [TestMethod]
        public void MainTest()
        {
            // Add 2 elements.
            EnqueueEventTest();

            // Dequeue 1 element.
            DequeueEventTest();

            // Peek 1 element.
            PeekEventTest();

            // Dequeue all elements
            // the queue should be empty
            EmptyQueueTest();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     De-queue event test.
        /// </summary>
        private void DequeueEventTest()
        {
            var result = _queue.TryDequeue(out var item);
            Assert.IsTrue(result);
            Assert.AreEqual(item, _queueDeletedItem);
            Assert.IsFalse(_isQueueEmpty);
        }

        /// <summary>
        ///     Empties the queue test.
        /// </summary>
        private void EmptyQueueTest()
        {
            while (_queue.TryDequeue(out int item))
            {
                Assert.AreEqual(item, _queueDeletedItem);
            }

            Assert.IsTrue(_isQueueEmpty);
        }

        /// <summary>
        ///     Enqueues the event test.
        /// </summary>
        private void EnqueueEventTest()
        {
            const int item = 11;
            _queue.Enqueue(item);
            Assert.AreEqual(item, _queueAddedItem);
            Assert.IsFalse(_isQueueEmpty);

            _queue.Enqueue(item + 1);
            Assert.AreEqual(item + 1, _queueAddedItem);
            Assert.IsFalse(_isQueueEmpty);
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
                        _queueAddedItem = args.ChangedItem;
                        _isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Dequeue:
                    {
                        _queueDeletedItem = args.ChangedItem;
                        _isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Peek:
                    {
                        _queuePeekedItem = args.ChangedItem;
                        _isQueueEmpty = false;
                        break;
                    }

                case NotifyConcurrentQueueChangedAction.Empty:
                    {
                        _isQueueEmpty = true;
                        break;
                    }
            }
        }

        /// <summary>
        ///     Peeks the event test.
        /// </summary>
        private void PeekEventTest()
        {
            var result = _queue.TryPeek(out int item);
            Assert.IsTrue(result);
            Assert.AreEqual(item, _queuePeekedItem);
            Assert.IsFalse(_isQueueEmpty);
        }

        #endregion
    }
}