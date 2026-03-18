using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelColor;
        private Label lblColor;
        private Button btnSelectColor;
        private Label lblSpeed;
        private TrackBar trackBarSpeed;
        private Label lblSpeedValue;
        private Button btnPreview;
        private Panel panelPreview;
        private Button btnOK;
        private Button btnCancel;

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
            this.panelColor = new System.Windows.Forms.Panel();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnSelectColor = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.lblSpeedValue = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.SuspendLayout();

            // panelColor
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(20, 30);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(50, 50);
            this.panelColor.TabIndex = 0;

            // lblColor
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(80, 30);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(100, 13);
            this.lblColor.TabIndex = 1;
            this.lblColor.Text = "Текущий цвет: Blue";

            // btnSelectColor
            this.btnSelectColor.Location = new System.Drawing.Point(80, 50);
            this.btnSelectColor.Name = "btnSelectColor";
            this.btnSelectColor.Size = new System.Drawing.Size(120, 30);
            this.btnSelectColor.TabIndex = 2;
            this.btnSelectColor.Text = "Выбрать цвет";
            this.btnSelectColor.UseVisualStyleBackColor = true;
            this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);

            // lblSpeed
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(20, 100);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(98, 13);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "Скорость (мс/кадр):";

            // trackBarSpeed
            this.trackBarSpeed.Location = new System.Drawing.Point(20, 120);
            this.trackBarSpeed.Minimum = 1;
            this.trackBarSpeed.Maximum = 100;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(200, 45);
            this.trackBarSpeed.TabIndex = 4;
            this.trackBarSpeed.TickFrequency = 10;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);

            // lblSpeedValue
            this.lblSpeedValue.AutoSize = true;
            this.lblSpeedValue.Location = new System.Drawing.Point(230, 120);
            this.lblSpeedValue.Name = "lblSpeedValue";
            this.lblSpeedValue.Size = new System.Drawing.Size(31, 13);
            this.lblSpeedValue.TabIndex = 5;
            this.lblSpeedValue.Text = "30 мс";

            // btnPreview
            this.btnPreview.Location = new System.Drawing.Point(20, 170);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 30);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);

            // panelPreview
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Location = new System.Drawing.Point(130, 170);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(90, 30);
            this.panelPreview.TabIndex = 7;

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(80, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(170, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form2
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 280);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.lblSpeedValue);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.btnSelectColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.panelColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}