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
    public partial class MasDatosTotalesForm : Form
    {
        public MasDatosTotalesForm()
        {
            InitializeComponent();
        }
        #region Inicializaciones en todas las Forms
        private int iMachineNumber = 1;
        public CZKEM dispositivo = new CZKEM();
        string IP;
        int puerto;
        public string CambiarIP
        {
            set { IP = value; }
        }
        public int CambiarPuerto
        {
            set { puerto = value; }
        }
        #endregion

        private void MasDatosTotalesForm_Load(object sender, EventArgs e)//Capacidades de usuarios, huellas, asistencias, Caras
        {
            bool conexion = dispositivo.Connect_Net(IP, puerto); //PONER EN TODAS LAS FORMS
            int tipoStatus; 
            string statusAMostrar = "";
            int valor = 0;
            for (tipoStatus = 7; tipoStatus <= 12; tipoStatus++)
            {
                switch (tipoStatus)
                {
                    case 7: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtCapacidadHuellas.Text = statusAMostrar; break;
                    case 8: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtCapacidadUsuarios.Text = statusAMostrar; break;
                    case 9: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtCapacidadAsistencias.Text = statusAMostrar; break;
                    case 10: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtHuellasRestantes.Text = statusAMostrar; break;
                    case 11: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtUsuariosRestantes.Text = statusAMostrar; break;
                    case 12: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtAsistenciasRestantes.Text = statusAMostrar; break;
                }
            }
            if (dispositivo.GetDeviceStatus(iMachineNumber, 22, ref valor))
            {
                txtCapacidadCaras.Text = valor.ToString();
            }
        }
        #region Funciones de apoyo
        private int obtenerStatus(int tipoStatus)//Funcion para obtener diferentes status en la region "datos totales de equipo"
        {
            int status = 0;
            if (dispositivo.GetDeviceStatus(iMachineNumber, tipoStatus, ref status))
            {
                return status;
            }
            return status;
        }
        #endregion
    }
}
