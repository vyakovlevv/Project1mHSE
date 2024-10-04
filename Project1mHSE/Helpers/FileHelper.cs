using System.Text;

namespace Project.Helpers
{
    public static class FileHelper
    {
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
            catch (IOException e)
            {
                return (null, "Проблемы с открытием файла");
            }
        }

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
            catch (IOException)
            {
                return "Проблемы с сохранением файла";
            }

            return null;
        }
    }
}