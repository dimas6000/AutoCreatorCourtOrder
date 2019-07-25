using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreatorCourtOrder
{
    // Теоретически в этой форме пользователь может просмотреть и, в случае нужды, изменить данные.
    // Практически же она не нужна, т.к. при обработке большого числа файлов каждый файл проверять
    // никто не будет. Так что я её только при отладке использую. Нужно переработать или удалить.

    public partial class DataViewForm : Form
    {
        public DataViewForm()
        {
            InitializeComponent();
            fullNameTextBox.Text = ExtractedData.FullName;
            addressTextBox.Text = ExtractedData.Address;
            bplTextBox.Text = ExtractedData.BirthPlace;
            dobTextBox.Text = ExtractedData.DateOfBirth;
            innTextBox.Text = ExtractedData.Inn;
            allDebtNumericUpDown.Value = Convert.ToDecimal(ExtractedData.AllDebt);
            debtStructureRichTextBox.Text = ExtractedData.DebtStructure;
            bankDetailsRichTextBox.Text = ExtractedData.BankDetails;
            stateDutyNumericUpDown.Value = ExtractedData.CalculateStateDuty();
        }
    }
}
