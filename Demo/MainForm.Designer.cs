namespace Heron.Demo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.runButton = new System.Windows.Forms.Button();
            this.showBox = new System.Windows.Forms.PictureBox();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.modeBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.showBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timePicker
            // 
            this.timePicker.Location = new System.Drawing.Point(86, 44);
            this.timePicker.Name = "timePicker";
            this.timePicker.Size = new System.Drawing.Size(200, 26);
            this.timePicker.TabIndex = 0;
            this.timePicker.Value = System.DateTime.Now;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(399, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(111, 58);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "OK";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // showBox
            // 
            this.showBox.Location = new System.Drawing.Point(12, 92);
            this.showBox.Name = "showBox";
            this.showBox.Size = new System.Drawing.Size(1014, 688);
            this.showBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.showBox.TabIndex = 2;
            this.showBox.TabStop = false;
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(1032, 44);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoBox.Size = new System.Drawing.Size(425, 736);
            this.infoBox.TabIndex = 3;
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(86, 12);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(200, 26);
            this.codeBox.TabIndex = 4;
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(570, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(93, 58);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(669, 12);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(93, 58);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // modeBox
            // 
            this.modeBox.AutoSize = true;
            this.modeBox.Location = new System.Drawing.Point(850, 30);
            this.modeBox.Name = "modeBox";
            this.modeBox.Size = new System.Drawing.Size(107, 24);
            this.modeBox.TabIndex = 7;
            this.modeBox.Text = "Real-Time";
            this.modeBox.UseVisualStyleBackColor = true;
            this.modeBox.CheckedChanged += new System.EventHandler(this.modeBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 792);
            this.Controls.Add(this.modeBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.showBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.timePicker);
            this.Name = "MainForm";
            this.Text = "demo";
            ((System.ComponentModel.ISupportInitialize)(this.showBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.PictureBox showBox;
        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.CheckBox modeBox;
    }
}

