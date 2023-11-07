namespace GlobalDemo
{
    partial class ModificarUsuario
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbTipoDeUsuario = new System.Windows.Forms.ComboBox();
            this.cBoxHabilitado = new System.Windows.Forms.CheckBox();
            this.lblTituloUsuario = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblTipoDeUsuario = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblHuellaEnrolada = new System.Windows.Forms.Label();
            this.cBox3Huella = new System.Windows.Forms.CheckBox();
            this.cBox2Huella = new System.Windows.Forms.CheckBox();
            this.cBox1Huella = new System.Windows.Forms.CheckBox();
            this.btnIniciarEnrolamientoModif = new System.Windows.Forms.Button();
            this.btnModificarHuella = new System.Windows.Forms.Button();
            this.cmbFPIndex = new System.Windows.Forms.ComboBox();
            this.lblFPIndex = new System.Windows.Forms.Label();
            this.btnModifUsuario = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmbTipoDeUsuario);
            this.panel1.Controls.Add(this.cBoxHabilitado);
            this.panel1.Controls.Add(this.lblTituloUsuario);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Controls.Add(this.lblTipoDeUsuario);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Location = new System.Drawing.Point(23, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 216);
            this.panel1.TabIndex = 0;
            // 
            // cmbTipoDeUsuario
            // 
            this.cmbTipoDeUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoDeUsuario.FormattingEnabled = true;
            this.cmbTipoDeUsuario.Items.AddRange(new object[] {
            "Super Admin",
            "Administrador",
            "Usuario"});
            this.cmbTipoDeUsuario.Location = new System.Drawing.Point(73, 169);
            this.cmbTipoDeUsuario.Name = "cmbTipoDeUsuario";
            this.cmbTipoDeUsuario.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoDeUsuario.TabIndex = 3;
            // 
            // cBoxHabilitado
            // 
            this.cBoxHabilitado.AutoSize = true;
            this.cBoxHabilitado.Checked = true;
            this.cBoxHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxHabilitado.Location = new System.Drawing.Point(286, 159);
            this.cBoxHabilitado.Name = "cBoxHabilitado";
            this.cBoxHabilitado.Size = new System.Drawing.Size(118, 17);
            this.cBoxHabilitado.TabIndex = 4;
            this.cBoxHabilitado.Text = "Usuario Habilitado?";
            this.cBoxHabilitado.UseVisualStyleBackColor = true;
            // 
            // lblTituloUsuario
            // 
            this.lblTituloUsuario.AutoSize = true;
            this.lblTituloUsuario.Location = new System.Drawing.Point(175, 12);
            this.lblTituloUsuario.Name = "lblTituloUsuario";
            this.lblTituloUsuario.Size = new System.Drawing.Size(118, 13);
            this.lblTituloUsuario.TabIndex = 12;
            this.lblTituloUsuario.Text = "MODIFICAR USUARIO";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(184, 85);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(354, 85);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(37, 50);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // lblTipoDeUsuario
            // 
            this.lblTipoDeUsuario.AutoSize = true;
            this.lblTipoDeUsuario.Location = new System.Drawing.Point(87, 137);
            this.lblTipoDeUsuario.Name = "lblTipoDeUsuario";
            this.lblTipoDeUsuario.Size = new System.Drawing.Size(82, 13);
            this.lblTipoDeUsuario.TabIndex = 3;
            this.lblTipoDeUsuario.Text = "Tipo de Usuario";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(212, 50);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(10, 85);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(379, 50);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblHuellaEnrolada);
            this.groupBox2.Controls.Add(this.cBox3Huella);
            this.groupBox2.Controls.Add(this.cBox2Huella);
            this.groupBox2.Controls.Add(this.cBox1Huella);
            this.groupBox2.Controls.Add(this.btnIniciarEnrolamientoModif);
            this.groupBox2.Controls.Add(this.btnModificarHuella);
            this.groupBox2.Controls.Add(this.cmbFPIndex);
            this.groupBox2.Controls.Add(this.lblFPIndex);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(23, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modificar Huella (opcional)";
            // 
            // lblHuellaEnrolada
            // 
            this.lblHuellaEnrolada.AutoSize = true;
            this.lblHuellaEnrolada.Location = new System.Drawing.Point(218, 75);
            this.lblHuellaEnrolada.Name = "lblHuellaEnrolada";
            this.lblHuellaEnrolada.Size = new System.Drawing.Size(104, 13);
            this.lblHuellaEnrolada.TabIndex = 21;
            this.lblHuellaEnrolada.Text = "Huellas detectadas: ";
            // 
            // cBox3Huella
            // 
            this.cBox3Huella.AutoSize = true;
            this.cBox3Huella.Location = new System.Drawing.Point(353, 68);
            this.cBox3Huella.Name = "cBox3Huella";
            this.cBox3Huella.Size = new System.Drawing.Size(90, 17);
            this.cBox3Huella.TabIndex = 20;
            this.cBox3Huella.Text = "Tercer Huella";
            this.cBox3Huella.UseVisualStyleBackColor = true;
            // 
            // cBox2Huella
            // 
            this.cBox2Huella.AutoSize = true;
            this.cBox2Huella.Location = new System.Drawing.Point(353, 45);
            this.cBox2Huella.Name = "cBox2Huella";
            this.cBox2Huella.Size = new System.Drawing.Size(102, 17);
            this.cBox2Huella.TabIndex = 19;
            this.cBox2Huella.Text = "Segunda Huella";
            this.cBox2Huella.UseVisualStyleBackColor = true;
            // 
            // cBox1Huella
            // 
            this.cBox1Huella.AutoSize = true;
            this.cBox1Huella.Location = new System.Drawing.Point(353, 19);
            this.cBox1Huella.Name = "cBox1Huella";
            this.cBox1Huella.Size = new System.Drawing.Size(88, 17);
            this.cBox1Huella.TabIndex = 18;
            this.cBox1Huella.Text = "Primer Huella";
            this.cBox1Huella.UseVisualStyleBackColor = true;
            // 
            // btnIniciarEnrolamientoModif
            // 
            this.btnIniciarEnrolamientoModif.Location = new System.Drawing.Point(214, 41);
            this.btnIniciarEnrolamientoModif.Name = "btnIniciarEnrolamientoModif";
            this.btnIniciarEnrolamientoModif.Size = new System.Drawing.Size(124, 23);
            this.btnIniciarEnrolamientoModif.TabIndex = 2;
            this.btnIniciarEnrolamientoModif.Text = "Iniciar Enrolamiento";
            this.btnIniciarEnrolamientoModif.UseVisualStyleBackColor = true;
            this.btnIniciarEnrolamientoModif.Click += new System.EventHandler(this.btnIniciarEnrolamientoModif_Click);
            // 
            // btnModificarHuella
            // 
            this.btnModificarHuella.Location = new System.Drawing.Point(105, 33);
            this.btnModificarHuella.Name = "btnModificarHuella";
            this.btnModificarHuella.Size = new System.Drawing.Size(199, 39);
            this.btnModificarHuella.TabIndex = 0;
            this.btnModificarHuella.Text = "Modificar Huellas";
            this.btnModificarHuella.UseVisualStyleBackColor = true;
            this.btnModificarHuella.Click += new System.EventHandler(this.btnModificarHuella_Click);
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
            this.cmbFPIndex.Location = new System.Drawing.Point(62, 58);
            this.cmbFPIndex.Name = "cmbFPIndex";
            this.cmbFPIndex.Size = new System.Drawing.Size(37, 21);
            this.cmbFPIndex.Sorted = true;
            this.cmbFPIndex.TabIndex = 1;
            // 
            // lblFPIndex
            // 
            this.lblFPIndex.AutoSize = true;
            this.lblFPIndex.Location = new System.Drawing.Point(28, 30);
            this.lblFPIndex.Name = "lblFPIndex";
            this.lblFPIndex.Size = new System.Drawing.Size(127, 13);
            this.lblFPIndex.TabIndex = 5;
            this.lblFPIndex.Text = "Numero de Huella (Index)";
            // 
            // btnModifUsuario
            // 
            this.btnModifUsuario.Location = new System.Drawing.Point(195, 492);
            this.btnModifUsuario.Name = "btnModifUsuario";
            this.btnModifUsuario.Size = new System.Drawing.Size(127, 23);
            this.btnModifUsuario.TabIndex = 2;
            this.btnModifUsuario.Text = "MODIFICAR USUARIO";
            this.btnModifUsuario.UseVisualStyleBackColor = true;
            this.btnModifUsuario.Click += new System.EventHandler(this.btnModifUsuario_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTarjeta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(23, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 97);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Tarjeta (Opcional)";
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Location = new System.Drawing.Point(151, 53);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(154, 20);
            this.txtTarjeta.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de tarjeta:";
            // 
            // ModificarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 540);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModifUsuario);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ModificarUsuario";
            this.Text = "Modificar Usuario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModificarUsuario_FormClosing);
            this.Load += new System.EventHandler(this.ModificarUsuario_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbTipoDeUsuario;
        private System.Windows.Forms.CheckBox cBoxHabilitado;
        private System.Windows.Forms.Label lblTituloUsuario;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblTipoDeUsuario;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblHuellaEnrolada;
        private System.Windows.Forms.CheckBox cBox3Huella;
        private System.Windows.Forms.CheckBox cBox2Huella;
        private System.Windows.Forms.CheckBox cBox1Huella;
        private System.Windows.Forms.Button btnIniciarEnrolamientoModif;
        private System.Windows.Forms.Button btnModificarHuella;
        private System.Windows.Forms.ComboBox cmbFPIndex;
        private System.Windows.Forms.Label lblFPIndex;
        private System.Windows.Forms.Button btnModifUsuario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.Label label1;
    }
}