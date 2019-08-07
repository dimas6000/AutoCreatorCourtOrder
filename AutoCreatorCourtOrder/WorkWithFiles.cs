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
        /// Перемещает обработанный файл в папку file.DirectoryName/Обработанные файлы.
        /// </summary>
        /// <param name="file">Перемещаемый файл.</param>
        public static void MoveProcessedFile(FileInfo file)
        {
            string directory = Path.Combine(file.DirectoryName, "Обработанные файлы");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            file.MoveTo(Path.Combine(directory, file.Name));
        }
        /// <summary>
        /// Путь к обрабатываемому файлу.
        /// </summary>
        public FileInfo FileBeingProcessed { get; set; }
        /// <summary>
        /// Файл шаблона приказа в формате RTF (данные из richTextBox.Rtf).
        /// </summary>
        public static string CourtOrderTemplate { get; set; }
    }
}
