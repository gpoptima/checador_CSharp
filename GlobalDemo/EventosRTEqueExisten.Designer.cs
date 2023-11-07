namespace GlobalDemo
{
    partial class EventosRTEqueExisten
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "OnFinger: Cuando pones el dedo en la huella";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(457, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "OnVerify: Al poner la huella o tarjeta, manda el enrolement si el usuario ya exis" +
    "te o -1 si no existe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(468, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "OnAttTransactionEx: Al pasar verificacion, el sistema puede mostrar todos los dat" +
    "os en tiempo real";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(513, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "OnFingerFeature: Al enrolar las 3 huellas al hacer un nuevo usuario, regresa la c" +
    "alidad de la huella enrolada";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(663, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "OnEnrollFingerEx: Cuando terminas de enrolar la huella, este evento se activa, ma" +
    "ndando # de huellas y el tamaño de template de la misma";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(296, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "OnDeleteTemplate: Evento que se activa al borrar una huella";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(390, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "OnNewUser: Evento que se activa el terminar de enrolar un usuario exitosamente";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(421, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "OnHIDNum: Cuando pasas una tarjeta por el equipo, este evento te da el # de la mi" +
    "sma";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(389, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "OnAlarm: Se activa este evento cuando se encienden diferentes tipos de alarma ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(331, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "OnDoor: Evento que se activa cuando el dispositivo abre una puerta";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(498, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "OnWriteCard: Se activa al borrar los datos de huella de la tarjeta Mifare (Necesa" +
    "rio configurar parámetro)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 288);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(552, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "OnEmptyCard: Se activa al escribir datos de huella en la tarjeta mifare (Necesari" +
    "o configurar parámetro en el equipo)";
            // 
            // EventosRTEqueExisten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 330);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EventosRTEqueExisten";
            this.Text = "EventosRTEqueExisten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}