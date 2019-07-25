using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;



public class cCurriculumVitae
{
    public int tcv_codCV { get; set; }
    public string tcv_nombre { get; set; }
    public string tcv_comentario { get; set; }
    public string tcv_mail { get; set; }
    public string tcv_dni { get; set; }
    public DateTime tcv_fecha { get; set; }
    public string tcv_fechaToString { get; set; }
    public int tcv_estado { get; set; }
    public string tcv_estadoToString { get; set; }
    public string tcv_puesto { get; set; }
    public string tcv_sucursal { get; set; }
    public DateTime tcv_fechaPresentacion { get; set; }
    public string tcv_fechaPresentacionToString { get; set; }
    public string arc_nombre { get; set; }
}
/// <summary>
/// Summary description for capaCV
/// </summary>
public class capaCV
{
    //public capaCV()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    public static DataSet GestiónCurriculumVitae(int? tcv_codCV, DateTime? tcv_fecha, string tcv_nombre, string tcv_comentario, string tcv_mail, string tcv_dni, int? tcv_estado, string filtro, string accion, string tcv_puesto, string tcv_sucursal, DateTime? tcv_fechaPresentacion)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("spCurriculumVitae", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paTcv_codCV = cmdComandoInicio.Parameters.Add("@tcv_codCV", SqlDbType.Int);
        SqlParameter paTcv_fecha = cmdComandoInicio.Parameters.Add("@tcv_fecha", SqlDbType.DateTime);
        SqlParameter paTcv_nombre = cmdComandoInicio.Parameters.Add("@tcv_nombre", SqlDbType.NVarChar, 500);
        SqlParameter paTcv_comentario = cmdComandoInicio.Parameters.Add("@tcv_comentario", SqlDbType.NVarChar, 500);
        SqlParameter paTcv_mail = cmdComandoInicio.Parameters.Add("@tcv_mail", SqlDbType.NVarChar, 500);
        SqlParameter paTcv_dni = cmdComandoInicio.Parameters.Add("@tcv_dni", SqlDbType.NVarChar, 50);
        SqlParameter paTcv_estado = cmdComandoInicio.Parameters.Add("@tcv_estado", SqlDbType.Int);
        SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
        SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        SqlParameter paTcv_puesto = cmdComandoInicio.Parameters.Add("@tcv_puesto", SqlDbType.NVarChar, 500);
        SqlParameter paTcv_sucursal = cmdComandoInicio.Parameters.Add("@tcv_sucursal", SqlDbType.NVarChar, 500);
        SqlParameter paTcv_fechaPresentacion = cmdComandoInicio.Parameters.Add("@tcv_fechaPresentacion", SqlDbType.DateTime);

        if (tcv_puesto == null)
        {
            paTcv_puesto.Value = DBNull.Value;
        }
        else
        {
            paTcv_puesto.Value = tcv_puesto;
        }
        if (tcv_sucursal == null)
        {
            paTcv_sucursal.Value = DBNull.Value;
        }
        else
        {
            paTcv_sucursal.Value = tcv_sucursal;
        }
        if (tcv_fechaPresentacion == null)
        {
            paTcv_fechaPresentacion.Value = DBNull.Value;
        }
        else
        {
            paTcv_fechaPresentacion.Value = tcv_fechaPresentacion;
        }
        ////


        if (tcv_codCV == null)
        {
            paTcv_codCV.Value = DBNull.Value;
        }
        else
        {
            paTcv_codCV.Value = tcv_codCV;
        }
        if (tcv_fecha == null)
        {
            paTcv_fecha.Value = DBNull.Value;
        }
        else
        {
            paTcv_fecha.Value = tcv_fecha;
        }
        if (tcv_nombre == null)
        {
            paTcv_nombre.Value = DBNull.Value;
        }
        else
        {
            paTcv_nombre.Value = tcv_nombre;
        }
        if (tcv_comentario == null)
        {
            paTcv_comentario.Value = DBNull.Value;
        }
        else
        {
            paTcv_comentario.Value = tcv_comentario;
        }
        if (tcv_mail == null)
        {
            paTcv_mail.Value = DBNull.Value;
        }
        else
        {
            paTcv_mail.Value = tcv_mail;
        }
        if (tcv_dni == null)
        {
            paTcv_dni.Value = DBNull.Value;
        }
        else
        {
            paTcv_dni.Value = tcv_dni;
        }

        if (tcv_estado == null)
        {
            paTcv_estado.Value = DBNull.Value;
        }
        else
        {
            paTcv_estado.Value = tcv_estado;
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
            da.Fill(dsResultado, "CurriculumVitae");
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
}