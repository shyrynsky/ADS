namespace ADS.DataStructures;

public class BinomialHeap
{
    public class BinomialNode
    {
        internal BinomialNode? Parent;

        public int Key { internal set; get; }
        internal int Degree;
        internal BinomialNode? Child;
        internal BinomialNode? Sibling;

        internal BinomialNode(int key)
        {
            Key = key;
        }
    }

    private BinomialNode _head;

    private void _merge(ref BinomialHeap other)
    {
        var list1 = _head.Sibling;
        var list2 = other._head.Sibling;
        _head = new BinomialNode(default);
        var currNode = _head;
        while (list1 != null && list2 != null)
        {
            if (list1.Degree <= list2.Degree)
            {
                currNode.Sibling = list1;
                list1 = list1.Sibling;
            }
            else
            {
                currNode.Sibling = list2;
                list2 = list2.Sibling;
            }

            currNode = currNode.Sibling;
        }

        while (list1 != null)
        {
            currNode.Sibling = list1;
            list1 = list1.Sibling;
            currNode = currNode.Sibling;
        }

        while (list2 != null)
        {
            currNode.Sibling = list2;
            list2 = list2.Sibling;
            currNode = currNode.Sibling;
        }
    }

    private static void _treeMerge(BinomialNode mainNode, BinomialNode attachedNode)
    {
        attachedNode.Sibling = mainNode.Child;
        mainNode.Child = attachedNode;
        attachedNode.Parent = mainNode;
        mainNode.Degree++;
    }

    public void Meld(ref BinomialHeap other)
    {
        _merge(ref other);
        var prevNode = _head;
        var currNode = _head.Sibling;
        if (currNode == null)
            return;
        var nextNode = currNode.Sibling;
        while (nextNode != null)
        {
            if (currNode.Degree != nextNode.Degree ||
                nextNode.Sibling != null && nextNode.Degree == nextNode.Sibling.Degree)
            {
                prevNode = currNode;
                currNode = nextNode;
            }
            else
            {
                if (currNode.Key <= nextNode.Key)
                {
                    currNode.Sibling = nextNode.Sibling;
                    _treeMerge(currNode, nextNode);
                }
                else
                {
                    prevNode.Sibling = nextNode;
                    _treeMerge(nextNode, currNode);
                    currNode = nextNode;
                }
            }

            nextNode = currNode.Sibling;
        }
    }

    public void DecreaseKey(ref BinomialNode node, int newKey)
    {
        if (newKey > node.Key)
            throw new Exception();
        node.Key = newKey;
        while (node.Parent != null && node.Parent.Key > newKey)
        {
            (node.Key, node.Parent.Key) = (node.Parent.Key, node.Key);
            node = node.Parent;
        }
    }

    public BinomialNode Add(int elem)
    {
        var newNode = new BinomialNode(elem);
        var newHeap = new BinomialHeap(newNode);
        Meld(ref newHeap);
        return newNode;
    }

    private static BinomialHeap _childListToHeap(BinomialNode childList)
    {
        var currNode = childList;
        var nextNode = childList.Sibling;
        currNode.Parent = null;
        currNode.Sibling = null;
        while (nextNode != null)
        {
            var prevNode = currNode;
            currNode = nextNode;
            nextNode = nextNode.Sibling;
            currNode.Parent = null;
            currNode.Sibling = prevNode;
        }

        return new BinomialHeap(currNode);
    }

    public int ExtractMin()
    {
        var prevMinNode = _getPrevMinNode();
        var childList = prevMinNode.Sibling!.Child;

        var minNode = prevMinNode.Sibling;
        var minKey = minNode.Key;
        prevMinNode.Sibling = minNode.Sibling;

        if (childList == null) return minKey;
        var childHeap = _childListToHeap(childList);
        Meld(ref childHeap);

        return minKey;
    }

    private BinomialNode _getPrevMinNode()
    {
        var minPrevNode = _head;
        var minKey = minPrevNode.Sibling!.Key;
        var prevNode = minPrevNode.Sibling;
        while (prevNode.Sibling != null)
        {
            if (prevNode.Sibling.Key < minKey)
            {
                minKey = prevNode.Sibling.Key;
                minPrevNode = prevNode;
            }

            prevNode = prevNode.Sibling;
        }

        return minPrevNode;
    }

    public int GetMin()
    {
        return _getPrevMinNode().Sibling!.Key;
    }

    public BinomialHeap()
    {
        _head = new BinomialNode(default);
    }

    private BinomialHeap(BinomialNode childList)
    {
        _head = new BinomialNode(default)
        {
            Sibling = childList
        };
    }
}