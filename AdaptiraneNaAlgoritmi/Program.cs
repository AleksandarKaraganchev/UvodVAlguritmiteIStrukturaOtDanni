using System;
using System.Diagnostics;
using System.Linq;

class SortingAlgorithmsComparison
{
    static void Main()
    {
        int[] sizes = { 10, 100, 1000, 10000 };
        foreach (int size in sizes)
        {
            int[] unsortedArray = GenerateRandomArrayWithDuplicates(size);
            Console.WriteLine($"Сортиране на списък с {size} елемента с повтарящи се стойности:");
            int[] quickSortedArray = (int[])unsortedArray.Clone();
            MeasureSortingTime("Quick Sort (Original)", () => QuickSort(quickSortedArray, 0, quickSortedArray.Length - 1));
            int[] modifiedQuickSortedArray = (int[])unsortedArray.Clone();
            MeasureSortingTime("Quick Sort (Three-way Partitioning)", () => ModifiedQuickSort(modifiedQuickSortedArray, 0, modifiedQuickSortedArray.Length - 1));
            int[] mergeSortedArray = (int[])unsortedArray.Clone();
            MeasureSortingTime("Merge Sort", () => MergeSort(mergeSortedArray, 0, mergeSortedArray.Length - 1));

            Console.WriteLine();
        }
    }
    static void MeasureSortingTime(string algorithmName, Action sortingAction)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        sortingAction.Invoke();
        stopwatch.Stop();
        Console.WriteLine($"{algorithmName} време за сортиране: {stopwatch.ElapsedMilliseconds} милисекунди");
    }
    static int[] GenerateRandomArrayWithDuplicates(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 100);
            if (random.Next(2) == 0)
            {
                int index = random.Next(i + 1);
                array[i] = array[index];
            }
        }
        return array;
    }
    static void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(array, low, high);

            QuickSort(array, low, partitionIndex - 1);
            QuickSort(array, partitionIndex + 1, high);
        }
    }
    static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;

                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        int temp1 = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp1;

        return i + 1;
    }
    static void ModifiedQuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            ThreeWayPartition(array, low, high, out int lt, out int gt);

            ModifiedQuickSort(array, low, lt - 1);
            ModifiedQuickSort(array, gt + 1, high);
        }
    }
    static void ThreeWayPartition(int[] array, int low, int high, out int lt, out int gt)
    {
        int pivot = array[high];
        int i = low;
        int ltIndex = low;
        int gtIndex = high;

        while (i <= gtIndex)
        {
            if (array[i] < pivot)
            {
                Swap(array, i, ltIndex);
                i++;
                ltIndex++;
            }
            else if (array[i] > pivot)
            {
                Swap(array, i, gtIndex);
                gtIndex--;
            }
            else
            {
                i++;
            }
        }

        lt = ltIndex;
        gt = gtIndex;
    }
    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    static void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);

            Merge(array, left, middle, right);
        }
    }
    static void Merge(int[] array, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];

        for (int i = 0; i < n1; ++i)
        {
            leftArray[i] = array[left + i];
        }

        for (int j = 0; j < n2; ++j)
        {
            rightArray[j] = array[middle + 1 + j];
        }

        int i1 = 0, i2 = 0;
        int k = left;

        while (i1 < n1 && i2 < n2)
        {
            if (leftArray[i1] <= rightArray[i2])
            {
                array[k] = leftArray[i1];
                i1++;
            }
            else
            {
                array[k] = rightArray[i2];
                i2++;
            }
            k++;
        }

        while (i1 < n1)
        {
            array[k] = leftArray[i1];
            i1++;
            k++;
        }

        while (i2 < n2)
        {
            array[k] = rightArray[i2];
            i2++;
            k++;
        }
    }
}
