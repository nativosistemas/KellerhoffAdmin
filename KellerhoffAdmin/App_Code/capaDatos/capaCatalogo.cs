using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SitioBase.capaDatos;



public class cCatalogo
{
    public int tbc_codigo { get; set; }
    public string tbc_titulo { get; set; }
    public string tbc_descripcion { get; set; }
    public int tbc_orden { get; set; }
    public DateTime? tbc_fecha { get; set; }
    public string tbc_fechaToString { get; set; }
    public int tbc_estado { get; set; }
    public bool? tbc_publicarHome { get; set; }
    public string tbc_publicarHomeToString { get; set; }
    public string tbc_estadoToString { get; set; }
}
/// <summary>
/// Summary description for capaCatalogo
/// </summary>
public class capaCatalogo
{
  
    //public capaCatalogo()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    public static DataTable RecuperarTodosCatalogos()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Catalogo.spRecuperarTodosCatalogos", Conn);
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
    public static int InsertarActualizarCatalogo(int tbc_codigo, string tbc_titulo, string tbc_descripcion,int? tbc_orden, DateTime? tbc_fecha, int tbc_estado)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Catalogo.spInsertarActualizarCatalogo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTbc_codigo = cmdComandoInicio.Parameters.Add("@tbc_codigo", SqlDbType.Int);
        SqlParameter paTbc_titulo = cmdComandoInicio.Parameters.Add("@tbc_titulo", SqlDbType.NVarChar, 300);
        SqlParameter paTbc_descripcion = cmdComandoInicio.Parameters.Add("@tbc_descripcion", SqlDbType.NVarChar, -1);
        SqlParameter paTbc_orden = cmdComandoInicio.Parameters.Add("@tbc_orden", SqlDbType.Int); 
        SqlParameter paTbc_fecha = cmdComandoInicio.Parameters.Add("@tbc_fecha", SqlDbType.DateTime);
        SqlParameter paTbc_estado = cmdComandoInicio.Parameters.Add("@tbc_estado", SqlDbType.Int);

        paTbc_codigo.Value = tbc_codigo;
        if (tbc_titulo == null)
        {
            paTbc_titulo.Value = DBNull.Value;
        }
        else
        {
            paTbc_titulo.Value = tbc_titulo;
        }
        if (tbc_descripcion == null)
        {
            paTbc_descripcion.Value = DBNull.Value;
        }
        else
        {
            paTbc_descripcion.Value = tbc_descripcion;
        }
        if (tbc_orden == null)
        {
            paTbc_orden.Value = DBNull.Value;
        }
        else
        {
            paTbc_orden.Value = tbc_orden;
        }
        if (tbc_fecha == null)
        {
            paTbc_fecha.Value = DBNull.Value;
        }
        else
        {
            paTbc_fecha.Value = tbc_fecha;
        }
        if (tbc_estado == null)
        {
            paTbc_estado.Value = DBNull.Value;
        }
        else
        {
            paTbc_estado.Value = tbc_estado;
        }
        
        try
        {
            Conn.Open();

          var resultado =  cmdComandoInicio.ExecuteScalar();
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
    public static bool EliminarCatalogo(int tbc_codigo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Catalogo.spEliminarCatalogo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTbc_codigo = cmdComandoInicio.Parameters.Add("@tbc_codigo", SqlDbType.Int);
       
        paTbc_codigo.Value = tbc_codigo;
   
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
    public static bool PublicarHomeCatalogo(int tbc_codigo, bool tbc_publicarHome)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Catalogo.spPublicarHomeCatalogo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTbc_codigo = cmdComandoInicio.Parameters.Add("@tbc_codigo", SqlDbType.Int);   
        paTbc_codigo.Value = tbc_codigo;
        SqlParameter paTbc_publicarHome = cmdComandoInicio.Parameters.Add("@tbc_publicarHome", SqlDbType.Bit);
        paTbc_publicarHome.Value = tbc_publicarHome;

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