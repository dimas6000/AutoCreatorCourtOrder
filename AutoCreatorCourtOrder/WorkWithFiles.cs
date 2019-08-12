using System.IO;
using System.Windows.Forms;

namespace AutoCreatorCourtOrder
{
    /// <summary>
    /// Класс для работы с файлами. Несколько костыльный.
    /// Хранит путь к обрабатываемому файлу и текст файла-шаблона. 
    /// </summary>
    class WorkWithFiles
    {
        /// <summary>
        /// Проверяет существование файла по полученному пути, если файл существует, добавляет цифру в конец имени.
        /// </summary>
        /// <param name="path">Путь к файлу с желаемым именем.</param>
        /// <returns>Уникальный путь к файлу.</returns>
        private static string CheckFileName(string path)
        {
            string newPath = path;
            int i = 0;
            while (File.Exists(newPath))
            {
                i++;
                newPath = Path.Combine(Path.GetDirectoryName(path),
                            Path.GetFileNameWithoutExtension(path), $" ({i})", Path.GetExtension(path));
            }
            return newPath;

        }

        /// <summary>
        /// Сохраняет созданный файл в папку file.DirectoryName/Приказы созданные программой
        /// </summary>
        /// <param name="file">Перемещаемый файл.</param>        
        /// <param name="fullName">ФИО на которое создан приказ. (ExtractedData.FullName)</param>
        /// <param name="box">Объект с текстом для сохранения в файл.</param>
        /// <returns>Результат сохранения файла.</returns>
        public static bool FileSavedSuccessfully(FileInfo file, string fullName, RichTextBox box)
        {
            string directory = Path.Combine(file.DirectoryName, "Приказы созданные программой");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            box.SaveFile(CheckFileName(Path.Combine(directory, $"Приказ {fullName}.rtf")), RichTextBoxStreamType.RichText);
            return true;
        }
        /// <summary>
        /// Перемещает обработанный файл в папку file.DirectoryName/Обработанные файлы
        /// </summary>
        /// <param name="file">Перемещаемый файл.</param>
        public static void MoveProcessedFile(FileInfo file)
        {
            string directory = Path.Combine(file.DirectoryName, "Обработанные файлы");
            /*  if (!Directory.Exists(directory))
                  Directory.CreateDirectory(directory);
  */
            file.MoveTo(CheckFileName(Path.Combine(directory, file.Name)));
        }

        /// <summary>
        /// Путь к обрабатываемому файлу.
        /// </summary>
        public FileInfo FileBeingProcessed { get; set; }
        /// <summary>
        /// Файл шаблона приказа в формате RTF (данные из RichTextBox.Rtf).
        /// </summary>
        public static string CourtOrderTemplate { get; set; }
    }
}
