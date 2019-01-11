using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;

/// <summary>
/// Summary description for CapaCarritoDiferido
/// </summary>
public class CapaCarritoDiferido
{
    public static DataSet RecuperarCarritosDiferidosPorCliente(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarCarritosDiferidos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@rcd_codCliente", SqlDbType.Int);

        paIdCliente.Value = pIdCliente;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "CarritosDiferido");
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
    public static bool CargarCarritoDiferido(string pSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoDiferido", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@rcd_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@rcd_codCliente", SqlDbType.Int);
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@rdd_codUsuario", SqlDbType.Int);
        SqlParameter paIdProducto = cmdComandoInicio.Parameters.Add("@rdd_codProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidad = cmdComandoInicio.Parameters.Add("@rdd_cantidad", SqlDbType.Int);
        SqlParameter paSumar = cmdComandoInicio.Parameters.Add("@isOk", SqlDbType.Bit);
        paSumar.Direction = ParameterDirection.Output;

        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        paSucursal.Value = pSucursal;
        paIdCliente.Value = pIdCliente;
        paIdProducto.Value = pIdProducto;
        paCantidad.Value = pCantidadProducto;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            return Convert.ToBoolean(paSumar.Value);
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
    public static void BorrarCarritosDiferidos(int pIdCarrito)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spBorrarCarritosDiferidos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paIdCarrito = cmdComandoInicio.Parameters.Add("@rcd_id", SqlDbType.Int);
        paIdCarrito.Value = pIdCarrito;
        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteNonQuery();
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
}