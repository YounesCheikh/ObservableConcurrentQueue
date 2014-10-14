// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="BledSoft">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------
namespace System.Collections.Concurrent
{
    /// <summary>
    /// Observable Concurrent queue changed event handler
    /// </summary>
    /// <typeparam name="T">
    /// The concurrent queue elements type
    /// </typeparam>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The <see cref="NotifyConcurrentQueueChangedEventArgs{T}"/> instance containing the event data.
    /// </param>
    public delegate void ConcurrentQueueChangedEventHandler<T>(
        object sender, 
        NotifyConcurrentQueueChangedEventArgs<T> args);
}