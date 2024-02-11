namespace ADS;

public static partial class Algorithms
{
    public static int KPartition(List<int> arr, int numb)
    {
        var lBoard = 0;
        var rBoard = arr.Count - 1;
        while (true)
        {
            var l = lBoard;
            var r = rBoard;
            var m = (lBoard + rBoard) / 2;
            _partition(arr, ref l, ref r, arr[m]);
            if (r >= numb)
                rBoard = r;
            else if (l <= numb)
                lBoard = l;
            else
                return arr[++r];
        }
    }

    private static void _partition(List<int> arr, ref int lBoard, ref int rBoard, int key)
    {
        do
        {
            while (arr[lBoard] < key)
                lBoard++;
            while (arr[rBoard] > key)
                rBoard--;
            if (lBoard > rBoard) continue;
            (arr[lBoard], arr[rBoard]) = (arr[rBoard], arr[lBoard]);
            lBoard++;
            rBoard--;
        } while (lBoard <= rBoard);
        
    }

    private static void _qSort(int lBoard, int rBoard)
    {
        while (true)
        {
            var m = (lBoard + rBoard) / 2;
            var l = lBoard;
            var r = rBoard;
            _partition(_qSortArr, ref l, ref r, _qSortArr[m]);
            if (lBoard < r) _qSort(lBoard, r);
            if (l < rBoard)
            {
                lBoard = l;
                continue;
            }
            break;
        }
    }

    private static List<int> _qSortArr = null!;

    public static void QSort(List<int> arr)
    {
        _qSortArr = arr;
        _qSort(0, arr.Count - 1);
        _qSortArr = null!;
    }
}