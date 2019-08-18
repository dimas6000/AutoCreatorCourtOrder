using System;
using System.IO;
using System.Windows.Forms;

namespace AutoCreatorCourtOrder
{
    /// <summary>
    /// Класс для работы с файлами. Несколько костыльный.
    /// Хранит путь к обрабатываемому файлу и текст файла-шаблона. 
    /// </summary>
    public class WorkWithFiles
    {
        /// <summary>
        /// Перемещает обработанный файл в папку file.DirectoryName/Обработанные файлы
        /// </summary>
        /// <param name="file">Перемещаемый файл.</param>
        public static void MoveProcessedFile(FileInfo file)
        {
            file.MoveTo(Path.Combine(DirectoryHelper.PathToProcessedFilesDirectory, file.Name));
        }
        /// <summary>
        /// Открывает OpenFileDialog для выбора файла пользователем. 
        /// Если файл не выбран или произошла ошибка возвращает String.Empty
        /// </summary>
        /// <returns>Путь к выбранному файлу.</returns>
        public static string OpenFile()
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;
                    dialog.Title = "Выберите файл шаблона.";
                    dialog.Filter = "rtf files (*.rtf)|*.rtf";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        return dialog.FileName;
                    }
                    return string.Empty;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой, " +
                                "закройте программу использующую файл шаблона и попробуйте снова.");
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла неизвестная ошибка:\n{ex}");
                Application.Exit();
                return string.Empty;
            }
        }

        /// <summary>
        /// Обрабатываемый файл.
        /// </summary>
        public FileInfo FileBeingProcessed { get; set; }
        /// <summary>
        /// Файл шаблона приказа в формате RTF (данные из RichTextBox.Rtf).
        /// </summary>
        public static string CourtOrderTemplate { get; set; }
    }
}
