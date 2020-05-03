using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;


public class cMensaje
{
    public int tme_codigo { get; set; }
    public string tme_asunto { get; set; }
    public string tme_mensaje { get; set; }
    public int tme_codClienteDestinatario { get; set; }
    public string cli_nombre { get; set; }
    public int tme_codUsuario { get; set; }
    public DateTime tme_fecha { get; set; }
    public string tme_fechaToString { get; set; }
    public int tme_estado { get; set; }
    public int? tme_todos { get; set; }
    public string est_nombre { get; set; }
    public DateTime? tme_fechaDesde { get; set; }
    public string tme_fechaDesdeToString { get; set; }
    public DateTime? tme_fechaHasta { get; set; }
    public string tme_fechaHastaToString { get; set; }
    public bool tme_importante { get; set; }
    public string tme_importanteToString { get; set; }
    public int? tme_todosSucursales { get; set; }
    public string tmn_todosSucursales { get; set; }
}
public class cMensajeNew
{
    public int tmn_codigo { get; set; }
    public string tmn_asunto { get; set; }
    public string tmn_mensaje { get; set; }
    public DateTime tmn_fecha { get; set; }
    public string tmn_fechaToString { get; set; }
    public DateTime? tmn_fechaDesde { get; set; }
    public string tmn_fechaDesdeToString { get; set; }
    public DateTime? tmn_fechaHasta { get; set; }
    public string tmn_fechaHastaToString { get; set; }
    public bool tmn_importante { get; set; }
    public string tmn_importanteToString { get; set; }
    public string tmn_todosSucursales { get; set; }
}

/// <summary>
/// Summary description for capaMensaje
/// </summary>
public class capaMensaje
{
    public capaMensaje()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable RecuperartTodosMensajesPorIdCliente(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperartTodosMensajesPorIdCliente", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@idClienteDestinatario", SqlDbType.Int);
        paIdCliente.Value = pIdCliente;

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
    public static DataTable RecuperarMensajePorId(int pIdMensaje)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarMensajePorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;

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
    public static void ElimimarTodosMensajePorId(int pIdTodoMensaje)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spElimimarTodosMensajePorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdTodoMensaje = cmdComandoInicio.Parameters.Add("@tme_todos", SqlDbType.Int);
        paIdTodoMensaje.Value = pIdTodoMensaje;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
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
    public static void ElimimarTodosMensajeSucursalesPorId(int pIdTodoSucursales)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spElimimarTodosMensajeSucursalPorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdTodoSucursales = cmdComandoInicio.Parameters.Add("@tme_todosSucursales", SqlDbType.Int);
        paIdTodoSucursales.Value = pIdTodoSucursales;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
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

    public static void ElimimarMensajePorId(int pIdMensaje)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spElimimarMensajePorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();

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
    public static void CambiarEstadoMensajePorId(int pIdMensaje, int pIdEstado)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCambiarEstadoMensajePorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;
        SqlParameter paIdEstado = cmdComandoInicio.Parameters.Add("@estado", SqlDbType.Int);
        paIdEstado.Value = pIdEstado;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
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
    public static DataTable RecuperartTodosMensajes()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperartTodosMensajes", Conn);
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

    public static int InsertarActualizarMensaje(int pIdMensaje, string pAsunto, string pMensaje, int pIdCliente, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spActualizarInsertarMensaje", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;
        SqlParameter paAsunto = cmdComandoInicio.Parameters.Add("@asunto", SqlDbType.NVarChar, 300);
        paAsunto.Value = pAsunto;
        SqlParameter paMensaje = cmdComandoInicio.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
        paMensaje.Value = pMensaje;
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@idClienteDestinatario", SqlDbType.Int);
        paIdCliente.Value = pIdCliente;
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        paIdUsuario.Value = pIdUsuario;
        SqlParameter paIdEstado = cmdComandoInicio.Parameters.Add("@estado", SqlDbType.Int);
        paIdEstado.Value = pIdEstado;

        SqlParameter paFechaDesde = cmdComandoInicio.Parameters.Add("@tme_fechaDesde", SqlDbType.DateTime);
        SqlParameter paFechaHasta = cmdComandoInicio.Parameters.Add("@tme_fechaHasta", SqlDbType.DateTime);
        SqlParameter paImportante = cmdComandoInicio.Parameters.Add("@tme_importante", SqlDbType.Bit);
        if (pFechaDesde == null)
        {
            paFechaDesde.Value = DBNull.Value;
        }
        else
        {
            paFechaDesde.Value = pFechaDesde;
        }
        if (pFechaHasta == null)
        {
            paFechaHasta.Value = DBNull.Value;
        }
        else
        {
            paFechaHasta.Value = pFechaHasta;
        }
        paImportante.Value = pImportante;

        try
        {
            Conn.Open();
            return Convert.ToInt32(cmdComandoInicio.ExecuteScalar());
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
    public static bool InsertarMensajeParaTodosClientes(int pIdMensaje, string pAsunto, string pMensaje, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, int tme_todos)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spInsertarActualizarMensajeParaTodosClientes", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;
        SqlParameter paAsunto = cmdComandoInicio.Parameters.Add("@asunto", SqlDbType.NVarChar, 300);
        paAsunto.Value = pAsunto;
        SqlParameter paMensaje = cmdComandoInicio.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
        paMensaje.Value = pMensaje;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        paIdUsuario.Value = pIdUsuario;
        SqlParameter paIdEstado = cmdComandoInicio.Parameters.Add("@estado", SqlDbType.Int);
        paIdEstado.Value = pIdEstado;

        SqlParameter paFechaDesde = cmdComandoInicio.Parameters.Add("@tme_fechaDesde", SqlDbType.DateTime);
        SqlParameter paFechaHasta = cmdComandoInicio.Parameters.Add("@tme_fechaHasta", SqlDbType.DateTime);
        SqlParameter paImportante = cmdComandoInicio.Parameters.Add("@tme_importante", SqlDbType.Bit);
        SqlParameter paTme_todos = cmdComandoInicio.Parameters.Add("@tme_todos", SqlDbType.Int);

        if (pFechaDesde == null)
        {
            paFechaDesde.Value = DBNull.Value;
        }
        else
        {
            paFechaDesde.Value = pFechaDesde;
        }
        if (pFechaHasta == null)
        {
            paFechaHasta.Value = DBNull.Value;
        }
        else
        {
            paFechaHasta.Value = pFechaHasta;
        }
        paImportante.Value = pImportante;
        paTme_todos.Value = tme_todos;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static bool InsertarActualizarMensajeParaTodasSucursales(int pIdMensaje, string pAsunto, string pMensaje, int pIdUsuario, int pIdEstado, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, int tme_todosSucursales, List<string> pListaSucursal)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spInsertarActualizarMensajeParaTodasSucursales", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_codigo", SqlDbType.Int);
        paIdMensaje.Value = pIdMensaje;
        SqlParameter paAsunto = cmdComandoInicio.Parameters.Add("@asunto", SqlDbType.NVarChar, 300);
        paAsunto.Value = pAsunto;
        SqlParameter paMensaje = cmdComandoInicio.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
        paMensaje.Value = pMensaje;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        paIdUsuario.Value = pIdUsuario;
        SqlParameter paIdEstado = cmdComandoInicio.Parameters.Add("@estado", SqlDbType.Int);
        paIdEstado.Value = pIdEstado;

        SqlParameter paFechaDesde = cmdComandoInicio.Parameters.Add("@tme_fechaDesde", SqlDbType.DateTime);
        SqlParameter paFechaHasta = cmdComandoInicio.Parameters.Add("@tme_fechaHasta", SqlDbType.DateTime);
        SqlParameter paImportante = cmdComandoInicio.Parameters.Add("@tme_importante", SqlDbType.Bit);
        SqlParameter paTme_todosSucursales = cmdComandoInicio.Parameters.Add("@tme_todosSucursales", SqlDbType.Int);
        SqlParameter paArray_codsuc = cmdComandoInicio.Parameters.Add("@array_codsuc", SqlDbType.NVarChar, -1);
        string arraySucursal = string.Empty;
        if (pListaSucursal != null)
            for (int i = 0; i < pListaSucursal.Count(); i++)
            {
                if (string.IsNullOrEmpty(arraySucursal))
                    arraySucursal += pListaSucursal[i];
                else
                    arraySucursal += "," + pListaSucursal[i];
            }
        paArray_codsuc.Value = arraySucursal;

        if (pFechaDesde == null)
        {
            paFechaDesde.Value = DBNull.Value;
        }
        else
        {
            paFechaDesde.Value = pFechaDesde;
        }
        if (pFechaHasta == null)
        {
            paFechaHasta.Value = DBNull.Value;
        }
        else
        {
            paFechaHasta.Value = pFechaHasta;
        }
        paImportante.Value = pImportante;
        paTme_todosSucursales.Value = tme_todosSucursales;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static DataTable ObtenerTodasSucursalesPorMensajeSucursalId(int pTodosSucursales)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spObtenerTodasSucursalesPorMensajeSucursalId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@tme_todosSucursales", SqlDbType.Int);
        paIdMensaje.Value = pTodosSucursales;

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

}