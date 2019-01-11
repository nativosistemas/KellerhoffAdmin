
using SitioBase.capaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Descripción breve de capaAuditoria
/// </summary>
public class capaAuditoria
{
    public static void guardarPedido(cCarrito pCarrito, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, bool pIsUrgente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("AUDIT.spCargarPedido", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paLrc_id = cmdComandoInicio.Parameters.Add("@lrc_id", SqlDbType.Int);
        SqlParameter paLrc_codSucursal = cmdComandoInicio.Parameters.Add("@lrc_codSucursal", SqlDbType.NVarChar, 2);
        SqlParameter palrc_codCliente = cmdComandoInicio.Parameters.Add("@lrc_codCliente", SqlDbType.Int);
        SqlParameter paFechaPedido = cmdComandoInicio.Parameters.Add("@FechaPedido", SqlDbType.DateTime);
        SqlParameter paMensajeEnFactura = cmdComandoInicio.Parameters.Add("@MensajeEnFactura", SqlDbType.NVarChar, -1);
        SqlParameter paMensajeEnRemito = cmdComandoInicio.Parameters.Add("@MensajeEnRemito", SqlDbType.NVarChar, -1);
        SqlParameter paTipoEnvio = cmdComandoInicio.Parameters.Add("@TipoEnvio", SqlDbType.NVarChar, -1);
        SqlParameter paIsUrgente = cmdComandoInicio.Parameters.Add("@IsUrgente", SqlDbType.Bit);
        SqlParameter paStrXML = cmdComandoInicio.Parameters.Add("@strXML", SqlDbType.Xml);
        paLrc_id.Value = pCarrito.lrc_id;
        paLrc_codSucursal.Value = pCarrito.codSucursal;
        palrc_codCliente.Value = (int)((SitioBase.capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente;
        paFechaPedido.Value = DateTime.Now;
        if (pMensajeEnFactura == null)
        {
            paMensajeEnFactura.Value = DBNull.Value;
        }
        else
        {
            paMensajeEnFactura.Value = pMensajeEnFactura;
        }
        if (pMensajeEnRemito == null)
        {
            paMensajeEnRemito.Value = DBNull.Value;
        }
        else
        {
            paMensajeEnRemito.Value = pMensajeEnRemito;
        }
        if (pTipoEnvio == null)
        {
            paTipoEnvio.Value = DBNull.Value;
        }
        else
        {
            paTipoEnvio.Value = pTipoEnvio;
        }
        paIsUrgente.Value = pIsUrgente;
        string strXML = string.Empty;
        strXML += "<Root>";
        foreach (cProductosEnCarrito item in pCarrito.listaProductos)
        {
            List<XAttribute> listaAtributos = new List<XAttribute>();

            listaAtributos.Add(new XAttribute("lcp_cantidad", item.cantidad));
            listaAtributos.Add(new XAttribute("codigo", item.codProducto));
            listaAtributos.Add(new XAttribute("nombre", item.pro_nombre));

            XElement nodo = new XElement("DetallePedido", listaAtributos);
            strXML += nodo.ToString();
        }
        strXML += "</Root>";
        paStrXML.Value = strXML;
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();

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
    public static void guardarCarrito(int pLrc_id)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("AUDIT.spCargarCarrito", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        SqlParameter paLrc_id = cmdComandoInicio.Parameters.Add("@lrc_id", SqlDbType.Int);

        paLrc_id.Value = pLrc_id;
      
        try
        {
            Conn.Open();
            cmdComandoInicio.ExecuteNonQuery();

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

}