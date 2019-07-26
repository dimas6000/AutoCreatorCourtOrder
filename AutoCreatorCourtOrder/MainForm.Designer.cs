namespace AutoCreatorCourtOrder
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.ExtractDataButton = new System.Windows.Forms.Button();
            this.ShowDataButton = new System.Windows.Forms.Button();
            this.CreateCourtOrderButton = new System.Windows.Forms.Button();
            this.chooseATemplateOrederButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.DirectoryCreateOrderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.RichTextBox.AcceptsTab = true;
            this.RichTextBox.DetectUrls = false;
            this.RichTextBox.Location = new System.Drawing.Point(13, 13);
            this.RichTextBox.Name = "richTextBox1";
            this.RichTextBox.ReadOnly = true;
            this.RichTextBox.Size = new System.Drawing.Size(1200, 613);
            this.RichTextBox.TabIndex = 0;
            this.RichTextBox.Text = "";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 632);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(163, 48);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Открыть файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // extractDataButton
            // 
            this.ExtractDataButton.Enabled = false;
            this.ExtractDataButton.Location = new System.Drawing.Point(181, 632);
            this.ExtractDataButton.Name = "extractDataButton";
            this.ExtractDataButton.Size = new System.Drawing.Size(163, 48);
            this.ExtractDataButton.TabIndex = 2;
            this.ExtractDataButton.Text = "Извлечь данные";
            this.ExtractDataButton.UseVisualStyleBackColor = true;
            this.ExtractDataButton.Click += new System.EventHandler(this.ExtractDataButton_Click);
            // 
            // showDataButton
            // 
            this.ShowDataButton.Enabled = false;
            this.ShowDataButton.Location = new System.Drawing.Point(181, 687);
            this.ShowDataButton.Name = "showDataButton";
            this.ShowDataButton.Size = new System.Drawing.Size(163, 48);
            this.ShowDataButton.TabIndex = 3;
            this.ShowDataButton.Text = "Просмотреть данные";
            this.ShowDataButton.UseVisualStyleBackColor = true;
            this.ShowDataButton.Click += new System.EventHandler(this.ShowDataButton_Click);
            // 
            // createCourtOrderButton
            // 
            this.CreateCourtOrderButton.Enabled = false;
            this.CreateCourtOrderButton.Location = new System.Drawing.Point(350, 632);
            this.CreateCourtOrderButton.Name = "createCourtOrderButton";
            this.CreateCourtOrderButton.Size = new System.Drawing.Size(163, 48);
            this.CreateCourtOrderButton.TabIndex = 4;
            this.CreateCourtOrderButton.Text = "Создать \r\nсудебный приказ";
            this.CreateCourtOrderButton.UseVisualStyleBackColor = true;
            this.CreateCourtOrderButton.Click += new System.EventHandler(this.CreateCourtOrderButton_Click);
            // 
            // chooseATemplateOrederButton
            // 
            this.chooseATemplateOrederButton.Location = new System.Drawing.Point(350, 687);
            this.chooseATemplateOrederButton.Name = "chooseATemplateOrederButton";
            this.chooseATemplateOrederButton.Size = new System.Drawing.Size(163, 48);
            this.chooseATemplateOrederButton.TabIndex = 5;
            this.chooseATemplateOrederButton.Text = "Выбрать шаблон приказа";
            this.chooseATemplateOrederButton.UseVisualStyleBackColor = true;
            this.chooseATemplateOrederButton.Click += new System.EventHandler(this.ChooseATemplateOrderButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(519, 632);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(163, 48);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Сохранить судебный приказ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // directoryCreateOrderButton
            // 
            this.DirectoryCreateOrderButton.Enabled = false;
            this.DirectoryCreateOrderButton.Location = new System.Drawing.Point(880, 633);
            this.DirectoryCreateOrderButton.Name = "directoryCreateOrderButton";
            this.DirectoryCreateOrderButton.Size = new System.Drawing.Size(332, 102);
            this.DirectoryCreateOrderButton.TabIndex = 7;
            this.DirectoryCreateOrderButton.Text = "Выбрать папку с заявлениями для создания судебных приказов и создать приказы для " +
    "всех файлов из папки";
            this.DirectoryCreateOrderButton.UseVisualStyleBackColor = true;
            this.DirectoryCreateOrderButton.Click += new System.EventHandler(this.directoryCreateOrderButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1224, 747);
            this.Controls.Add(this.DirectoryCreateOrderButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.chooseATemplateOrederButton);
            this.Controls.Add(this.CreateCourtOrderButton);
            this.Controls.Add(this.ShowDataButton);
            this.Controls.Add(this.ExtractDataButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.RichTextBox);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button ExtractDataButton;
        private System.Windows.Forms.Button ShowDataButton;
        private System.Windows.Forms.Button CreateCourtOrderButton;
        private System.Windows.Forms.Button chooseATemplateOrederButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button DirectoryCreateOrderButton;
    }
}

