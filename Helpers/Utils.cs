using System;
using System.IO;

namespace Project1mHSE.Helpers
{
    /// <summary>
    /// Структура используется для группировки всех значений C1...C4 в одном месте.
    /// </summary>
    public struct CalculatedC
    {
        public int C1, C2, C3, C4;
    }
    
    public static class Utils
    {
        /// <summary>
        /// Метод пытается создать 4 цикла по условию задания, в случае неудачи возвращает
        /// строку с ошибкой и номер некорректного массива первым аргументом, и незаполненную до конца структуру;
        /// иначе - первый аргумент null и 0, а структура содержит все вычисленные значения.
        /// </summary>
        /// <param name="arrA">Массив с числами A.</param>
        /// <param name="arrB">Массив с числами B.</param>
        /// <returns>Nullable строка с ошибкой и номер некорректного массива и экземпляр структуры CalculatedC.</returns>
        public static ((string?, int), CalculatedC) Get4Cycles(int[] arrA, int[] arrB)
        {
            CalculatedC resultCalculatedC = new CalculatedC();
            Func<int, int, int> delegateSum = (a, b) => a + b; 
            Func<int, int, int> delegateMul = (a, b) => a * b;

            (string? err, int c2) = ProcessArray(arrA, 0, delegateSum);
            if (err != null) { return ((err, 1), resultCalculatedC); }
            resultCalculatedC.C2 = c2;
            
            (err, int c4) = ProcessArray(arrB, 0, delegateSum);
            if (err != null) { return ((err, 2), resultCalculatedC); }
            resultCalculatedC.C4 = c4;
            
            (err, int c1) = ProcessArray(arrA, 1, delegateMul);
            if (err != null) { return ((err, 1), resultCalculatedC); }
            resultCalculatedC.C1 = c1;

            (err, int c3) = ProcessArray(arrB, 1, delegateMul);
            resultCalculatedC.C3 = c3;
            
            return err != null ? ((err, 2), resultCalculatedC) : ((null, 0), resultCalculatedC);
        }
        
        /// <summary>
        /// Метод вычисляет значение, последовательно применяя функцию-делегат к элементам массива.
        /// </summary>
        /// <param name="arr">Массив чисел.</param>
        /// <param name="startValue">Стартовое значение для результата.</param>
        /// <param name="function">Функция, принимающая два числа в аргументы и возвращающая вычисленный
        /// по этим числам результат.</param>
        /// <returns>Nullable строка с ошибкой и значение вычисленного выражения (в случае ошибки - 0).</returns>
        private static (string?, int) ProcessArray(int[] arr, int startValue, Func<int, int, int> function)
        {
            int result = startValue;
            try
            {
                foreach (int el in arr)
                {
                    result = function(result, el);
                }
            }
            catch (OverflowException)
            {
                return ("Ошибка переполнения типов", 0);
            }
            
            return (null, result);
        }
    
        /// <summary>
        /// Метод конвертирует массив из string с числами в массив с numbers
        /// Если встречается некорректный элемент, то он игнорируется, а пользователю
        /// выводится информационное сообщение.
        /// </summary>
        /// <param name="rawNums">Массив строк, содержащих числа.</param>
        /// <returns>Созданный из корректных данных массив чисел.</returns>
        public static int[] ConvertS2N(string[] rawNums)
        {
            int[] result = new int[rawNums.Length];
            int incorrectValues = 0, currentIndexOfResult = 0;
            foreach (string rawNum in rawNums)
            {
                if (!int.TryParse(rawNum, out result[currentIndexOfResult]))
                {
                    Console.WriteLine($"Замените значение {rawNum} на корректное, дальнейшие вычисления будут проводиться " +
                                      $"без учета этого элемента.");
                    incorrectValues++;
                    continue;
                }

                currentIndexOfResult++;
            }
            return result[..^incorrectValues];
        }
        
        /// <summary>
        /// Метод для получения пути к папке WorkingFiles
        /// Если WorkingFiles лежит не папке, где находится исполняемый файл, то решение и проект
        /// должны называться Project1mHSE.
        /// </summary>
        /// <returns>Путь к папке WorkingFiles.</returns>
        public static string GetPathWorkingFiles()
        {
            if (Path.Exists("WorkingFiles"))
            {
                return Path.GetFullPath("WorkingFiles") + Path.DirectorySeparatorChar;
            }
            string curDir = Directory.GetCurrentDirectory();
            int indexOfProject = curDir.LastIndexOf("Project1mHSE", StringComparison.Ordinal);
            string path = curDir[..(indexOfProject + "Project1mHSE".Length)];
            return path + Path.DirectorySeparatorChar + "WorkingFiles" + Path.DirectorySeparatorChar;

        }
    }
}