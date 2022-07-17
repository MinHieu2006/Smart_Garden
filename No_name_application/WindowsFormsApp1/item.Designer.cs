
namespace WindowsFormsApp1
{
    partial class item
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(item));
            //this.them = new Bunifu.Framework.UI.BunifuThinButton2();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            this.SuspendLayout();

            //them


            //this.them.ActiveBorderThickness = 1;
            //this.them.ActiveCornerRadius = 20;
            //this.them.ActiveFillColor = System.Drawing.Color.SeaGreen;
            //this.them.ActiveForecolor = System.Drawing.Color.White;
            //this.them.ActiveLineColor = System.Drawing.Color.SeaGreen;
            //this.them.BackColor = System.Drawing.SystemColors.Control;
            //this.them.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("them.BackgroundImage")));
            //this.them.ButtonText = "OK";
            //this.them.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.them.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.them.ForeColor = System.Drawing.Color.SeaGreen;
            //this.them.IdleBorderThickness = 1;
            //this.them.IdleCornerRadius = 20;
            //this.them.IdleFillColor = System.Drawing.Color.White;
            //this.them.IdleForecolor = System.Drawing.Color.SeaGreen;
            //this.them.IdleLineColor = System.Drawing.Color.SeaGreen;
            //this.them.Location = new System.Drawing.Point(139, 107);
            //this.them.Margin = new System.Windows.Forms.Padding(5);
            //this.them.Name = "them";
            //this.them.Size = new System.Drawing.Size(138, 49);
            //this.them.TabIndex = 5;
            //this.them.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //this.them.Click += new System.EventHandler(this.them_Click);
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2021, 4, 10, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(139, 44);
            this.timeEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Size = new System.Drawing.Size(176, 24);
            this.timeEdit1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Thêm thởi gian";
            // 
            // item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeEdit1);
            //\\this.Controls.Add(this.them);
            this.Name = "item";
            this.Text = "item";
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        //private Bunifu.Framework.UI.BunifuThinButton2 them;
        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private System.Windows.Forms.Label label1;
    }
}