namespace ADS.DataStructures;

public class SegmentTree
{
    private readonly int[] _data;
    private readonly int _length;


    private void _update(int currInd, int currL, int currR, int finInd, int addend)
    {
        while (true)
        {
            _data[currInd] += addend;
            if (currL == currR) return;
            var currM = (currL + currR) / 2;
            if (finInd <= currM)
            {
                currInd = currInd * 2 + 1;
                currR = currM;
            }
            else
            {
                currInd = currInd * 2 + 2;
                currL = currM + 1;
            }
        }
    }

    public void Update(int index, int addend)
    {
        _update(0, 0, _length - 1, index, addend);
    }

    private static int _minIndex(int ind1, int ind2) => ind1 <= ind2 ? ind1 : ind2;
    
    private static int _maxIndex(int ind1, int ind2) => ind1 >= ind2 ? ind1 : ind2;
    
    private int _getSum(int currInd, int currL, int currR, int finL, int finR)
    {
        if (currL == finL && currR == finR)
            return _data[currInd];
        var currM = (currL + currR) / 2;
        var sum = 0;
        if (finL <= currM)
            sum += _getSum(currInd * 2 + 1, currL, currM, finL, _minIndex(currM, finR));
        if (finR > currM)
            sum += _getSum(currInd * 2 + 2, currM + 1, currR, _maxIndex(currM + 1, finL), finR);
        
        return sum;
    }

    public int GetSum(int lBorder, int rBorder) =>
        _getSum(0, 0, _length - 1, lBorder, rBorder);

    public SegmentTree(int length)
    {
        _length = length;
        _data = new int[length * 4];
    }

    public SegmentTree(IReadOnlyList<int> initArr) : this(initArr.Count)
    {
        var length = initArr.Count;
        for (var i = 0; i < length; i++)
        {
            Update(i, initArr[i]);
        }
    }
}