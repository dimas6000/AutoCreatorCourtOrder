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
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.ExtractDataButton = new System.Windows.Forms.Button();
            this.ShowDataButton = new System.Windows.Forms.Button();
            this.CreateCourtOrderButton = new System.Windows.Forms.Button();
            this.ChooseATemplateOrderButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DirectoryCreateCourtOrderButton = new System.Windows.Forms.Button();
            this.ProgressBarMultiThreading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // RichTextBox
            // 
            this.RichTextBox.AcceptsTab = true;
            this.RichTextBox.DetectUrls = false;
            this.RichTextBox.Location = new System.Drawing.Point(13, 13);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.ReadOnly = true;
            this.RichTextBox.Size = new System.Drawing.Size(1200, 613);
            this.RichTextBox.TabIndex = 0;
            this.RichTextBox.Text = "";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(182, 632);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(163, 48);
            this.OpenFileButton.TabIndex = 1;
            this.OpenFileButton.Text = "Открыть файл";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // ExtractDataButton
            // 
            this.ExtractDataButton.Location = new System.Drawing.Point(351, 632);
            this.ExtractDataButton.Name = "ExtractDataButton";
            this.ExtractDataButton.Size = new System.Drawing.Size(163, 48);
            this.ExtractDataButton.TabIndex = 2;
            this.ExtractDataButton.Text = "Извлечь данные";
            this.ExtractDataButton.UseVisualStyleBackColor = true;
            this.ExtractDataButton.Click += new System.EventHandler(this.ExtractDataButton_Click);
            // 
            // ShowDataButton
            // 
            this.ShowDataButton.Location = new System.Drawing.Point(351, 686);
            this.ShowDataButton.Name = "ShowDataButton";
            this.ShowDataButton.Size = new System.Drawing.Size(163, 48);
            this.ShowDataButton.TabIndex = 3;
            this.ShowDataButton.Text = "Просмотреть данные";
            this.ShowDataButton.UseVisualStyleBackColor = true;
            this.ShowDataButton.Click += new System.EventHandler(this.ShowDataButton_Click);
            // 
            // CreateCourtOrderButton
            // 
            this.CreateCourtOrderButton.Location = new System.Drawing.Point(520, 632);
            this.CreateCourtOrderButton.Name = "CreateCourtOrderButton";
            this.CreateCourtOrderButton.Size = new System.Drawing.Size(163, 48);
            this.CreateCourtOrderButton.TabIndex = 4;
            this.CreateCourtOrderButton.Text = "Создать \r\nсудебный приказ";
            this.CreateCourtOrderButton.UseVisualStyleBackColor = true;
            this.CreateCourtOrderButton.Click += new System.EventHandler(this.CreateCourtOrderButton_Click);
            // 
            // ChooseATemplateOrderButton
            // 
            this.ChooseATemplateOrderButton.Location = new System.Drawing.Point(12, 632);
            this.ChooseATemplateOrderButton.Name = "ChooseATemplateOrderButton";
            this.ChooseATemplateOrderButton.Size = new System.Drawing.Size(163, 48);
            this.ChooseATemplateOrderButton.TabIndex = 5;
            this.ChooseATemplateOrderButton.Text = "Выбрать шаблон приказа";
            this.ChooseATemplateOrderButton.UseVisualStyleBackColor = true;
            this.ChooseATemplateOrderButton.Click += new System.EventHandler(this.ChooseATemplateOrderButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(689, 632);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(163, 48);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Сохранить судебный приказ";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DirectoryCreateCourtOrderButton
            // 
            this.DirectoryCreateCourtOrderButton.Location = new System.Drawing.Point(880, 632);
            this.DirectoryCreateCourtOrderButton.Name = "DirectoryCreateCourtOrderButton";
            this.DirectoryCreateCourtOrderButton.Size = new System.Drawing.Size(332, 102);
            this.DirectoryCreateCourtOrderButton.TabIndex = 7;
            this.DirectoryCreateCourtOrderButton.Text = "Выбрать папку с заявлениями для создания судебных приказов и создать приказы для " +
    "всех файлов из папки";
            this.DirectoryCreateCourtOrderButton.UseVisualStyleBackColor = true;
            this.DirectoryCreateCourtOrderButton.Click += new System.EventHandler(this.DirectoryCreateOrderButton_Click);
            // 
            // ProgressBarMultiThreading
            // 
            this.ProgressBarMultiThreading.Location = new System.Drawing.Point(520, 686);
            this.ProgressBarMultiThreading.Name = "ProgressBarMultiThreading";
            this.ProgressBarMultiThreading.Size = new System.Drawing.Size(332, 48);
            this.ProgressBarMultiThreading.Step = 1;
            this.ProgressBarMultiThreading.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1224, 747);
            this.Controls.Add(this.ProgressBarMultiThreading);
            this.Controls.Add(this.DirectoryCreateCourtOrderButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ChooseATemplateOrderButton);
            this.Controls.Add(this.CreateCourtOrderButton);
            this.Controls.Add(this.ShowDataButton);
            this.Controls.Add(this.ExtractDataButton);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.RichTextBox);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBox;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button ExtractDataButton;
        private System.Windows.Forms.Button ShowDataButton;
        private System.Windows.Forms.Button CreateCourtOrderButton;
        private System.Windows.Forms.Button ChooseATemplateOrderButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DirectoryCreateCourtOrderButton;
        private System.Windows.Forms.ProgressBar ProgressBarMultiThreading;
    }
}

