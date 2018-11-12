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
            fullNameTextBox.Text = Data.fullName;
            addressTextBox.Text = Data.address;
            bplTextBox.Text = Data.BPL;
            dobTextBox.Text = Data.DOB;
            innTextBox.Text = Data.INN;
            allDebtTextBox.Text = Data.allDebt.ToString();
            debtStructureRichTextBox.Text = Data.DebtStructure;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
