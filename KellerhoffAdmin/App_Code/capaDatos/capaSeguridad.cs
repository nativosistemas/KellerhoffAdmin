using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;

namespace SitioBase.capaDatos
{
    public class Usuario
    {
        public Usuario()
        {
            id = -1;
            //User = string.Empty;
            NombreYApellido = string.Empty;
            idUsuarioLog = -1;
        }
        public int id
        {
            get;
            set;
        }
        public int idRol
        {
            get;
            set;
        }
        public string NombreYApellido
        {
            get;
            set;
        }
        public string usu_pswDesencriptado
        {
            get;
            set;
        }
        public int idUsuarioLog
        {
            get;
            set;
        }
        public int usu_estado
        {
            get;
            set;
        }
        public int? usu_codCliente
        {
            get;
            set;
        }
    }
    public class cUsuario
    {
        public int usu_codigo { get; set; }
        public int usu_codRol { get; set; }
        public int? usu_codCliente { get; set; }
        public string rol_Nombre { get; set; }
        public string NombreYapellido { get; set; }
        public string usu_nombre { get; set; }
        public string usu_apellido { get; set; }
        public string usu_login { get; set; }
        public string usu_mail { get; set; }
        public string usu_pswDesencriptado { get; set; }
        public string usu_observacion { get; set; }
        public int usu_estado { get; set; }
        public string usu_estadoToString { get; set; }
        public string cli_nombre { get; set; }
    }
    public class cRegla
    {
        public cRegla()
        {

        }
        public int rgl_codRegla { get; set; }
        public string rgl_Descripcion { get; set; }
        public string rgl_PalabraClave { get; set; }
        public bool? rgl_IsAgregarSoporta { get; set; }
        public bool? rgl_IsEditarSoporta { get; set; }
        public bool? rgl_IsEliminarSoporta { get; set; }
        public int? rgl_codReglaPadre { get; set; }
    }
    public class cRol
    {
        public int rol_codRol { get; set; }
        public string rol_Nombre { get; set; }
    }
    public class AcccionesRol
    {
        public int idRegla
        {
            get;
            set;
        }
        public int? idReglaRol
        {
            get;
            set;
        }
        public string palabraClave
        {
            get;
            set;
        }
        public bool isActivo
        {
            get;
            set;
        }
        public bool isAgregar
        {
            get;
            set;
        }
        public bool isEditar
        {
            get;
            set;
        }
        public bool isEliminar
        {
            get;
            set;
        }

    }
    public class ListaAcccionesRol
    {
        public ListaAcccionesRol()
        {
            lista = new List<AcccionesRol>();
        }
        public List<AcccionesRol> lista { get; set; }

        public void Agregar(AcccionesRol pAcccionesRol)
        {
            lista.Add(pAcccionesRol);
        }
        public AcccionesRol Buscar(string pPalabraClave)
        {
            AcccionesRol resultado = new AcccionesRol();
            foreach (AcccionesRol item in lista)
            {
                if (item.palabraClave == pPalabraClave)
                {
                    resultado = item;
                    break;
                }
            }
            return resultado;
        }
        public bool isActivo(string pPalabraClave)
        {
            return Buscar(pPalabraClave).isActivo;
        }
        public bool isAgregar(string pPalabraClave)
        {
            return Buscar(pPalabraClave).isAgregar;
        }
        public bool isEditar(string pPalabraClave)
        {
            return Buscar(pPalabraClave).isEditar;
        }
        public bool isEliminar(string pPalabraClave)
        {
            return Buscar(pPalabraClave).isEliminar;
        }
    }
    [DataContract]
    public class ListaCheck
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string palabra { get; set; }
        [DataMember]
        public int checkAgregar { get; set; }
        [DataMember]
        public int checkEditar { get; set; }
        [DataMember]
        public int checkEliminar { get; set; }
        [DataMember]
        public int? idPadreRegla { get; set; }
        [DataMember]
        public List<int> listaIdPadre { get; set; }
        [DataMember]
        public List<int> listaIdHijas { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public bool isGraficada { get; set; }
    }
    [DataContract]
    public class ReglaAGrabar
    {
        public int idRelacionReglaRol { get; set; }
        public int idRegla { get; set; }
        public bool isActivo { get; set; }
        public bool? isAgregado { get; set; }
        public bool? isEditado { get; set; }
        public bool? isEliminado { get; set; }
    }
    [DataContract]
    public class ReglaPorRol
    {
        [DataMember]
        public int idRegla { get; set; }
        [DataMember]
        public int idRelacionReglaRol { get; set; }
        [DataMember]
        public bool isActivo { get; set; }
        [DataMember]
        public bool? isAgregar { get; set; }
        [DataMember]
        public bool? isEditar { get; set; }
        [DataMember]
        public bool? isEliminar { get; set; }
    }
    public class capaSeguridad
    {
        public static DataSet Login(string pNombreLogin, string pPassword, string pIp, string pHostName, string pUserAgent)
        {

            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spInicioSession", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paLogin = cmdComandoInicio.Parameters.Add("@login", SqlDbType.NVarChar, 255);
            SqlParameter paPassword = cmdComandoInicio.Parameters.Add("@Password", SqlDbType.NVarChar, 255);
            SqlParameter paIp = cmdComandoInicio.Parameters.Add("@Ip", SqlDbType.NVarChar, 255);
            SqlParameter paHost = cmdComandoInicio.Parameters.Add("@Host", SqlDbType.NVarChar, 255);
            SqlParameter paUserName = cmdComandoInicio.Parameters.Add("@UserName", SqlDbType.NVarChar, 255);

            paLogin.Value = pNombreLogin;
            paPassword.Value = pPassword;
            paIp.Value = pIp;
            paHost.Value = pHostName;
            paUserName.Value = pUserAgent;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Login");
                return dsResultado;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static void CerrarSession(int pIdUsuarioLog)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spCerrarSession", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdUsuarioLog = cmdComandoInicio.Parameters.Add("@IdUsuarioLog", SqlDbType.Int);
            paIdUsuarioLog.Value = pIdUsuarioLog;

            try
            {
                Conn.Open();
                int count = cmdComandoInicio.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }

        }
        public static DataSet GestiónRol(int? rol_codRol, string rol_Nombre, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionRol", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paRol_codRol = cmdComandoInicio.Parameters.Add("@rol_codRol", SqlDbType.Int);
            SqlParameter paRol_Nombre = cmdComandoInicio.Parameters.Add("@rol_Nombre", SqlDbType.NVarChar, 80);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (rol_codRol == null)
            {
                paRol_codRol.Value = DBNull.Value;
            }
            else
            {
                paRol_codRol.Value = rol_codRol;
            }
            if (rol_Nombre == null)
            {
                paRol_Nombre.Value = DBNull.Value;
            }
            else
            {
                paRol_Nombre.Value = rol_Nombre;
            }
            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }
            paAccion.Value = accion;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Rol");
                return dsResultado;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataSet GestiónRegla(int? rgl_codRegla, string rgl_Descripcion, string rgl_PalabraClave, bool? rgl_IsAgregarSoporta, bool? rgl_IsEditarSoporta, bool? rgl_IsEliminarSoporta, int? rgl_codReglaPadre, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionRegla", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;


            SqlParameter paRgl_codRegla = cmdComandoInicio.Parameters.Add("@rgl_codRegla", SqlDbType.Int);
            SqlParameter paRgl_Descripcion = cmdComandoInicio.Parameters.Add("@rgl_Descripcion", SqlDbType.NVarChar, 250);
            SqlParameter paRgl_PalabraClave = cmdComandoInicio.Parameters.Add("@rgl_PalabraClave", SqlDbType.NVarChar, 50);
            SqlParameter paRgl_IsAgregarSoporta = cmdComandoInicio.Parameters.Add("@rgl_IsAgregarSoporta", SqlDbType.Bit);
            SqlParameter paRgl_IsEditarSoporta = cmdComandoInicio.Parameters.Add("@rgl_IsEditarSoporta", SqlDbType.Bit);
            SqlParameter paRgl_IsEliminarSoporta = cmdComandoInicio.Parameters.Add("@rgl_IsEliminarSoporta", SqlDbType.Bit);
            SqlParameter paRgl_codReglaPadre = cmdComandoInicio.Parameters.Add("@rgl_codReglaPadre", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (rgl_codRegla == null)
            {
                paRgl_codRegla.Value = DBNull.Value;
            }
            else
            {
                paRgl_codRegla.Value = rgl_codRegla;
            }
            if (rgl_Descripcion == null)
            {
                paRgl_Descripcion.Value = DBNull.Value;
            }
            else
            {
                paRgl_Descripcion.Value = rgl_Descripcion;
            }
            if (rgl_PalabraClave == null)
            {
                paRgl_PalabraClave.Value = DBNull.Value;
            }
            else
            {
                paRgl_PalabraClave.Value = rgl_PalabraClave;
            }
            if (rgl_IsAgregarSoporta == null)
            {
                paRgl_IsAgregarSoporta.Value = DBNull.Value;
            }
            else
            {
                paRgl_IsAgregarSoporta.Value = rgl_IsAgregarSoporta;
            }
            if (rgl_IsEditarSoporta == null)
            {
                paRgl_IsEditarSoporta.Value = DBNull.Value;
            }
            else
            {
                paRgl_IsEditarSoporta.Value = rgl_IsEditarSoporta;
            }
            if (rgl_IsEliminarSoporta == null)
            {
                paRgl_IsEliminarSoporta.Value = DBNull.Value;
            }
            else
            {
                paRgl_IsEliminarSoporta.Value = rgl_IsEliminarSoporta;
            }
            if (rgl_codReglaPadre == null)
            {
                paRgl_codReglaPadre.Value = DBNull.Value;
            }
            else
            {
                paRgl_codReglaPadre.Value = rgl_codReglaPadre;
            }
            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }
            //if (accion == null)
            //{
            //    paAccion.Value = DBNull.Value;
            //}
            //else
            //{
            paAccion.Value = accion;
            //}

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Regla");
                return dsResultado;
                //switch ( accion)
                //{
                //    case "INSERT":
                //        SqlDataAdapter da = new SqlDataAdapter( cmdComandoInicio);
                //        da.Fill(dsResultado, "Regla");
                //        return dsResultado;
                //        break;
                //    case "SELECT":
                //        SqlDataAdapter da = new SqlDataAdapter( cmdComandoInicio);
                //        da.Fill(dsResultado, "Regla");
                //        return dsResultado;
                //        break;
                //    default:
                //        return null;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataTable RecuperarTodasReglasPorNivel()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarTodasReglasPorNiveles", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataTable RecuperarTodasAccionesRol(int pIdRol)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarAccionesUsuario", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdRol = cmdComandoInicio.Parameters.Add("@IdRol", SqlDbType.Int);
            paIdRol.Direction = ParameterDirection.Input;

            paIdRol.Value = pIdRol;

            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataSet GestiónRoleRegla(int? rrr_codRol, int? rrr_codRegla, string pXML, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionRelacionRoleRegla", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paCodRol = cmdComandoInicio.Parameters.Add("@rrr_codRol", SqlDbType.Int);
            SqlParameter paCodRegla = cmdComandoInicio.Parameters.Add("@rrr_codRegla", SqlDbType.Int);
            SqlParameter paStrXML = cmdComandoInicio.Parameters.Add("@strXML", SqlDbType.Xml);
            //SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (rrr_codRol == null)
            {
                paCodRol.Value = DBNull.Value;
            }
            else
            {
                paCodRol.Value = rrr_codRol;
            }
            if (rrr_codRegla == null)
            {
                paCodRegla.Value = DBNull.Value;
            }
            else
            {
                paCodRegla.Value = rrr_codRegla;
            }
            if (pXML == null)
            {
                paStrXML.Value = DBNull.Value;
            }
            else
            {
                paStrXML.Value = pXML;
            }
            //if (filtro == null)
            //{
            //    paFiltro.Value = DBNull.Value;
            //}
            //else
            //{
            //    paFiltro.Value = filtro;
            //}
            paAccion.Value = accion;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "RelacionRoleRegla");
                return dsResultado;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataSet GestiónUsuario(int? usu_codigo, int? usu_codRol, int? usu_codCliente, string usu_nombre, string usu_apellido, string usu_mail, string usu_login, string usu_psw, string usu_observacion, int? usu_codUsuarioUltMov, int? usu_codAccion, int? usu_estado, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionUsuario", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paUsu_codigo = cmdComandoInicio.Parameters.Add("@usu_codigo", SqlDbType.Int);
            SqlParameter paUsu_codRol = cmdComandoInicio.Parameters.Add("@usu_codRol", SqlDbType.Int);
            SqlParameter paUsu_codCliente = cmdComandoInicio.Parameters.Add("@usu_codCliente", SqlDbType.Int);
            SqlParameter paUsu_nombre = cmdComandoInicio.Parameters.Add("@usu_nombre", SqlDbType.NVarChar, 50);
            SqlParameter paUsu_apellido = cmdComandoInicio.Parameters.Add("@usu_apellido", SqlDbType.NVarChar, 50);
            SqlParameter paUsu_mail = cmdComandoInicio.Parameters.Add("@usu_mail", SqlDbType.NVarChar, 255);
            SqlParameter paUsu_login = cmdComandoInicio.Parameters.Add("@usu_login", SqlDbType.NVarChar, 255);
            SqlParameter paUsu_psw = cmdComandoInicio.Parameters.Add("@usu_psw", SqlDbType.NVarChar, 255);// SqlDbType.VarBinary, 255
            SqlParameter paUsu_observacion = cmdComandoInicio.Parameters.Add("@usu_observacion", SqlDbType.NVarChar, -1);
            SqlParameter paUsu_codUsuarioUltMov = cmdComandoInicio.Parameters.Add("@usu_codUsuarioUltMov", SqlDbType.Int);
            SqlParameter paUsu_codAccion = cmdComandoInicio.Parameters.Add("@usu_codAccion", SqlDbType.Int);
            SqlParameter paUsu_estado = cmdComandoInicio.Parameters.Add("@usu_estado", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (usu_codigo == null)
            {
                paUsu_codigo.Value = DBNull.Value;
            }
            else
            {
                paUsu_codigo.Value = usu_codigo;
            }
            if (usu_codRol == null)
            {
                paUsu_codRol.Value = DBNull.Value;
            }
            else
            {
                paUsu_codRol.Value = usu_codRol;
            }
            if (usu_codCliente == null)
            {
                paUsu_codCliente.Value = DBNull.Value;
            }
            else
            {
                paUsu_codCliente.Value = usu_codCliente;
            }
            if (usu_nombre == null)
            {
                paUsu_nombre.Value = DBNull.Value;
            }
            else
            {
                paUsu_nombre.Value = usu_nombre;
            }
            if (usu_apellido == null)
            {
                paUsu_apellido.Value = DBNull.Value;
            }
            else
            {
                paUsu_apellido.Value = usu_apellido;
            }
            if (usu_mail == null)
            {
                paUsu_mail.Value = DBNull.Value;
            }
            else
            {
                paUsu_mail.Value = usu_mail;
            }
            if (usu_login == null)
            {
                paUsu_login.Value = DBNull.Value;
            }
            else
            {
                paUsu_login.Value = usu_login;
            }
            if (usu_psw == null)
            {
                paUsu_psw.Value = DBNull.Value;
            }
            else
            {
                paUsu_psw.Value = usu_psw;
            }
            if (usu_observacion == null)
            {
                paUsu_observacion.Value = DBNull.Value;
            }
            else
            {
                paUsu_observacion.Value = usu_observacion;
            }
            if (usu_estado == null)
            {
                paUsu_estado.Value = DBNull.Value;
            }
            else
            {
                paUsu_estado.Value = usu_estado;
            }
            if (usu_codUsuarioUltMov == null)
            {
                paUsu_codUsuarioUltMov.Value = DBNull.Value;
            }
            else
            {
                paUsu_codUsuarioUltMov.Value = usu_codUsuarioUltMov;
            }
            if (usu_codAccion == null)
            {
                paUsu_codAccion.Value = DBNull.Value;
            }
            else
            {
                paUsu_codAccion.Value = usu_codAccion;
            }
            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }
            paAccion.Value = accion;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Usuario");
                return dsResultado;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static string obtenerStringEstado(int pIdEstado)
        {
            string resultado = string.Empty;
            switch (pIdEstado.ToString())
            {
                case "1":
                    return Constantes.cESTADO_STRING_SINESTADO;
                case "2":
                    return Constantes.cESTADO_STRING_ACTIVO;
                case "3":
                    return Constantes.cESTADO_STRING_INACTIVO;
                default:
                    break;
            }
            return resultado;
        }
        public static int CambiarContraseñaPersonal(int pUsu_codigo, string pPasswordViejo, string pPasswordNuevo)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spCambiarContraseñaPersonal", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paUsu_codigo = cmdComandoInicio.Parameters.Add("@usu_codigo", SqlDbType.Int);
            SqlParameter paPasswordViejo = cmdComandoInicio.Parameters.Add("@PasswordViejo", SqlDbType.NVarChar, 255);
            SqlParameter paPasswordNuevo = cmdComandoInicio.Parameters.Add("@PasswordNuevo", SqlDbType.NVarChar, 255);
            SqlParameter paUsu_codAccion = cmdComandoInicio.Parameters.Add("@usu_codAccion", SqlDbType.Int);

            paUsu_codigo.Value = pUsu_codigo;
            paPasswordViejo.Value = pPasswordViejo;
            paPasswordNuevo.Value = pPasswordNuevo;
            paUsu_codAccion.Value = Constantes.cACCION_CAMBIOCONTRASEÑA;

            try
            {
                Conn.Open();
                object resultado = cmdComandoInicio.ExecuteScalar();
                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static DataTable IsRepetidoLogin(int usu_codigo, string usu_login)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spIsRepetidoLogin", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paUsu_codigo = cmdComandoInicio.Parameters.Add("@usu_codigo", SqlDbType.Int);
            SqlParameter paUsu_login = cmdComandoInicio.Parameters.Add("@usu_login", SqlDbType.NVarChar, 255);

            paUsu_codigo.Value = usu_codigo;
            paUsu_login.Value = usu_login;
   
            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataTable RecuperarTablaBandera(string ban_codigo)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spEstadoBandera", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paBan_codigo = cmdComandoInicio.Parameters.Add("@ban_codigo", SqlDbType.NVarChar, 50);
            paBan_codigo.Value = ban_codigo;

            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static DataTable RecuperarSinPermisoUsuarioIntranetPorIdUsuario(int pIdUsuario)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarSinPermisoUsuarioIntranetPorIdUsuario", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
            paIdUsuario.Direction = ParameterDirection.Input;

            paIdUsuario.Value = pIdUsuario;

            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static int InsertarSinPermisoUsuarioIntranetPorIdUsuario(int pIdUsuario, DataTable pTablaNombresSeccion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spInsertarSinPermisoUsuarioIntranet", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
            SqlParameter paTablaNombresSeccion = cmdComandoInicio.Parameters.Add("@Tabla_NombreSeccion", SqlDbType.Structured);

            paIdUsuario.Value = pIdUsuario;
            paTablaNombresSeccion.Value = pTablaNombresSeccion;

            try
            {
                Conn.Open();
                int count =  Convert.ToInt32(cmdComandoInicio.ExecuteScalar());
                return count;

            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
    }
}