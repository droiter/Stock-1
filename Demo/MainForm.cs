using System;
using System.Drawing;
using System.Windows.Forms;

namespace Heron.Demo
{
    public partial class MainForm : Form
    {
        private Painter _painter;
        private Workflow _instance;

        private void infoHandler(Image img, string info)
        {
            this.Invoke(new Action(() =>
            {
                showBox.Image = img;
                infoBox.AppendText(info);
            }));
        }

        public MainForm()
        {
            InitializeComponent();
            _painter = new Painter();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            _instance = new Workflow(timePicker.Value.Date, codeBox.Text, _painter);

            string info = _instance.Process();

            showBox.Image = _painter.GetImage();
            infoBox.Text = info;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            infoBox.Text = "";
            _instance = new Workflow(this.infoHandler, codeBox.Text, _painter);

            _instance.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _instance.Stop();
        }

        private void modeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (modeBox.Checked)
            {
                timePicker.Enabled = false;
                runButton.Enabled = false;
                startButton.Enabled = true;
                stopButton.Enabled = true;
            }
            else
            {
                timePicker.Enabled = true;
                runButton.Enabled = true;
                startButton.Enabled = false;
                stopButton.Enabled = false;
            }
        }
    }
}
