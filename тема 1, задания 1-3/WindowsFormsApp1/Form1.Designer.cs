namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.labelEpsilon = new System.Windows.Forms.Label();
            this.textBoxEpsilon = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelResultFuncTitle = new System.Windows.Forms.Label();
            this.labelResultSeriesTitle = new System.Windows.Forms.Label();
            this.labelCountTitle = new System.Windows.Forms.Label();
            this.labelErrorTitle = new System.Windows.Forms.Label();
            this.labelResultFunc = new System.Windows.Forms.Label();
            this.labelResultSeries = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.pictureBoxFormula = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormula)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(250, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(402, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Вычисление arctg(x) через ряд Тейлора";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(50, 160);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(95, 13);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "Введите x (x < -1):";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(200, 157);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(150, 20);
            this.textBoxX.TabIndex = 3;
            this.textBoxX.TextChanged += new System.EventHandler(this.textBoxX_TextChanged);
            this.textBoxX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxX_KeyPress);
            // 
            // labelEpsilon
            // 
            this.labelEpsilon.AutoSize = true;
            this.labelEpsilon.Location = new System.Drawing.Point(50, 200);
            this.labelEpsilon.Name = "labelEpsilon";
            this.labelEpsilon.Size = new System.Drawing.Size(109, 13);
            this.labelEpsilon.TabIndex = 4;
            this.labelEpsilon.Text = "Введите точность ε:";
            // 
            // textBoxEpsilon
            // 
            this.textBoxEpsilon.Location = new System.Drawing.Point(200, 197);
            this.textBoxEpsilon.Name = "textBoxEpsilon";
            this.textBoxEpsilon.Size = new System.Drawing.Size(150, 20);
            this.textBoxEpsilon.TabIndex = 5;
            this.textBoxEpsilon.TextChanged += new System.EventHandler(this.textBoxEpsilon_TextChanged);
            this.textBoxEpsilon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEpsilon_KeyPress);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Enabled = false;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalculate.Location = new System.Drawing.Point(200, 240);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(150, 35);
            this.buttonCalculate.TabIndex = 6;
            this.buttonCalculate.Text = "Вычислить";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // labelResultFuncTitle
            // 
            this.labelResultFuncTitle.AutoSize = true;
            this.labelResultFuncTitle.Location = new System.Drawing.Point(50, 295);
            this.labelResultFuncTitle.Name = "labelResultFuncTitle";
            this.labelResultFuncTitle.Size = new System.Drawing.Size(51, 13);
            this.labelResultFuncTitle.TabIndex = 7;
            this.labelResultFuncTitle.Text = "arctg(x) =";
            // 
            // labelResultSeriesTitle
            // 
            this.labelResultSeriesTitle.AutoSize = true;
            this.labelResultSeriesTitle.Location = new System.Drawing.Point(50, 325);
            this.labelResultSeriesTitle.Name = "labelResultSeriesTitle";
            this.labelResultSeriesTitle.Size = new System.Drawing.Size(77, 13);
            this.labelResultSeriesTitle.TabIndex = 9;
            this.labelResultSeriesTitle.Text = "Сумма ряда =";
            // 
            // labelCountTitle
            // 
            this.labelCountTitle.AutoSize = true;
            this.labelCountTitle.Location = new System.Drawing.Point(50, 355);
            this.labelCountTitle.Name = "labelCountTitle";
            this.labelCountTitle.Size = new System.Drawing.Size(101, 13);
            this.labelCountTitle.TabIndex = 11;
            this.labelCountTitle.Text = "Число слагаемых:";
            // 
            // labelErrorTitle
            // 
            this.labelErrorTitle.AutoSize = true;
            this.labelErrorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelErrorTitle.ForeColor = System.Drawing.Color.Blue;
            this.labelErrorTitle.Location = new System.Drawing.Point(50, 385);
            this.labelErrorTitle.Name = "labelErrorTitle";
            this.labelErrorTitle.Size = new System.Drawing.Size(90, 13);
            this.labelErrorTitle.TabIndex = 13;
            this.labelErrorTitle.Text = "Погрешность:";
            // 
            // labelResultFunc
            // 
            this.labelResultFunc.AutoSize = true;
            this.labelResultFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResultFunc.Location = new System.Drawing.Point(150, 295);
            this.labelResultFunc.Name = "labelResultFunc";
            this.labelResultFunc.Size = new System.Drawing.Size(0, 15);
            this.labelResultFunc.TabIndex = 8;
            // 
            // labelResultSeries
            // 
            this.labelResultSeries.AutoSize = true;
            this.labelResultSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResultSeries.Location = new System.Drawing.Point(150, 325);
            this.labelResultSeries.Name = "labelResultSeries";
            this.labelResultSeries.Size = new System.Drawing.Size(0, 15);
            this.labelResultSeries.TabIndex = 10;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCount.Location = new System.Drawing.Point(150, 355);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(0, 15);
            this.labelCount.TabIndex = 12;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelError.ForeColor = System.Drawing.Color.Blue;
            this.labelError.Location = new System.Drawing.Point(150, 385);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 15);
            this.labelError.TabIndex = 14;
            // 
            // pictureBoxFormula
            // 
            this.pictureBoxFormula.Image = global::WindowsFormsApp1.Properties.Resources.formula;
            this.pictureBoxFormula.Location = new System.Drawing.Point(53, 60);
            this.pictureBoxFormula.Name = "pictureBoxFormula";
            this.pictureBoxFormula.Size = new System.Drawing.Size(746, 80);
            this.pictureBoxFormula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxFormula.TabIndex = 1;
            this.pictureBoxFormula.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 450);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelErrorTitle);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelCountTitle);
            this.Controls.Add(this.labelResultSeries);
            this.Controls.Add(this.labelResultSeriesTitle);
            this.Controls.Add(this.labelResultFunc);
            this.Controls.Add(this.labelResultFuncTitle);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.textBoxEpsilon);
            this.Controls.Add(this.labelEpsilon);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.pictureBoxFormula);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вычисление arctg(x) - Вариант 14";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormula)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxFormula;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label labelEpsilon;
        private System.Windows.Forms.TextBox textBoxEpsilon;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelResultFuncTitle;
        private System.Windows.Forms.Label labelResultSeriesTitle;
        private System.Windows.Forms.Label labelCountTitle;
        private System.Windows.Forms.Label labelErrorTitle;
        private System.Windows.Forms.Label labelResultFunc;
        private System.Windows.Forms.Label labelResultSeries;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelError;
    }
}