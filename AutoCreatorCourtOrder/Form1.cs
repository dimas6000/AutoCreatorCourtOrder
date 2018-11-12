using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //для регулярных выражений

namespace AutoCreatorCourtOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Поиск данных с помощью регулярных выражений
        /// </summary>
        /// <param name="regex">Выражение для поиска</param>
        private string FindDataWithRegex(Regex regex)
        {
            return regex.Match(richTextBox1.Text).Value;
        }

        /// <summary>
        /// Считывает все необходимые данные из файла и сохраняет в структуру
        /// </summary>
        private void readData()
        {
            //Ограничиваем время поиска во всех регулярках

            //Находим ФИО
            Regex findFullNameRegex = new Regex(@"(?<=Должник.*)[а-яё]+\s+[а-яё]+\s+[а-яё]+", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.FullName = FindDataWithRegex(findFullNameRegex);

            //Находим Адрес
            Regex findAddress = new Regex(@"(?<=" + Data.FullName + @"\s*)\w.+?(?=\s*Дата)", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.Address = FindDataWithRegex(findAddress);

            //Находим Дату рождения
            Regex findDOB = new Regex(@"(?<=Дата\s*рождения.\s*)\d+.\d+.\d+", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.DOB = FindDataWithRegex(findDOB);

            //Находим Место рождения
            Regex findBPL = new Regex(@"(?<=Место\s*рождения.\s*)\w.+?(?=\s*Общая)", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.BPL = FindDataWithRegex(findBPL);

            //Находим ИНН
            Regex findINN = new Regex(@"(?<=ИНН\s*)\d+", RegexOptions.None, TimeSpan.FromSeconds(3));
            Data.INN = FindDataWithRegex(findINN);

            //Определяем какие задолженности
            Regex findDebtDescription = new Regex(@"(?<=недоимки\s*по.\s*)\S(.|\s)+?(?=\s*В\sсоответс)", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.DebtStructure = FindDataWithRegex(findDebtDescription);

            //Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек
            Regex findAllDebt = new Regex(@"(?<=Общая\s*сумма.\s*)\d+", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            string test = FindDataWithRegex(findAllDebt);
            Data.AllDebt = Convert.ToInt32(FindDataWithRegex(findAllDebt));

            //Находим КБК
            Regex findBankDetails = new Regex(@"Получат(.|\s)+?(?=\s*Прилож)", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
            Data.BankDetails = FindDataWithRegex(findBankDetails);
        }

        /// <summary>
        /// Загружает текст из файла в richTextBox
        /// </summary>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                //чужой код без изменений во всём using
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.Multiselect = false;
                    dialog.Title = "Выберите файл";
                    dialog.Filter = "rtf files (*.rtf)|*.rtf";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string filename = dialog.FileName;
                        richTextBox1.LoadFile(filename);
                        extractDataButton.Enabled = true; //после открытия файла позволяем извлечь данные
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
            string test = richTextBox1.Rtf;
        }

        /// <summary>
        /// При нажатии кнопки находит все нужные данные и сохраняет в отдельные строки
        /// </summary>
        private void extractDataButton_Click(object sender, EventArgs e)
        {
            readData();
            extractDataButton.Enabled = false;
            showDataButton.Enabled = true; //после извлечения данных позволяем просмотреть их
        }

        /// <summary>
        /// Отображает извлеченные данные в отдельном окне с возможностью редактирования
        /// </summary>
        private void showDataButton_Click(object sender, EventArgs e)
        {
            dataForm f = new dataForm();
            f.ShowDialog();
        }

        private void createCourtOrderButton_Click(object sender, EventArgs e)
        {

            try
            {
                richTextBox1.LoadFile(Data.PathToTemplate);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Выбранный шаблон не может быть открыт, возможно он используется другой программой, " +
                    "закройте программу использующую файл шаблона и попробуйте заново.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка/n" + ex.ToString());
                Application.Exit();

            }
        
           
            
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#FULLNAME#", Data.FullName);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#DATEOFBIRTH#", Data.DOB);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#PLACEOFBIRTH#", Data.BPL);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#ADDRESS#", Data.Address);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#INDIVIDUALTAXNUMBER#", Data.INN);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#DEBTSTRUCTURE#", Data.DebtStructure);
            richTextBox1.Rtf = richTextBox1.Rtf.Replace("#GOSPOSHLINA#", Data.StateDuty(Data.AllDebt).ToString());


           
            // richTextBox1.SaveFile("!" + Data.FullName + "приказ.rtf");
            /// <summary>
            /// #FULLNAME# - заменяется на ФИО 
            ///#DATEOFBIRTH# - заменяется на дату рождения
            ///#PLACEOFBIRTH# - заменяется на место рождения
            ///#ADDRESS# - заменяется на адрес
            ///#INDIVIDUALTAXNUMBER# - заменяется на ИНН
            ///#DEBTSTRUCTURE# - заменяется на текст описывающий задолженность
            ///#GOSPOSHLINA# - заменяется на сумму госпошлины
            ///<summary>
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
                        Data.PathToTemplate = dialog.FileName; //используем шаблон приказа
                        createCourtOrderButton.Enabled = true; //после открытия файла позволяем создание приказа
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
            richTextBox1.SaveFile(Data.PathToTemplate + Data.FullName + ".rtf", RichTextBoxStreamType.RichText);
        }


        //Далее стоит возможно придумать, как заменять регулярки из файла
        //Далее как-то создавать судебный приказ по шаблону

    }
}
