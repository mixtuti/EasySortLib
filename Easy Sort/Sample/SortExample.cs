using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySortLib;

public class SortExample : MonoBehaviour
{
    // ソート対象のリスト
    [SerializeField] private List<int> numbers = new List<int> { 23, 42, 1, 88, 9, 7, 34, 11, 5 };

    void Start()
    {
        // ソート前のリストを表示
        Debug.Log("Before sorting:");
        PrintList(numbers);

        // ソートの実行（昇順でバブルソート）
        List<int> sortedNumbers = EasySort.Sort(numbers, SortAlgorithm.BubbleSort, SortOrder.Ascending);
        Debug.Log("\nAfter BubbleSort (Ascending):");
        PrintList(sortedNumbers);

        // ソートの実行（降順でクイックソート）
        List<int> sortedNumbersDesc = EasySort.Sort(numbers, SortAlgorithm.QuickSort, SortOrder.Descending);
        Debug.Log("\nAfter QuickSort (Descending):");
        PrintList(sortedNumbersDesc);

        // ソートの実行（非破壊的マージソート）
        List<int> sortedNumbersMerge = EasySort.Sort(numbers, SortAlgorithm.MergeSort, SortOrder.Ascending, SortMode.NonInPlace);
        Debug.Log("\nAfter MergeSort (Ascending) - NonInPlace:");
        PrintList(sortedNumbersMerge);

        // ソートの実行（選択ソート、破壊的）
        List<int> sortedNumbersSelection = EasySort.Sort(numbers, SortAlgorithm.SelectionSort, SortOrder.Ascending, SortMode.InPlace);
        Debug.Log("\nAfter SelectionSort (Ascending) - InPlace:");
        PrintList(sortedNumbersSelection);

        // ソートの実行（シェルソート、降順）
        List<int> sortedNumbersShell = EasySort.Sort(numbers, SortAlgorithm.ShellSort, SortOrder.Descending);
        Debug.Log("\nAfter ShellSort (Descending):");
        PrintList(sortedNumbersShell);
    }

    // リストを表示するためのヘルパーメソッド
    private void PrintList(List<int> list)
    {
        string result = string.Join(" ", list);
        Debug.Log(result);
    }
}
