using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SitioBase.capaDatos;



/// <summary>
/// Summary description for capaSincronizador
/// </summary>
public class capaSincronizador
{
    public static void IngresarAuditoriaServicioWindows(string asw_descripcion, string asw_Intervalo, DateTime? asw_FechaAnterior, DateTime asw_FechaActual)
    {

        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spIngresarAuditoriaServicioWindows", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paAsw_descripcion = cmdComandoInicio.Parameters.Add("@asw_descripcion", SqlDbType.NVarChar, 500);
        paAsw_descripcion.Value = asw_descripcion;
        SqlParameter paAsw_Intervalo = cmdComandoInicio.Parameters.Add("@asw_Intervalo", SqlDbType.NVarChar, 500);
        paAsw_Intervalo.Value = asw_Intervalo;
        SqlParameter paAsw_FechaAnterior = cmdComandoInicio.Parameters.Add("@asw_FechaAnterior", SqlDbType.DateTime);
        if (asw_FechaAnterior == null)
        {
            paAsw_FechaAnterior.Value = DBNull.Value;
        }
        else
        {
            paAsw_FechaAnterior.Value = asw_FechaAnterior;
        }
        SqlParameter paAsw_FechaActual = cmdComandoInicio.Parameters.Add("@asw_FechaActual", SqlDbType.DateTime);
        paAsw_FechaActual.Value = asw_FechaActual;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarClientes()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }

    public static void SincronizarClientes()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spSincronizarClientes", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarClientes()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarClientes_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spSincronizarClientes_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarClientes_Todos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarTransfers()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spSincronizarTransfers", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarTransfers()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarTransfers_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spSincronizarTransfers_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarTransfers_Todos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    //public static void SincronizarTransfersDetalle()
    //{
    //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
    //    SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spSincronizarTransfersDetalle", Conn);
    //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;
    //    try
    //    {
    //        Conn.Open();
    //        cmdComandoInicio.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarTransfersDetalle()", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (Conn.State == ConnectionState.Open)
    //        {
    //            Conn.Close();
    //        }
    //    }
    //}
    public static void SincronizarProductos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarProductos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarProductos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarProductos_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarProductos_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarProductos_Todos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarOfertas()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarOfertas", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarOfertas()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarOfertas_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarOfertas_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarOfertas_Todos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarPrecios()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarPrecios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarPrecios()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarStocks()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarStocks", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarStocks()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarVales_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spSincronizarVales_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarVales_Todos()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarTransfers_Clientes()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spTransfers_Clientes", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //grabarLog("capaSincronizador.SincronizarTransfers_Clientes()", ex, DateTime.Now);
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarTransfers_Clientes()", ex.Message.ToString());
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void SincronizarTransfers_Clientes_Todos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spTransfers_Clientes_Todos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //grabarLog("capaSincronizador.SincronizarTransfers_Clientes_Todos()", ex, DateTime.Now);
            SitioBase.clases.AccesoDisco.LogError("capaSincronizador.SincronizarTransfers_Clientes_Todos()", ex.Message.ToString());
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