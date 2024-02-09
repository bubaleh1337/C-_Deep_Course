/* Объедините две предыдущих работы (практические работы 2 и 3) (???): 
поиск файла и поиск текста в файле написав утилиту которая ищет файлы определенного 
расширения с указанным текстом. Рекурсивно. Пример вызова утилиты: utility.exe txt текст.*/

/* Чтобы проверить работу программы, нужно сначала использовать команду dotnet build,
  а далее использовать поиск файлов по тексту внутри папки с проектом*/

using System;
using System.Collections.Generic;
using System.IO;

namespace FileSearchUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Использование: hw8.exe <расширение файла> <текст для поиска>");
                return;
            }

            string extension = args[0];
            string searchText = args[1];

            string currentDirectory = Directory.GetCurrentDirectory();
            SearchFiles(currentDirectory, extension, searchText);
        }

        static void SearchFiles(string directory, string extension, string searchText)
        {
            try
            {
                foreach (string file in Directory.GetFiles(directory, $"*.{extension}", SearchOption.AllDirectories))
                {
                    if (FileContainsText(file, searchText))
                    {
                        Console.WriteLine($"Найдено в файле: {file}");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Нет доступа к директории: {directory}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Директория не найдена: {directory}");
            }
        }

        static bool FileContainsText(string filePath, string searchText)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains(searchText))
                    {
                        return true;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка при чтении файла {filePath}: {e.Message}");
            }
            return false;
        }
    }
}
