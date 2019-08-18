using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreatorCourtOrder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Деактивируем недоступные изначально кнопки.
            openFileButton.Enabled = false;
            extractDataButton.Enabled = false;
            showDataButton.Enabled = false;
            createCourtOrderButton.Enabled = false;
            saveButton.Enabled = false;
            directoryCreateCourtOrderButton.Enabled = false;
        }

        // Извлеченные данные для поштучной обработки файлов. 
        ExtractedData extractedData = new ExtractedData();
        // Объект только чтобы хранить путь к текущему обрабатываемому файлу.
        WorkWithFiles workWithFiles = new WorkWithFiles();

        /// <summary>
        /// Позволяет пользователю выбрать файл шаблона приказа.
        /// Сохраняет текст шаблона и отображает его в richTextBox.
        /// </summary>
        private void ChooseATemplateOrder()
        {
            string pathToFile = OpenFile();
            if (pathToFile != string.Empty)
            {
                RichTextBox box = new RichTextBox { Rtf = ExtractTextFromRtf(pathToFile) };
                ShowFile(box);
                WorkWithFiles.CourtOrderTemplate = box.Rtf;

                DirectoryHelper.CreateDirectories(new FileInfo(pathToFile));

                TemplateFileSelected();
            }
            else
                MessageBox.Show("Для работы программы необходимо выбрать файл шаблона судебного приказа.");
        }

        /// <summary>
        /// Извлекает текст из файла.
        /// </summary>
        /// <param name="pathToFile">Путь к файлу.</param>
        /// <returns>Весь текст из файла.</returns>
        private string ExtractTextFromRtf(string pathToFile)
        {
            if (pathToFile != string.Empty)
                return File.ReadAllText(pathToFile);
            else
                return string.Empty;
        }
        /// <summary>
        /// Записывает текст из файла в элемент richTextBox на форме и отображает его.
        /// </summary>
        /// <param name="text">Текст файла.</param>
        private void ShowFile(RichTextBox box)
        {
            richTextBox.Rtf = box.Rtf;
            richTextBox.SelectionStart = 0;
            richTextBox.ScrollToCaret();
        }

        /// <summary>
        /// Извлекает все необходимые данные из файла и сохраняет в класс ExtractedData.
        /// </summary>
        /// <param name="richTextBox">Объект richTextBox для извлечения данных из текста</param>
        /// <returns>Извлеченные данные из текста.</returns>
        private ExtractedData ExtractData(RichTextBox richTextBox)
        {
            // Непонятно. Почему-то структура долга и кбк при сохранении в rtf теряет переносы строки если не добавить экранирование к \n.
            // Пока на всякий случай оставил .Replace т.к. это был костыль для последних документов, без которого ничего не работало.
            // Причина бага не найдена. Будет исправлено переделкой формата выходного документа из .rtf в .docx.
            ExtractedData extractedData = new ExtractedData
            {
                FullName = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.FullName, RegexOptions.IgnoreCase),
                Address = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.Address, RegexOptions.IgnoreCase),
                DateOfBirth = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.DateOfBirth, RegexOptions.IgnoreCase),
                BirthPlace = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.BirthPlace, RegexOptions.IgnoreCase),
                Inn = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.Inn, RegexOptions.IgnoreCase),
                DebtStructure = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.DebtStructure).Replace("\n", "\\\n"),
                BankDetails = MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.BankDetails).Replace("\n", "\\\n")
            };
            // Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек.
            if (Int32.TryParse(MyRegex.FindDataWithRegex(richTextBox.Text, MyRegex.AllDebt), out int allDebt))
                extractedData.AllDebt = allDebt;
            else
                MessageBox.Show("В тексте не было найдено общей суммы задолженности, возможно вы пытаетесь извлечь данные из неподходящего документа");
            return extractedData;
        }

        /// <summary>
        /// Вставляет извлеченные данные в шаблон.
        /// </summary>
        /// <param name="rtf">Строка в формате RTF (richTextBox.Rtf).</param>
        /// <param name="extractedData">Объект с извлеченными из заявления данными.</param>
        /// <returns>Строка в формате RTF (richTextBox.Rtf).</returns>
        private string CreateCourtOrder(string rtf, ExtractedData extractedData)
        {
            rtf = rtf.Replace("#FULLNAME#", extractedData.FullName);
            rtf = rtf.Replace("#FULLNAMEGENITIVE#", extractedData.FullNameGenitive);
            rtf = rtf.Replace("#DATEOFBIRTH#", extractedData.DateOfBirth);
            rtf = rtf.Replace("#PLACEOFBIRTH#", extractedData.BirthPlace);
            rtf = rtf.Replace("#ADDRESS#", extractedData.Address);
            rtf = rtf.Replace("#INDIVIDUALTAXNUMBER#", extractedData.Inn);
            rtf = rtf.Replace("#DEBTSTRUCTURE#", extractedData.DebtStructure);
            rtf = rtf.Replace("#GOSPOSHLINA#", extractedData.CalculateStateDuty().ToString());
            rtf = rtf.Replace("#BANKDETAILS#", extractedData.BankDetails);
            return rtf;
        }

        /// <summary>
        /// Сохраняет судебный приказ. Переносит заявление о вынесении приказа в подпапку "/Обработанные файлы".
        /// </summary>
        /// <param name="fileBeingProcessed">Файл заявления о вынесении судебного приказа.</param>
        /// <param name="fullName">ФИО на которое создан приказ. (ExtractedData.FullName)</param>
        /// <param name="box">Объект с текстом для сохранения в файл.</param>
        private void SaveCourtOrder(FileInfo fileBeingProcessed, string fullName, RichTextBox box)
        {
            try
            {
                box.SaveFileWithUniqueName(fullName);
                WorkWithFiles.MoveProcessedFile(fileBeingProcessed);
            }
            // todo:
            catch (Exception ex)
            {
                // todo: Имя файлов в неукю строку которые с косяком
            }
        }

        /// <summary>
        /// Открывает файл и отбражает текст в richTextBox.
        /// </summary>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            string pathToFile = OpenFile();
            if (pathToFile != string.Empty)
            {
                workWithFiles.FileBeingProcessed = new FileInfo(pathToFile);
                RichTextBox box = new RichTextBox
                { Rtf = ExtractTextFromRtf(workWithFiles.FileBeingProcessed.FullName) };
                ShowFile(box);
                ProcessedFileIsOpen();
            }
        }

        /// <summary>
        /// При нажатии кнопки извлекает все необходимые данные и сохраняет в объект extractedData.
        /// </summary>
        private void ExtractDataButton_Click(object sender, EventArgs e)
        {
            extractedData = ExtractData(richTextBox);
            DataExtracted();
        }

        /// <summary>
        /// Отображает извлеченные данные в отдельном окне.
        /// </summary>
        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            DataViewForm f = new DataViewForm(extractedData);
            f.ShowDialog();
        }

        /// <summary>
        /// Создает судебный приказ по шаблону и отображает в richTextBox на форме.
        /// </summary>
        private void CreateCourtOrderButton_Click(object sender, EventArgs e)
        {
            RichTextBox box = new RichTextBox { Rtf = CreateCourtOrder(WorkWithFiles.CourtOrderTemplate, extractedData) };
            ShowFile(box);
            CourtOrderCreated();
        }

        /// <summary>
        /// Сохраняет созданный судебный приказ из richTextBox на форме.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveCourtOrder(workWithFiles.FileBeingProcessed, extractedData.FullName, richTextBox);
            CourtOrderSaved();
        }
        
        /// <summary>
        /// Выбирает шаблон для создания судебного приказа.
        /// </summary>
        private void ChooseATemplateOrderButton_Click(object sender, EventArgs e)
        {
            ChooseATemplateOrder();
        }

        /// <summary>
        /// Создаёт судебный приказа из файла. 
        /// Предназначен для многопоточной обработки файлов в папке.
        /// </summary>
        /// <param name="file">Путь к обрабатываемому файлу.</param>
        private void ForСreatesOrdersInMultipleThreads(string file)
        {
            RichTextBox box = new RichTextBox { Rtf = ExtractTextFromRtf(file) };
            ExtractedData extData = ExtractData(box);
            box.Rtf = CreateCourtOrder(WorkWithFiles.CourtOrderTemplate, extData);
            SaveCourtOrder(new FileInfo(file), extData.FullName, box);
            progressBarMultiThreading.Invoke((Action)(() => { progressBarMultiThreading.PerformStep(); }));
        }
        /// <summary>
        /// Автоматическое создание судебных приказов для всех файлов в папке.
        /// </summary>
        private void DirectoryCreateOrderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выбор директории";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var files = Directory.GetFiles(dialog.SelectedPath);
                    var rtfFiles = files.Where(f => Path.GetExtension(f).ToLower() == ".rtf");
                    // Максимум прогрессбара равен числу обрабатываемых файлов.
                    progressBarMultiThreading.Maximum = rtfFiles.Count();
                    LockAllButtons();

                    Thread th = new Thread(new ThreadStart(() =>
                    {
                        Parallel.ForEach(rtfFiles, ForСreatesOrdersInMultipleThreads);
                        MessageBox.Show($"Обработано документов: {rtfFiles.Count()}");
                        OrdersInDirectoryWasCreated();
                    }));
                    th.Start();

                }
            }
        }

        #region Методы для активации\деактивации кнопок.
        /// <summary>
        /// Файл шаблона выбран. Деактивирует кнопку выбора шаблона. 
        /// Активирует кнопки открытия файла и создания приказов для всех файлов из папки.
        /// </summary>
        private void TemplateFileSelected()
        {
            chooseATemplateOrderButton.Enabled = false;
            openFileButton.Enabled = true;
            directoryCreateCourtOrderButton.Enabled = true;
        }
        /// <summary>
        /// Обрабатываемый файл выбран и открыт. Активирует кнопку извлечения данных.
        /// </summary>
        private void FileBeingProcessedIsOpen()
        { extractDataButton.Enabled = true; }
        /// <summary>
        /// Данные извлечены. Деактивирует кнопку извлечения данных.
        /// Активиурет кнопки просмотра данных и создания судебного приказа.
        /// </summary>
        private void DataExtracted()
        {
            extractDataButton.Enabled = false;
            showDataButton.Enabled = true;
            createCourtOrderButton.Enabled = true;
        }
        /// <summary>
        /// Судебный приказ создан. Деактивирует кнопки открытия заявления, создания приказа 
        /// и просмотра данных. Активирует кнопку сохранения приказа.
        /// </summary>
        private void CourtOrderCreated()
        {
            showDataButton.Enabled = false;
            createCourtOrderButton.Enabled = false;
            openFileButton.Enabled = false;
            saveButton.Enabled = true;
        }
        /// <summary>
        /// Судебный приказ создан. Деактивирует кнопку сохранения приказа. 
        /// Активирует кнопку открытия заявления.
        /// </summary>
        private void CourtOrderSaved()
        {
            saveButton.Enabled = false;
            openFileButton.Enabled = true;
        }
        /// <summary>
        /// Блокирует все кнопки.
        /// </summary>
        private void LockAllButtons()
        {
            chooseATemplateOrderButton.Enabled = false;
            openFileButton.Enabled = false;
            extractDataButton.Enabled = false;
            showDataButton.Enabled = false;
            createCourtOrderButton.Enabled = false;
            saveButton.Enabled = false;
            directoryCreateCourtOrderButton.Enabled = false;
        }
        /// <summary>
        /// Массовая обработка файлов в папке завершена. Позволяет выбор нового шаблона и обнуляет прогресс-бар.
        /// </summary>
        private void OrdersInDirectoryWasCreated()
        {
            progressBarMultiThreading.Invoke((Action)(() => { progressBarMultiThreading.Value = 0; }));
            chooseATemplateOrderButton.Invoke((Action)(() => { chooseATemplateOrderButton.Enabled = true; }));
        }
        #endregion
    }
}
