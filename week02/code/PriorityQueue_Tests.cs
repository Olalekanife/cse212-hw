using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three values with different priorities and remove each in order until the queue is empty.
    // Expected Result: 20 is removed first because it has the highest priority; 10 is removed next; 5 is removed last.
    // Defect(s) Found: The current dequeue logic selects the wrong item and never removes the chosen item from the list.
    public void TestPriorityQueue_DequeuesHighestPriorityItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 5);
        priorityQueue.Enqueue("medium", 10);
        priorityQueue.Enqueue("high", 20);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add two items with the same top priority in FIFO order, followed by a lower-priority item.
    // Expected Result: The earliest high-priority item is removed first, then the next item with the same priority.
    // Defect(s) Found: The current implementation does not handle priority ties correctly and can skip the final item.
    public void TestPriorityQueue_UsesFIFOForSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 15);
        priorityQueue.Enqueue("second", 15);
        priorityQueue.Enqueue("third", 7);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to remove an item from an empty priority queue.
    // Expected Result: InvalidOperationException is thrown with the message "The queue is empty."
    // Defect(s) Found: The empty-queue check is present, but the code does not actually remove the selected item and can also skip items during selection.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}