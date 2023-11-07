namespace GlobalDemo
{
    partial class BorrarDatosDeUsuario
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFPIndex = new System.Windows.Forms.ComboBox();
            this.btnBorrar1Huella = new System.Windows.Forms.Button();
            this.btnBorrarPassword = new System.Windows.Forms.Button();
            this.btnBorrarHuellas = new System.Windows.Forms.Button();
            this.btnBorrarUsuario = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.btnBorrar1Huella);
            this.groupBox1.Controls.Add(this.cmbFPIndex);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Numero de Huella";
            // 
            // cmbFPIndex
            // 
            this.cmbFPIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFPIndex.FormattingEnabled = true;
            this.cmbFPIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cmbFPIndex.Location = new System.Drawing.Point(33, 23);
            this.cmbFPIndex.Name = "cmbFPIndex";
            this.cmbFPIndex.Size = new System.Drawing.Size(37, 21);
            this.cmbFPIndex.Sorted = true;
            this.cmbFPIndex.TabIndex = 16;
            // 
            // btnBorrar1Huella
            // 
            this.btnBorrar1Huella.Location = new System.Drawing.Point(275, 21);
            this.btnBorrar1Huella.Name = "btnBorrar1Huella";
            this.btnBorrar1Huella.Size = new System.Drawing.Size(169, 23);
            this.btnBorrar1Huella.TabIndex = 17;
            this.btnBorrar1Huella.Text = "Borrar Huella";
            this.btnBorrar1Huella.UseVisualStyleBackColor = true;
            this.btnBorrar1Huella.Click += new System.EventHandler(this.btnBorrar1Huella_Click);
            // 
            // btnBorrarPassword
            // 
            this.btnBorrarPassword.Location = new System.Drawing.Point(12, 92);
            this.btnBorrarPassword.Name = "btnBorrarPassword";
            this.btnBorrarPassword.Size = new System.Drawing.Size(147, 23);
            this.btnBorrarPassword.TabIndex = 1;
            this.btnBorrarPassword.Text = "Borrar Contraseña";
            this.btnBorrarPassword.UseVisualStyleBackColor = true;
            this.btnBorrarPassword.Click += new System.EventHandler(this.btnBorrarPassword_Click);
            // 
            // btnBorrarHuellas
            // 
            this.btnBorrarHuellas.Location = new System.Drawing.Point(345, 92);
            this.btnBorrarHuellas.Name = "btnBorrarHuellas";
            this.btnBorrarHuellas.Size = new System.Drawing.Size(147, 23);
            this.btnBorrarHuellas.TabIndex = 2;
            this.btnBorrarHuellas.Text = "Borrar Todas las Huellas";
            this.btnBorrarHuellas.UseVisualStyleBackColor = true;
            this.btnBorrarHuellas.Click += new System.EventHandler(this.btnBorrarHuellas_Click);
            // 
            // btnBorrarUsuario
            // 
            this.btnBorrarUsuario.Location = new System.Drawing.Point(178, 92);
            this.btnBorrarUsuario.Name = "btnBorrarUsuario";
            this.btnBorrarUsuario.Size = new System.Drawing.Size(147, 23);
            this.btnBorrarUsuario.TabIndex = 3;
            this.btnBorrarUsuario.Text = "Borrar Usuario";
            this.btnBorrarUsuario.UseVisualStyleBackColor = true;
            this.btnBorrarUsuario.Click += new System.EventHandler(this.btnBorrarUsuario_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(126, 13);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(24, 13);
            this.lblID.TabIndex = 18;
            this.lblID.Text = "ID: ";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(126, 30);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 13);
            this.lblNombre.TabIndex = 19;
            this.lblNombre.Text = "Nombre: ";
            // 
            // BorrarDatosDeUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 129);
            this.Controls.Add(this.btnBorrarUsuario);
            this.Controls.Add(this.btnBorrarHuellas);
            this.Controls.Add(this.btnBorrarPassword);
            this.Controls.Add(this.groupBox1);
            this.Name = "BorrarDatosDeUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BorrarDatosDeUsuario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BorrarDatosDeUsuario_FormClosing);
            this.Load += new System.EventHandler(this.BorrarDatosDeUsuario_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFPIndex;
        private System.Windows.Forms.Button btnBorrar1Huella;
        private System.Windows.Forms.Button btnBorrarPassword;
        private System.Windows.Forms.Button btnBorrarHuellas;
        private System.Windows.Forms.Button btnBorrarUsuario;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblNombre;
    }
}