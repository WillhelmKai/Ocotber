namespace WindowsFormsApp6
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            CCWin.SkinControl.SkinRollingBarThemeBase skinRollingBarThemeBase1 = new CCWin.SkinControl.SkinRollingBarThemeBase();
            CCWin.SkinControl.SkinRollingBarThemeBase skinRollingBarThemeBase2 = new CCWin.SkinControl.SkinRollingBarThemeBase();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.skinLine1 = new CCWin.SkinControl.SkinLine();
            this.skinLine2 = new CCWin.SkinControl.SkinLine();
            this.skinRollingBar1 = new CCWin.SkinControl.SkinRollingBar();
            this.skinRollingBar2 = new CCWin.SkinControl.SkinRollingBar();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(84, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 76);
            this.label2.TabIndex = 3;
            this.label2.Text = "Teacher Assistance \r\n           System";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = "C:\\Users\\Stonylix\\Desktop\\大三（下）\\Computer_Graphics\\IrisSkin2sskpages\\皮肤控件\\皮肤\\MacOS" +
    "\\MacOS.ssk";
            this.skinEngine1.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine1.SkinStreamMain")));
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(200, 296);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(219, 34);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // skinLine1
            // 
            this.skinLine1.BackColor = System.Drawing.Color.Transparent;
            this.skinLine1.LineColor = System.Drawing.Color.Black;
            this.skinLine1.LineHeight = 1;
            this.skinLine1.Location = new System.Drawing.Point(44, 192);
            this.skinLine1.Name = "skinLine1";
            this.skinLine1.Size = new System.Drawing.Size(421, 24);
            this.skinLine1.TabIndex = 8;
            this.skinLine1.Text = "skinLine1";
            // 
            // skinLine2
            // 
            this.skinLine2.BackColor = System.Drawing.Color.Transparent;
            this.skinLine2.LineColor = System.Drawing.Color.Black;
            this.skinLine2.LineHeight = 1;
            this.skinLine2.Location = new System.Drawing.Point(44, 65);
            this.skinLine2.Name = "skinLine2";
            this.skinLine2.Size = new System.Drawing.Size(421, 24);
            this.skinLine2.TabIndex = 9;
            this.skinLine2.Text = "skinLine2";
            // 
            // skinRollingBar1
            // 
            this.skinRollingBar1.Location = new System.Drawing.Point(-10, 103);
            this.skinRollingBar1.Name = "skinRollingBar1";
            this.skinRollingBar1.Radius2 = 24;
            this.skinRollingBar1.Size = new System.Drawing.Size(75, 23);
            this.skinRollingBar1.TabIndex = 10;
            this.skinRollingBar1.TabStop = false;
            skinRollingBarThemeBase1.BackColor = System.Drawing.Color.Transparent;
            skinRollingBarThemeBase1.BaseColor = System.Drawing.Color.Red;
            skinRollingBarThemeBase1.DiamondColor = System.Drawing.Color.White;
            skinRollingBarThemeBase1.PenWidth = 2F;
            skinRollingBarThemeBase1.Radius1 = 10;
            skinRollingBarThemeBase1.Radius2 = 24;
            skinRollingBarThemeBase1.SpokeNum = 12;
            this.skinRollingBar1.XTheme = skinRollingBarThemeBase1;
            // 
            // skinRollingBar2
            // 
            this.skinRollingBar2.Location = new System.Drawing.Point(458, 103);
            this.skinRollingBar2.Name = "skinRollingBar2";
            this.skinRollingBar2.Radius2 = 24;
            this.skinRollingBar2.Size = new System.Drawing.Size(75, 23);
            this.skinRollingBar2.TabIndex = 11;
            this.skinRollingBar2.TabStop = false;
            skinRollingBarThemeBase2.BackColor = System.Drawing.Color.Transparent;
            skinRollingBarThemeBase2.BaseColor = System.Drawing.Color.Red;
            skinRollingBarThemeBase2.DiamondColor = System.Drawing.Color.White;
            skinRollingBarThemeBase2.PenWidth = 2F;
            skinRollingBarThemeBase2.Radius1 = 10;
            skinRollingBarThemeBase2.Radius2 = 24;
            skinRollingBarThemeBase2.SpokeNum = 12;
            this.skinRollingBar2.XTheme = skinRollingBarThemeBase2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(185, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 57);
            this.button1.TabIndex = 12;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.skinRollingBar2);
            this.Controls.Add(this.skinRollingBar1);
            this.Controls.Add(this.skinLine2);
            this.Controls.Add(this.skinLine1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.TextBox textBox1;
        private CCWin.SkinControl.SkinLine skinLine1;
        private CCWin.SkinControl.SkinLine skinLine2;
        private CCWin.SkinControl.SkinRollingBar skinRollingBar1;
        private CCWin.SkinControl.SkinRollingBar skinRollingBar2;
        private System.Windows.Forms.Button button1;
    }
}

