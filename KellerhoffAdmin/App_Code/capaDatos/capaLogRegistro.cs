using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;

public class cPalabraBusqueda
{
    public int hbp_id { get; set; }
    public string hbp_Palabra { get; set; }
    //public string hbp_NombreTabla { get; set; }
    //public int hbp_codUsuario { get; set; }
    //public DateTime? hbp_Fecha { get; set; }
}
public class cHistorialProcesos
{
    public int his_id { get; set; }
    public string his_Descripcion { get; set; }
    public string his_NombreProcedimiento { get; set; }
    public DateTime? his_Fecha { get; set; }
    public string his_FechaToString { get; set; }
}
public class cFaltantesConProblemasCrediticiosPadre
{
    //public int fpc_id { get; set; }
    //public int fpc_codCarrito { get; set; }
    public string fpc_codSucursal { get; set; }
    public string suc_nombre { get; set; }

    //public int fpc_codCliente { get; set; }
    //public string fpc_nombreProducto { get; set; }
    //public int fpc_cantidad { get; set; }
    public int fpc_tipo { get; set; }
    //public DateTime? fpc_fecha { get; set; }
    //public string fpc_fechaToString { get; set; }
    //public string stk_stock { get; set; }
    public List<cFaltantesConProblemasCrediticios> listaProductos { get; set; }
}
public class cFaltantesConProblemasCrediticios
{
    //public int fpc_id { get; set; }
    public string fpc_nombreProducto { get; set; }
    public int fpc_cantidad { get; set; }
    public int pro_ofeunidades { get; set; }
    public decimal pro_ofeporcentaje { get; set; }
    //public DateTime? fpc_fecha { get; set; }
    //public string fpc_fechaToString { get; set; }
    public string stk_stock { get; set; }
}
public class cHistorialArchivoSubir
{
    public int has_id { get; set; }
    public int has_codCliente { get; set; }
    public string has_NombreArchivo { get; set; }
    public string has_NombreArchivoOriginal { get; set; }
    public string has_sucursal { get; set; }
    public string suc_nombre { get; set; }
    public DateTime has_fecha { get; set; }
    public string has_fechaToString { get; set; }
}
public class cFrasesFront
{
    public int tff_id { get; set; }
    public string tff_nombre { get; set; }
    public bool tff_publicar { get; set; }
}
/// <summary>
/// Summary description for capaLogRegistro
/// </summary>
public class capaLogRegistro
{
    public static int InsertarActualizarFrasesFront(int pTff_id, string pTff_nombre)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spInsertarActualizarFrasesFront", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTff_id = cmdComandoInicio.Parameters.Add("@tff_id", SqlDbType.Int);
        SqlParameter paTff_nombre = cmdComandoInicio.Parameters.Add("@tff_nombre", SqlDbType.NVarChar, 500);

        paTff_id.Value = pTff_id;
        paTff_nombre.Value = pTff_nombre;

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

    public static void EliminarFrasesFront(int pTff_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spEliminarFrasesFront", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTff_id = cmdComandoInicio.Parameters.Add("@tff_id", SqlDbType.Int);
        paTff_id.Value = pTff_id;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            //object objResultado = cmdComandoInicio.ExecuteScalar();
            //return Convert.ToInt32(objResultado);
        }
        catch (Exception ex)
        {
            //return -1;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void CambiarEstadoPublicarFrasesFront(int pTff_id, bool pTff_publicar)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCambiarEstadoPublicarFrasesFront", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTff_id = cmdComandoInicio.Parameters.Add("@tff_id", SqlDbType.Int);
        SqlParameter paTff_publicar = cmdComandoInicio.Parameters.Add("@tff_publicar", SqlDbType.Bit);
        paTff_id.Value = pTff_id;
        paTff_publicar.Value = pTff_publicar;

        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            //object objResultado = cmdComandoInicio.ExecuteScalar();
            //return Convert.ToInt32(objResultado);
        }
        catch (Exception ex)
        {
            //return -1;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static DataTable RecuperarTodasFrasesFront()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarTodasFrasesFront", Conn);
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
    public static DataTable RecuperarTodoHistorialProcesos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarTodoHistorialProcesos", Conn);
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

    public static DataTable RecuperarLasPalabrasMasBusquedasPorUsuario(int pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarLasPalabrasMasBusquedasPorUsuario", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
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

    public static DataTable RecuperarTodasPalabrasBusquedas(int? pIdUsuario, string pNombreTabla)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarTodasPalabrasBusquedas", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        SqlParameter paNombreTabla = cmdComandoInicio.Parameters.Add("@nombreTabla", SqlDbType.NVarChar, 75);

        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        if (pNombreTabla == null)
        {
            paNombreTabla.Value = DBNull.Value;
        }
        else
        {
            paNombreTabla.Value = pNombreTabla;
        }

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
    public static int IngresarPalabraBusqueda(int? pIdUsuario, string pNombreTabla, string pPalabra)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spIngresarPalabraBusqueda", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        SqlParameter paNombreTabla = cmdComandoInicio.Parameters.Add("@nombreTabla", SqlDbType.NVarChar, 75);
        SqlParameter paPalabra = cmdComandoInicio.Parameters.Add("@hbp_Palabra", SqlDbType.NVarChar, 200);


        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        if (pNombreTabla == null)
        {
            paNombreTabla.Value = DBNull.Value;
        }
        else
        {
            paNombreTabla.Value = pNombreTabla;
        }
        if (pPalabra == null)
        {
            paPalabra.Value = DBNull.Value;
        }
        else
        {
            paPalabra.Value = pPalabra;
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
    public static void IndicarPalabraBusquedaLaElegida(int pId, string pIdFila, string pPalabraElegida)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spIndicarPalabraElegidaBusqueda", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paHbp_id = cmdComandoInicio.Parameters.Add("@hbp_id", SqlDbType.Int);
        SqlParameter paIdFila = cmdComandoInicio.Parameters.Add("@hbp_idFila", SqlDbType.NVarChar, 75);
        SqlParameter paPalabraElegida = cmdComandoInicio.Parameters.Add("@hbp_PalabraElegida", SqlDbType.NVarChar, 200);


        paHbp_id.Value = pId;
        paIdFila.Value = pIdFila;
        paPalabraElegida.Value = pPalabraElegida;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteNonQuery();
            //return Convert.ToInt32(objResultado);
        }
        catch (Exception ex)
        {
            //return -1;
        }
        finally
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
    public static void AgregarProductosBuscadosDelCarrito(int pIdCliente, string pIdProducto, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spAgregarProductosBuscadosDelCarrito", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@hpb_codUsuario", SqlDbType.Int);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@hpb_codCliente", SqlDbType.Int);
        SqlParameter paIdProducto = cmdComandoInicio.Parameters.Add("@hpb_codProducto", SqlDbType.NVarChar, 75);

        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        //if (pIdCliente == null)
        //{
        //    paIdCliente.Value = DBNull.Value;
        //}
        //else
        //{
        //   paIdCliente.Value = pIdCliente;   
        //}
        paIdCliente.Value = pIdCliente;

        if (paIdProducto == null)
        {
            paIdProducto.Value = DBNull.Value;
        }
        else
        {
            paIdProducto.Value = pIdProducto;
        }
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
    public static void AgregarProductosBuscadosDelCarritoTransfer(int pIdCliente, DataTable pTablaProducto, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spAgregarProductosBuscadosDelCarritoTransferPorNombreProducto", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@hpb_codUsuario", SqlDbType.Int);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@hpb_codCliente", SqlDbType.Int);
        SqlParameter paTablaProductos = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);

        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        paIdCliente.Value = pIdCliente;

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
    public static bool AgregarProductoAlCarrito(string pSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoProductoSucursal", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@lrc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@lcp_codUsuario", SqlDbType.Int);
        SqlParameter paIdProducto = cmdComandoInicio.Parameters.Add("@lcp_codProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidad = cmdComandoInicio.Parameters.Add("@lcp_cantidad", SqlDbType.Int);
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
        //paSumar.Value = pIsSumar;
        try
        {
            Conn.Open(); 
            //object objResultado = cmdComandoInicio.ExecuteNonQuery();
            cmdComandoInicio.ExecuteNonQuery();
           return Convert.ToBoolean( paSumar.Value);
        }
        catch (Exception ex)
        {
            //throw ex;
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
    public static bool AgregarProductoAlCarrito_TempSubirArchivo(string pSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoProductoSucursal_Temp", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@lrc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@lcp_codUsuario", SqlDbType.Int);
        SqlParameter paIdProducto = cmdComandoInicio.Parameters.Add("@lcp_codProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidad = cmdComandoInicio.Parameters.Add("@lcp_cantidad", SqlDbType.Int);
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
        //paSumar.Value = pIsSumar;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();
            return Convert.ToBoolean(paSumar.Value);
        }
        catch (Exception ex)
        {
            //throw ex;
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
    public static void AgregarProductoAlCarritoDesdeRecuperador(string pSucursal, string pNombreProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoProductoSucursalDesdeRecuperador", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@lrc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@lcp_codUsuario", SqlDbType.Int);
        SqlParameter paNombreProducto = cmdComandoInicio.Parameters.Add("@NombreProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidad = cmdComandoInicio.Parameters.Add("@lcp_cantidad", SqlDbType.Int);

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
        paNombreProducto.Value = pNombreProducto;
        paCantidad.Value = pCantidadProducto;
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
    public static DataSet RecuperarCarritosPorSucursalYProductos(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarCarritosPorSucursalYProductos", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);

        paIdCliente.Value = pIdCliente;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "CarritosPorSucursalYProductos");
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
    public static void GuardarHistorialIdCarrito(int pIdCarrito)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spGuardarHistorialCarrito", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCarrito = cmdComandoInicio.Parameters.Add("@lrc_id", SqlDbType.Int);

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
    public static DataSet GestiónFaltantesProblemasCrediticios(int? fpc_id, int? fpc_codCarrito, string fpc_codSucursal, int? fpc_codCliente, string fpc_nombreProducto, int? fpc_cantidad, int? fpc_tipo, DateTime? fpc_fecha, string accion)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spGestionFaltasProblemasCrediticios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paFpc_id = cmdComandoInicio.Parameters.Add("@fpc_id", SqlDbType.Int);
        SqlParameter paFpc_codCarrito = cmdComandoInicio.Parameters.Add("@fpc_codCarrito", SqlDbType.Int);
        SqlParameter paFpc_codSucursal = cmdComandoInicio.Parameters.Add("@fpc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paFpc_codCliente = cmdComandoInicio.Parameters.Add("@fpc_codCliente", SqlDbType.Int);
        SqlParameter paFpc_nombreProducto = cmdComandoInicio.Parameters.Add("@fpc_nombreProducto", SqlDbType.NVarChar, 75);
        SqlParameter paFpc_cantidad = cmdComandoInicio.Parameters.Add("@fpc_cantidad", SqlDbType.Int);
        SqlParameter paFpc_tipo = cmdComandoInicio.Parameters.Add("@fpc_tipo", SqlDbType.Int);
        SqlParameter paFpc_fecha = cmdComandoInicio.Parameters.Add("@fpc_fecha", SqlDbType.DateTime);
        SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        if (fpc_id == null)
        {
            paFpc_id.Value = DBNull.Value;
        }
        else
        {
            paFpc_id.Value = fpc_id;
        }
        if (fpc_codCarrito == null)
        {
            paFpc_codCarrito.Value = DBNull.Value;
        }
        else
        {
            paFpc_codCarrito.Value = fpc_codCarrito;
        }
        if (fpc_codSucursal == null)
        {
            paFpc_codSucursal.Value = DBNull.Value;
        }
        else
        {
            paFpc_codSucursal.Value = fpc_codSucursal;
        }
        if (fpc_codCliente == null)
        {
            paFpc_codCliente.Value = DBNull.Value;
        }
        else
        {
            paFpc_codCliente.Value = fpc_codCliente;
        }
        if (fpc_nombreProducto == null)
        {
            paFpc_nombreProducto.Value = DBNull.Value;
        }
        else
        {
            paFpc_nombreProducto.Value = fpc_nombreProducto;
        }

        if (fpc_cantidad == null)
        {
            paFpc_cantidad.Value = DBNull.Value;
        }
        else
        {
            paFpc_cantidad.Value = fpc_cantidad;
        }
        if (fpc_tipo == null)
        {
            paFpc_tipo.Value = DBNull.Value;
        }
        else
        {
            paFpc_tipo.Value = fpc_tipo;
        }
        if (fpc_fecha == null)
        {
            paFpc_fecha.Value = DBNull.Value;
        }
        else
        {
            paFpc_fecha.Value = fpc_fecha;
        }
        paAccion.Value = accion;
        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "FaltantesProblemasCrediticios");
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
    public static DataTable RecuperarFaltasProblemasCrediticios(int fpc_codCliente, int fpc_tipo, int pDia)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarFaltasProblemasCrediticios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@fpc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@fpc_codCliente", SqlDbType.Int);
        SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@fpc_tipo", SqlDbType.Int);
        SqlParameter paCantidadDia = cmdComandoInicio.Parameters.Add("@cantidadDia", SqlDbType.Int);

        paCantidadDia.Value = pDia;
        paCodCliente.Value = fpc_codCliente;
        paTipo.Value = fpc_tipo;
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
    public static DataTable RecuperarFaltasProblemasCrediticios_TodosEstados(int fpc_codCliente, int fpc_tipo, int pDia)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarFaltasProblemasCrediticiosTodosEstados", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@fpc_codCliente", SqlDbType.Int);
        SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@fpc_tipo", SqlDbType.Int);
        SqlParameter paCantidadDia = cmdComandoInicio.Parameters.Add("@cantidadDia", SqlDbType.Int);

        paCantidadDia.Value = pDia;
        paCodCliente.Value = fpc_codCliente;
        paTipo.Value = fpc_tipo;
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
    public static bool BorrarPorProductosFaltasProblemasCrediticios(string fpc_codSucursal, int fpc_codCliente, int fpc_tipo, string fpc_nombreProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spBorrarPorProductosFaltasProblemasCrediticios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@fpc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@fpc_codCliente", SqlDbType.Int);
        SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@fpc_tipo", SqlDbType.Int);
        SqlParameter paFpc_nombreProducto = cmdComandoInicio.Parameters.Add("@fpc_nombreProducto", SqlDbType.NVarChar, 75);

        paCodSucursal.Value = fpc_codSucursal;
        paCodCliente.Value = fpc_codCliente;
        paTipo.Value = fpc_tipo;
        paFpc_nombreProducto.Value = fpc_nombreProducto;
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
    public static bool BorrarPorProductosFaltasProblemasCrediticiosV2(string fpc_codSucursal, int fpc_codCliente, int fpc_tipo, string fpc_nombreProducto, int pCantidadDia, int pCantidadProductoGrabarNuevo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spBorrarPorProductosFaltasProblemasCrediticiosV2", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@fpc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@fpc_codCliente", SqlDbType.Int);
        SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@fpc_tipo", SqlDbType.Int);
        SqlParameter paFpc_nombreProducto = cmdComandoInicio.Parameters.Add("@fpc_nombreProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidadDia = cmdComandoInicio.Parameters.Add("@cantidadDia", SqlDbType.Int);
        SqlParameter paCantidadProductoGrabarNuevo = cmdComandoInicio.Parameters.Add("@cantidadProductoGrabarNuevo", SqlDbType.Int);

        paCodSucursal.Value = fpc_codSucursal;
        paCodCliente.Value = fpc_codCliente;
        paTipo.Value = fpc_tipo;
        paFpc_nombreProducto.Value = fpc_nombreProducto;
        paCantidadDia.Value = pCantidadDia;
        paCantidadProductoGrabarNuevo.Value = pCantidadProductoGrabarNuevo;

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
    public static DataSet RecuperarCantidadProductoEnCarritoYCarritoTransferFacturacionDirecta(int codCliente, string codSucursal, string codProducto, string nombreProducto)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarCantidadProductoEnCarritoYCarritoTransferFacturacionDirecta", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paCodCliente = cmdComandoInicio.Parameters.Add("@codCliente", SqlDbType.Int);
        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@codSucursal", SqlDbType.NVarChar,2);
        SqlParameter paCodProducto = cmdComandoInicio.Parameters.Add("@codProducto", SqlDbType.NVarChar,50);
        SqlParameter paNombreProducto = cmdComandoInicio.Parameters.Add("@nombreProducto", SqlDbType.NVarChar, 75);
      
        paCodCliente.Value = codCliente;
        paCodSucursal.Value = codSucursal;
        paCodProducto.Value = codProducto;
        paNombreProducto.Value = nombreProducto;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "CantidadProductoEnCarritoYCarritoTransferFacturacionDirecta");
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
    public static bool BorrarCarrito(int lrc_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spBorrarCarrito", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paLrc_id = cmdComandoInicio.Parameters.Add("@lrc_id", SqlDbType.Int);
        paLrc_id.Value = lrc_id;

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

  
    public static DataSet AgregarProductoAlCarritoDesdeArchivoPedidosV5(string pSucursalElejida, string pSucursalCliente, DataTable pTabla, string pTipoDeArchivo, int pIdCliente, string pCli_codprov, bool pCli_isGLN, int? pIdUsuario)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoProductoSucursalDesdeArchivoPedidosV5", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSucursalElejida = cmdComandoInicio.Parameters.Add("@lrc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSucursalCliente = cmdComandoInicio.Parameters.Add("@Sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@lcp_codUsuario", SqlDbType.Int);
        SqlParameter paTabla_Detalle = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);
        SqlParameter paTipoDeArchivo = cmdComandoInicio.Parameters.Add("@TipoDeArchivo", SqlDbType.NVarChar, 1);
        SqlParameter paCli_codprov = cmdComandoInicio.Parameters.Add("@cli_codprov", SqlDbType.NVarChar, 75);
        SqlParameter paCli_isGLN = cmdComandoInicio.Parameters.Add("@cli_isGLN", SqlDbType.Bit);

        if (pIdUsuario == null)
        {
            paIdUsuario.Value = DBNull.Value;
        }
        else
        {
            paIdUsuario.Value = pIdUsuario;
        }
        paSucursalCliente.Value = pSucursalCliente;
        paSucursalElejida.Value = pSucursalElejida;
        paIdCliente.Value = pIdCliente;
        paTabla_Detalle.Value = pTabla;
        paTipoDeArchivo.Value = pTipoDeArchivo;
        paCli_codprov.Value = pCli_codprov;
        paCli_isGLN.Value = pCli_isGLN;

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
    public static DataTable RecuperarHistorialSubirArchivo(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarHistorialSubirArchivo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@has_codCliente", SqlDbType.Int);
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
    public static DataTable RecuperarHistorialSubirArchivoPorId(int pId)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarHistorialSubirArchivoPorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paId = cmdComandoInicio.Parameters.Add("@has_id", SqlDbType.Int);
        paId.Value = pId;

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
    public static DataTable RecuperarHistorialSubirArchivoPorNombreArchivoOriginal(string pNombreArchivoOriginal)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarHistorialSubirArchivoPorNombreArchivoOriginal", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paNombreArchivoOriginal = cmdComandoInicio.Parameters.Add("@has_NombreArchivoOriginal", SqlDbType.NVarChar, 100);
        paNombreArchivoOriginal.Value = pNombreArchivoOriginal;

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



    public static bool AgregarHistorialSubirArchivo(int has_codCliente, string has_NombreArchivo, string has_NombreArchivoOriginal, string has_sucursal, DateTime has_fecha)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spAgregarHistorialSubirArchivo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paHas_NombreArchivo = cmdComandoInicio.Parameters.Add("@has_NombreArchivo", SqlDbType.NVarChar, 100);
        SqlParameter paHas_NombreArchivoOriginal = cmdComandoInicio.Parameters.Add("@has_NombreArchivoOriginal", SqlDbType.NVarChar, 100);
        SqlParameter paHas_codCliente = cmdComandoInicio.Parameters.Add("@has_codCliente", SqlDbType.Int);
        SqlParameter paHas_Sucursal = cmdComandoInicio.Parameters.Add("@has_sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paHas_fecha = cmdComandoInicio.Parameters.Add("@has_fecha", SqlDbType.DateTime);

        paHas_NombreArchivo.Value = has_NombreArchivo;
        paHas_NombreArchivoOriginal.Value = has_NombreArchivoOriginal;
        paHas_codCliente.Value = has_codCliente;
        paHas_Sucursal.Value = has_sucursal;
        paHas_fecha.Value = has_fecha;

        try
        {
            Conn.Open();
            object objResultado = cmdComandoInicio.ExecuteNonQuery();
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
    public static bool CargarCarritoProductoSucursalAndCarritoTransferPorDetalles(string pCodSucursal, int pIdCliente, string pIdProducto, int pCantidadProducto, DataTable pTablaDetalleProductos,  int pIdTransfers, int pIdUsuario )
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spCargarCarritoProductoSucursalAndCarritoTransferPorDetalles", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@idCliente", SqlDbType.Int);
        SqlParameter paIdProducto = cmdComandoInicio.Parameters.Add("@lcp_codProducto", SqlDbType.NVarChar, 75);
        SqlParameter paCantidad = cmdComandoInicio.Parameters.Add("@lcp_cantidad", SqlDbType.Int);
        SqlParameter paTabla_Detalle = cmdComandoInicio.Parameters.Add("@Tabla_Detalle", SqlDbType.Structured);
        SqlParameter paIdTransfer = cmdComandoInicio.Parameters.Add("@idTransfer", SqlDbType.Int);
        SqlParameter paCodSucursal = cmdComandoInicio.Parameters.Add("@codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSumar = cmdComandoInicio.Parameters.Add("@isOk", SqlDbType.Bit);
        paSumar.Direction = ParameterDirection.Output;

        paTabla_Detalle.TypeName = "TransferDetalleTableType";
        paTabla_Detalle.Value = pTablaDetalleProductos;
        paIdUsuario.Value = pIdUsuario;
        paIdCliente.Value = pIdCliente;
        paIdTransfer.Value = pIdTransfers;
        paIdProducto.Value = pIdProducto;
        paCantidad.Value = pCantidadProducto;
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
}


