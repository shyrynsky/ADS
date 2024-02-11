namespace ADS;

public static partial class Algorithms
{
    public static void Merge(List<int> arr, int l, int m, int r)
    {
        var lIndex = l;
        var rIndex = m;
        var length = r - l;
        var finIndex = 0;
        var tempArr = length < 1024 ? stackalloc int[length] : new int[length];
        while (lIndex < m && rIndex < r)
        {
            if (arr[lIndex] <= arr[rIndex])
                tempArr[finIndex++] = arr[lIndex++];
            else
                tempArr[finIndex++] = arr[rIndex++];
        }
        while (lIndex < m)
            tempArr[finIndex++] = arr[lIndex++];
        while (rIndex < r)
            tempArr[finIndex++] = arr[rIndex++];
        for (var i = 0; i < length; i++)
            arr[l++] = tempArr[i];
    }

    public static void MergeLessMem()
    {
        throw new NotImplementedException();
    }

    public static void InPlaceMerge()
    {
        throw new NotImplementedException();
    }

    private static List<int> _mergeSortArr = null!;

    private static void _mergeSort(int lBoard, int rBoard)
    {
        if (lBoard + 1 >= rBoard) return;
        var m = (lBoard + rBoard) / 2;
        _mergeSort(lBoard, m);
        _mergeSort(m, rBoard);
        Merge(_mergeSortArr, lBoard, m, rBoard);
    }
    
    public static void MergeSort(List<int> arr)
    {
        _mergeSortArr = arr;
        _mergeSort(0, arr.Count);
    }
}
