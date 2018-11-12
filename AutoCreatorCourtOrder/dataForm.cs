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
    public partial class dataForm : Form
    {
        public dataForm()
        {
            InitializeComponent();
            fullNameTextBox.Text = Data.FullName;
            addressTextBox.Text = Data.Address;
            bplTextBox.Text = Data.BPL;
            dobTextBox.Text = Data.DOB;
            innTextBox.Text = Data.INN;
            allDebtNumericUpDown.Value = Convert.ToDecimal(Data.AllDebt);
            debtStructureRichTextBox.Text = Data.DebtStructure;
            stateDutyNumericUpDown.Value = Data.StateDuty(Data.AllDebt);
        }

        private void editDataButton_Click(object sender, EventArgs e)
        {
            Data.FullName = fullNameTextBox.Text;
            Data.Address = addressTextBox.Text;
            Data.BPL = bplTextBox.Text;
            Data.DOB = dobTextBox.Text;
            Data.INN = innTextBox.Text;
            Data.DebtStructure = debtStructureRichTextBox.Text;
            Data.AllDebt = Convert.ToInt32(allDebtNumericUpDown.Value);
        }

        /// <summary>
        /// При изменении значения суммы иска пересчитываем госпошлину
        /// </summary>
        private void allDebtNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            stateDutyNumericUpDown.Value = Data.StateDuty(Convert.ToInt32(allDebtNumericUpDown.Value));
        }
    }
}
