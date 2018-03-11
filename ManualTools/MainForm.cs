using System;
using System.Windows.Forms;
using Heron.Utility;

namespace Heron.ManualTools
{
    public partial class MainForm : Form
    {
        int sum = 0;

        public MainForm()
        {
            InitializeComponent();

            actionBox.Items.AddRange(new object[] {
                Actions.OpenLong,
                Actions.CloseLong,
                Actions.OpenShort,
                Actions.CloseShort,
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            runButton.Visible = false;
            codeBox.Visible = false;
            actionBox.Visible = false;

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            var info = Helper.Retrieve();
            displayBox.DataSource = info;
            displayBox.FirstDisplayedScrollingRowIndex = info.Rows.Count;

            if (info.Rows.Count > sum)
                Console.Beep(800, 1000);

            sum = info.Rows.Count;

            timer.Enabled = true;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (actionBox.SelectedItem == null)
                return;

            var signal = Helper.GenerateOrder(codeBox.Text, (Actions)actionBox.SelectedItem);
            Helper.Add(signal);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (idBox.Text == "HeronZhao")
            {
                runButton.Visible = true;
                codeBox.Visible = true;
                actionBox.Visible = true;
            }
        }
    }
}
