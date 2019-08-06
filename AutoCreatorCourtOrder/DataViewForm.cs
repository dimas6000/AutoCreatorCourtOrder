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
    // Теоретически в этой форме пользователь может просмотреть данные.Практически же она не нужна,
    // т.к. при обработке большого числа файлов каждый файл проверять никто не будет.
    // Так что я её только при отладке использую. Нужно переработать концепцию или удалить.
    public partial class DataViewForm : Form
    {
        public DataViewForm(ExtractedData extractedData)
        {
            InitializeComponent();
            fullNameTextBox.Text = extractedData.FullName;
            addressTextBox.Text = extractedData.Address;
            bplTextBox.Text = extractedData.BirthPlace;
            dobTextBox.Text = extractedData.DateOfBirth;
            innTextBox.Text = extractedData.Inn;
            allDebtNumericUpDown.Value = Convert.ToDecimal(extractedData.AllDebt);
            debtStructureRichTextBox.Text = extractedData.DebtStructure;
            bankDetailsRichTextBox.Text = extractedData.BankDetails;
            stateDutyNumericUpDown.Value = extractedData.CalculateStateDuty();
        }
    }
}
