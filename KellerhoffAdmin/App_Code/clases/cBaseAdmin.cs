using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitioBase.capaDatos;

namespace SitioBase.clases
{
    public class cBaseAdmin : System.Web.UI.Page
    {
        public void Seguridad(string pConsPalabraClave)
        {
            if (!IsPostBack)
            {
                //string strVariables = " var varIsAgregar = true; ";
                //strVariables += " var varIsEditar = true; ";
                //strVariables += " var varIsEliminar = true; ";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscriptSegurida", strVariables, true);
                if (Session["BaseAdmin_PermisosRol"] != null)
                {
                    if (((capaDatos.ListaAcccionesRol)Session["BaseAdmin_PermisosRol"]).isActivo(pConsPalabraClave))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscriptSeguridad", SitioBase.clases.cBaseAdmin.ObtenerPermisosPorPalabraClave(pConsPalabraClave), true);
                    }
                    else
                    {
                        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PagSinPermiso"]);
                    }
                }
                else
                {
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["PagSinPermiso"]);
                }
            }
        }
        public static string ObtenerPermisosPorPalabraClave(string pPalabraClave)
        {
            string strVariables = string.Empty;
            if (HttpContext.Current.Session["BaseAdmin_PermisosRol"] != null)
            {
                if (((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isAgregar(pPalabraClave))
                {
                    strVariables += " var varIsAgregar = true; ";
                }
                else
                {
                    strVariables += " var varIsAgregar = false; ";
                }
                if (((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isEditar(pPalabraClave))
                {
                    strVariables += " var varIsEditar = true; ";
                }
                else
                {
                    strVariables += " var varIsEditar = false; ";
                }
                if (((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isEliminar(pPalabraClave))
                {
                    strVariables += " var varIsEliminar = true; ";
                }
                else
                {
                    strVariables += " var varIsEliminar = false; ";
                }
            }
            return strVariables;
        }
        public static string HabilitarBotonAccionToString(int pAccion, string pPalabraClave)
        {
            string resultado;// = "False";
            resultado = HabilitarBotonAccion(pAccion, pPalabraClave) ? "True" : "False";
            return resultado;
        }
        public static Boolean HabilitarBotonAccion(int pAccion, string pPalabraClave)
        {
            bool resultado = false;
            if (SitioBase.Constantes.cACCION_ALTA == pAccion)
            {
                resultado = isAgregar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_MODIFICACION == pAccion)
            {
                resultado = isEditar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_CAMBIOESTADO == pAccion)
            {
                resultado = isEditar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_CAMBIOORDEN == pAccion)
            {
                resultado = isEditar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_ISPUBLICAR == pAccion)
            {
                resultado = isEditar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_CAMBIOCONTRASEÑA == pAccion)
            {
                resultado = isEditar(pPalabraClave);
            }
            else if (SitioBase.Constantes.cACCION_ELIMINAR == pAccion)
            {
                resultado = isEliminar(pPalabraClave);
            }
            return resultado;
        }
        public static bool isEliminar(string pPalabraClave)
        {
            bool resultado = false;
            if (HttpContext.Current.Session["BaseAdmin_PermisosRol"] != null)
            {
                resultado = ((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isEliminar(pPalabraClave);
            }
            return resultado;
        }
        public static bool isActivo(string pPalabraClave)
        {
            bool resultado = false;
            if (HttpContext.Current.Session["BaseAdmin_PermisosRol"] != null)
            {
                resultado = ((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isActivo(pPalabraClave);
            }
            return resultado;
        }
        public static bool isAgregar(string pPalabraClave)
        {
            bool resultado = false;
            if (HttpContext.Current.Session["BaseAdmin_PermisosRol"] != null)
            {
                resultado = ((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isAgregar(pPalabraClave);
            }
            return resultado;
        }
        public static bool isEditar(string pPalabraClave)
        {
            bool resultado = false;
            if (HttpContext.Current.Session["BaseAdmin_PermisosRol"] != null)
            {
                resultado = ((SitioBase.capaDatos.ListaAcccionesRol)HttpContext.Current.Session["BaseAdmin_PermisosRol"]).isEditar(pPalabraClave);
            }
            return resultado;
        }
        public virtual void Modificar(int pId) { }
        public virtual void Eliminar(int pId) { }
        public virtual void Insertar() { }
        public virtual void CambiarEstado(int pId) { }
        public virtual void Publicar(int pId) { }
        public void LlamarMetodosAcciones(string pAccion, int? pId, string pPalabraClave)
        {
            switch (pAccion)
            {
                case "INSERT":
                    if (isAgregar(pPalabraClave))
                    {
                        Insertar();
                    }
                    break;
                case "UPDATE":
                    if (isEditar(pPalabraClave) && pId != null)
                    {
                        Modificar((int)pId);
                    }
                    break;
                case "DELETE":
                    if (isEliminar(pPalabraClave) && pId != null)
                    {
                        Eliminar((int)pId);
                    }
                    break;
                case "ESTADO":
                    if (isEditar(pPalabraClave) && pId != null)
                    {
                        CambiarEstado((int)pId);
                    }
                    break;
                case "PUBLICAR":
                    if (isEditar(pPalabraClave) && pId != null)
                    {
                        Publicar((int)pId);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void CargarAccionesEnVariableSession()
        {
            if (HttpContext.Current.Session["BaseAdmin_Usuario"] != null)
            {
                ListaAcccionesRol listaAcciones = SitioBase.clases.Seguridad.RecuperarTodasAccionesPorIdRol((((SitioBase.capaDatos.Usuario)(HttpContext.Current.Session["BaseAdmin_Usuario"])).idRol));
                HttpContext.Current.Session["BaseAdmin_PermisosRol"] = listaAcciones;
            }
        }
        public List<SitioBase.clases.cCombo> CargarComboSucursalesDependiente()
        {
            List<SitioBase.clases.cCombo> resultado = new List<SitioBase.clases.cCombo>();
            List<cSucursal> lista = WebService.RecuperarTodasSucursalesDependientes();
            //if (lista.Count > 0)
            //{
            //    HttpContext.Current.Session["GestionHorarioSucursal_Suc"] = lista[0].sde_sucursal;
            //    HttpContext.Current.Session["GestionHorarioSucursal_SucDependiente"] = lista[0].sde_sucursalDependiente;
            //}
            foreach (cSucursal item in lista)
            {
                SitioBase.clases.cCombo obj = new SitioBase.clases.cCombo();
                obj.id = item.sde_codigo;
                obj.nombre = item.sde_sucursal + " - " + item.sde_sucursalDependiente;
                resultado.Add(obj);
            }

            return resultado;
        }
        public List<SitioBase.clases.cCombo> CargarComboTodosCodigoReparto()
        {
            List<SitioBase.clases.cCombo> resultado = new List<SitioBase.clases.cCombo>();
            List<string> lista = WebService.RecuperarTodosCodigoReparto();
            foreach (string item in lista)
            {
                SitioBase.clases.cCombo obj = new SitioBase.clases.cCombo();
                obj.nombre = item;
                resultado.Add(obj);
            }
            return resultado;
        }

    }
}