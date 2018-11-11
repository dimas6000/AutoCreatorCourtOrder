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
        /// Открытие .rtf
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
            string initialText = richTextBox1.Text;

            //Находим ФИО
            Regex findFullName = new Regex(@"Должник:\s*[а-яё]*\s*[а-яё]*\s*[а-яё]*", RegexOptions.IgnoreCase);
            Match fullName = findFullName.Match(initialText);
            MessageBox.Show(fullName.Value);

            //Находим Адрес НЕ ОБРАБОТАНО
            Regex findAddress = new Regex(@"Должник:\s*[а-яё]*\s*[а-яё]*\s*[а-яё]*", RegexOptions.IgnoreCase);
            Match address = findAddress.Match(initialText);
            MessageBox.Show(address.Value);

            //Находим Дату рождения
            Regex findDOB = new Regex(@"Дата\s*рождения.\s*[0-9]*.[0-9]*.[0-9]*", RegexOptions.IgnoreCase);
            Match DOB = findDOB.Match(initialText);
            MessageBox.Show(DOB.Value);

            //Находим ИНН
            Regex findINN = new Regex(@"ИНН\s*[0-9]+");
            Match INN = findINN.Match(initialText);
            MessageBox.Show(INN.Value);


        }
    }
}
