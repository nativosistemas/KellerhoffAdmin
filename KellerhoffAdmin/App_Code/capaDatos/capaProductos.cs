using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;


public class cProductos
{
    public cProductos()
    {
    }
    public cProductos(string pPro_codigo, string pPro_nombre)
    {
        pro_codigo = pPro_codigo;
        pro_nombre = pPro_nombre;
    }
    public string pro_codigo { get; set; }
    public string pro_nombre { get; set; }
    //public string pro_codtpopro { get; set; }
    //public decimal pro_descuentoweb { get; set; }
    public decimal pro_precio { get; set; }
    public decimal pro_preciofarmacia { get; set; }
    public int pro_ofeunidades { get; set; }
    public decimal pro_ofeporcentaje { get; set; }
    public bool pro_neto { get; set; }
    public string pro_codtpopro { get; set; }
    public decimal pro_descuentoweb { get; set; }
    public string pro_laboratorio { get; set; }
    public string pro_monodroga { get; set; }
    public string pro_codtpovta { get; set; }
    public string pro_codigobarra { get; set; }
    public string pro_troquel { get; set; }
    public string pro_codigoalfabeta { get; set; }
    public decimal PrecioFinal { get; set; }
    public decimal PrecioConDescuentoOferta { get; set; }
    public bool pro_isTrazable { get; set; }
    public bool pro_isCadenaFrio { get; set; }
    public int? pro_canmaxima { get; set; }
    public bool pro_entransfer { get; set; }
    public bool pro_vtasolotransfer { get; set; }
    public int pro_acuerdo { get; set; }
    public string pri_nombreArchivo { get; set; }
    public bool pro_NoTransfersEnClientesPerf { get; set; }
    private bool _isMostrarTransfersEnClientesPerf = true;
    public bool isMostrarTransfersEnClientesPerf
    {
        get { return _isMostrarTransfersEnClientesPerf; }
        set { _isMostrarTransfersEnClientesPerf = value; }
    }
    private bool _isPermitirPedirProducto = true;
    public bool isPermitirPedirProducto
    {
        get { return _isPermitirPedirProducto; }
        set { _isPermitirPedirProducto = value; }
    }
}
//public class cProductosImagen
//{
//    public string pri_codigo { get; set; }
//    public string pri_nombreArchivo { get; set; }
//}

public class cProductosBuscador : cTransferDetalle
{
    public cProductosBuscador()
    {
        listaSucursalStocks = new List<cSucursalStocks>();
        isProductoNoEncontrado = false;
    }
    public cProductosBuscador(cProductos pProducto)
    {
        base.pro_codigo = pProducto.pro_codigo;
        base.pro_nombre = pProducto.pro_nombre;
        base.PrecioFinal = pProducto.PrecioFinal;
        base.pro_codigoalfabeta = pProducto.pro_codigoalfabeta;
        base.pro_codigobarra = pProducto.pro_codigobarra;
        base.pro_codtpopro = pProducto.pro_codtpopro;
        base.pro_descuentoweb = pProducto.pro_descuentoweb;
        base.pro_laboratorio = pProducto.pro_laboratorio;
        base.pro_monodroga = pProducto.pro_monodroga;
        base.pro_codtpovta = pProducto.pro_codtpovta;
        base.pro_neto = pProducto.pro_neto;
        base.pro_ofeporcentaje = pProducto.pro_ofeporcentaje;
        base.pro_ofeunidades = pProducto.pro_ofeunidades;
        base.pro_precio = pProducto.pro_precio;
        base.pro_preciofarmacia = pProducto.pro_preciofarmacia;
        base.pro_isTrazable = pProducto.pro_isTrazable;
        base.pro_NoTransfersEnClientesPerf = pProducto.pro_NoTransfersEnClientesPerf;
        listaSucursalStocks = new List<cSucursalStocks>();
        isProductoNoEncontrado = false;
    }
    public int pro_Ranking { get; set; }
    public bool isTieneTransfer { get; set; }
    public bool isValePsicotropicos { get; set; }
    public int cantidad { get; set; }
    public int nroordenamiento { get; set; }
    // public List<cSucursalStocks> listaSucursalStocks { get; set; }
    public bool isProductoNoEncontrado { get; set; }
    public bool isProductoFacturacionDirecta { get; set; }
    //public void CargarTransferDetalle(cTransferDetalle pValor)
    //{
    //    this.CargarTransferDetalle(pValor);
    //}
    public void CargarTransferYTransferDetalle(cTransferDetalle pValor)
    {
        base.tde_codpro = pValor.tde_codpro;
        base.tde_codtfr = pValor.tde_codtfr;
        base.tde_descripcion = pValor.tde_descripcion;
        base.tde_fijuni = pValor.tde_fijuni;
        base.tde_maxuni = pValor.tde_maxuni;
        base.tde_minuni = pValor.tde_minuni;
        base.tde_muluni = pValor.tde_muluni;
        base.tde_predescuento = pValor.tde_predescuento;
        base.tde_prepublico = pValor.tde_prepublico;
        base.tde_proobligatorio = pValor.tde_proobligatorio;
        base.tde_unidadesbonificadas = pValor.tde_unidadesbonificadas;
        base.tde_unidadesbonificadasdescripcion = pValor.tde_unidadesbonificadasdescripcion;
        base.tfr_codigo = pValor.tfr_codigo;
        base.tfr_accion = pValor.tfr_accion;
        base.tfr_nombre = pValor.tfr_nombre;
        base.tfr_deshab = pValor.tfr_deshab;
        base.tfr_pordesadi = pValor.tfr_pordesadi;
        base.tfr_tipo = pValor.tfr_tipo;
        base.tfr_mospap = pValor.tfr_mospap;
        base.tfr_minrenglones = pValor.tfr_minrenglones;
        base.tfr_minunidades = pValor.tfr_minunidades;
        base.tfr_maxunidades = pValor.tfr_maxunidades;
        base.tfr_mulunidades = pValor.tfr_mulunidades;
        base.tfr_fijunidades = pValor.tfr_fijunidades;
        base.tfr_facturaciondirecta = pValor.tfr_facturaciondirecta;
        base.tfr_descripcion = pValor.tfr_descripcion;
    }

}
public class cSucursalStocks
{
    public string stk_codpro { get; set; }
    public string stk_codsuc { get; set; }
    public string stk_stock { get; set; }
    public int cantidadSucursal { get; set; }
}
/// <summary>
/// Summary description for capaProductos
/// </summary>
public class capaProductos
{
    public static DataSet RecuperarTodosProductosBuscadorV3(string pTextoBuscador, List<string> pListaColumna, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorV3", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        paWhere.Value = pListaColumna == null ? FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pop_columnaWhereDefault") : FuncionesPersonalizadas.GenerarWhereLikeConVariasColumnas(pTextoBuscador, pListaColumna);

        SqlParameter paWherePrimeraOrdenacion = cmdComandoInicio.Parameters.Add("@WherePrimeraOrdenacion", SqlDbType.NVarChar, 4000);
        paWherePrimeraOrdenacion.Value = FuncionesPersonalizadas.GenerarWhereLikeConColumna_EmpiezaCon(pTextoBuscador, "pro_nombre");

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorOferta(int pIdOferta, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorOferta", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdOferta = cmdComandoInicio.Parameters.Add("@ofe_idOferta", SqlDbType.Int);

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paIdOferta.Value = pIdOferta;
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorV2(string pTextoBuscador, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorV2", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        string strWhere = string.Empty;
        strWhere += FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pop_columnaWhereDefault"); // pop_columnaWhere
        paWhere.Value = strWhere;

        SqlParameter paWherePrimeraOrdenacion = cmdComandoInicio.Parameters.Add("@WherePrimeraOrdenacion", SqlDbType.NVarChar, 4000);
        string strWherePrimeraOrdenacion = string.Empty;
        strWherePrimeraOrdenacion += FuncionesPersonalizadas.GenerarWhereLikeConColumna_EmpiezaCon(pTextoBuscador, "pro_nombre");
        paWherePrimeraOrdenacion.Value = strWherePrimeraOrdenacion;



        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscador(string pTextoBuscador, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscador", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        string strWhere = string.Empty;

        strWhere += FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pop_columnaWhereDefault"); // pop_columnaWhere

        //strWhere += FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_nombre"); // Nombre producto
        //strWhere +=  " OR " + FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_codigo"); // Código
        //strWhere += " OR " + FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_laboratorio");  // Laboratorio
        //strWhere += " OR " + FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_monodroga"); // Mono droga
        //strWhere += " OR " + FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_codigobarra"); // Código de barra
        //strWhere += " OR " + FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pro_codigoalfabeta"); // Código alfa beta
        paWhere.Value = strWhere;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorConVariasColumnasV2(string pTextoBuscador, List<string> pListaColumna, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorV2", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        string strWhere = string.Empty;
        strWhere += FuncionesPersonalizadas.GenerarWhereLikeConVariasColumnas(pTextoBuscador, pListaColumna);
        paWhere.Value = strWhere;

        SqlParameter paWherePrimeraOrdenacion = cmdComandoInicio.Parameters.Add("@WherePrimeraOrdenacion", SqlDbType.NVarChar, 4000);
        string strWherePrimeraOrdenacion = string.Empty;
        strWherePrimeraOrdenacion += FuncionesPersonalizadas.GenerarWhereLikeConColumna_EmpiezaCon(pTextoBuscador, "pro_nombre");
        paWherePrimeraOrdenacion.Value = strWherePrimeraOrdenacion;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        paSucursal.Value = pSucursalPerteneciente;
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paCli_codprov.Value = pCli_codprov;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorConVariasColumnas(string pTextoBuscador, List<string> pListaColumna, string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscador", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        string strWhere = string.Empty;

        strWhere += FuncionesPersonalizadas.GenerarWhereLikeConVariasColumnas(pTextoBuscador, pListaColumna);
        paWhere.Value = strWhere;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        paSucursal.Value = pSucursalPerteneciente;
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paCli_codprov.Value = pCli_codprov;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorEnTransfer(string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorEnTransfer", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorEnOferta(string pSucursalPerteneciente, int? pIdCliente, string pCli_codprov)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorEnOferta", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paSucursal.Value = pSucursalPerteneciente;
        if (pIdCliente == null)
        {
            paIdCliente.Value = DBNull.Value;
        }
        else
        {
            paIdCliente.Value = (int)pIdCliente;
        }
        paCli_codprov.Value = pCli_codprov;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataSet RecuperarTodosProductosBuscadorOptimizado(string pTextoBuscador)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarTodosProductosBuscadorOptimizado", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        string strWhere = string.Empty;

        strWhere += FuncionesPersonalizadas.GenerarWhereLikeConColumna(pTextoBuscador, "pop_columnaWhereDefault"); //pop_columnaWhere

        paWhere.Value = strWhere;

        //SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        //paSucursal.Value = pSucursalPerteneciente;
        //SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        //if (pIdCliente == null)
        //{
        //    paIdCliente.Value = DBNull.Value;
        //}
        //else
        //{
        //    paIdCliente.Value = (int)pIdCliente;
        //}
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "ProductosBuscador");
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
    public static DataTable RecuperarTodosProductos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperadorTodosProductos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Productos");
            return dsResultado.Tables["Productos"];
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
    public static DataTable RecuperadorProductoPorCodigo(string pCodigoProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperadorProductoPorCodigo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paPro_nombre = cmdComandoInicio.Parameters.Add("@pro_codigo", SqlDbType.NVarChar, 50);
        paPro_nombre.Value = pCodigoProducto;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Productos");
            return dsResultado.Tables["Productos"];
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
    public static DataTable RecuperarProductoPorNombre(string pNombreProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperadorTodosProductosPorNombre", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paPro_nombre = cmdComandoInicio.Parameters.Add("@pro_nombre", SqlDbType.NVarChar, 75);
        paPro_nombre.Value = pNombreProducto;

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
    public static DataSet RecuperarTodosProductosEnOferta()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperadorTodosProductosEnOferta", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Productos");
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
    public static DataTable DescargaTodosProductos(string pProvincia)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spDescargaTodosProductos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paProvincia = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paProvincia.Value = pProvincia;

        try
        {
            Conn.Open();
            DataTable table = new DataTable();
            table.Load(cmdComandoInicio.ExecuteReader());
            return table;
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
    public static DataTable DescargaTodosProductosDrogueria(string pProvincia)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spDescargaTodosProductosDrogueria", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paProvincia = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paProvincia.Value = pProvincia;

        try
        {
            Conn.Open();
            DataTable table = new DataTable();
            table.Load(cmdComandoInicio.ExecuteReader());
            return table;
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
    public static DataTable DescargaTodosProductosPerfumeria(string pProvincia)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spDescargaTodosProductosPerfumeria", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paProvincia = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        paProvincia.Value = pProvincia;


        try
        {
            Conn.Open();
            DataTable table = new DataTable();
            table.Load(cmdComandoInicio.ExecuteReader());
            return table;
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
    public static DataTable DescargaTodosProductosEnOferta()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spDescargaTodosProductosEnOferta", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            DataTable table = new DataTable();
            table.Load(cmdComandoInicio.ExecuteReader());
            return table;
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
    //
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DataTable RecuperarProductoParametrizadoCantidad()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarProductoParametrizadoCantidad", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            DataTable table = new DataTable();
            table.Load(cmdComandoInicio.ExecuteReader());
            return table;
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
    public static int InsertarActualizarProductoParametrizadoCantidad(int pCpc_cantidadParametrizada)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spInsertarActualizarProductoParametrizadoCantidad", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCpc_cantidadParametrizada = cmdComandoInicio.Parameters.Add("@cpc_cantidadParametrizada", SqlDbType.Int);
        SqlParameter paCpc_tipoParametro = cmdComandoInicio.Parameters.Add("@cpc_tipoParametro", SqlDbType.Int);
        paCpc_tipoParametro.Value = 1;
        paCpc_cantidadParametrizada.Value = pCpc_cantidadParametrizada;

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
    public static DataSet RecuperarProductosDesdeTabla(DataTable pTablaProducto, string pSucursalPerteneciente, string pCli_codprov,int pCodCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarProductosDesdeTabla", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paTablaProductos = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);
        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        paCodCliente.Value = pCodCliente;
        paSucursal.Value = pSucursalPerteneciente;
        paCli_codprov.Value = pCli_codprov;
        if (pTablaProducto == null)
        {
            paTablaProductos.Value = DBNull.Value;
        }
        else
        {
            paTablaProductos.Value = pTablaProducto;
        }
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Productos");
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
    public static DataTable RecuperarStockPorProductosAndSucursal(DataTable pTablaSucursales, DataTable pTablaProductos)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Productos.spRecuperarStockPorProductosAndSucursal", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTabla_Sucursales = cmdComandoInicio.Parameters.Add("@Tabla_Sucursales", SqlDbType.Structured);
        SqlParameter paTabla_Productos = cmdComandoInicio.Parameters.Add("@Tabla_Productos", SqlDbType.Structured);
        paTabla_Sucursales.Value = pTablaSucursales;
        paTabla_Productos.Value = pTablaProductos;

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
            //throw ex;
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
    // Nuevo
    public static DataTable ObtenerProductosImagenesBusqueda(string pTxtBuscador)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("dbo.spObtenerProductosImagenesBusqueda", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paWhere = cmdComandoInicio.Parameters.Add("@Where", SqlDbType.NVarChar, 4000);
        List<string> pListaColumna = new List<string>();
        pListaColumna.Add("pro_nombre");
        pListaColumna.Add("pro_codigo");
        pListaColumna.Add("pro_codigobarra");
        paWhere.Value = FuncionesPersonalizadas.GenerarWhereLikeConVariasColumnas(pTxtBuscador, pListaColumna);
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
    public static DataTable ObtenerProductosImagenes()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("dbo.spObtenerProductosImagenes", Conn);
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
    public static bool ActualizarInsertarProductosImagen(string pCodigoProducto, string pNombreArchivo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("dbo.spActualizarInsertarProductosImagen", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paPri_codigo = cmdComandoInicio.Parameters.Add("@pri_codigo", SqlDbType.NVarChar, 50);
        SqlParameter paPri_nombreArchivo = cmdComandoInicio.Parameters.Add("@pri_nombreArchivo", SqlDbType.NVarChar, 100);
        paPri_codigo.Value = pCodigoProducto;
        paPri_nombreArchivo.Value = pNombreArchivo;

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
    public static bool ElimimarProductoImagenPorId(string pCodigoProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("dbo.spElimimarProductoImagenPorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paPri_codigo = cmdComandoInicio.Parameters.Add("@pri_codigo", SqlDbType.NVarChar, 50);
        paPri_codigo.Value = pCodigoProducto;
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