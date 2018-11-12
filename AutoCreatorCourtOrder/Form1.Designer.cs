namespace AutoCreatorCourtOrder
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.extractDataButton = new System.Windows.Forms.Button();
            this.showDataButton = new System.Windows.Forms.Button();
            this.createCourtOrderButton = new System.Windows.Forms.Button();
            this.chooseATemplateOrederButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1200, 613);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 632);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(163, 48);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Открыть файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // extractDataButton
            // 
            this.extractDataButton.Enabled = false;
            this.extractDataButton.Location = new System.Drawing.Point(181, 632);
            this.extractDataButton.Name = "extractDataButton";
            this.extractDataButton.Size = new System.Drawing.Size(163, 48);
            this.extractDataButton.TabIndex = 2;
            this.extractDataButton.Text = "Извлечь данные";
            this.extractDataButton.UseVisualStyleBackColor = true;
            this.extractDataButton.Click += new System.EventHandler(this.extractDataButton_Click);
            // 
            // showDataButton
            // 
            this.showDataButton.Enabled = false;
            this.showDataButton.Location = new System.Drawing.Point(1049, 632);
            this.showDataButton.Name = "showDataButton";
            this.showDataButton.Size = new System.Drawing.Size(163, 48);
            this.showDataButton.TabIndex = 3;
            this.showDataButton.Text = "Просмотреть данные";
            this.showDataButton.UseVisualStyleBackColor = true;
            this.showDataButton.Click += new System.EventHandler(this.showDataButton_Click);
            // 
            // createCourtOrderButton
            // 
            this.createCourtOrderButton.Enabled = false;
            this.createCourtOrderButton.Location = new System.Drawing.Point(350, 632);
            this.createCourtOrderButton.Name = "createCourtOrderButton";
            this.createCourtOrderButton.Size = new System.Drawing.Size(163, 48);
            this.createCourtOrderButton.TabIndex = 4;
            this.createCourtOrderButton.Text = "Создать \r\nсудебный приказ";
            this.createCourtOrderButton.UseVisualStyleBackColor = true;
            this.createCourtOrderButton.Click += new System.EventHandler(this.createCourtOrderButton_Click);
            // 
            // chooseATemplateOrederButton
            // 
            this.chooseATemplateOrederButton.Location = new System.Drawing.Point(880, 632);
            this.chooseATemplateOrederButton.Name = "chooseATemplateOrederButton";
            this.chooseATemplateOrederButton.Size = new System.Drawing.Size(163, 48);
            this.chooseATemplateOrederButton.TabIndex = 5;
            this.chooseATemplateOrederButton.Text = "Выбрать шаблон приказа";
            this.chooseATemplateOrederButton.UseVisualStyleBackColor = true;
            this.chooseATemplateOrederButton.Click += new System.EventHandler(this.chooseATemplateOrederButton_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1224, 692);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.chooseATemplateOrederButton);
            this.Controls.Add(this.createCourtOrderButton);
            this.Controls.Add(this.showDataButton);
            this.Controls.Add(this.extractDataButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button extractDataButton;
        private System.Windows.Forms.Button showDataButton;
        private System.Windows.Forms.Button createCourtOrderButton;
        private System.Windows.Forms.Button chooseATemplateOrederButton;
        private System.Windows.Forms.Button saveButton;
    }
}

