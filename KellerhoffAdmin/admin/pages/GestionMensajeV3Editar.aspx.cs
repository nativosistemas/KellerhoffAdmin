﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SitioBase.clases;
using System.Web.Services;

public partial class admin_pages_GestionMensajeV3Editar : cBaseAdmin
{
    public const string consPalabraClave = "gestionmensaje";
    protected void Page_Load(object sender, EventArgs e)
    {
        Seguridad(consPalabraClave);
        if (Request.QueryString.AllKeys.Contains("id"))
        {
            HttpContext.Current.Session["GestionMensajeV3Editar_id"] = Request.QueryString.Get("id");
            cMensajeNew obj = WebService.RecuperarMensajeNewPorId(Convert.ToInt32(HttpContext.Current.Session["GestionMensajeV3Editar_id"]));
            HttpContext.Current.Session["GestionMensajeV3Editar_obj"] = obj;
        }
        if (!IsPostBack)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static string isValidoFechas(string pFechaDesde_string, string pFechaHasta_string)
    {
        bool result = false;
        DateTime? FechaDesde = null;
        if (!string.IsNullOrEmpty(pFechaDesde_string))
        {
            FechaDesde = Convert.ToDateTime(pFechaDesde_string);
        }
        DateTime? FechaHasta = null;
        if (!string.IsNullOrEmpty(pFechaHasta_string))
        {
            FechaHasta = Convert.ToDateTime(pFechaHasta_string);
        }
        if (FechaDesde != null && FechaHasta != null)
        {
            result = FechaDesde.Value <= FechaHasta.Value;
        }
        return result?"Ok":"Fecha desde mayor a fecha hasta";
    }
    [WebMethod(EnableSession = true)]
    public static string ActualizarInsertarMensajeNew(string pAsunto, string pMensaje, string pFechaDesde_string, string pFechaHasta_string, string pSucursales)
    {
        //int pIdMensaje, string pAsunto, string pMensaje, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, List< string > pListaSucursal
        if (HttpContext.Current.Session["GestionMensajeV3Editar_id"] != null)
        {
            DateTime? FechaDesde = null;
            if (!string.IsNullOrEmpty(pFechaDesde_string))
            {
                FechaDesde = Convert.ToDateTime(pFechaDesde_string);
            }
            DateTime? FechaHasta = null;
            if (!string.IsNullOrEmpty(pFechaHasta_string))
            {
                FechaHasta = Convert.ToDateTime(pFechaHasta_string);
            }
            string sucursales = string.Empty;
            if (!string.IsNullOrWhiteSpace(pSucursales))
            {
                string[] words = pSucursales.Split('-');
                foreach (string word in words)
                {
                    sucursales += "<" + word + ">";
                }
            }
            int resultado = WebService.ActualizarInsertarMensajeNew(Convert.ToInt32(HttpContext.Current.Session["GestionMensajeV3Editar_id"]), pAsunto, pMensaje, FechaDesde, FechaHasta, false, sucursales);
        }
        return "";
    }
    [WebMethod(EnableSession = true)]
    public static string vistaPreviaMensajeNew(string pAsunto, string pMensaje)
    {
        HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Asunto"] = pAsunto;
        HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Mensaje"] = pMensaje;
        return string.Empty;
    }
    public void AgregarHtmlOculto()
    {
        string resultado = string.Empty;
        resultado += "<input type=\"hidden\" id=\"hiddenTodasSucursales\" value=\"" + Server.HtmlEncode(SitioBase.clases.Serializador.SerializarAJson(WebService.RecuperarTodasSucursales())) + "\" />";
        if (HttpContext.Current.Session["GestionMensajeV3Editar_id"] != null)
            resultado += "<input type=\"hidden\" id=\"hidden_rec_id\" value=\"" + Server.HtmlEncode(HttpContext.Current.Session["GestionMensajeV3Editar_id"].ToString()) + "\" />";
        if (HttpContext.Current.Session["GestionMensajeV3Editar_obj"] != null)
        {
            cMensajeNew o = (cMensajeNew)HttpContext.Current.Session["GestionMensajeV3Editar_obj"];
            resultado += "<input type=\"hidden\" id=\"hidden_todosSucursales\" value=\"" + Server.HtmlEncode(o.tmn_todosSucursales == null ? "" : o.tmn_todosSucursales) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_asunto\" value=\"" + Server.HtmlEncode(o.tmn_asunto) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_mensaje\" value=\"" + Server.HtmlEncode(o.tmn_mensaje) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_fechaDesde\" value=\"" + Server.HtmlEncode(o.tmn_fechaDesde == null ? "" : o.tmn_fechaDesde.Value.ToString("dd'/'MM'/'yyyy")) + "\" />";
            resultado += "<input type=\"hidden\" id=\"hidden_fechaHasta\" value=\"" + Server.HtmlEncode(o.tmn_fechaHasta == null ? "" : o.tmn_fechaHasta.Value.ToString("dd'/'MM'/'yyyy")) + "\" />";

        }
        Response.Write(resultado);
    }
}