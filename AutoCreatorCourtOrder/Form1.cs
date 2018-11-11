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

       /* /// <summary>
        /// Поиск данных с помощью регулярных выражений и удаление лишнего
        /// </summary>
        /// <param name="regex">Выражение для поиска</param>
        /// <param name="forDelete">Что удалить из результирующей строки</param>
        /// <returns>Возвращает найденные данные удалив forDelete</returns>
        private string FindDataWithRegex(Regex regex, string forDelete)
        {
            string data = regex.Match(richTextBox1.Text).Value;
            return data.Replace(forDelete, "");
        }*/


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

            //Находим ФИО
            Regex findFullNameRegex = new Regex(@"(?<=Должник.*)[а-яё]+\s+[а-яё]+\s+[а-яё]+", RegexOptions.IgnoreCase);
            string fullName = FindDataWithRegex(findFullNameRegex);
            MessageBox.Show(fullName);

            //Находим Адрес
            Regex findAddress = new Regex(@"(?<=" + fullName + @"\s*)\w.+?(?=\s*Дата)", RegexOptions.IgnoreCase);
            string address = FindDataWithRegex(findAddress);
            MessageBox.Show(address);

            //Находим Дату рождения
            Regex findDOB = new Regex(@"(?<=Дата\s*рождения.\s*)\d+.\d+.\d+", RegexOptions.IgnoreCase);
            string DOB = FindDataWithRegex(findDOB);
            MessageBox.Show(DOB);

            //Находим Место рождения
            Regex findBPL = new Regex(@"(?<=Место\s*рождения.\s*)\w.+?(?=\s*Общая)", RegexOptions.IgnoreCase);
            string BPL = FindDataWithRegex(findBPL);
            MessageBox.Show(BPL);

            //Находим ИНН
            Regex findINN = new Regex(@"(?<=ИНН\s*)\d+");
            string INN = FindDataWithRegex(findINN);
            MessageBox.Show(INN);

            //Определяем какие задолженности
            Regex findDebtDescription = new Regex(@"(?<=недоимки\s*по.\s*)\S(.|\s)+?(?=\s*В\sсоответс)");
            string DebtDescription = FindDataWithRegex(findDebtDescription);
            MessageBox.Show(DebtDescription);

            //Определяем общую сумму задолженности для расчета госпошлины БЕЗ учета копеек
            Regex findAllDebt = new Regex(@"(?<=Общая\s*сумма.\s*)\d+");
            int allDebt =Convert.ToInt32(FindDataWithRegex(findAllDebt));
            MessageBox.Show(allDebt.ToString());

        }
    }
}
