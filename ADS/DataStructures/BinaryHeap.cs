namespace ADS.DataStructures;

public class BinaryHeap<T>
    where T : IComparable<T>
{
    public enum HeapType
    {
        MinHeap,
        MaxHeap
    }

    private delegate int HeapComparer(T t1, T t2);

    private HeapComparer _heapComparer = null!;

    private static int _minComparer(T t1, T t2)
    {
        return t1.CompareTo(t2);
    }

    private static int _maxComparer(T t1, T t2)
    {
        return -t1.CompareTo(t2);
    }


    private readonly List<T> _data;

    private void _siftDown(int index)
    {
        var length = _data.Count;
        while (true)
        {
            var child1 = index * 2 + 1;
            var child2 = child1 + 1;
            var nextChild = index;
            if (child1 < length && _heapComparer(_data[child1], _data[nextChild]) < 0)
                nextChild = child1;
            if (child2 < length && _heapComparer(_data[child2], _data[nextChild]) < 0)
                nextChild = child2;
            if (nextChild == index)
                break;
            (_data[nextChild], _data[index]) = (_data[index], _data[nextChild]);
            index = nextChild;
        }
    }

    private void _siftUp(int index)
    {
        var parentIndex = (index - 1) / 2;
        while (parentIndex >= 0 && _heapComparer(_data[parentIndex], _data[index]) > 0)
        {
            (_data[parentIndex], _data[index]) = (_data[index], _data[parentIndex]);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }


    public void Add(T elem)
    {
        _data.Add(elem);
        _siftUp(_data.Count - 1);
    }

    public T ExtractElem()
    {
        (_data[0], _data[^1]) = (_data[^1], _data[0]);
        var result = _data[^1];
        _data.RemoveAt(_data.Count - 1);
        _siftDown(0);
        return result;
    }

    public T GetElem()
    {
        return _data[0];
    }

    private void _setComparer(HeapType type)
    {
        _heapComparer = type switch
        {
            HeapType.MinHeap => _minComparer,
            HeapType.MaxHeap => _maxComparer,
            _ => throw new Exception()
        }; 
    }
    
    public BinaryHeap(HeapType type)
    {
        _setComparer(type);
        _data = new List<T>();
    }
    
    public BinaryHeap(HeapType type, List<T> initList)
    {
        _setComparer(type);
        _data = new List<T>(initList);
        for (var i = initList.Count / 2 - 1; i >= 0; i--)
        {
            _siftDown(i);
        }
    }
}