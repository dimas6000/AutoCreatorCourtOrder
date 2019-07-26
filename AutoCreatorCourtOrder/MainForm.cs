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
            // Явно стоит переписать параметр ignoreCase, выглядит не очень, да и нелогично. Пока не придумал как.
            try
            {
                // Время поиска в регулярном выражении ограничивается 3 секундами.
                Regex regex = new Regex(regexPattern, ignoreCase, TimeSpan.FromSeconds(3));
                return regex.Match(RichTextBox.Text).Value;
            }
            catch (RegexMatchTimeoutException)
            {
                MessageBox.Show("Данные не найдены в тексте, возможно вы пытаетесь использовать неподходящий документ.");
                return "!!!ДАННЫЕ НЕ ОБНАРУЖЕНЫ!!!";
            }
        }

        /// <summary>
        /// Извлекает все необходимые данные из файла и сохраняет в класс ExtractedData.
        /// </summary>
        private void ExtractData()
        {
            ExtractedData.FullName = FindDataWithRegex(RegexPatterns.FullName);
            ExtractedData.Address = FindDataWithRegex(RegexPatterns.Address);
            ExtractedData.DateOfBirth = FindDataWithRegex(RegexPatterns.DateOfBirth);
            ExtractedData.BirthPlace = FindDataWithRegex(RegexPatterns.BirthPlace);
            ExtractedData.Inn = FindDataWithRegex(RegexPatterns.Inn, RegexOptions.None);

            // Непонятно. Почему-то структура долга и кбк при сохранении в rtf теряет переносы строки если не добавить экранирование к \n.
            // Пока на всякий случай оставил .Replace т.к. это был костыль для последних документов, без которого ничего не работало.
            // Причина бага не найдена. Будет исправлено переделкой формата выходного документа из .rtf в .docx.
            // RegexOptions.None в данном случае костыль,если игнорировать регистр, то регулярка может работать неверно.). 
            ExtractedData.DebtStructure = FindDataWithRegex(RegexPatterns.DebtStructure, RegexOptions.None).Replace("\n", "\\\n");
            ExtractedData.BankDetails = FindDataWithRegex(RegexPatterns.BankDetails).Replace("\n", "\\\n");

            // Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек.
            if (Int32.TryParse(FindDataWithRegex(RegexPatterns.AllDebt), out int allDebt))
                ExtractedData.AllDebt = allDebt;
            else
                MessageBox.Show("В тексте не было найдено общей суммы задолженности, возможно вы пытаетесь извлечь данные из неподходящего документа");

            // Если сумму не нашли, явно что-то не так, не даем работать дальше!!!
        }

        /// <summary>
        /// Создает судебный приказ по шаблону
        /// </summary>
        private void CreateCourtOrder()
        {
            try
            {
                RichTextBox.LoadFile(PathsData.PathToTemplate);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Выбранный шаблон не может быть открыт, возможно он используется другой программой, " +
                    "закройте программу использующую файл шаблона и попробуйте заново.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Программа будет закрыта т.к. произошла неизвестная ошибка/n" + ex.ToString());
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

        /// <summary>
        /// Сохраняет судебный приказ
        /// </summary>
        /// <param name="rename">Переименовывать ли исходный файл? Удобно с ручном режиме, в автоматическом вылетает</param>
        private void SaveCourtOrder(bool rename = true)
        {
            String directory = Path.GetDirectoryName(PathsData.PathToTemplate) + "\\Приказы созданные программой";
            if (!Directory.Exists(directory)) //создаем директорию если её не существует
            {
                Directory.CreateDirectory(directory);
            }
            RichTextBox.SaveFile(directory + "\\Приказ " + ExtractedData.FullName + ".rtf", RichTextBoxStreamType.RichText);

            if (rename)
                File.Move(PathsData.PathToFileBeingProcessed, Path.GetDirectoryName(PathsData.PathToFileBeingProcessed)
                    + "//!" + Path.GetFileName(PathsData.PathToFileBeingProcessed));

            // saveButton.Enabled = false;
        }

        /// <summary>
        /// Загружает текст из файла в richTextBox
        /// </summary>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            //  CreateCourtOrderButton.Enabled = false;
            //  saveButton.Enabled = false;
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
                        RichTextBox.LoadFile(PathsData.PathToFileBeingProcessed);
                        //  ExtractDataButton.Enabled = true; //после открытия файла позволяем извлечь данные
                        //  DirectoryCreateOrderButton.Enabled = false;
                    }
                }

            }
            catch (IOException)
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
        /// При нажатии кнопки извлекает все необходимые данные и сохраняет в отдельные строки.
        /// </summary>
        private void ExtractDataButton_Click(object sender, EventArgs e)
        {
            ExtractData();
            //  ExtractDataButton.Enabled = false;
            //  ShowDataButton.Enabled = true; //после извлечения данных позволяем просмотреть их
            //  if (PathsData.PathToTemplate != null)
            //      CreateCourtOrderButton.Enabled = true;
        }

        /// <summary>
        /// Отображает извлеченные данные в отдельном окне.
        /// </summary>
        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            using (DataViewForm f = new DataViewForm())
                f.ShowDialog();
        }

        /// <summary>
        /// Создает судебный приказ по шаблону.
        /// </summary>
        private void CreateCourtOrderButton_Click(object sender, EventArgs e)
        {
            CreateCourtOrder();
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
                                                                    // CreateCourtOrderButton.Enabled = true; // после открытия файла позволяем создание приказа
                                                                    // DirectoryCreateOrderButton.Enabled = true; // или позволяем выбор папки для автосоздания
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка/n" + ex.ToString());
                Application.Exit();
            }
        }


        /// <summary>
        /// Сохраняет созданный судебный приказ из RichTextBox.
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveCourtOrder();
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
                    foreach (var file in files)
                    {
                        RichTextBox.LoadFile(file);
                        ExtractData();
                        CreateCourtOrder();
                        SaveCourtOrder(false);
                    }
                }
            }
        }
    }
}
