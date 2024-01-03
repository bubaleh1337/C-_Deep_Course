using System;

// Дан двумерный массив.

//      732
//      496
//      185

//Отсортировать данные в нем по возрастанию.

//      123
//      456
//      789

//Вывести результат на печать.

class Program
{
    static void Main(string[] args)
    {
        int[,] arr = { { 7, 3, 2 }, { 4, 9, 6 }, { 1, 8, 5 } };

        printArray(arr);
        Console.WriteLine();
        Console.WriteLine("Отсортированный массив:");
        getSortArray(get1DArray(arr), arr);
        Console.WriteLine();
    }

    static void printArray(int[,] arr)
    {
        for (int i = 0; arr.GetLength(0) > i; i++)
        {
            for (int j = 0; arr.GetLength(1) > j; j++)
            {
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static int[] get1DArray(int[,] arr)
    {
        int[] allArray = new int[arr.Length];
        int index = 0;
        foreach (var element in arr)
        {
            allArray[index++] = element;
        }
        return allArray;
    }

    static void getSortArray(int[] allArray, int[,] arr)
    {
        int size = allArray.Length;

        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; size - i - 1 > j; j++)
            {
                if (allArray[j] > allArray[j + 1])
                {
                    int tmp = allArray[j];
                    allArray[j] = allArray[j + 1];
                    allArray[j + 1] = tmp;
                }
            }
        }

        int index = 0;

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                arr[i, j] = allArray[index++];
            }
        }

        printArray(arr);
    }
}