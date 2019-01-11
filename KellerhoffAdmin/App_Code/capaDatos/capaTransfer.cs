using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;

public class cTransfer
{
    public int tfr_codigo { get; set; }
    public string tfr_accion { get; set; }
    public string tfr_nombre { get; set; }
    public bool tfr_deshab { get; set; }
    public decimal? tfr_pordesadi { get; set; }
    public string tfr_tipo { get; set; }
    public bool tfr_mospap { get; set; }
    public int? tfr_minrenglones { get; set; }
    public int? tfr_minunidades { get; set; }
    public int? tfr_maxunidades { get; set; }
    public int? tfr_mulunidades { get; set; }
    public int? tfr_fijunidades { get; set; }
    public bool? tfr_facturaciondirecta { get; set; }
    public string tfr_descripcion { get; set; }
    public string tfr_provincia { get; set; }
public List<cTransferDetalle> listaDetalle { get; set; }

}
public class cTransferDetalle : cProductos
{
    public cTransferDetalle()
    {
        listaSucursalStocks = new List<cSucursalStocks>();
    }
    public int tde_codtfr { get; set; }
    public string tde_codpro { get; set; }
    public string tde_descripcion { get; set; }
    public decimal? tde_prepublico { get; set; }
    public decimal? tde_predescuento { get; set; }
    public int? tde_minuni { get; set; }
    public int? tde_maxuni { get; set; }
    public int? tde_muluni { get; set; }
    public int? tde_fijuni { get; set; }
    public bool tde_proobligatorio { get; set; }
    public decimal PrecioFinalTransfer { get; set; }
    public int? tde_unidadesbonificadas { get; set; }
    public string tde_unidadesbonificadasdescripcion { get; set; }
    //public string stk_stock { get; set; }
    public List<cSucursalStocks> listaSucursalStocks { get; set; }
    //public void CargarTransferDetalle(cTransferDetalle pValor)
    //{
    //    this.CargarTransferDetalle(pValor);
    //}
    //

    public int tfr_codigo { get; set; }
    public string tfr_accion { get; set; }
    public string tfr_nombre { get; set; }
    public bool tfr_deshab { get; set; }
    public decimal? tfr_pordesadi { get; set; }
    public string tfr_tipo { get; set; }
    public bool tfr_mospap { get; set; }
    public int? tfr_minrenglones { get; set; }
    public int? tfr_minunidades { get; set; }
    public int? tfr_maxunidades { get; set; }
    public int? tfr_mulunidades { get; set; }
    public int? tfr_fijunidades { get; set; }
    public bool? tfr_facturaciondirecta { get; set; }
    public string tfr_descripcion { get; set; }
    public string tfr_provincia { get; set; }
    public void CargarTransfer(cTransfer pValor)
    {
        tfr_codigo = pValor.tfr_codigo;
        tfr_accion = pValor.tfr_accion;
        tfr_nombre = pValor.tfr_nombre;
        tfr_deshab = pValor.tfr_deshab;
        tfr_pordesadi = pValor.tfr_pordesadi;
        tfr_tipo = pValor.tfr_tipo;
        tfr_mospap = pValor.tfr_mospap;
        tfr_minrenglones = pValor.tfr_minrenglones;
        tfr_minunidades = pValor.tfr_minunidades;
        tfr_maxunidades = pValor.tfr_maxunidades;
        tfr_mulunidades = pValor.tfr_mulunidades;
        tfr_fijunidades = pValor.tfr_fijunidades;
        tfr_facturaciondirecta = pValor.tfr_facturaciondirecta;
        tfr_descripcion = pValor.tfr_descripcion;
        tfr_provincia = pValor.tfr_provincia;
    }
}

/// <summary>
/// Summary description for capaTransfer
/// </summary>
public class capaTransfer
{
    public static DataSet RecuperarTodosTransfer()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.RecuperarTodosTransfer", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Transfers");
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
    public static DataSet RecuperarTodosTransferMasDetallePorIdProducto(string pSucursal, string pNombreProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spRecuperarTodosTransferMasDetallePorIdProducto", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTde_codpro = cmdComandoInicio.Parameters.Add("@tde_codpro", SqlDbType.NVarChar, 75);
        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@sucursal", SqlDbType.NVarChar, 2);
        paTde_codpro.Value = pNombreProducto;
        paSucursal.Value = pSucursal;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Transfers");
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
    public static DataSet RecuperarTransferMasDetallePorIdTransfer(string pSucursal, int pTfr_codigo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Transfers.spRecuperarTransferMasDetallePorIdTransfer", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTfr_codigo = cmdComandoInicio.Parameters.Add("@tfr_codigo", SqlDbType.Int);
        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@sucursal", SqlDbType.NVarChar, 2);
        paTfr_codigo.Value = pTfr_codigo;
        paSucursal.Value = pSucursal;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Transfers");
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
    public static bool AgregarProductosTransfersAlCarrito(DataTable pTablaDetalleProductos, int pIdCliente, int pIdUsuario, int pIdTransfers, string pCodSucursal)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoTransferPorDetalles", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@idCliente", SqlDbType.Int);
        SqlParameter paTabla_Detalle = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);
        SqlParameter paIdTransfer = cmdComandoInicio.Parameters.Add("@idTransfer", SqlDbType.Int);
        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@ctr_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSumar = cmdComandoInicio.Parameters.Add("@isOk", SqlDbType.Bit);
        paSumar.Direction = ParameterDirection.Output;

        paTabla_Detalle.TypeName = "TransferDetalleTableType";
        paTabla_Detalle.Value = pTablaDetalleProductos;
        paIdUsuario.Value = pIdUsuario;
        paIdCliente.Value = pIdCliente;
        paIdTransfer.Value = pIdTransfers;
        paCodSucursal.Value = pCodSucursal;


        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            return Convert.ToBoolean(paSumar.Value);
            //object objResultado = cmdComandoInicio.ExecuteNonQuery();
            //return true;

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
    public static bool AgregarProductosTransfersAlCarrito_TempSubirArchivo(DataTable pTablaDetalleProductos, int pIdCliente, int pIdUsuario, int pIdTransfers, string pCodSucursal)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoTransferPorDetalles_Temp", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@idCliente", SqlDbType.Int);
        SqlParameter paTabla_Detalle = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);
        SqlParameter paIdTransfer = cmdComandoInicio.Parameters.Add("@idTransfer", SqlDbType.Int);
        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@ctr_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSumar = cmdComandoInicio.Parameters.Add("@isOk", SqlDbType.Bit);
        paSumar.Direction = ParameterDirection.Output;

        paTabla_Detalle.TypeName = "TransferDetalleTableType";
        paTabla_Detalle.Value = pTablaDetalleProductos;
        paIdUsuario.Value = pIdUsuario;
        paIdCliente.Value = pIdCliente;
        paIdTransfer.Value = pIdTransfers;
        paCodSucursal.Value = pCodSucursal;


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
    public static DataSet RecuperarCarritoTransferPorIdCliente(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarCarritoTransferPorIdCliente", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        paIdCliente.Value = pIdCliente;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Transfers");
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
    public static bool BorrarCarritoTransfer(int pIdCliente, string pSucursal)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spBorrarCarritoTransfer", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCtr_codCliente = cmdComandoInicio.Parameters.Add("@ctr_codCliente", SqlDbType.Int);
        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@sucursal", SqlDbType.NVarChar, 2);

        paCtr_codCliente.Value = pIdCliente;
        paSucursal.Value = pSucursal;
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

}