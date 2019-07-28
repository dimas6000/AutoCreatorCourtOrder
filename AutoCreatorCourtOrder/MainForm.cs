using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace AutoCreatorCourtOrder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Деактивируем недоступные изначально кнопки.
            OpenFileButton.Enabled = false;
            ExtractDataButton.Enabled = false;
            ShowDataButton.Enabled = false;
            CreateCourtOrderButton.Enabled = false;
            SaveButton.Enabled = false;
            DirectoryCreateCourtOrderButton.Enabled = false;
        }
        // Кнопки активируются через свойства, после того как сделал так,
        // мне это кажется странной реализацией.    
        /// <summary>
        /// Файл шаблона выбран.
        /// </summary>
        public bool TemplateFileSelected
        {
            get { return TemplateFileSelected; }
            set
            {
                ChooseATemplateOrderButton.Enabled = !value;
                OpenFileButton.Enabled = value;
                DirectoryCreateCourtOrderButton.Enabled = value;
            }
        }
        /// <summary>
        /// Обрабатываемый файл выбран и открыт.
        /// </summary>
        public bool FileIsOpen
        {
            get { return FileIsOpen; }
            set
            {
               // OpenFileButton.Enabled = !value;
                ExtractDataButton.Enabled = value;
            }
        }
        /// <summary>
        /// Данные извлечены.
        /// </summary>
        public bool DataExtracted
        {
            get { return DataExtracted; }
            set
            {
                ExtractDataButton.Enabled = !value;
                ShowDataButton.Enabled = value;
                CreateCourtOrderButton.Enabled = value;
            }
        }
        /// <summary>
        /// Судебный приказ создан.
        /// </summary>
        public bool CourtOrderCreated
        {
            get { return CourtOrderCreated; }
            set
            {
                ShowDataButton.Enabled = !value;
                CreateCourtOrderButton.Enabled = !value;
                OpenFileButton.Enabled = !value;
                SaveButton.Enabled = value;
            }
        }
        /// <summary>
        /// Судебный приказ создан.
        /// </summary>
        public bool CourtOrderSaved
        {
            get { return CourtOrderSaved; }
            set
            {
                SaveButton.Enabled = !value;
                OpenFileButton.Enabled = value;
            }
        }

        /// <summary>
        /// Извлекает все необходимые данные из файла и сохраняет в класс ExtractedData.
        /// </summary>
        private void ExtractData()
        {
            ExtractedData.FullName = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.FullName, RegexOptions.IgnoreCase);
            ExtractedData.Address = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.Address, RegexOptions.IgnoreCase);
            ExtractedData.DateOfBirth = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.DateOfBirth, RegexOptions.IgnoreCase);
            ExtractedData.BirthPlace = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.BirthPlace, RegexOptions.IgnoreCase);
            ExtractedData.Inn = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.Inn, RegexOptions.IgnoreCase);

            // Непонятно. Почему-то структура долга и кбк при сохранении в rtf теряет переносы строки если не добавить экранирование к \n.
            // Пока на всякий случай оставил .Replace т.к. это был костыль для последних документов, без которого ничего не работало.
            // Причина бага не найдена. Будет исправлено переделкой формата выходного документа из .rtf в .docx.
            ExtractedData.DebtStructure = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.DebtStructure).Replace("\n", "\\\n");
            ExtractedData.BankDetails = MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.BankDetails).Replace("\n", "\\\n");

            // Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек.
            if (Int32.TryParse(MyRegex.FindDataWithRegex(RichTextBox.Text, MyRegex.AllDebt), out int allDebt))
                ExtractedData.AllDebt = allDebt;
            else
                MessageBox.Show("В тексте не было найдено общей суммы задолженности, возможно вы пытаетесь извлечь данные из неподходящего документа");
        }

        /// <summary>
        /// Создает судебный приказ по шаблону
        /// </summary>
        private void CreateCourtOrder()
        {
            try
            {
                RichTextBox.LoadFile(PathsData.PathToTemplate, RichTextBoxStreamType.RichText);
            }
            catch (IOException)
            {
                MessageBox.Show("Выбранный шаблон не может быть открыт, возможно он используется другой программой, " +
                    "закройте программу использующую файл шаблона и попробуйте заново.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Программа будет закрыта т.к. произошла неизвестная ошибка:\n" + ex.ToString());
                Application.Exit();
            }

            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#FULLNAME#", ExtractedData.FullName);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#FULLNAMEGENITIVE#", ExtractedData.FullNameGenitive);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#DATEOFBIRTH#", ExtractedData.DateOfBirth);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#PLACEOFBIRTH#", ExtractedData.BirthPlace);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#ADDRESS#", ExtractedData.Address);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#INDIVIDUALTAXNUMBER#", ExtractedData.Inn);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#DEBTSTRUCTURE#", ExtractedData.DebtStructure);
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#GOSPOSHLINA#", ExtractedData.CalculateStateDuty().ToString());
            RichTextBox.Rtf = RichTextBox.Rtf.Replace("#BANKDETAILS#", ExtractedData.BankDetails);

            // Вроде лучше иначе активировать\деактивировать элементы формы (с т.з. оформления кода). Узнать как. 
            // saveButton.Enabled = true;
            // CreateCourtOrderButton.Enabled = false;
            // ShowDataButton.Enabled = false;
            /// <summary>
            /// #FULLNAME# - заменяется на ФИО 
            /// #GENITIVE# - заменяется на ФИО в родительном
            /// #DATEOFBIRTH# - заменяется на дату рождения
            /// #PLACEOFBIRTH# - заменяется на место рождения
            /// #ADDRESS# - заменяется на адрес
            /// #INDIVIDUALTAXNUMBER# - заменяется на ИНН
            /// #DEBTSTRUCTURE# - заменяется на текст описывающий задолженность
            /// #GOSPOSHLINA# - заменяется на сумму госпошлины
            /// #BANKDETAILS# - заменяется на реквизиты
            ///<summary>
        }

        // Изменить внутри
        /// <summary>
        /// Сохраняет судебный приказ
        /// </summary>
        private void SaveCourtOrder()
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Передалать работу с файлами через класс File
            string directory = Path.Combine(Path.GetDirectoryName(PathsData.PathToTemplate), "Приказы созданные программой");
            if (!Directory.Exists(directory)) //создаем директорию если её не существует
            {
                Directory.CreateDirectory(directory);
            }
            // Переделать пути через интерполированные строки
            RichTextBox.SaveFile(Path.Combine(directory, "Приказ ") + $"{ExtractedData.FullName}.rtf", RichTextBoxStreamType.RichText);
            File.Move(PathsData.PathToFileBeingProcessed, Path.Combine(Path.GetDirectoryName(PathsData.PathToFileBeingProcessed),
                "!" + Path.GetFileName(PathsData.PathToFileBeingProcessed)));

            // saveButton.Enabled = false;
        }

        /// <summary>
        /// Загружает текст из файла в richTextBox
        /// </summary>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Чужой код почти без изменений во всём using.
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;
                    dialog.Title = "Выберите файл";
                    dialog.Filter = "rtf files (*.rtf)|*.rtf";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        PathsData.PathToFileBeingProcessed = dialog.FileName;
                        RichTextBox.LoadFile(PathsData.PathToFileBeingProcessed, RichTextBoxStreamType.RichText);
                        FileIsOpen = true;
                    }
                }

            }
            catch (IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка при считывании файла:\n" + ex.ToString());
                Application.Exit();
            }
        }

        /// <summary>
        /// При нажатии кнопки извлекает все необходимые данные и сохраняет в отдельные строки.
        /// </summary>
        private void ExtractDataButton_Click(object sender, EventArgs e)
        {
            ExtractData();
            DataExtracted = true;
        }

        /// <summary>
        /// Отображает извлеченные данные в отдельном окне.
        /// </summary>
        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            DataViewForm f = new DataViewForm();
            f.ShowDialog();
        }

        /// <summary>
        /// Создает судебный приказ по шаблону.
        /// </summary>
        private void CreateCourtOrderButton_Click(object sender, EventArgs e)
        {
            CreateCourtOrder();
            CourtOrderCreated = true;
        }

        /// <summary>
        /// Выбираем шаблон для создания судебного приказа.
        /// </summary>
        private void ChooseATemplateOrderButton_Click(object sender, EventArgs e)
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
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        PathsData.PathToTemplate = dialog.FileName; // Сохраняем путь к шаблону приказа.
                        TemplateFileSelected = true;
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка:\n" + ex.ToString());
                Application.Exit();
            }
        }

        /// <summary>
        /// Сохраняет созданный судебный приказ из RichTextBox.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveCourtOrder();
            CourtOrderSaved = true;
        }

        /// <summary>
        /// Автоматическое создание судебных приказов для всех файлов в папке. Сырое!
        /// </summary>
        private void DirectoryCreateOrderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выбор директории";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // saveButton.Enabled = false;
                    // CreateCourtOrderButton.Enabled = false;
                    // ExtractDataButton.Enabled = false;
                    // openFileButton.Enabled = false;
                    // ShowDataButton.Enabled = false;
                    // chooseATemplateOrederButton.Enabled = false;

                    var files = Directory.GetFiles(dialog.SelectedPath);
                    var rtfFiles = files.Where(f => Path.GetExtension(f).ToLower() == ".rtf");

                    foreach (var file in rtfFiles)
                    {
                        // Костыль для переименования файла, надо переработать само переименование в принципе.
                        PathsData.PathToFileBeingProcessed = file;

                        RichTextBox.LoadFile(PathsData.PathToFileBeingProcessed, RichTextBoxStreamType.RichText);
                        ExtractData();
                        CreateCourtOrder();
                        SaveCourtOrder();
                    }
                    MessageBox.Show("Обработано документов: " + rtfFiles.Count().ToString());
                }
            }
        }
    }
}
