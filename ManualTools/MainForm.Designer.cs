namespace Heron.ManualTools
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
            this.components = new System.ComponentModel.Container();
            this.displayBox = new System.Windows.Forms.DataGridView();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.codeBox = new System.Windows.Forms.TextBox();
            this.actionBox = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.logButton = new System.Windows.Forms.Button();
            this.idBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // displayBox
            // 
            this.displayBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayBox.Location = new System.Drawing.Point(12, 12);
            this.displayBox.Name = "displayBox";
            this.displayBox.ReadOnly = true;
            this.displayBox.RowTemplate.Height = 28;
            this.displayBox.Size = new System.Drawing.Size(985, 633);
            this.displayBox.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(1018, 307);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(166, 26);
            this.codeBox.TabIndex = 1;
            // 
            // actionBox
            // 
            this.actionBox.FormattingEnabled = true;
            this.actionBox.Location = new System.Drawing.Point(1018, 367);
            this.actionBox.Name = "actionBox";
            this.actionBox.Size = new System.Drawing.Size(166, 28);
            this.actionBox.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1018, 445);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 51);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Add";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(1059, 159);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 52);
            this.logButton.TabIndex = 4;
            this.logButton.Text = "login";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(1048, 85);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(100, 26);
            this.idBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 676);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.actionBox);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.displayBox);
            this.Name = "MainForm";
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView displayBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox codeBox;
        private System.Windows.Forms.ComboBox actionBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.TextBox idBox;
    }
}

