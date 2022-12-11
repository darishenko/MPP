namespace TestsGeneratorApplication
{
    partial class AppForm
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbMaxRead = new System.Windows.Forms.TextBox();
            this.tbMaxProcess = new System.Windows.Forms.TextBox();
            this.tbMaxWrite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChooseFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(37, 296);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(185, 51);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Начать генерацию";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tbMaxRead
            // 
            this.tbMaxRead.Location = new System.Drawing.Point(570, 39);
            this.tbMaxRead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxRead.Name = "tbMaxRead";
            this.tbMaxRead.Size = new System.Drawing.Size(112, 26);
            this.tbMaxRead.TabIndex = 1;
            this.tbMaxRead.TextChanged += new System.EventHandler(this.NumbersOnly_TextChanged);
            this.tbMaxRead.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumbersOnly_KeyPress);
            // 
            // tbMaxProcess
            // 
            this.tbMaxProcess.Location = new System.Drawing.Point(570, 108);
            this.tbMaxProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxProcess.Name = "tbMaxProcess";
            this.tbMaxProcess.Size = new System.Drawing.Size(112, 26);
            this.tbMaxProcess.TabIndex = 2;
            this.tbMaxProcess.TextChanged += new System.EventHandler(this.NumbersOnly_TextChanged);
            this.tbMaxProcess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumbersOnly_KeyPress);
            // 
            // tbMaxWrite
            // 
            this.tbMaxWrite.Location = new System.Drawing.Point(570, 171);
            this.tbMaxWrite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxWrite.Name = "tbMaxWrite";
            this.tbMaxWrite.Size = new System.Drawing.Size(112, 26);
            this.tbMaxWrite.TabIndex = 3;
            this.tbMaxWrite.TextChanged += new System.EventHandler(this.NumbersOnly_TextChanged);
            this.tbMaxWrite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumbersOnly_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество файлов, загружаемых за раз: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(519, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Максимальное количество одновременно обрабатываемых задач:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(399, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Количество одновременно записываемых файлов:";
            // 
            // btnChooseFiles
            // 
            this.btnChooseFiles.Location = new System.Drawing.Point(37, 221);
            this.btnChooseFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnChooseFiles.Name = "btnChooseFiles";
            this.btnChooseFiles.Size = new System.Drawing.Size(185, 51);
            this.btnChooseFiles.TabIndex = 4;
            this.btnChooseFiles.Text = "Выбрать файл";
            this.btnChooseFiles.UseVisualStyleBackColor = true;
            this.btnChooseFiles.Click += new System.EventHandler(this.btnChooseFiles_Click);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 379);
            this.Controls.Add(this.btnChooseFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMaxWrite);
            this.Controls.Add(this.tbMaxProcess);
            this.Controls.Add(this.tbMaxRead);
            this.Controls.Add(this.btnGenerate);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AppForm";
            this.Text = "Tests Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox tbMaxRead;
        private System.Windows.Forms.TextBox tbMaxProcess;
        private System.Windows.Forms.TextBox tbMaxWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChooseFiles;
    }
}

