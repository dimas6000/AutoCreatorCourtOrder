using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Для регулярных выражений.
using System.IO;

namespace AutoCreatorCourtOrder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Поиск данных во входном документе с помощью регулярных выражений.
        /// </summary>
        /// <param name="regexPattern">Паттерн регулярного выражения.</param>
        /// <param name="ignoreCase">Если не нужно игнорировать регистр, то передаем сюда RegexOptions.None</param>
        /// <returns>Строка с результатом поиска.</returns>
        private string FindDataWithRegex(string regexPattern, RegexOptions ignoreCase = RegexOptions.IgnoreCase)
        {
            //Явно стоит переписать параметр ignoreCase, выглядит не очень, да и нелогично. Пока не придумал как.
            try
            {
                // Время поиска в регулярном выражении ограничено 3 секундами
                Regex regex = new Regex(regexPattern, ignoreCase, TimeSpan.FromSeconds(3));
                return regex.Match(richTextBox1.Text).Value;
            }
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                MessageBox.Show("Данные не найдены в тексте, возможно вы пытаетесь использовать неподходящий документ.");
                return "!!!ДАННЫЕ НЕ ОБНАРУЖЕНЫ!!!";
            }
        }

        /// <summary>
        /// Считывает все необходимые данные из файла и сохраняет в структуру.
        /// </summary>
        private void readData()
        {
            // Находим ФИО.
            ExtractedData.FullName = FindDataWithRegex(RegexPatterns.FullName);

            // Находим адрес.
            ExtractedData.Address = FindDataWithRegex(RegexPatterns.Address);

            // Находим дату рождения.
            ExtractedData.DateOfBirth = FindDataWithRegex(RegexPatterns.DateOfBirth);

            // Находим место рождения.
            ExtractedData.BirthPlace = FindDataWithRegex(RegexPatterns.BirthPlace);

            // Находим ИНН, не игнорируем регистр!
            ExtractedData.Inn = FindDataWithRegex(RegexPatterns.Inn, RegexOptions.None);

            // Определяем какие задолженности (RegexOptions.None в данном случае костыль, 
            // т.к. если игнорировать регистр, то регулярка может работать неверно.). 
            ExtractedData.DebtStructure = FindDataWithRegex(RegexPatterns.DebtStructure, RegexOptions.None).Replace("\n", "\\\n");
            // Непонятно. Почему-то структура долга и кбк при сохранении в rtf теряет переносы строки если не добавить экранирование к \n.
            // Пока на всякий случай оставил .Replace т.к. это был костыль для последних документов, без которого ничего не работало.
            // Причина бага не найдена. Будет исправлено переделкой формата выходного документа из .rtf в .docx.

            // Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек.
            if (Int32.TryParse(FindDataWithRegex(RegexPatterns.AllDebt), out int allDebt))
                ExtractedData.AllDebt = allDebt;
            else
                MessageBox.Show("В тексте не было найдено общей суммы задолженности, возможно вы пытаетесь извлечь данные из неподходящего документа");

            // Находим КБК\реквизиты.
            ExtractedData.BankDetails = FindDataWithRegex(RegexPatterns.BankDetails).Replace("\n", "\\\n");
        }

        /// <summary>
        /// Создает судебный приказ по шаблону
        /// </summary>
        private void createCourtOrder()
        {

            try
            {
                richTextBox1.LoadFile(ExtractedData.PathToTemplate);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Выбранный шаблон не может быть открыт, возможно он используется другой программой, " +
                    "закройте программу использующую файл шаблона и попробуйте заново.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка/n" + ex.ToString());
                Application.Exit();
            }

            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#FULLNAME#", ExtractedData.FullName);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#FULLNAMEGENITIVE#", ExtractedData.FullNameGenitive);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#DATEOFBIRTH#", ExtractedData.DateOfBirth);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#PLACEOFBIRTH#", ExtractedData.BirthPlace);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#ADDRESS#", ExtractedData.Address);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#INDIVIDUALTAXNUMBER#", ExtractedData.Inn);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#DEBTSTRUCTURE#", ExtractedData.DebtStructure);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#GOSPOSHLINA#", ExtractedData.CalculateStateDuty().ToString());
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#BANKDETAILS#", ExtractedData.BankDetails);

            saveButton.Enabled = true;
            createCourtOrderButton.Enabled = false;
            showDataButton.Enabled = false;
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

        /// <summary>
        /// Сохраняет судебный приказ
        /// </summary>
        /// <param name="rename">Переименовывать ли исходный файл? Удобно с ручном режиме, в автоматическом вылетае</param>
        private void saveCourtOrder(bool rename = true)
        {
            String directory = Path.GetDirectoryName(ExtractedData.PathToTemplate) + "\\Приказы созданные программой";
            if (!Directory.Exists(directory)) //создаем директорию если её не существует
            {
                Directory.CreateDirectory(directory);
            }
            richTextBox1.SaveFile(directory + "\\Приказ " + ExtractedData.FullName + ".rtf", RichTextBoxStreamType.RichText);

            if (rename)
                File.Move(ExtractedData.PathToProcessedFile, Path.GetDirectoryName(ExtractedData.PathToProcessedFile)
                    + "//!" + Path.GetFileName(ExtractedData.PathToProcessedFile));

            saveButton.Enabled = false;
        }

        /// <summary>
        /// Загружает текст из файла в richTextBox
        /// </summary>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            createCourtOrderButton.Enabled = false;
            saveButton.Enabled = false;
            try
            {
                // чужой код почти без изменений во всём using
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;
                    dialog.Title = "Выберите файл";
                    dialog.Filter = "rtf files (*.rtf)|*.rtf";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ExtractedData.PathToProcessedFile = dialog.FileName;
                        richTextBox1.LoadFile(ExtractedData.PathToProcessedFile);
                        extractDataButton.Enabled = true; //после открытия файла позволяем извлечь данные
                        directoryCreateOrderButton.Enabled = false;
                    }
                }

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка при считывании файла/n" + ex.ToString());
                Application.Exit();
            }
        }

        /// <summary>
        /// При нажатии кнопки находит все нужные данные и сохраняет в отдельные строки
        /// </summary>
        private void extractDataButton_Click(object sender, EventArgs e)
        {
            readData();
            extractDataButton.Enabled = false;
            showDataButton.Enabled = true; //после извлечения данных позволяем просмотреть их
            if (ExtractedData.PathToTemplate != null)
                createCourtOrderButton.Enabled = true;
        }

        /// <summary>
        /// Отображает извлеченные данные в отдельном окне с возможностью редактирования
        /// </summary>
        private void showDataButton_Click(object sender, EventArgs e)
        {
                DataViewForm f = new DataViewForm();
            f.ShowDialog();
        }

        private void createCourtOrderButton_Click(object sender, EventArgs e)
        {
            createCourtOrder();
        }

        /// <summary>
        /// Выбирем шаблон для создания судебного приказа
        /// </summary>
        private void chooseATemplateOrederButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;
                    dialog.Title = "Выберите файл шаблона";
                    dialog.Filter = "rtf files (*.rtf)|*.rtf";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ExtractedData.PathToTemplate = dialog.FileName; // используем шаблон приказа
                        createCourtOrderButton.Enabled = true; // после открытия файла позволяем создание приказа
                        directoryCreateOrderButton.Enabled = true; // или позволяем выбор папки для автосоздания
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка/n" + ex.ToString());
                Application.Exit();
            }
        }


        /// <summary>
        /// Сохраняет файл из richTextBox1
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            saveCourtOrder();
        }

        private void directoryCreateOrderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выбор директории";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    saveButton.Enabled = false;
                    createCourtOrderButton.Enabled = false;
                    extractDataButton.Enabled = false;
                    openFileButton.Enabled = false;
                    showDataButton.Enabled = false;
                    chooseATemplateOrederButton.Enabled = false;

                    var files = Directory.GetFiles(dialog.SelectedPath);
                    foreach (var file in files)
                    {
                        richTextBox1.LoadFile(file);
                        readData();
                        createCourtOrder();
                        saveCourtOrder(false);
                    }
                }
            }
        }


    }
}
