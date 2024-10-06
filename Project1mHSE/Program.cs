/*
ФИО:Яковлев Владимир Андреевич
Группа: БПИ249-1
Вариант: 2
*/

using Project1mHSE.Helpers;

namespace Project1mHSE
{
    public static class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
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

                Console.WriteLine("Для выхода нажмите Q....");
                keyToExit = Console.ReadKey();
            } while (keyToExit.Key != ConsoleKey.Q);
        }

        /// <summary>
        /// Функция, выполняющая основной функционал программы.
        /// </summary>
        /// <returns>Nullable строка с ошибкой.</returns>
        public static string? MainTask()
        {
            // Получение строк из файла.
            string pathWorkingFiles = Utils.GetPathWorkingFiles();
            (string[]? lines, string? err) = FileHelper.ReadLines($"{pathWorkingFiles}input.txt");

            if (err != null)
            {
                return err;
            }

            if (lines is { Length: < 2 } or null)
            {
                return "Данных в файле недостаточно";
            }

            // Конвертация строк в массив чисел.
            int[] arrA = Utils.ConvertS2N(lines[0].Split(" "));
            int[] arrB = Utils.ConvertS2N(lines[1].Split(" "));

            if (arrA.Length == 0 || arrB.Length == 0)
            {
                return "Корректных данных в файле нет";
            }

            // Получение значений для C1..C4 и обработка возможных ошибок.
            ((err, int numIncorrectArr), CalculatedC calculatedC) = Utils.Get4Cycles(arrA, arrB);
            if (err != null)
            {
                if (numIncorrectArr > 0)
                {
                    // Если ошибка связана с конкретным массивом.
                    do
                    {
                        Console.WriteLine(
                            $"Произошла следующая ошибка с {numIncorrectArr} массивом: {err}. Введите значения для " +
                            $"этого массива заново: ");
                        string? rawNums = Console.ReadLine();
                        if (rawNums == null) { continue; }

                        int[] newArr = Utils.ConvertS2N(rawNums.Split(" "));
                        if (newArr.Length == 0) { return "Корректных данных в файле нет"; }

                        switch (numIncorrectArr)
                        {
                            // Замена некорректного массива на новый (1 - array A, 2 - array B)
                            case 1:
                                arrA = newArr;
                                break;
                            case 2:
                                arrB = newArr;
                                break;
                        }

                        ((err, numIncorrectArr), calculatedC) = Utils.Get4Cycles(arrA, arrB);
                    } while (err != null);
                }
                else { return err; }
            }

            if (calculatedC.C2 == 0 || calculatedC.C4 == 0)
            {
                return "Деление на 0 невозможно";
            }

            // Вычисление результата и запись в файл.
            double resultCyclesA = (double)calculatedC.C1 / calculatedC.C2;
            double resultCyclesB = (double)calculatedC.C3 / calculatedC.C4;

            err = FileHelper.WriteLines($"{pathWorkingFiles}output.txt", [$"{resultCyclesA:f2}:{resultCyclesB:f2}"]);
            return err;
        }
    }
}