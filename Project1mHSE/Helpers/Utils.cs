namespace Project.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Метод пытается создать 4 цикла по условию задания, в случае неудачи возвращает
        /// строку с ошибкой первым аргументом, а остальные нули,
        /// иначе - первый аргумент null, а остальные вычисленные значения циклов
        /// </summary>
        /// <param name="arrA">Массив с числами A</param>
        /// <param name="arrB">Массив с числами B</param>
        /// <returns>nullable строка с ошибкой и 4 числа с </returns>
        public static (int, int, int, int) Get4Cycles(int[] arrA, int[] arrB)
        {
        
        
            Func<int, int, int> delegateSum = (a, b) => a + b; 
            Func<int, int, int> delegateMul = (a, b) => a * b; 
        
            int c2 = ProcessArray(arrA, 0, delegateSum);
            int c4 = ProcessArray(arrB, 0, delegateSum);
        
            int c1 = ProcessArray(arrA, 1, delegateMul);
            int c3 = ProcessArray(arrB, 1, delegateMul);
            return (c1, c2, c3, c4);
        }

        private static int ProcessArray(int[] arr, int startValue, Func<int, int, int> function)
        {
            int result = startValue;
            foreach (int i in arr)
            {
                result = function(result, i);
            }

            return result;
        }
    
        /// <summary>
        /// Метод конвертирует массив из string с числами в массив с numbers
        /// </summary>
        /// <param name="rawNums">Массив строк, содержащих числа</param>
        /// <returns>Созданный из корректных данных массив чисел</returns>
        public static int[] ConvertS2N(string[] rawNums)
        {
            int[] result = new int[rawNums.Length];
            int incorrectValues = 0, currentIndexOfResult = 0;
            foreach (string rawNum in rawNums)
            {
                if (!int.TryParse(rawNum, out result[currentIndexOfResult]))
                {
                    incorrectValues++;
                }

                currentIndexOfResult++;
            }
            return result[..^incorrectValues];
        }
    }
}