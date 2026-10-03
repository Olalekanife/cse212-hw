using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);

        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1

        Node newNode = new(value);

        // If the list is empty, both head and tail point to the new node.
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // Connect the new node to the current tail.
            newNode.Prev = _tail;

            // Connect the current tail to the new node.
            _tail.Next = newNode;

            // Update the tail.
            _tail = newNode;
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail
        // to null resulting in an empty list. This condition also
        // covers an empty list.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2

        // Covers both an empty list and a list with one node.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            // Disconnect the second-to-last node from the old tail.
            _tail.Prev!.Next = null;

            // Move tail backward.
            _tail = _tail.Prev;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the
        // head of the list.
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call InsertTail to add 'newValue'.
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location, create a new node and reconnect links.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }

                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3

        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the matching node is the head.
                if (curr == _head)
                {
                    RemoveHead();
                }
                // If the matching node is the tail.
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                // Otherwise remove a node from the middle.
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }

                // Stop after removing the first match.
                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4

        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }

            // Continue because every matching node must be replaced.
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // Call the generic version of the method.
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List.
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List.
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5

        var curr = _tail;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}