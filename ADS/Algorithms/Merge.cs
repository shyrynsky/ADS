namespace ADS;

public static partial class Algorithms
{
    public static void BasicMerge(List<int> arr, int l, int m, int r)
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

    // ReSharper disable once MemberCanBePrivate.Global
    public static void MergeLessMem(List<int> arr, int l, int m, int r)
    {
        var lIndex = 0;
        var finIndex = l;
        var rIndex = m;
        var length = m - l;
        var tempArr = length < 1024 ? stackalloc int[length] : new int[length];
        for (var i = 0; i < length; i++)
            tempArr[i] = arr[l + i];
        while (lIndex < length && rIndex < r)
        {
            if (tempArr[lIndex] <= arr[rIndex])
                arr[finIndex++] = tempArr[lIndex++];
            else
                arr[finIndex++] = arr[rIndex++];
        }

        while (lIndex < length)
            arr[finIndex++] = tempArr[lIndex++];
        while (rIndex < r)
            arr[finIndex++] = arr[rIndex++];
    }
    
    private static int _mergeInversion(List<int> arr, int l, int m, int r)
    {
        var lIndex = l;
        var rIndex = m;
        var length = r - l;
        var finIndex = 0;
        var tempInversions = 0;
        var finalInversions = 0;
        var tempArr = length < 1024 ? stackalloc int[length] : new int[length];
        while (lIndex < m && rIndex < r)
        {
            if (arr[lIndex] <= arr[rIndex])
            {
                tempArr[finIndex++] = arr[lIndex++];
                finalInversions += tempInversions;
            }
            else
            {
                tempArr[finIndex++] = arr[rIndex++];
                tempInversions++;
            }
        }

        while (lIndex < m)
        {
            tempArr[finIndex++] = arr[lIndex++];
            finalInversions += tempInversions;
        }
        while (rIndex < r)
        {
            tempArr[finIndex++] = arr[rIndex++];
        }

        for (var i = 0; i < length; i++)
            arr[l++] = tempArr[i];
        return finalInversions;
    }
    
    private static int _mergeInversionSort(int lBoard, int rBoard)
    {
        if (lBoard + 1 >= rBoard) return 0;
        var m = (lBoard + rBoard) / 2;
        var inversions = _mergeInversionSort(lBoard, m);
        inversions += _mergeInversionSort(m, rBoard);
        inversions += _mergeInversion(_mergeSortArr, lBoard, m, rBoard);
        return inversions;
    }
    
    public static int GetInversions(List<int> arr)
    {
        _mergeSortArr = (arr);
        var result = _mergeInversionSort(0, arr.Count);
        _mergeSortArr = null!;
        return result;
    }

    private static List<int> _mergeSortArr = null!;

    private static void _mergeSort(int lBoard, int rBoard)
    {
        if (lBoard + 1 >= rBoard) return;
        var m = (lBoard + rBoard) / 2;
        _mergeSort(lBoard, m);
        _mergeSort(m, rBoard);
        MergeLessMem(_mergeSortArr, lBoard, m, rBoard);
    }
    
    public static void MergeSort(List<int> arr)
    {
        _mergeSortArr = arr;
        _mergeSort(0, arr.Count);
        _mergeSortArr = null!;
    }
}
