using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySortLib
{
    public enum SortOrder
    {
        Ascending,  // 昇順
        Descending  // 降順
    }

    public enum SortAlgorithm
    {
        BubbleSort,
        QuickSort,
        MergeSort,
        SelectionSort,
        InsertionSort,
        HeapSort,
        ShellSort,
        RadixSort,
        BucketSort
    }

    public enum SortMode
    {
        InPlace,        // 破壊的（元のリストを変更）
        NonInPlace      // 非破壊的（新しいリストを返す）
    }

    public static class EasySort
    {
        // ソートアルゴリズムを選ぶ
        public static List<T> Sort<T>(List<T> list, SortAlgorithm algorithm, SortOrder order = SortOrder.Ascending, SortMode mode = SortMode.InPlace, List<T> outputList = null) where T : IComparable<T>
        {
            List<T> sortedList;
            
            // 非破壊的モードの場合、新しいリストを作成
            if (mode == SortMode.NonInPlace)
            {
                sortedList = outputList ?? new List<T>(list);
            }
            else
            {
                sortedList = list; // 破壊的モードでは元のリストを使用
            }

            switch (algorithm)
            {
                case SortAlgorithm.BubbleSort:
                    BubbleSort(sortedList, order);
                    break;
                case SortAlgorithm.QuickSort:
                    QuickSort(sortedList, 0, sortedList.Count - 1, order);
                    break;
                case SortAlgorithm.MergeSort:
                    sortedList = MergeSort(sortedList, order);
                    break;
                case SortAlgorithm.SelectionSort:
                    SelectionSort(sortedList, order);
                    break;
                case SortAlgorithm.InsertionSort:
                    InsertionSort(sortedList, order);
                    break;
                case SortAlgorithm.HeapSort:
                    HeapSort(sortedList, order);
                    break;
                case SortAlgorithm.ShellSort:
                    ShellSort(sortedList, order);
                    break;
                case SortAlgorithm.RadixSort:
                    RadixSort(sortedList, order);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported sort algorithm.");
            }

            return sortedList;
        }

        // バブルソート
        private static void BubbleSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if ((order == SortOrder.Ascending && list[j].CompareTo(list[j + 1]) > 0) ||
                        (order == SortOrder.Descending && list[j].CompareTo(list[j + 1]) < 0))
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        // クイックソート
        private static void QuickSort<T>(List<T> list, int low, int high, SortOrder order) where T : IComparable<T>
        {
            if (low < high)
            {
                int pi = Partition(list, low, high, order);
                QuickSort(list, low, pi - 1, order);
                QuickSort(list, pi + 1, high, order);
            }
        }

        private static int Partition<T>(List<T> list, int low, int high, SortOrder order) where T : IComparable<T>
        {
            T pivot = list[high];
            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if ((order == SortOrder.Ascending && list[j].CompareTo(pivot) <= 0) ||
                    (order == SortOrder.Descending && list[j].CompareTo(pivot) >= 0))
                {
                    i++;
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            T temp2 = list[i + 1];
            list[i + 1] = list[high];
            list[high] = temp2;

            return i + 1;
        }

        // マージソート
        private static List<T> MergeSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            if (list.Count <= 1)
                return list;

            int mid = list.Count / 2;
            var left = MergeSort(list.GetRange(0, mid), order);
            var right = MergeSort(list.GetRange(mid, list.Count - mid), order);

            return Merge(left, right, order);
        }

        private static List<T> Merge<T>(List<T> left, List<T> right, SortOrder order) where T : IComparable<T>
        {
            List<T> result = new List<T>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if ((order == SortOrder.Ascending && left[i].CompareTo(right[j]) <= 0) ||
                    (order == SortOrder.Descending && left[i].CompareTo(right[j]) >= 0))
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            result.AddRange(left.GetRange(i, left.Count - i));
            result.AddRange(right.GetRange(j, right.Count - j));

            return result;
        }

        // 選択ソート
        private static void SelectionSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if ((order == SortOrder.Ascending && list[j].CompareTo(list[index]) < 0) ||
                        (order == SortOrder.Descending && list[j].CompareTo(list[index]) > 0))
                    {
                        index = j;
                    }
                }

                T temp = list[index];
                list[index] = list[i];
                list[i] = temp;
            }
        }

        // 挿入ソート
        private static void InsertionSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            for (int i = 1; i < list.Count; i++)
            {
                T key = list[i];
                int j = i - 1;

                while (j >= 0 && ((order == SortOrder.Ascending && list[j].CompareTo(key) > 0) ||
                                   (order == SortOrder.Descending && list[j].CompareTo(key) < 0)))
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }
        }

        // ヒープソート
        private static void HeapSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            int n = list.Count;

            // ヒープを構築
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(list, n, i, order);

            // 最大要素を末尾に移動し、再度ヒープ化
            for (int i = n - 1; i > 0; i--)
            {
                T temp = list[0];
                list[0] = list[i];
                list[i] = temp;

                Heapify(list, i, 0, order);
            }
        }

        private static void Heapify<T>(List<T> list, int n, int i, SortOrder order) where T : IComparable<T>
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && ((order == SortOrder.Ascending && list[left].CompareTo(list[largest]) > 0) ||
                             (order == SortOrder.Descending && list[left].CompareTo(list[largest]) < 0)))
                largest = left;

            if (right < n && ((order == SortOrder.Ascending && list[right].CompareTo(list[largest]) > 0) ||
                              (order == SortOrder.Descending && list[right].CompareTo(list[largest]) < 0)))
                largest = right;

            if (largest != i)
            {
                T swap = list[i];
                list[i] = list[largest];
                list[largest] = swap;

                Heapify(list, n, largest, order);
            }
        }

        // シェルソート
        private static void ShellSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            int n = list.Count;
            int gap = n / 2;
            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = list[i];
                    int j = i;
                    while (j >= gap && ((order == SortOrder.Ascending && list[j - gap].CompareTo(temp) > 0) || 
                                         (order == SortOrder.Descending && list[j - gap].CompareTo(temp) < 0)))
                    {
                        list[j] = list[j - gap];
                        j -= gap;
                    }
                    list[j] = temp;
                }
                gap /= 2;
            }
        }

        // 基数ソート
        private static void RadixSort<T>(List<T> list, SortOrder order) where T : IComparable<T>
        {
            if (typeof(T) != typeof(int))
            {
                throw new InvalidOperationException("RadixSortは整数型のみサポートしています。");
            }

            List<int> intList = list.Cast<int>().ToList();
            int max = intList.Max();

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(intList, exp, order);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (T)(object)intList[i]; // T型に戻す
            }
        }

        private static void CountingSort(List<int> list, int exp, SortOrder order)
        {
            int n = list.Count;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(list[i] / exp) % 10]++;
            }

            if (order == SortOrder.Descending)
            {
                for (int i = 8; i >= 0; i--)
                {
                    count[i] += count[i + 1];
                }
            }
            else
            {
                for (int i = 1; i < 10; i++)
                {
                    count[i] += count[i - 1];
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(list[i] / exp) % 10] - 1] = list[i];
                count[(list[i] / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
            {
                list[i] = output[i];
            }
        }
    }
}
