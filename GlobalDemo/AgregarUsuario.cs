using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;

namespace GlobalDemo
{
    public partial class AgregarUsuario : Form
    {
        public AgregarUsuario()
        {
            InitializeComponent();
            //wForms de huella escondidas por default
            lblFPIndex.Hide();
            cmbFPIndex.Hide();
            btnIniciarEnrolamiento.Hide();
            cBox1Huella.Hide();
            cBox2Huella.Hide();
            cBox3Huella.Hide();
            lblHuellaEnrolada.Hide();
            cmbFPIndex.SelectedIndex = 0;
        }

        #region Inicializaciones
        private int iMachineNumber = 1;
        public CZKEM dispositivo = new CZKEM();
        string IP;
        int puerto;
        int error = 5;
        private void AgregarUsuario_Load(object sender, EventArgs e)
        {
            bool conexion = dispositivo.Connect_Net(IP, puerto); //PONER EN TODAS LAS FORMS
            if (dispositivo.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all) --> REGISTRO DE EVENTOS
            {
                this.dispositivo.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(dispositivo_OnHIDNum); //Activamos el evento de registrar dedo
            }
        }
        private void AgregarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.dispositivo.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(dispositivo_OnHIDNum);//Para cuando la Form cierre, no se quede activado el RTE y no se ejecute doble al volverla a abrir
        }

        #region Errores
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void errorList(CZKEM device, int error)
        {
            device.GetLastError(ref error);

            switch (error)
            {
                case -100:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rLa operacion falló o los datos no existen", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -10:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rLa longitud de datos transmitidos es incorrecta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -8:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rNO HAY CONNEXION.\n\n-Confirmar que el cable no esta roto\n-Verificar que la IP del dispositivo es correcta\n-Verificar que se esta usando el Puerto 4370 (A menos que este haya sido cambiado)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -5:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rLos datos ya existian anteriormente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -4:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rEl espacio no es suficiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -3:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rError de tamaño", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rError en file read/write\n\n-Intenta repetir la operación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -1:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rEl SDK no se inicializó y necesita ser reconectado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 0:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rLos datos no se encontraron o estan repetidos.\n\n-Escribe los campos requeridos de forma completa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case 1:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rLa operación es incorrecta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 4:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rEl parámetro es incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 101:
                    MessageBox.Show("ERROR= " + error.ToString() + ".\n\rError en alojar el buffer", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("ERROR DESCONOCIDO= " + error.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            return;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        public string CambiarIP
        {
            set { IP = value; }
        }
        public int CambiarPuerto
        {
            set { puerto = value; }
        }
        #endregion

        string userID;
        int huellaEnrolada = 0; //Checa el estado de la huella

        private void btnRegistrarHuella_Click(object sender, EventArgs e) //Activa el registro de la huella
        {
            if(txtID.Text.Length != 0 && txtID.Text.Trim().Length!=0)
            {
                if (MessageBox.Show("¿Los datos generales del Usuario estan correctos?", "Confirmar Datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Mostrar wForms de huella
                    lblFPIndex.Show();
                    cmbFPIndex.Show();
                    btnIniciarEnrolamiento.Show();
                    btnRegistrarHuella.Hide();
                    cBox1Huella.Show();
                    cBox2Huella.Show();
                    cBox3Huella.Show();
                    lblHuellaEnrolada.Show();
                    txtID.ReadOnly = true;

                    //Deshabilitar llenado de datos
                    txtID.Enabled = false;
                    txtNombre.Enabled = false;
                    txtPassword.Enabled = false;
                    cmbTipoDeUsuario.Enabled = false;
                    cBoxHabilitado.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Favor de llenar el campo de ID para registrar una huella");
            }
        }

        #region Enrolar Huella
        private void btnIniciarEnrolamiento_Click(object sender, EventArgs e)
        {
            //Variables para metodo de enrolamiento
            int verifyID = Convert.ToInt32(txtID.Text);
            userID = txtID.Text;
            int huellaValida = 1;//0=huella invalida, 1=valida,2=Modificada (falsa)
            int numeroHuella = Convert.ToInt32(cmbFPIndex.Text); //Podemos poner hasta 10 huellas (nuestros 10 dedos)
            btnIniciarEnrolamiento.Enabled = false;
            dispositivo.CancelOperation();
            dispositivo.SSR_DelUserTmpExt(iMachineNumber, userID, numeroHuella); //Si la huella existe anteriormente, se sobre-escribira
            if (dispositivo.StartEnrollEx(userID, numeroHuella, huellaValida))
            {
                    if (dispositivo.RegEvent(iMachineNumber, 65535))// REGISTRO DE EVENTOS
                    {
                        this.dispositivo.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(dispositivo_OnFingerFeature); //Activamos el evento de registrar dedo
                    }
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
            }
            MessageBox.Show("Iniciando enrolamiento de huella, favor de hacerlo en el dispositivo","Iniciando Proceso",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
        }
        #endregion

        #region Real Time Events
        void dispositivo_OnFingerFeature(int iScore)//Al enrolar nuevo usuario, regresa la calidad de la huella enrolada
        {
            
            if (iScore > 0)
            {
                huellaEnrolada++;
                lblHuellaEnrolada.Text = "Huellas detectadas: " + huellaEnrolada;
            }
            else
            {
                MessageBox.Show("Una de las Huellas no se registro con buena calidad, favor de repetir el proceso");
                huellaEnrolada = 0;
                btnIniciarEnrolamiento.Enabled = true;
                lblHuellaEnrolada.Text = "Huellas detectadas: " + huellaEnrolada;
                cBox1Huella.Checked = false;
                cBox2Huella.Checked = false;
                cBox3Huella.Checked = false;
                this.dispositivo.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(dispositivo_OnFingerFeature); //Si no desactivamos el evento, se repite el doble de veces la proxima vez que lo activemos
                return; //Si no ponemos esto se sigue al switch
            }
            switch (huellaEnrolada)
            {
                case 1: cBox1Huella.Checked = true; break;
                case 2: cBox2Huella.Checked = true; break;
                case 3: 
                    cBox3Huella.Checked = true;
                    if (MessageBox.Show("¿Desea registrar otra huella?", "Confirmar Datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        btnIniciarEnrolamiento.Enabled = true;
                        huellaEnrolada = 0;
                        lblHuellaEnrolada.Text = "Huellas detectadas: " + huellaEnrolada;
                        cBox1Huella.Checked = false;
                        cBox2Huella.Checked = false;
                        cBox3Huella.Checked = false;
                        this.dispositivo.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(dispositivo_OnFingerFeature); //Si no desactivamos el evento, se repite el doble de veces la proxima vez que lo activemos
                    }
                    else
                    {
                        //Salir de la funcion
                    }
                    break;
            }
        }

        void dispositivo_OnHIDNum(int iCardNumber)
        {
            string eventoTarjeta = "";
            if (dispositivo.GetHIDEventCardNumAsStr(out eventoTarjeta))
            {
                txtTarjeta.Text = eventoTarjeta.ToString();
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
            }
        }

        #endregion

        #region Dar de alta el usuario
        private void btnAddUsuario_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            userID = txtID.Text;

            string nombreUsuario = txtNombre.Text, Password = txtPassword.Text;
            int tipoDeUsuario = tipoDeUser(cmbTipoDeUsuario.Text); //Administrador = 3, Usuario = 0
            bool usuarioHabilitado = cBoxHabilitado.Checked; //Si el usuario esta habilitado manda true

            dispositivo.EnableDevice(iMachineNumber, false);

            if (txtID.Text.Length != 0 && txtID.Text.Trim().Length != 0)//Si el textbox no esta vacio o no tiene espacios en blanco
            dispositivo.SetStrCardNumber(txtTarjeta.Text);//Subir el numero de tarjeta

            if (dispositivo.SSR_SetUserInfo(iMachineNumber, userID, nombreUsuario, Password, tipoDeUsuario, usuarioHabilitado))//Agregar los datos de usuario
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Usuario Agregado de forma exitosa", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
                Cursor = Cursors.Default;
            }

            dispositivo.EnableDevice(iMachineNumber, true);
        }

        private int tipoDeUser(string usuario)
        {
            int tipo = 0;
            switch (usuario)
            {
                case "Super Admin": tipo = 3; break;
                case "Usuario": tipo = 0; break;
                case "Administrador": tipo = 1; break;
            }
            return tipo;
        }
        #endregion
    }
}
