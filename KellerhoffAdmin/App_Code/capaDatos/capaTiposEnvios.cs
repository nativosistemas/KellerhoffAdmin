using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SitioBase.capaDatos;

public class cTiposEnvios
{
    public cTiposEnvios()
    {
    }
    public cTiposEnvios(int id, string nombreSucursal) {
        env_id = id;
        env_codigo = string.Empty;
        env_nombre = nombreSucursal;
    }
    public int env_id { get; set; }
    public string env_codigo { get; set; }
    public string env_nombre { get; set; }
}
public class cTiposEnviosSucursal : cSucursal
{
    public int tes_id { get; set; }
    public int tes_idTipoEnvio { get; set; }
    public string tes_idSucursal { get; set; }

    public int env_id { get; set; }
    public string env_codigo { get; set; }
    public string env_nombre { get; set; }
}
public class cSucursalDependienteTipoEnviosCliente
{
    public int tsd_id { get; set; }
    public int tsd_idSucursalDependiente { get; set; }
    public int? tsd_idTipoEnvioCliente { get; set; }
    public int sde_codigo { get; set; }
    public string sde_sucursal { get; set; }
    public string sde_sucursalDependiente { get; set; }
    public int env_id { get; set; }
    public string env_codigo { get; set; }
    public string env_nombre { get; set; }
    List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTiposEnvios { get; set; }
}
public class cSucursalDependienteTipoEnviosCliente_TiposEnvios
{
    public int tdt_id { get; set; }
    public int tdt_idSucursalDependienteTipoEnvioCliente { get; set; }
    public int tdt_idTipoEnvio { get; set; }
    public int env_id { get; set; }
    public string env_codigo { get; set; }
    public string env_nombre { get; set; }
}
public class cCadeteriaRestricciones
{
    public int tcr_id { get; set; }
    public string tcr_codigoSucursal { get; set; }
    public int tcr_UnidadesMinimas { get; set; }
    public int tcr_UnidadesMaximas { get; set; }
    public double tcr_MontoMinimo { get; set; }
    public double tcr_MontoIgnorar { get; set; }
    public string suc_nombre { get; set; }
}
/// <summary>
/// Summary description for capaTiposEnvios
/// </summary>
public class capaTiposEnvios
{
    public static int InsertarActualizarTiposEnvios(int pEnv_id, string pEnv_codigo, string pEnv_nombre)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spInsertarActualizarTiposEnvios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paEnv_id = cmdComandoInicio.Parameters.Add("@env_id", SqlDbType.Int);
        SqlParameter paEnv_codigo = cmdComandoInicio.Parameters.Add("@env_codigo", SqlDbType.NVarChar, 2);
        SqlParameter paEnv_nombre = cmdComandoInicio.Parameters.Add("@env_nombre", SqlDbType.NVarChar, 50);

        paEnv_id.Value = pEnv_id;
        paEnv_codigo.Value = pEnv_codigo;
        paEnv_nombre.Value = pEnv_nombre;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteScalar();
            return Convert.ToInt32(objResultado);
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
    public static void EliminarTiposEnvios(int pEnv_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spEliminarTiposEnvios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paEnv_id = cmdComandoInicio.Parameters.Add("@env_id", SqlDbType.Int);
        paEnv_id.Value = pEnv_id;

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
    public static DataTable RecuperarTodosTiposEnvios()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spRecuperarTodosTiposEnvios", Conn);
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
    public static int InsertarActualizarSucursalDependienteTipoEnvioCliente(int pTsd_id, int pTsd_idSucursalDependiente, int? pTsd_idTipoEnvioCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spInsertarActualizarSucursalDependienteTipoEnvioCliente", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTsd_id = cmdComandoInicio.Parameters.Add("@tsd_id", SqlDbType.Int);
        SqlParameter paTsd_idSucursalDependiente = cmdComandoInicio.Parameters.Add("@tsd_idSucursalDependiente", SqlDbType.Int);
        SqlParameter paTsd_idTipoEnvioCliente = cmdComandoInicio.Parameters.Add("@tsd_idTipoEnvioCliente", SqlDbType.Int);

        paTsd_id.Value = pTsd_id;
        paTsd_idSucursalDependiente.Value = pTsd_idSucursalDependiente;
        if (pTsd_idTipoEnvioCliente == null)
        {
            paTsd_idTipoEnvioCliente.Value = DBNull.Value;
        }
        else
        {
            paTsd_idTipoEnvioCliente.Value = pTsd_idTipoEnvioCliente;
        }

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteScalar();
            return Convert.ToInt32(objResultado);
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
    public static void EliminarSucursalDependienteTipoEnvioCliente(int pTsd_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spEliminarSucursalDependienteTipoEnvioCliente", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTsd_id = cmdComandoInicio.Parameters.Add("@tsd_id", SqlDbType.Int);
        paTsd_id.Value = pTsd_id;

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
    public static DataTable RecuperarTodosSucursalDependienteTipoEnvioCliente()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spRecuperarTodosSucursalDependienteTipoEnvioCliente", Conn);
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
    ////////////////////////////////
    public static int InsertarActualizarSucursalDependienteTipoEnvioCliente_TiposEnvios(int pTdt_id, int pTdt_idSucursalDependienteTipoEnvioCliente, int pTdt_idTipoEnvio)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spInsertarActualizarSucursalDependienteTipoEnvioCliente_TipoEnvios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTdt_id = cmdComandoInicio.Parameters.Add("@tdt_id", SqlDbType.Int);
        SqlParameter paTdt_idSucursalDependienteTipoEnvioCliente = cmdComandoInicio.Parameters.Add("@tdt_idSucursalDependienteTipoEnvioCliente", SqlDbType.Int);
        SqlParameter paTdt_idTipoEnvio = cmdComandoInicio.Parameters.Add("@tdt_idTipoEnvio", SqlDbType.Int);

        paTdt_id.Value = pTdt_id;
        paTdt_idSucursalDependienteTipoEnvioCliente.Value = pTdt_idSucursalDependienteTipoEnvioCliente;
        paTdt_idTipoEnvio.Value = pTdt_idTipoEnvio;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteScalar();
            return Convert.ToInt32(objResultado);
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
    public static void EliminarSucursalDependienteTipoEnvioCliente_TiposEnvios(int pTdt_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spEliminarSucursalDependienteTipoEnvioCliente_TipoEnvios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTdt_id = cmdComandoInicio.Parameters.Add("@tdt_id", SqlDbType.Int);
        paTdt_id.Value = pTdt_id;

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
    public static DataTable RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spRecuperarTodosSucursalDependienteTipoEnvioCliente_TipoEnvios", Conn);
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
    ///////////
    public static int InsertarActualizarCadeteriaRestricciones(int pTcr_id, string pTcr_codigoSucursal, int pTcr_UnidadesMinimas, int pTcr_UnidadesMaximas, double pTcr_MontoMinimo, double pTcr_MontoIgnorar)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spInsertarActualizarCadeteriaRestricciones", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTcr_id = cmdComandoInicio.Parameters.Add("@tcr_id", SqlDbType.Int);
        SqlParameter paTcr_codigoSucursal = cmdComandoInicio.Parameters.Add("@tcr_codigoSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paTcr_UnidadesMinimas = cmdComandoInicio.Parameters.Add("@tcr_UnidadesMinimas", SqlDbType.Int);
        SqlParameter paTcr_UnidadesMaximas = cmdComandoInicio.Parameters.Add("@tcr_UnidadesMaximas", SqlDbType.Int);
        SqlParameter paTcr_MontoMinimo = cmdComandoInicio.Parameters.Add("@tcr_MontoMinimo", SqlDbType.Decimal,20);
        SqlParameter paTcr_MontoIgnorar = cmdComandoInicio.Parameters.Add("@tcr_MontoIgnorar", SqlDbType.Decimal, 20);

        paTcr_id.Value = pTcr_id;
        paTcr_codigoSucursal.Value = pTcr_codigoSucursal;
        paTcr_UnidadesMinimas.Value = pTcr_UnidadesMinimas;
        paTcr_UnidadesMaximas.Value = pTcr_UnidadesMaximas;
        paTcr_MontoMinimo.Value = pTcr_MontoMinimo;
        paTcr_MontoIgnorar.Value = pTcr_MontoIgnorar;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteScalar();
            return Convert.ToInt32(objResultado);
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
    public static void EliminarCadeteriaRestricciones(int pTcr_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spEliminarCadeteriaRestricciones", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTcr_id = cmdComandoInicio.Parameters.Add("@tcr_id", SqlDbType.Int);
        paTcr_id.Value = pTcr_id;

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
    public static DataTable RecuperarTodosCadeteriaRestricciones()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("TiposEnvios.spRecuperarTodosCadeteriaRestricciones", Conn);
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
}