using System;

// Написать программу-калькулятор, вычисляющую выражения вида a + b, a - b, a / b, a * b, 
// введенные из командной строки, и выводящую результат выполнения на экран.

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            int a = getNumber("Input first number --> ");
            int b = getNumber("Input second number --> ");
            string sign = getChar("Input +, -, * or / --> ");
            mathOperation(a, b, sign);
        }
    }

    static int getNumber(string msg)
    {
        Console.Write(msg);
        int number = int.Parse(System.Console.ReadLine()!);
        return number;
    }

    static string getChar(string msg)
    {
        System.Console.Write(msg);
        string symbol = System.Console.ReadLine()!;
        return symbol;
    }

    static void mathOperation(int a, int b, string sign)
    {
        switch (sign)
        {
            case "+":
                System.Console.WriteLine($"{a} + {b} = {a + b}");
                break;
            case "-":
                System.Console.WriteLine($"{a} - {b} = {a - b}");
                break;
            case "*":
                System.Console.WriteLine($"{a} * {b} = {a * b}");
                break;
            case "/":
                System.Console.WriteLine($"{a} / {b} = {a / b}");
                break;
            default:
                System.Console.WriteLine("Try again, use only +, -, *, /");
                break;

        }
    }
}