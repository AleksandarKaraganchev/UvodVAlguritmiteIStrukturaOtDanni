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
            int[] unsortedArray = GenerateRandomArray(size);
            Console.WriteLine($"Сортиране на списък с {size} елемента:");
            int[] bubbleSortedArray = (int[])unsortedArray.Clone();
            MeasureSortingTime("Bubble Sort", () => BubbleSort(bubbleSortedArray));
            int[] quickSortedArray = (int[])unsortedArray.Clone();
            MeasureSortingTime("Quick Sort", () => QuickSort(quickSortedArray, 0, quickSortedArray.Length - 1));
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
    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        return Enumerable.Range(1, size).Select(_ => random.Next(1, 1000)).ToArray();
    }
    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
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
