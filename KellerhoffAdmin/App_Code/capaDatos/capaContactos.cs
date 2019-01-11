using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;


/// <summary>
/// Descripción breve de capaContactos
/// </summary>
/// 
public class cContacto
{
    public int con_codContacto { get; set; }
    public string con_fecha { get; set; }
    public string con_nombre { get; set; }
    public string con_empresa { get; set; }
    public string con_comentario { get; set; }
    public string con_asunto { get; set; }
    public string con_mail { get; set; }
    public string con_leido { get; set; }

}




public class capaContactos
{
	public capaContactos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public static DataSet GestiónContacto(int? con_codContacto, DateTime? con_fecha, string con_nombre, string con_empresa, string con_mail, string con_asunto, string con_comentario, string con_leido, string filtro, string accion)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("spGestionContacto", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter pacon_codContacto = cmdComandoInicio.Parameters.Add("@con_codContacto", SqlDbType.Int);
        SqlParameter pacon_fecha = cmdComandoInicio.Parameters.Add("@con_fecha", SqlDbType.DateTime);
        SqlParameter pacon_nombre = cmdComandoInicio.Parameters.Add("@con_nombre", SqlDbType.NVarChar, 500);
        SqlParameter pacon_empresa = cmdComandoInicio.Parameters.Add("@con_empresa", SqlDbType.NVarChar, 500);
        SqlParameter pacon_mail = cmdComandoInicio.Parameters.Add("@con_mail", SqlDbType.NVarChar, 500);
        SqlParameter pacon_asunto = cmdComandoInicio.Parameters.Add("@con_asunto", SqlDbType.NVarChar, 500);
        SqlParameter pacon_comentario = cmdComandoInicio.Parameters.Add("@con_comentario", SqlDbType.NVarChar, 500);
        SqlParameter pacon_leido = cmdComandoInicio.Parameters.Add("@con_leido", SqlDbType.NVarChar, 50);
        SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
        SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        if (con_codContacto == null)
        {
            pacon_codContacto.Value = DBNull.Value;
        }
        else
        {
            pacon_codContacto.Value = con_codContacto;
        }
        
        if (con_fecha == null)
        {
            pacon_fecha.Value = DBNull.Value;
        }
        else
        {
            pacon_fecha.Value = con_fecha  ;
        }
        if (con_nombre == null)
        {
            pacon_nombre.Value = DBNull.Value;
        }
        else
        {
            pacon_nombre.Value = con_nombre;
        }
        if (con_empresa == null)
        {
            pacon_empresa.Value = DBNull.Value;
        }
        else
        {
            pacon_empresa.Value = con_empresa;
        }
        if (con_comentario == null)
        {
            pacon_comentario.Value = DBNull.Value;
        }
        else
        {
            pacon_comentario.Value = con_comentario;
        }

        if (con_asunto == null)
        {
            pacon_asunto.Value = DBNull.Value;
        }
        else
        {
            pacon_asunto.Value = con_asunto;
        }
        if (con_mail == null)
        {
            pacon_mail.Value = DBNull.Value;
        }
        else
        {
            pacon_mail.Value = con_mail;
        }
        if (con_leido == null)
        {
            pacon_leido.Value = DBNull.Value;
        }
        else
        {
            pacon_leido.Value = con_leido;
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
            da.Fill(dsResultado, "Contacto");
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