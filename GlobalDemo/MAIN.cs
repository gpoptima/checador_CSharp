using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using zkemkeeper;
//No olvidar referenciar la libreria al proyecto en Reference > Add reference > Buscarla en SYSWOW64
//No olvidar compilar en x86
//button1.PerformClick();
namespace GlobalDemo
{
    public partial class formZKDemo : Form
    {
        private System.Threading.Timer timer;  // Usamos la clase Timer del espacio de nombres System.Threading

        public formZKDemo()
        {
            InitializeComponent();

            this.ActiveControl = txtIP; //Hace focus en el textBox de la IP
            txtIP.SelectionStart = txtIP.Text.Length; // Cursor al final del texto
            txtIP.SelectionLength = 0; // Cursor al final del texto
            lblEscenarioCA.Font = new Font("Arial", 24, FontStyle.Bold);

        }

        #region Inicializaciones

       public CZKEM dispositivo = new CZKEM(); // Objeto de tipo ZK, es lo primero que se crea
        public int error = 5; //Variable para obtener diferentes errores ocurridos
        private int iMachineNumber = 1; //Representara el numero de serie del equipo, es usado en muchas funciones
        private bool conexion = false; //Variable que checa el estado de la conexion
        public MySqlConnection sql = new MySqlConnection("server = 108.175.209.182 ;database = rh; Uid = jmeza; pwd = FgGLQw3WWMZM7hdn;");
    //   public MySqlConnection sql = new MySqlConnection("server = localhost; database=rh; Uid=wdylan; pwd=FgGLQw3WWMZM7hdn;");

      //  public int tiempoejecucion = 28800000;//300000;//
        #endregion
//        108.175.209.182
//rh
//checador
//yt2[1EV8Jn * m.tz9
//192.168.1.184
//4370
//emp
//si
//Grupo Optima


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
                    //MessageBox.Show("ERROR= " + error.ToString() + ".\n\rError en file read/write\n\n-Intenta repetir la operación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region Conexion
        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // Aqui el cursor se pone en modo espera cuando se esta ejecutando algun proceso
            if (btnConectar.Text == "Desconectar") //Funcion para desconectar
            {
                btnConectar.Text = "Conectar";
                lblEstadoConexion.Text = "Estado: Desconectado";
                dispositivo.Disconnect();
                ClearTextBoxes(); //Quita el texto de las textbox
              //  txtIP.Text = "10.10.10.155";
                txtPuerto.Text = "4370";        

                this.ActiveControl = txtIP; //Hace focus en el textBox de la IP
                txtIP.SelectionStart = txtIP.Text.Length; // Cursor al final del texto de IP
                txtIP.SelectionLength = 0; // Cursor al final del texto de IP

                dgvLogs.Rows.Clear(); //Borra los datos de los logs
                dgvUsuarios.Rows.Clear(); //Borra los datos de as huellas
                dgvTarjetasCaras.Rows.Clear(); //Borra datos de tarjetas y Caras

                if (btnRTE.Text == "Apagar Real Time Events")
                {
                    btnRTE.PerformClick(); //Apaga el monitore en tiempo real al desconectar el equipo
                }

                Cursor = Cursors.Default;
                return; //Si no ponemos este return, no se sale del boton y se vuelve a conectar
            }

            try
            {
                conexion = dispositivo.Connect_Net(txtIP.Text, Convert.ToInt32(txtPuerto.Text)); //Parametros: IP del dispositivo y puerto del dispositivo
            }

            catch { } //Este catch se pone para agarrar todas las excepciones y no haga crash el programa

            if (conexion == true)
            {
                btnConectar.Text = "Desconectar";
                lblEstadoConexion.Text = "Estado: Conectado";
                dispositivo.PlayVoiceByIndex(10); //Sonido, para saber que se conecto
                #region Informacion basica del equipo

                string modeloDispositivo = ""; //El nombre comercial del dispositivo
                if (dispositivo.GetProductCode(iMachineNumber, out modeloDispositivo))
                {
                    txtNombreDispositivo.Text = modeloDispositivo;
                }

                bool tipoDispositivo;//Si el dispositivo es Black & White o TFT
                string ifaceDispositivo = "";//Si el dispositivo es TFT o iFace
                tipoDispositivo = dispositivo.IsTFTMachine(iMachineNumber); //Metodo para saber si es B&W o TFT
                if (tipoDispositivo == true)
                {
                    dispositivo.GetSysOption(iMachineNumber, "FaceFunOn", out ifaceDispositivo);
                    if (ifaceDispositivo == "1")
                    {
                        txtTipoDispositivo.Text = "iFace";
                    }
                    else //Aqui ifaceDispositivo = 0
                    {
                        txtTipoDispositivo.Text = "TFT";
                    }
                }
                else
                {
                    txtTipoDispositivo.Text = "Black & White";
                }

                string tipoFirmware = ""; //Si es el nuevo o el viejo firmware
                dispositivo.GetPlatform(iMachineNumber, ref tipoFirmware);
                if (tipoFirmware.Contains("ZMM"))
                {
                    txtTipoFirmware.Text = "New Firmware";
                }
                else
                {
                    txtTipoFirmware.Text = "Old Firmware";
                }

                string versionFirmware = "";
                if (dispositivo.GetFirmwareVersion(iMachineNumber, ref versionFirmware))
                {
                    txtVersionFirmware.Text = versionFirmware;
                }

                int anio = 0, mes = 0, dia = 0, hora = 0, minuto = 0, segundo = 0; //Fecha del dispositivo en formato DD/MM/AAAA HH/MM/SS Universal
                if (dispositivo.GetDeviceTime(iMachineNumber, ref anio, ref mes, ref dia, ref hora, ref minuto, ref segundo))
                {
                    txtFechaDispositivo.Text = formatoFechaCorrecto(anio, mes, dia, hora, minuto, segundo);
                }

                string numeroDeSerie = "";// Numero de serie del equipo
                if(dispositivo.GetSerialNumber(iMachineNumber, out numeroDeSerie))
                {
                    txtNumeroDeSerie.Text = numeroDeSerie;
                }

                string versionSDK = "";//Obtiene la version SDK
                if (dispositivo.GetSDKVersion(ref versionSDK))
                {
                    txtVersionSDK.Text = versionSDK;
                }

                int valor = 0, infoDispositivo = 3; //Obtener el idioma, esta funcion tiene muchas informaciones, por ahora solo 
                if (dispositivo.GetDeviceInfo(iMachineNumber, infoDispositivo, ref valor))
                {
                    string idioma = valor.ToString();
                    switch(idioma)
                    {
                        case "0": idioma = "Ingles"; break;
                        case "1": idioma = "Otro"; break;
                        case "2": idioma = "Chino"; break;
                        case "3": idioma = "Thai"; break;
                    }
                    txtIdiomaDispositivo.Text = idioma;
                }

                string MAC = ""; // MAC Address del dispositivo
                if(dispositivo.GetDeviceMAC(iMachineNumber, ref MAC))
                {
                    txtMAC.Text = MAC;
                }

                int tipoControlAcceso = 0; //Si el dispositivo soporta control de acceso
                if (dispositivo.GetACFun(ref tipoControlAcceso))
                {
                    string opcionControlAcceso = tipoControlAcceso.ToString();
                    switch (opcionControlAcceso)
                    {
                        case "0": opcionControlAcceso = "Sin control de Acceso"; break;
                        case "1": opcionControlAcceso = "Simple"; break;
                        case "2": opcionControlAcceso = "Nivel medio"; break;
                        case "6": opcionControlAcceso = "Nivel avanzado"; break;
                        case "14": opcionControlAcceso = "Avanzado + NO"; break;
                        case "15": opcionControlAcceso = "Disponible"; break;
                    }
                    txtAccessControl.Text = opcionControlAcceso;
                }

                #endregion

                #region Datos totales de equipo

                int tipoStatus; //Registros de Administradores, usuarios, huellas, asistencias, operaciones, Caras
                string statusAMostrar = "";
                for (tipoStatus = 1; tipoStatus <= 6; tipoStatus++)
                {
                    switch (tipoStatus)
                    {
                        case 1: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtTotAdmin.Text = statusAMostrar; break;
                        case 2: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtTotUsuarios.Text = statusAMostrar; break;
                        case 3: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtTotHuellas.Text = statusAMostrar; break;
                        case 5: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtTotOperaciones.Text = statusAMostrar; break;
                        case 6: statusAMostrar = obtenerStatus(tipoStatus).ToString(); txtTotAsistencias.Text = statusAMostrar; break;
                    }
                }
                if (dispositivo.GetDeviceStatus(iMachineNumber, 21, ref valor))
                {
                    txtTotCaras.Text = valor.ToString();
                }

                #endregion
                CargarUnidadesNegocio();

                //btnMostrarAsistencia.PerformClick(); //Presiona automatico el boton
                
                Cursor = Cursors.Default;

               
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Boton Mas Datos
        private void btnMasDatos_Click(object sender, EventArgs e)
        {
            MasDatosTotalesForm MostrarForma = new MasDatosTotalesForm();
            MostrarForma.CambiarPuerto = Convert.ToInt32(txtPuerto.Text);
            MostrarForma.CambiarIP = txtIP.Text;
            DialogResult dialogresult = MostrarForma.ShowDialog();
        }
        #endregion

        #region Funciones de apoyo

        private void txtIP_KeyDown(object sender, KeyEventArgs e) //Para presionar enter mientras estoy agregando IP
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConectar_Click(this, new EventArgs());
            }
        }

        private void txtPuerto_KeyDown(object sender, KeyEventArgs e) //Para presionar enter mientras estoy agregando puerto
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConectar_Click(this, new EventArgs());
            }
        }

        private void ClearTextBoxes() //Funcion para borrar todo el contenido de las textbox
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private string formatoFechaCorrecto(int anios, int meses, int dias, int horas, int minutos, int segundos) //Funcion para darle un buen formato a la fecha
        {
            string anio = anios.ToString();
            string mes = meses.ToString();
            string dia = dias.ToString();
            string hora = horas.ToString();
            string minuto = minutos.ToString();
            string segundo = segundos.ToString();

            switch (mes)
            {
                case "1": mes= "Jan"; break;
                case "2": mes= "Feb"; break;
                case "3": mes= "Mar"; break;
                case "4": mes= "Apr"; break;
                case "5": mes= "May"; break;
                case "6": mes= "Jun"; break;
                case "7": mes= "Jul"; break;
                case "8": mes= "Aug"; break;
                case "9": mes= "Sep"; break;
                case "10": mes = "Oct"; break;
                case "11": mes = "Nov"; break;
                case "12": mes = "Dec"; break;
            }

            if (hora.ToString().Length == 1) hora = "0" + hora;
            if (minuto.ToString().Length == 1) minuto = "0" + minuto;
            if (segundo.ToString().Length == 1) segundo = "0" + segundo;
            return dia + " " + mes + " " + anio + "  " + hora + ":" + minuto + ":" + segundo;
        }

        public string MandarIP
        {
            get { return txtIP.Text; }
        }
        public string MandarPuerto
        {
            get { return txtPuerto.Text; }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) //Activar botones dentro de las tabs de forma automatica
        {
            int opcion = tabmenu.SelectedIndex;

            switch (opcion)
            {
                case 1: 
                    btnMostrarUsuarios.PerformClick();
                    btnModificarUsuario.Enabled = false;
                    btnBorrarUsuario.Enabled = false;
                break;
                case 4:
                    btnMostrarDatos.PerformClick();
                break;
            }

        }

        private void tabUsuarios_Click(object sender, EventArgs e) //Mostrar y esconder botones de la pestaña Usuarios
        {
            btnModificarUsuario.Enabled = false;
            btnBorrarUsuario.Enabled = false;
            btnAgregarUsuario.Enabled = true;
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e) // Mostrar y esconder botones relacionados con la tabla de usuarios
        {
            btnModificarUsuario.Enabled = true;
            btnBorrarUsuario.Enabled = true;
            btnAgregarUsuario.Enabled = false;
            bool habilitado;
            if(dgvUsuarios.CurrentRow.Cells[5].Value.ToString() != null) // Si no ponemos esta condicion, nos marca una excepcion tipo Null
            {
                habilitado = (Convert.ToBoolean(dgvUsuarios.CurrentRow.Cells[5].Value.ToString()));
                if (habilitado)
                    btnDeshabilitarUsuario.Text = "Deshabilitar Usuario";
                else
                    btnDeshabilitarUsuario.Text = "Habilitar Usuario";
            }
            else
            {
                dgvUsuarios.CurrentRow.Cells[5].Value = "Deshabilitar Usuario"; //Damos un valor default si ocurre la excepcion Null
            }
                
        }
        private void tabTarjetasCaras_Click(object sender, EventArgs e) //Manejo de botones en la pestaña tarjetas
        {
            btnBorrarCara.Enabled = false;
        }

        private void dgvTarjetasCaras_CellClick(object sender, DataGridViewCellEventArgs e) //Habilitar botones al hacer click
        {
            btnBorrarCara.Enabled = true;
        }
        private void coloreardgvRTE()
        {
            for (int i = 0; i < dgvRTE.Rows.Count-1; i++)
            {
                string color = dgvRTE.Rows[i].Cells[1].Value.ToString();
                switch (color)
                {
                    case "OnFinger": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine; break;
                    case "OnVerify": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Green; break;
                    case "OnAttTransactionEx": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case "OnFingerFeature": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Gold; break;
                    case "OnEnrollFingerEx": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Lavender; break;
                    case "OnDeleteTemplate": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed; break;
                    case "OnNewUser": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.RoyalBlue; break;
                    case "OnHIDNum": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue; break;
                    case "OnAlarm": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Red; break;
                    case "OnDoor": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Brown; break;
                    case "OnWriteCard": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.Tomato; break;
                    case "OnEmptyCard": dgvRTE.Rows[i].DefaultCellStyle.BackColor = Color.LightGray; break;
                }
            }
        }
        
        #endregion
        
        #region Funciones Importantes de apoyo

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
        
        #region Asistencia
        private void btnMostrarAsistencia_Click(object sender, EventArgs e)
        {
            dgvLogs.Rows.Clear(); //Borra los datos de los logs si existia alguno
            Cursor = Cursors.WaitCursor;

            string idUsuario = "",fecha,verificacion, registro;
            int modoDeVerificacion=0; // Opcion de multiverificacion: 0=Cualquier modo, 1=Huella, 2=PIN, etc.
            int tipoDeRegistro=0; //0=Check In, 1=Check Out, 2=Break Out, 3=Break In, 4=Other In, 5=Other Out
            int anio=0, mes=0, dia=0, hora=0, minuto=0, segundo = 0, codigoDeTrabajo = 0; 

            int id = 0;

            dispositivo.EnableDevice(iMachineNumber, false); //Deshabilita el equipo para obtener los datos
            if(dispositivo.ReadAllGLogData(iMachineNumber)) //Almacena los datos de logs a la pc, es igual a ReadGeneralLogData
            {
                var datos = dispositivo.SSR_GetGeneralLogData(iMachineNumber, out idUsuario, out modoDeVerificacion, out tipoDeRegistro, out anio, out mes, out dia, out hora, out minuto, out segundo, ref codigoDeTrabajo);
                if (datos) {
                    while (dispositivo.SSR_GetGeneralLogData(iMachineNumber, out idUsuario, out modoDeVerificacion, out tipoDeRegistro, out anio, out mes, out dia, out hora, out minuto, out segundo, ref codigoDeTrabajo))
                    {
                        fecha = formatoFechaCorrecto(anio, mes, dia, hora, minuto, segundo);
                        verificacion = modoDeVerifString(modoDeVerificacion);
                        registro = tipoDeRegistroString(tipoDeRegistro);
                        dgvLogs.Rows.Add(id + 1, idUsuario, "", verificacion, registro, fecha, codigoDeTrabajo); //Mostrar datos en la tabla
                        id++;
                    }
                }
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
                Cursor = Cursors.Default;
            }
            dispositivo.EnableDevice(iMachineNumber, true);//Habilitar el equipo nuevamente
            Cursor = Cursors.Default;
        }

        private string modoDeVerifString(int modoVerif)
        {
            string verificacion = modoVerif.ToString();
            switch (modoVerif)
            {
                case 0: verificacion = "Cualquier tipo"; break;
                case 1: verificacion = "Huella Digital"; break;
                case 2: verificacion = "PIN"; break;
                case 3: verificacion = "Password"; break;
                case 4: verificacion = "Tarjeta"; break;
                case 5: verificacion = "FP o Pwd"; break; //FP = Fingerprint, Pwd = Password
                case 6: verificacion = "FP o Card"; break;
                case 7: verificacion = "Pwd o Card"; break;
                case 8: verificacion = "PIN y FP"; break;
                case 9: verificacion = "FP y Pwd"; break;
                case 10: verificacion = "FP y Card"; break;
                case 11: verificacion = "Pwd y Card"; break;
                case 12: verificacion = "FP y Pwd y Card"; break;
                case 13: verificacion = "PIN y FP y Pwd"; break;
                case 14: verificacion = "FP y Card o PIN"; break;
                case 16: verificacion = "Rec. Facial"; break;
                default: verificacion = "S/Identificaion";break;
            }
            return verificacion;
        }
        private string tipoDeRegistroString(int tipoReg)
        {
            string registro = tipoReg.ToString();
            switch (tipoReg)
            {
                case 0: registro = "Check In"; break;
                case 1: registro = "Check Out"; break;
                case 2: registro = "Break Out"; break;
                case 3: registro = "Break In"; break;
                case 4: registro = "Other In"; break;
                case 5: registro = "Other Out"; break;
            }
            return registro;
        }
        #endregion

        #region Usuarios

        #region Mostrar usuarios
        public void btnMostrarUsuarios_Click(object sender, EventArgs e)
        {
            dgvUsuarios.Rows.Clear(); //Borra los datos de los logs si existia alguno
            dispositivo.RefreshData(iMachineNumber);
            Cursor = Cursors.WaitCursor;

            int num = 1;
            //Variables de usuario
            string userID, nombreUsuario, Password, privilegioUser;
            int tipoDeUsuario; //Super Administrador = 3, Usuario = 0
            bool usuarioHabilitado; //Si el usuario esta habilitado manda true
            //Variables de huella
            int numeroHuella; //Podemos poner hasta 10 huellas (nuestros 10 dedos)
            string templateHuella;
            int tamanoHuella;
            int huellaValida;//0=huella invalida, 1=valida,2=Modificada (falsa)
            
            dispositivo.EnableDevice(iMachineNumber,false); //Deshabilitar el equipo para la extraccion de datos

            //Lectura de datos en el dispositivo
            dispositivo.ReadAllUserID(iMachineNumber); //Lee User ID, password, nombre, y numero de tarjeta, sin esto, getAllUserID o getAllUserInfo no funciona
            dispositivo.ReadAllTemplate(iMachineNumber);// 
           
            //Obtencion de datos del equipo
            while (dispositivo.SSR_GetAllUserInfo(iMachineNumber, out userID, out nombreUsuario, out Password, out tipoDeUsuario, out usuarioHabilitado))//Extraer toda la informacion de usuario
            {
                for (numeroHuella = 0; numeroHuella < 10; numeroHuella++) //Como solo tenemos 10 huellas, por eso el loop es de 0 a 9
                {
                    if (dispositivo.GetUserTmpExStr(iMachineNumber, userID, numeroHuella, out huellaValida, out templateHuella, out tamanoHuella) || (numeroHuella == 0)) //El OR es por si el usuario no tiene huella, tambien aparezca
                    {
                        privilegioUser = tipoDePrivilegio(tipoDeUsuario);
                        dgvUsuarios.Rows.Add(num, userID, nombreUsuario, Password, privilegioUser, usuarioHabilitado, numeroHuella, tamanoHuella, templateHuella, huellaValida); //Mostrar datos en la tabla
                        num++;
                    }
                }
            }
            
            dispositivo.EnableDevice(iMachineNumber, true); //Volver a habilitar el equipo
            Cursor = Cursors.Default;
        }
        private string tipoDePrivilegio(int tipoUser)
        {
            string privilegio = tipoUser.ToString();
            switch (tipoUser)
            {
                case 0: privilegio = "Usuario"; break;
                case 1: privilegio = "Administrador"; break;
                case 3: privilegio = "Super Admin"; break;
            }
            return privilegio;
        }

        #endregion

        #region Agregar Usuarios
        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            AgregarUsuario AbrirForma = new AgregarUsuario();
            AbrirForma.CambiarPuerto = Convert.ToInt32(txtPuerto.Text);
            AbrirForma.CambiarIP = txtIP.Text;

            if (AbrirForma.ShowDialog() == System.Windows.Forms.DialogResult.Abort)//Automatizar Refresh de tabla, si no ponemos asi, la ventana aparece 2 veces
            {
                btnMostrarUsuarios.PerformClick();
            }
            else
            {
                AbrirForma.ShowDialog();
            }
        }
        #endregion

        #region Modificar Usuarios

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            ModificarUsuario EmpezarForma = new ModificarUsuario();
            EmpezarForma.CambiarPuerto = Convert.ToInt32(txtPuerto.Text);
            EmpezarForma.CambiarIP = txtIP.Text;

            EmpezarForma.IdAModificar = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            EmpezarForma.NombreAModificar = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
            EmpezarForma.PasswordAModificar = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
            EmpezarForma.PrivilegioUsuarioAModificar = dgvUsuarios.CurrentRow.Cells[4].Value.ToString();
            EmpezarForma.HabilitadoAModificar = Convert.ToBoolean(dgvUsuarios.CurrentRow.Cells[5].Value);

            //Parte de obtener el numero de tarjeta del usuario en especifico
            
            dispositivo.EnableDevice(iMachineNumber, false); //Deshabilitar el equipo para la extraccion de datos

            string userID = dgvUsuarios.CurrentRow.Cells[1].Value.ToString(), nombreUsuario, Password;//Obtienes el User ID de la tabla
            int tipoDeUsuario; 
            bool usuarioHabilitado;
            string numTarjeta;

            if (dispositivo.SSR_GetUserInfo(iMachineNumber, userID, out nombreUsuario, out Password, out tipoDeUsuario, out usuarioHabilitado))//Extraer toda la informacion de 1 usuario
            {
                dispositivo.GetStrCardNumber(out numTarjeta); //Obtener el numero de tarjeta
                EmpezarForma.NumeroTarjetaAModificar = numTarjeta;
            }
            else
            {
                EmpezarForma.NumeroTarjetaAModificar = "0";
            }
            dispositivo.EnableDevice(iMachineNumber, true); //Volver a habilitar el equipo

            if(EmpezarForma.ShowDialog() == System.Windows.Forms.DialogResult.Abort)//Automatizar Refresh de tabla, si no ponemos asi, la ventana aparece 2 veces
            {
                btnMostrarUsuarios.PerformClick();
            }
            else
            {
                EmpezarForma.ShowDialog();
            }
            
        }
        #endregion

        #region Borrar Usuarios
        private void btnBorrarUsuario_Click(object sender, EventArgs e)
        {
            BorrarDatosDeUsuario OpenForma = new BorrarDatosDeUsuario();
            OpenForma.CambiarPuerto = Convert.ToInt32(txtPuerto.Text);
            OpenForma.CambiarIP = txtIP.Text;
            OpenForma.IDaBorrar = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            OpenForma.NombreABorrar = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();

            if (OpenForma.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnMostrarUsuarios.PerformClick();
            }
            else
            {
                OpenForma.ShowDialog();
            }
        }
        #endregion

        #region Habilitar/Deshabilitar usuarios

        private void btnDeshabilitarUsuario_Click(object sender, EventArgs e)
        {
            bool habilitado = (Convert.ToBoolean(dgvUsuarios.CurrentRow.Cells[5].Value.ToString()));

            bool bandera = (habilitado) ? !habilitado : !habilitado; //Si habilitado es true o false, cambiar el valor a su opuesto
            string IDUsuario = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            if (dispositivo.SSR_EnableUser(iMachineNumber, IDUsuario, bandera))
            {
                if (bandera == true)
                {
                    MessageBox.Show("El usuario ha sido Habilitado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("El usuario ha sido Deshabilitado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            btnMostrarUsuarios.PerformClick();
        }

        #endregion

        #endregion

        #region Tarjetas y Caras
        private void btnMostrarDatos_Click(object sender, EventArgs e)
        {
            dgvTarjetasCaras.Rows.Clear(); //Borra los datos de los logs si existia alguno
            dispositivo.RefreshData(iMachineNumber); //Actualizar datos de dispositivo en caso de modificacion o borrado
            Cursor = Cursors.WaitCursor;

            int num = 1;
            //Variables de usuario
            string userID, nombreUsuario, Password, privilegioUser;
            int tipoDeUsuario, tamanoCara = 0; //Super Administrador = 3, Usuario = 0
            bool usuarioHabilitado; //Si el usuario esta habilitado manda true
            //Variables de Tarjeta
            string numTarjeta;
            //Variables de cara
            string templateCara="";

            dispositivo.EnableDevice(iMachineNumber, false); //Deshabilitar el equipo para la extraccion de datos

            //Lectura de datos en el dispositivo
            dispositivo.ReadAllUserID(iMachineNumber); //Lee User ID, password, nombre, y numero de tarjeta, sin esto, getAllUserID o getAllUserInfo no funciona
            dispositivo.ReadAllTemplate(iMachineNumber);//Para los datos de las huellas 

            //Obtencion de datos del equipo
            while (dispositivo.SSR_GetAllUserInfo(iMachineNumber, out userID, out nombreUsuario, out Password, out tipoDeUsuario, out usuarioHabilitado))//Extraer toda la informacion de usuario
            {
                dispositivo.GetStrCardNumber(out numTarjeta); //Obtener el numero de tarjeta
                dispositivo.GetUserFaceStr(iMachineNumber, userID, 50, ref templateCara, ref tamanoCara); //Index solo puede ser 50
                privilegioUser = tipoDePrivilegio(tipoDeUsuario);
                dgvTarjetasCaras.Rows.Add(num, userID, nombreUsuario, numTarjeta, tamanoCara, templateCara, privilegioUser, usuarioHabilitado); //Mostrar datos en la tabla
                num++;
            }

            dispositivo.EnableDevice(iMachineNumber, true); //Volver a habilitar el equipo
            Cursor = Cursors.Default;
        }
        private void btnEscribirTarjeta_Click(object sender, EventArgs e)
        {
            //Por el momento no funciona
        }
        private void btnSubirCara_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrarCara_Click(object sender, EventArgs e)
        {
            string id = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            Cursor = Cursors.WaitCursor;
            if (dispositivo.DelUserFace(iMachineNumber, id, 50))
            {
                MessageBox.Show("La Cara se ha borrado! ", "Success");
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
            }
            Cursor = Cursors.Default;
        }

        private void btnBorrarContMifare_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (dispositivo.EmptyCard(iMachineNumber))
            {
                MessageBox.Show("La tarjeta se ha borrado! ", "Success");
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
            }
            Cursor = Cursors.Default;
        }
        #endregion

        #region Real Time Events 

        #region Activacion de eventos
        private void btnRTE_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (btnRTE.Text == "Apagar Real Time Events")
            {
                //Borrar los eventos ya ocurridos
                this.dgvRTE.DataSource = null;
                dgvRTE.Rows.Clear();

                //Cambiar texto del boton
                btnRTE.Text = "Iniciar Real Time Events";

                //Desactivar RTE
                this.dispositivo.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(dispositivo_OnFinger);
                this.dispositivo.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(dispositivo_OnVerify);
                this.dispositivo.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(dispositivo_OnAttTransactionEx);
                this.dispositivo.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(dispositivo_OnFingerFeature);
                this.dispositivo.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(dispositivo_OnEnrollFingerEx);
                this.dispositivo.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(dispositivo_OnDeleteTemplate);
                this.dispositivo.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(dispositivo_OnNewUser);
                this.dispositivo.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(dispositivo_OnHIDNum);
                this.dispositivo.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(dispositivo_OnAlarm);
                this.dispositivo.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(dispositivo_OnDoor);
                this.dispositivo.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(dispositivo_OnWriteCard);
                this.dispositivo.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(dispositivo_OnEmptyCard);

                Cursor = Cursors.Default;
                return;
            }
            else
            {
                btnRTE.Text = "Apagar Real Time Events";

                if (dispositivo.RegEvent(iMachineNumber, 65535))//Habilitar el registro de eventos: 65535 significa, registrar todos los eventos
                {
                    this.dispositivo.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(dispositivo_OnFinger);
                    this.dispositivo.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(dispositivo_OnVerify);
                    this.dispositivo.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(dispositivo_OnAttTransactionEx);
                    this.dispositivo.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(dispositivo_OnFingerFeature);
                    this.dispositivo.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(dispositivo_OnEnrollFingerEx);
                    this.dispositivo.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(dispositivo_OnDeleteTemplate);
                    this.dispositivo.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(dispositivo_OnNewUser);
                    this.dispositivo.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(dispositivo_OnHIDNum);
                    this.dispositivo.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(dispositivo_OnAlarm);
                    this.dispositivo.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(dispositivo_OnDoor);
                    this.dispositivo.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(dispositivo_OnWriteCard);
                    this.dispositivo.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(dispositivo_OnEmptyCard);
                    
                }
            }
            Cursor = Cursors.Default;
        }
        #endregion

        #region Funciones de los eventos

        private void dispositivo_OnFinger() //Cuando pones el dedo en la huella
        {
            string fecha = RegistrarFecha();
            dgvRTE.Rows.Add(fecha, "OnFinger", "Has puesto el dedo en la huella", "Ningun Parámetro");
            coloreardgvRTE();
        }

        private void dispositivo_OnVerify(int iUserID) //Al poner la huella o tarjeta, manda el enrolement si el usuario ya existe o -1 si no existe
        {
            string fecha = RegistrarFecha(), opcion;

            if (iUserID != -1)
            {
                opcion = "Verificacion correcta, el ID regresado es " + iUserID.ToString();
            }
            else
            {
                opcion = "La verificacion falló";
            }
            dgvRTE.Rows.Add(fecha, "OnVerify", opcion, "ID = El usuario existe ; -1 = El usuario no existe");
            coloreardgvRTE();
        }

        private void dispositivo_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)//Al pasar verificacion, el sistema puede mostrar todos los datos en tiempo real
        {
            string fecha = RegistrarFecha(), opcion, transValida="", verificacion, registro;

            if (iIsInValid == 0)
                transValida = "Si ";
            else
                transValida = "No ";

            verificacion = modoDeVerifString(iVerifyMethod); // Multiverificacion, activar por telnet
            registro = tipoDeRegistroString(iAttState); // 0=Check In, 1=Check Out, 2=Break Out, 3=Break In, 4=Other In, 5=Other Out

            opcion = "ID = " + sEnrollNumber + "; Record Valido = " + transValida + "; Tipo de Registro = " + registro + "; Tipo de Verificacion = " + verificacion; //Tambien si queremos, podemos extraer la fecha de verif del dispositivo
            dgvRTE.Rows.Add(fecha, "OnAttTransactionEx", opcion, "ID, Record valido, Tipo de Registro, Verificacion (MultiVerif), Fecha");
            coloreardgvRTE();
        }

        private void dispositivo_OnFingerFeature(int iScore)//Al enrolar las 3 huellas al hacer un nuevo usuario, regresa la calidad de la huella enrolada
        {
            string fecha = RegistrarFecha(), opcion;
            if (iScore < 0)
            {
                opcion = "La calidad de la huella es muy pobre";
            }
            else
            {
                opcion = "Score de la huella: " + iScore.ToString();
            }
            dgvRTE.Rows.Add(fecha, "OnFingerFeature", opcion, "Score: Calidad de la huella, 0 = Si no se logra registrar");
            coloreardgvRTE();
        }

        private void dispositivo_OnEnrollFingerEx(string sEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)// Cuando terminas de enrolar la huella, este evento se activa, mandando # de huellas y el tamaño de template de la misma
        {
            string fecha = RegistrarFecha(), opcion;

            if (iActionResult == 0)
            {
                opcion = "ID: " + sEnrollNumber + "; Index de huella: " + iFingerIndex.ToString() + "; Tamaño de Huella: " + iTemplateLength.ToString();
            }
            else
            {
                opcion = "Ha ocurrido un error al enrolar la huella , ActionResult = " + iActionResult.ToString();
            }
            dgvRTE.Rows.Add(fecha, "OnEnrollFingerEx", opcion, "Accion: 0 = Exitoso , Otro numero = si hay un error");
            coloreardgvRTE();
        }

        private void dispositivo_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)//Evento que se activa al borrar una huella
        {
            string fecha = RegistrarFecha(), opcion = "ID = " + iEnrollNumber.ToString() + "; FingerIndex = " + iFingerIndex.ToString();
            dgvRTE.Rows.Add(fecha, "OnDeleteTemplate", opcion, "ID, Indice (0 a 9) de la huella borrada");
            coloreardgvRTE();
        }

        private void dispositivo_OnNewUser(int iEnrollNumber)//Evento que se activa el terminar de enrolar un usuario exitosamente
        {
            string fecha = RegistrarFecha(), opcion = "ID = " + iEnrollNumber.ToString();
            dgvRTE.Rows.Add(fecha, "OnNewUser", opcion, "Muestra el ID del usuario recien creado");
            coloreardgvRTE();
        }

        private void dispositivo_OnHIDNum(int iCardNumber) //Cuando pasas una tarjeta por el equipo, este evento te da el # de la misma
        {
            string fecha = RegistrarFecha(), opcion = "Numero de tarjeta = " + iCardNumber.ToString();
            dgvRTE.Rows.Add(fecha, "OnHIDNum", opcion, "Numero de tarjeta al ser detectada por el equipo");
            coloreardgvRTE();
        }

        private void dispositivo_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)//Se activa este evento cuando se encienden diferentes tipos de alarma 
        {
            string fecha = RegistrarFecha(), opcion, tipoAlarma="", verificarAlarma="";
            
            switch (iAlarmType)
            {
                case 32: tipoAlarma = "Huella de amago"; break;
                case 34: tipoAlarma = "Anti-Pass Back"; break;
                case 55: tipoAlarma = "Tamper"; break;
                case 58: tipoAlarma = "Falsa"; break;
            }

            if (iVerified == 0)
                verificarAlarma = "Tamper, false o amago";
            else
                verificarAlarma = "Antipass-back u otras alarmas";

            opcion = "Tipo Alarma = " + tipoAlarma + "; ID = " + iEnrollNumber.ToString() + "; Verificacion = " + verificarAlarma; //55 Tamper, 58 Falsa alarma, 32 Alarma de Huella de amago, 34 Anti-pass back. En verificacion: 0 cuando es tamper, falsa o de amago y 1 todo lo demas
            dgvRTE.Rows.Add(fecha, "OnAlarm", opcion, "Tipo de alarma, ID (0 para false,tamper,amago) y Verificacion");
            coloreardgvRTE();
        }

        private void dispositivo_OnDoor(int iEventType)//Evento que se activa cuando el dispositivo abre una puerta
        {
            string fecha = RegistrarFecha(), puerta="", opcion;

            switch (iEventType)
            {
                case 1: puerta = "La puerta se abrió inesperadamente"; break;
                case 4: puerta = "Puerta Abierta"; break;
                case 5: puerta = "Puerta Cerrada"; break;
                case 53: puerta = "Boton Exit"; break;
            }

            opcion = "Tipo de Evento = " + puerta;

            dgvRTE.Rows.Add(fecha, "OnDoor", opcion, "Eventos que ocurre con la puerta");
            coloreardgvRTE();
        }

        private void dispositivo_OnEmptyCard(int iActionResult)//Se activa al borrar los datos de huella de la tarjeta Mifare (Necesario configurar parámetro en el equipo)
        {
            string fecha = RegistrarFecha(), opcion;
            if (iActionResult == 0)
            {
                opcion = "Vaciado de tarjeta exitoso";
            }
            else
            {
                opcion = "El vaciado ha fallado";
            }
            dgvRTE.Rows.Add(fecha, "OnEmptyCard", opcion, "0 = Tarjeta vaciada, Otro = Error en vaciado de tarjeta");
            coloreardgvRTE();
        }

        private void dispositivo_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength) //Se activa al escribir datos de huella en la tarjeta mifare (Necesario configurar parámetro en el equipo)
        {
            string fecha = RegistrarFecha(), opcion;
            if (iActionResult == 0)
            {
                opcion = "La escritura de la Mifare Card fue exitosa. ID = " + iEnrollNumber.ToString() + "; Cantidad de Datos escritos = " + iLength.ToString();
            }
            else
            {
                opcion = "La escritura ha fallado";
            }
            dgvRTE.Rows.Add(fecha, "OnWriteCard", opcion, "0 = Tarjeta vaciada, Otro = Error en vaciado de tarjeta. ID y Length");
            coloreardgvRTE();
        }

        #endregion

        #region Registrar el momento del evento

        public string RegistrarFecha() //Mostrar con la fecha de la computadora el momento en que el RTE ocurre
        {
            DateTime registroDeEvento = DateTime.UtcNow;
            string fechaAImprimirCorrecta = "";
            try
            {
                TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime fechaAimprimir = TimeZoneInfo.ConvertTimeFromUtc(registroDeEvento, zonaHoraria);
                fechaAImprimirCorrecta = formatoFechaCorrecto(fechaAimprimir.Year, fechaAimprimir.Month, fechaAimprimir.Day, fechaAimprimir.Hour, fechaAimprimir.Minute, fechaAimprimir.Second);
            }
            catch (TimeZoneNotFoundException)
            {
                MessageBox.Show("El registro no define la zona Central Standard Time.");
            }
            catch (InvalidTimeZoneException)
            {
                MessageBox.Show("Los datos en la zona Central Standard Time han sido corruptos.");
            }
            
            return fechaAImprimirCorrecta;
        }
        
        #endregion

        #region Boton Eventos que existen

        private void btnTutRTE_Click(object sender, EventArgs e)
        {
            EventosRTEqueExisten expEventos = new EventosRTEqueExisten();
            expEventos.Show();
        }

        #endregion

        #region Informacion Extra

        //After function GetRTLog() is called ,RealTime Events will be triggered. 
        //When you are using these two functions, it will request data from the device forwardly.
        private void rtTimer_Tick(object sender, EventArgs e)
        {
            if (dispositivo.ReadRTLog(iMachineNumber))
            {
                while (dispositivo.GetRTLog(iMachineNumber))
                {
                    ;
                }
            }
        }

        #endregion

        #endregion

        #region Opciones - Borrado
        private void btnBorrarAsistencia_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esto va a borrar todas las asistencias en el equipo. Confirmar?", "Eliminar Datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dispositivo.EnableDevice(iMachineNumber, false); //Deshabilita el equipo para obtener los datos
                if (dispositivo.ClearGLog(iMachineNumber)) //Tambien se puede usar ClearData con el parametro 1 en el tipo de borrado
                {
                    dispositivo.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    MessageBox.Show("Todas las asistencias han sido borradas", "Datos eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgvLogs.Rows.Clear(); //Borra los datos de los logs si existia alguno
                }
                else
                {
                    errorList(dispositivo, error); //Chequeo de errores
                }
                dispositivo.EnableDevice(iMachineNumber, true);
            }
            else
            {
                //Solo sale del boton
            }
        }

        //A continuacion los diferentes tipos de borrado
        //1=Asistencias, 2=Huellas, 4=Operaciones, 5=TODO
        private void btnBorrarHuellas_Click(object sender, EventArgs e)
        {
            tipoDeBorrado("Todas las Huellas", 2);
        }

        private void btnBorrarOperaciones_Click(object sender, EventArgs e)
        {
            tipoDeBorrado("Todas las Operaciones", 4);
        }

        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            tipoDeBorrado("Todos los Datos", 5);
        }

        private void tipoDeBorrado(string nombreDeBorrado, int tipoDeBorrado)
        {
            if (MessageBox.Show("Esto va a borrar " + nombreDeBorrado + " en el equipo y un reinicio ocurrira (Es necesario reconectarse). Confirmar?", "Eliminar Datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dispositivo.ClearData(iMachineNumber, tipoDeBorrado))
                {
                    dispositivo.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    MessageBox.Show(nombreDeBorrado + " han sido borradas", "Datos Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnConectar.PerformClick();
                }
                else
                {
                    errorList(dispositivo, error); //Chequeo de errores
                }
            }
            else
            {
                //Solo sale del boton
            }
        }

        private void btnBorrarAdmin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esto va a borrar todos los administradores en el equipo. Confirmar?", "Eliminar Datos", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dispositivo.ClearAdministrators(iMachineNumber))
                {
                    dispositivo.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    MessageBox.Show("Todas los administradores han sido borrados", "Datos eliminados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    errorList(dispositivo, error); //Chequeo de errores
                }
            }
            else
            {
                //Solo sale del boton
            }
        }



        #endregion

        #region Opciones - Control del equipo
        private void btnReset_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            dispositivo.RestartDevice(iMachineNumber);
            MessageBox.Show("El equipo se reiniciara en estos momentos", "Reiniciando...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor = Cursors.Default;
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            if (dispositivo.PowerOffDevice(iMachineNumber))
            {
                MessageBox.Show("El equipo se apagará en estos momentos", "Apagando...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor = Cursors.Default;
        }




        #endregion

        #region Programacion
        private void formZKDemo_Load(object sender, EventArgs e)
        {
            // Configurar el temporizador para ejecutar la función cada 8 horas
            timer = new System.Threading.Timer(SincronizarBD, null, Timeout.Infinite, Timeout.Infinite);


        }

        private void SincronizarBD(object state)
        {
             
            // Coloca aquí la llamada a tu función de sincronización SincronizarBDWG();
            
         //   MessageBox.Show("activado");
            Invoke(new Action(() =>
            {
                DateTime horaFechaActual = DateTime.Now;
                string mensaje = $"Sincronización de BD {horaFechaActual.ToShortTimeString()} el {horaFechaActual.ToShortDateString()}";
                listBox1.Items.Add(mensaje);

                dataGridView1.Rows.Clear();
            }));
            
           SincronizarBDWG();

        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int i = rnd.Next(1, 13);
            dispositivo.PlayVoiceByIndex(i);
        }

        private void tabSync_Click(object sender, EventArgs e)
        {

        }



        private void cmbModeloNegocio_SelectedIndexChanged(object sender, EventArgs e)
        {
       


            switch (cmbModeloNegocio.Text)
            {
                case "CYPO":
                    txtIP.Text = "192.168.1.201";
                    break;
                case "Bodega":
                    txtIP.Text = "192.168.1.201";
                    break;
                case "Honda Tijuana":
                    txtIP.Text = "10.10.10.155";
                    break;
                case "Honda ensenada":
                    txtIP.Text = "10.10.112.43";
                    break;
                case "Kia ensenada":
                    txtIP.Text = "192.168.3.150";
                    break;
                case "Kia mexicali":
                    txtIP.Text = "192.168.2.162";
                    break;
                case "Honda mexicali":
                    txtIP.Text = "10.10.11.51";
                    break;
                case "Grupo Optima":
                    txtIP.Text = "192.168.1.184";
                    break;
                case "KIA":
                    txtIP.Text = "192.168.1.201";
                    break;                                

                default:
                    // Acción por defecto si no se cumple ninguna de las condiciones anteriores
                    break;
            }



        }

        #endregion

        #region Base de datos
        public void CargarUnidadesNegocio()
        {
            try
            {
                string query = "select * from checador_modelos_negocio where Activo = 1";
                sql.Open();
                MySqlCommand command = new MySqlCommand(query, sql);
                MySqlDataAdapter mysqldl = new MySqlDataAdapter(command);

                DataTable dt = new DataTable();
                mysqldl.Fill(dt);

                cmbModeloNegocio.ValueMember = "id_modelonegocio";
                cmbModeloNegocio.DisplayMember = "Nombre";
                cmbModeloNegocio.DataSource = dt;
                sql.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        public string GetRFC(string idchecador, string idmodelonegocio)
        {
            try
            {
                string query = "select emp_rfc as RFC from empleados where emp_id_checador = "+ idchecador;
                string rfc = "";
                sql.Open();
                MySqlCommand command = new MySqlCommand(query, sql);
                MySqlDataReader rdr = command.ExecuteReader();
                
                while (rdr.Read())
                {
                    rfc = rdr.GetString(0);
                }
                sql.Close();
                return rfc;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "SIN IDENTIFIACION";
            }
        }


       

        public void SincronizarBDoldis()
            
        {
            dataGridView1.Rows.Clear();
        }
        public void SincronizarBDWG()
        {

            int num = 1;
            try
            {
                lblEstatusSync.Text = "Sincronizacion Iniciada";
                Sincronizar.Enabled = false;
                string modelonegocio = ((System.Data.DataRowView)cmbModeloNegocio.SelectedItem).Row.ItemArray[0].ToString();
                string idUsuario = "", fecha, verificacion, registro, rfc;
                int modoDeVerificacion = 0; // Opcion de multiverificacion: 0=Cualquier modo, 1=Huella, 2=PIN, etc.
                int tipoDeRegistro = 0; //0=Check In, 1=Check Out, 2=Break Out, 3=Break In, 4=Other In, 5=Other Out
                int anio = 0, mes = 0, dia = 0, hora = 0, minuto = 0, segundo = 0, codigoDeTrabajo = 0;
                bool resultado = true;
                dispositivo.EnableDevice(iMachineNumber, false); //Deshabilita el equipo para obtener los datos
                if (dispositivo.ReadAllGLogData(iMachineNumber))
                {
                    lblEstatusSync.Text = "Sincronizacion en proceso";
                    var datos = dispositivo.SSR_GetGeneralLogData(iMachineNumber, out idUsuario, out modoDeVerificacion, out tipoDeRegistro, out anio, out mes, out dia, out hora, out minuto, out segundo, ref codigoDeTrabajo);
                    if (datos)
                    {
                        while (dispositivo.SSR_GetGeneralLogData(iMachineNumber, out idUsuario, out modoDeVerificacion, out tipoDeRegistro, out anio, out mes, out dia, out hora, out minuto, out segundo, ref codigoDeTrabajo))
                        {
                            rfc = GetRFC(idUsuario, modelonegocio);
                            if (!string.IsNullOrEmpty(rfc)) // Verificar si el RFC no está vacío
                            {
                                fecha = formatoFechaCorrecto(anio, mes, dia, hora, minuto, segundo);
                                verificacion = modoDeVerifString(modoDeVerificacion);

                                var query = "INSERT INTO `checador_asistencia`(`RFC`, `Verificacion`, `Fecha_Registro`, `hora_registro`) VALUES (@RFC,@ver,@fecha,@hora)";
                                sql.Open();
                                MySqlCommand command = new MySqlCommand(query, sql);
                                command.Parameters.AddWithValue("@RFC", rfc);
                                command.Parameters.AddWithValue("@ver", verificacion);
                                command.Parameters.AddWithValue("@fecha", "" + anio.ToString() + "-" + mes.ToString() + "-" + dia.ToString() + " " + hora.ToString() + ":" + minuto.ToString() + ":" + segundo.ToString());
                                command.Parameters.AddWithValue("@hora", "" + hora.ToString() + ":" + minuto.ToString() + ":" + segundo.ToString());
                                command.Prepare();
                                command.ExecuteNonQuery();
                                sql.Close();

                                dataGridView1.Rows.Add(num, idUsuario, rfc, verificacion, fecha, hora.ToString() + ":" + minuto.ToString() + ":" + segundo.ToString(), codigoDeTrabajo);
                                num++;
                            }
                        }
                    }
                }

                else
                {
                    //errorList(dispositivo, error); //Chequeo de errores
                    lblEstatusSync.Text = "Sincronizacion Fallada, Sin Datos";
                    resultado = false;
                }
                dispositivo.EnableDevice(iMachineNumber, true);//Habilitar el equipo nuevamente

                if (resultado)
                {

                    if (BorrarRegistros())
                        resultado = true;
                    else
                        resultado = false;


                    resultado = true;
                }

                lblEstatusSync.Text = "";
                Sincronizar.Enabled = true;

            }
            catch (Exception ex)
            {
                lblEstatusSync.Text = "";
                Sincronizar.Enabled = true;

            }


        }


        //**********************************************
        public bool BorrarRegistros()
        {
            bool respuesta = true;
            dispositivo.EnableDevice(iMachineNumber, false); //Deshabilita el equipo para obtener los datos
            if (dispositivo.ClearGLog(iMachineNumber)) //Tambien se puede usar ClearData con el parametro 1 en el tipo de borrado
            {
                dispositivo.RefreshData(iMachineNumber);//the data in the device should be refreshed
            }
            else
            {
                errorList(dispositivo, error); //Chequeo de errores
                respuesta = false;
            }
            dispositivo.EnableDevice(iMachineNumber, true);
            return respuesta;
        }

        #endregion

        private void Sincronizar_Click(object sender, EventArgs e)
        {
             try
            {
                DateTime now = DateTime.Now;
                SincronizarBDWG();
                lblUltimaSync.Text = "Ultima Sincronizacion: " + now.ToString();
                txtSync1.BackColor = Color.Green;
                txtSync2.BackColor = Color.Red;
                //timer2.Interval = tiempoejecucion;
                //timer1.Enabled = false;
                //timer2.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (ComboAgencia.Text)
            {
                case "CYPO":
                    txtIP.Text = "192.168.1.201";
                    break;
                case "Bodega":
                    txtIP.Text = "192.168.1.201";
                    break;
                case "Honda Tijuana":
                    txtIP.Text = "10.10.10.155";
                    break;
                case "Honda ensenada":
                    txtIP.Text = "10.10.112.43";
                    break;
                case "Kia ensenada":
                    txtIP.Text = "192.168.3.150";
                    break;
                case "Kia Ensenada Exterior":
                    txtIP.Text = "186.96.9.212";
                    break;
                case "Kia mexicali":
                    txtIP.Text = "192.168.2.164";
                    break;
                case "Honda mexicali":
                    txtIP.Text = "10.10.11.51";
                    break;
                case "Grupo Optima":
                    txtIP.Text = "192.168.1.184";
                    break;
                case "KIA":
                    txtIP.Text = "192.168.1.201";
                    break;

                default:
                    // Acción por defecto si no se cumple ninguna de las condiciones anteriores
                    break;
            }

        }

        private void limpiardatagrid_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            // Activar el temporizador para ejecutar la función cada 8 horas
              timer.Change(TimeSpan.Zero, TimeSpan.FromHours(4));
          //  timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(10));

            listBox1.BackColor = Color.Green;
            listBox1.ForeColor = Color.White;
            listBox1.Items.Add("Temporizador activado.");
            
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            // Detener el temporizador
            listBox1.BackColor = Color.White;
            listBox1.ForeColor = Color.Black;
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            listBox1.Items.Add("Temporizador detenido.");
        }
    }
}
