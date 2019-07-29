using System.IO;
namespace AutoCreatorCourtOrder
{
    /// <summary>
    /// Хранит пути к файлам и папкам.
    /// </summary>
    static class WorkWithFiles
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
            FileBeingProcessed.MoveTo(Path.Combine(directory, FileBeingProcessed.Name));
        }

        /// <summary>
        /// Файл шаблона приказа.
        /// </summary>
        public static FileInfo CourtOrderTemplate { get; set; }

        /// <summary>
        /// Обрабатываемый файл.
        /// </summary>
        public static FileInfo FileBeingProcessed { get; set; }
    }
}
