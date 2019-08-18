using System.IO;
using System.Windows.Forms;
namespace AutoCreatorCourtOrder
{
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// Сохраняет созданный файл в папку pathToDirectory/Приказы созданные программой
        /// </summary>
        /// <param name="pathToDirectory">Директория исходного файла</param>        
        /// <param name="debtorName">ФИО на которое создан приказ. (ExtractedData.FullName)</param>
        /// <param name="box">Объект с текстом для сохранения в файл.</param>
        /// <returns>Результат сохранения файла.</returns>
        public static void SaveFileWithUniqueName(this RichTextBox box, string debtorName)
        {
            string path = AddPostfixIfFileExists(Path.Combine
                (DirectoryHelper.PathToCourtOrdersDirectory, $"Приказ {debtorName}.rtf"));
            box.SaveFile(path, RichTextBoxStreamType.RichText);
        }
        /// <summary>
        /// Проверяет существование файла по полученному пути, если файл существует, добавляет нумерацию в конце имени.
        /// </summary>
        /// <param name="path">Путь к файлу с желаемым именем.</param>
        /// <returns>Уникальный путь к файлу.</returns>
        private static string AddPostfixIfFileExists(string path)
        {
            string newPath = path;
            int i = 0;
            while (File.Exists(newPath))
            {
                i++;
                newPath = Path.Combine(Path.GetDirectoryName(path),
                            $"{Path.GetFileNameWithoutExtension(path)} ({i}){Path.GetExtension(path)}");
            }
            return newPath;
        }
    }
}
