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
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.extractDataButton = new System.Windows.Forms.Button();
            this.showDataButton = new System.Windows.Forms.Button();
            this.createCourtOrderButton = new System.Windows.Forms.Button();
            this.chooseATemplateOrderButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.directoryCreateCourtOrderButton = new System.Windows.Forms.Button();
            this.progressBarMultiThreading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.DetectUrls = false;
            this.richTextBox.Location = new System.Drawing.Point(13, 13);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(1200, 613);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(182, 632);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(163, 48);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Открыть заявление";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // extractDataButton
            // 
            this.extractDataButton.Location = new System.Drawing.Point(351, 632);
            this.extractDataButton.Name = "extractDataButton";
            this.extractDataButton.Size = new System.Drawing.Size(163, 48);
            this.extractDataButton.TabIndex = 2;
            this.extractDataButton.Text = "Извлечь данные";
            this.extractDataButton.UseVisualStyleBackColor = true;
            this.extractDataButton.Click += new System.EventHandler(this.ExtractDataButton_Click);
            // 
            // showDataButton
            // 
            this.showDataButton.Location = new System.Drawing.Point(351, 686);
            this.showDataButton.Name = "showDataButton";
            this.showDataButton.Size = new System.Drawing.Size(163, 48);
            this.showDataButton.TabIndex = 3;
            this.showDataButton.Text = "Просмотреть данные";
            this.showDataButton.UseVisualStyleBackColor = true;
            this.showDataButton.Click += new System.EventHandler(this.ShowDataButton_Click);
            // 
            // createCourtOrderButton
            // 
            this.createCourtOrderButton.Location = new System.Drawing.Point(520, 632);
            this.createCourtOrderButton.Name = "createCourtOrderButton";
            this.createCourtOrderButton.Size = new System.Drawing.Size(163, 48);
            this.createCourtOrderButton.TabIndex = 4;
            this.createCourtOrderButton.Text = "Создать \r\nсудебный приказ";
            this.createCourtOrderButton.UseVisualStyleBackColor = true;
            this.createCourtOrderButton.Click += new System.EventHandler(this.CreateCourtOrderButton_Click);
            // 
            // chooseATemplateOrderButton
            // 
            this.chooseATemplateOrderButton.Location = new System.Drawing.Point(12, 632);
            this.chooseATemplateOrderButton.Name = "chooseATemplateOrderButton";
            this.chooseATemplateOrderButton.Size = new System.Drawing.Size(163, 48);
            this.chooseATemplateOrderButton.TabIndex = 5;
            this.chooseATemplateOrderButton.Text = "Выбрать шаблон приказа";
            this.chooseATemplateOrderButton.UseVisualStyleBackColor = true;
            this.chooseATemplateOrderButton.Click += new System.EventHandler(this.ChooseATemplateOrderButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(689, 632);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(163, 48);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Сохранить судебный приказ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // directoryCreateCourtOrderButton
            // 
            this.directoryCreateCourtOrderButton.Location = new System.Drawing.Point(880, 632);
            this.directoryCreateCourtOrderButton.Name = "directoryCreateCourtOrderButton";
            this.directoryCreateCourtOrderButton.Size = new System.Drawing.Size(332, 102);
            this.directoryCreateCourtOrderButton.TabIndex = 7;
            this.directoryCreateCourtOrderButton.Text = "Выбрать папку с заявлениями для создания судебных приказов и создать приказы для " +
    "всех файлов из папки";
            this.directoryCreateCourtOrderButton.UseVisualStyleBackColor = true;
            this.directoryCreateCourtOrderButton.Click += new System.EventHandler(this.DirectoryCreateOrderButton_Click);
            // 
            // progressBarMultiThreading
            // 
            this.progressBarMultiThreading.Location = new System.Drawing.Point(520, 686);
            this.progressBarMultiThreading.Name = "progressBarMultiThreading";
            this.progressBarMultiThreading.Size = new System.Drawing.Size(332, 48);
            this.progressBarMultiThreading.Step = 1;
            this.progressBarMultiThreading.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1224, 747);
            this.Controls.Add(this.progressBarMultiThreading);
            this.Controls.Add(this.directoryCreateCourtOrderButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.chooseATemplateOrderButton);
            this.Controls.Add(this.createCourtOrderButton);
            this.Controls.Add(this.showDataButton);
            this.Controls.Add(this.extractDataButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.richTextBox);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button extractDataButton;
        private System.Windows.Forms.Button showDataButton;
        private System.Windows.Forms.Button createCourtOrderButton;
        private System.Windows.Forms.Button chooseATemplateOrderButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button directoryCreateCourtOrderButton;
        private System.Windows.Forms.ProgressBar progressBarMultiThreading;
    }
}

