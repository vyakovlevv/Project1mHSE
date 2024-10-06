using System.Security;
using System.Text;

namespace Project1mHSE.Helpers
{
    /// <summary>
    /// Статический класс, методы которого безопасно выполняют функции I/O с файлами
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Метод безопасно выполняет функцию считывания данных из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Nullable массив с строками из файла, nullable строка с ошибкой</returns>
        public static (string[]?, string?) ReadLines(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                return (lines, null);
            }
            catch (FileNotFoundException)
            {
                return (null, "Входной Файл на диске отсутствует");
            }
            catch (FileLoadException)
            {
                return (null, "Проблемы с чтением данных из файла");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException)
            {
                Console.WriteLine(ex.Message, path);
                return (null, "Проблемы с открытием файла");
            }
        }
        /// <summary>
        /// Метод безопасно выполняет функцию записи данных в файл
        /// </summary>
        /// <param name="path">Путь к изменяемому файлу</param>
        /// <param name="contents">Данные для заполнения</param>
        /// <returns>Nullable строка с ошибкой</returns>
        public static string? WriteLines(string path, string[] contents)
        {
            try
            {
                File.WriteAllLines(path, contents, Encoding.UTF8);
            }
            catch (EndOfStreamException)
            {
                return "Проблемы с записью данных в файл";
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or SecurityException )
            {
                return "Проблемы с сохранением файла";
            }

            return null;
        }
    }
}