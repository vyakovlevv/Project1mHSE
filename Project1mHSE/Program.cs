/*
ФИО:Яковлев Владимир Андреевич
Группа: БПИ249-1
Вариант: 2
*/

using System;
using Project.Helpers;

public static class Program
{
    public static void Main()
    {
        ConsoleKeyInfo keyToExit;
        do
        {
            string? err = MainTask();
            if (err != null)
            {
                Console.WriteLine(err);
            }

            Console.WriteLine("Для выхода нажмите Escape....");
            keyToExit = Console.ReadKey();
        } while (keyToExit.Key != ConsoleKey.Escape);
    }
    
    public static string? MainTask()
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        (string[]? lines, string? err) = FileHelper.ReadLines("../../../../WorkingFiles/input.txt");

        if (err != null)
        {
            return err;
        }
        if (lines is { Length: < 2 } or null)
        {
            return "Данных в файле недостаточно";
        }

        int[] arrA = Utils.ConvertS2N(lines[0].Split(" "));
        int[] arrB = Utils.ConvertS2N(lines[1].Split(" "));

        if (arrA.Length == 0 || arrB.Length == 0)
        {
            return "Корректных данных в файле нет";
        }

        (int c1, int c2, int c3, int c4) = Utils.Get4Cycles(arrA, arrB);
        if (c2 == 0 || c4 == 0)
        {
            return "Деление на 0 невозможно";
        }

        double resultCyclesA = (double)c1 / c2;
        double resultCyclesB = (double)c3 / c4;
        
        err = FileHelper.WriteLines("../../../../WorkingFiles/output.txt", [$"{resultCyclesA:f2}:{resultCyclesB:f2}"]);
        return err;
    }
}


