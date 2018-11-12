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
            //Находим ФИО
            Regex findFullNameRegex = new Regex(@"(?<=Должник.*)[а-яё]+\s+[а-яё]+\s+[а-яё]+", RegexOptions.IgnoreCase);
            Data.fullName = FindDataWithRegex(findFullNameRegex);

            //Находим Адрес
            Regex findAddress = new Regex(@"(?<=" + Data.fullName + @"\s*)\w.+?(?=\s*Дата)", RegexOptions.IgnoreCase);
            Data.address = FindDataWithRegex(findAddress);

            //Находим Дату рождения
            Regex findDOB = new Regex(@"(?<=Дата\s*рождения.\s*)\d+.\d+.\d+", RegexOptions.IgnoreCase);
            Data.DOB = FindDataWithRegex(findDOB);

            //Находим Место рождения
            Regex findBPL = new Regex(@"(?<=Место\s*рождения.\s*)\w.+?(?=\s*Общая)", RegexOptions.IgnoreCase);
            Data.BPL = FindDataWithRegex(findBPL);

            //Находим ИНН
            Regex findINN = new Regex(@"(?<=ИНН\s*)\d+");
            Data.INN = FindDataWithRegex(findINN);

            //Определяем какие задолженности
            Regex findDebtDescription = new Regex(@"(?<=недоимки\s*по.\s*)\S(.|\s)+?(?=\s*В\sсоответс)");
            Data.DebtStructure = FindDataWithRegex(findDebtDescription);

            //Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек
            Regex findAllDebt = new Regex(@"(?<=Общая\s*сумма.\s*)\d+");
            Data.allDebt = Convert.ToInt32(FindDataWithRegex(findAllDebt));
        }


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
                        this.richTextBox1.LoadFile(filename);
                    }
                }

                extractDataButton.Enabled = true; //после открытия файла позволяем извлечь данные
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Выбранный файл не может быть открыт, возможно он используется другой программой.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла неизвестная ошибка при считывании файла/n" + ex.ToString());
            }
        }


        /// <summary>
        /// При нажатии кнопки находит все нужные данные и сохраняет в отдельные строки
        /// </summary>
        private void extractDataButton_Click(object sender, EventArgs e)
        {
            //Далее перенести все данные в статическую структуру, возможно придумать как заменять регулярки из файла
            //После вывести все извлеченные данные в отдельные text box'ы или в listbox, далее как-то создавать судебный приказ по шаблону
            //По хорошему стоит придумать как сделать шаблон изменяемым, пока что видится такой же .rtf через richTextBox в котором 
            //По ключевым словам вставлять данные

            readData();
                        
            dataForm f = new dataForm();
            f.ShowDialog();
        }
    }
}
