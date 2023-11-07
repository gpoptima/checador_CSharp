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
    public partial class BorrarDatosDeUsuario : Form
    {
        public BorrarDatosDeUsuario()
        {
            InitializeComponent();
        }

        #region Inicializaciones en todas las Forms
        private int iMachineNumber = 1;
        public CZKEM dispositivo = new CZKEM();
        string IP;
        int puerto;
        int error = 5;
        private void BorrarDatosDeUsuario_Load(object sender, EventArgs e)
        {
            bool conexion = dispositivo.Connect_Net(IP, puerto); //PONER EN TODAS LAS FORMS
            lblID.Text = "ID: " + IDUsuario;
            lblNombre.Text = "Nombre: " + nombreUsuario;
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

        #region Parametros Necesarios

        string IDUsuario,nombreUsuario;
        int tipoBorrado;
        public string IDaBorrar
        {
            set { IDUsuario = value; }
        }

        public string NombreABorrar
        {
            set { nombreUsuario = value; }
        }

        #endregion

        private void btnBorrar1Huella_Click(object sender, EventArgs e) //Se puede usar SSR_DelUserTmpExt y DelUserTmp, esta ultima soporta borrar hasta 24 bits User ID
        {
            tipoBorrado = Convert.ToInt32(cmbFPIndex.Text);
            borrado("La huella " + cmbFPIndex.Text, tipoBorrado);
        }

        private void btnBorrarPassword_Click(object sender, EventArgs e)
        {
            borrado("El password del usuario " + nombreUsuario, 10); 
        }

        private void btnBorrarUsuario_Click(object sender, EventArgs e)
        {
            borrado("El usuario " + nombreUsuario, 12);
        }

        private void btnBorrarHuellas_Click(object sender, EventArgs e)
        {
            borrado("Todas las huellas ", 11);
        }

        private void borrado(string mensaje, int tipoBorrado)
        {
            
            Cursor = Cursors.WaitCursor;
            if (dispositivo.SSR_DeleteEnrollData(iMachineNumber, IDUsuario, tipoBorrado))
            {
                dispositivo.RefreshData(iMachineNumber);//the data in the device should be refreshed
                if(tipoBorrado == 11)
                    MessageBox.Show(mensaje + " se han borrado", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(mensaje + " se ha borrado", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
            }
            Cursor = Cursors.Default;
        }

        private void BorrarDatosDeUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
