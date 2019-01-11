using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SitioBase.clases
{
    public class ordenamientoExpresion
    {
        public ordenamientoExpresion(string sortExpression)
        {
            isOrderBy = false;
            OrderByField = string.Empty;
            OrderByAsc = true;
            if (!string.IsNullOrEmpty(sortExpression))
            {
                if (sortExpression.Length > Constantes.cDESC.Length && sortExpression.Length > Constantes.cASC.Length)
                {
                    if (sortExpression.Substring(sortExpression.Length - Constantes.cASC.Length, Constantes.cASC.Length).Contains(Constantes.cASC))
                    {
                        OrderByField = sortExpression.Substring(0, sortExpression.Length - Constantes.cASC.Length).Trim();
                        isOrderBy = true;
                    }
                    else if (sortExpression.Substring(sortExpression.Length - Constantes.cDESC.Length, Constantes.cDESC.Length).Contains(Constantes.cDESC))
                    {
                        OrderByField = sortExpression.Substring(0, sortExpression.Length - Constantes.cDESC.Length).Trim();
                        OrderByAsc = false;
                        isOrderBy = true;
                    }
                }
                if (!isOrderBy)
                {
                    OrderByField = sortExpression.Trim();
                    isOrderBy = true;
                }
            }
        }
        public bool isOrderBy { get; set; }
        public string OrderByField { get; set; }
        public bool OrderByAsc { get; set; }
    }

    public class AccesoGrilla
    {
        public static List<cFrasesFront> GetFrasesFront(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodasFrasesFront();
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "tff_nombre":
                            query = query.OrderBy(b => b.tff_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "tff_nombre":
                            query = query.OrderByDescending(b => b.tff_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<capaDatos.cRol> GetRoles(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = clases.Seguridad.RecuperarTodasRoles(filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "rol_Nombre":
                            query = query.OrderBy(b => b.rol_Nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "rol_Nombre":
                            query = query.OrderByDescending(b => b.rol_Nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }

        public static List<cSucursal> GetSucursales(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodasSucursalesDependientes();// (filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "sde_sucursal":
                            query = query.OrderBy(b => b.sde_sucursal).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "sde_sucursal":
                            query = query.OrderByDescending(b => b.sde_sucursal).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<cSucursal> GetSucursalesMontoMinimo(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodasSucursales();// (filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "suc_nombre":
                            query = query.OrderBy(b => b.suc_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "suc_nombre":
                            query = query.OrderByDescending(b => b.suc_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }

        public static List<capaDatos.cUsuario> GetUsuarios(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = clases.Seguridad.RecuperarTodosUsuarios(filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "usu_nombre":
                            query = query.OrderBy(b => b.usu_nombre).ToList();
                            break;
                        case "usu_apellido":
                            query = query.OrderBy(b => b.usu_apellido).ToList();
                            break;
                        case "usu_mail":
                            query = query.OrderBy(b => b.usu_mail).ToList();
                            break;
                        case "usu_login":
                            query = query.OrderBy(b => b.usu_login).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderBy(b => b.NombreYapellido).ToList();
                            break;
                        case "rol_Nombre":
                            query = query.OrderBy(b => b.rol_Nombre).ToList();
                            break;
                        case "usu_estadoToString":
                            query = query.OrderBy(b => b.usu_estadoToString).ToList();
                            break;
                        case "cli_nombre":
                            query = query.OrderBy(b => b.cli_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "usu_nombre":
                            query = query.OrderByDescending(b => b.usu_nombre).ToList();
                            break;
                        case "usu_apellido":
                            query = query.OrderByDescending(b => b.usu_apellido).ToList();
                            break;
                        case "usu_mail":
                            query = query.OrderByDescending(b => b.usu_mail).ToList();
                            break;
                        case "usu_login":
                            query = query.OrderByDescending(b => b.usu_login).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderByDescending(b => b.NombreYapellido).ToList();
                            break;
                        case "rol_Nombre":
                            query = query.OrderByDescending(b => b.rol_Nombre).ToList();
                            break;
                        case "usu_estadoToString":
                            query = query.OrderByDescending(b => b.usu_estadoToString).ToList();
                            break;
                        case "cli_nombre":
                            query = query.OrderByDescending(b => b.cli_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<capaDatos.cArchivo> GetArchivos(int pArc_codRelacion, string pArc_galeria, string pFiltro, string sortExpression)
        {
            //string sortExpression = "arc_titulo"; 
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodosArchivos(pArc_codRelacion, pArc_galeria, filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "arc_titulo":
                            query = query.OrderBy(b => b.arc_titulo).ToList();
                            break;
                        case "arc_orden":
                            query = query.OrderBy(b => b.arc_orden).ToList();
                            break;
                        case "arc_fecha":
                            query = query.OrderBy(b => b.arc_fecha).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderBy(b => b.NombreYapellido).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "arc_titulo":
                            query = query.OrderByDescending(b => b.arc_titulo).ToList();
                            break;
                        case "arc_orden":
                            query = query.OrderByDescending(b => b.arc_orden).ToList();
                            break;
                        case "arc_fecha":
                            query = query.OrderByDescending(b => b.arc_fecha).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderByDescending(b => b.NombreYapellido).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<capaDatos.cNoticia> GetNoticias(string sortExpression, string pFiltro, int pTipo)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodasNoticias(filtro, pTipo).OrderByDescending(b => b.not_fechaDesde).ToList();
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "not_titulo":
                            query = query.OrderBy(b => b.not_titulo).ToList();
                            break;
                        case "not_fechaDesde":
                            query = query.OrderBy(b => b.not_fechaDesde).ToList();
                            break;
                        case "not_fechaHasta":
                            query = query.OrderBy(b => b.not_fechaHasta).ToList();
                            break;
                        case "not_bajada":
                            query = query.OrderBy(b => b.not_bajada).ToList();
                            break;
                        case "not_estadoToString":
                            query = query.OrderBy(b => b.not_estadoToString).ToList();
                            break;
                        case "not_isPublicarToString":
                            query = query.OrderBy(b => b.not_isPublicarToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "not_titulo":
                            query = query.OrderByDescending(b => b.not_titulo).ToList();
                            break;
                        case "not_fechaDesde":
                            query = query.OrderByDescending(b => b.not_fechaDesde).ToList();
                            break;
                        case "not_fechaHasta":
                            query = query.OrderByDescending(b => b.not_fechaHasta).ToList();
                            break;
                        case "not_bajada":
                            query = query.OrderByDescending(b => b.not_bajada).ToList();
                            break;
                        case "not_estadoToString":
                            query = query.OrderByDescending(b => b.not_estadoToString).ToList();
                            break;
                        case "not_isPublicarToString":
                            query = query.OrderByDescending(b => b.not_isPublicarToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<capaDatos.cNoticia> GetLinkInteres(string sortExpression, string pFiltro, int pTipo)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarTodosLinks(filtro, pTipo);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "lnk_titulo":
                            query = query.OrderBy(b => b.lnk_titulo).ToList();
                            break;
                        case "lnk_bajada":
                            query = query.OrderBy(b => b.lnk_bajada).ToList();
                            break;
                        case "lnk_web":
                            query = query.OrderBy(b => b.lnk_web).ToList();
                            break;
                        case "lnk_origen":
                            query = query.OrderBy(b => b.lnk_origen).ToList();
                            break;
                        case "lnk_estadoToString":
                            query = query.OrderBy(b => b.lnk_estadoToString).ToList();
                            break;
                        case "lnk_isPublicarToString":
                            query = query.OrderBy(b => b.lnk_isPublicarToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "lnk_titulo":
                            query = query.OrderBy(b => b.lnk_titulo).ToList();
                            break;
                        case "lnk_bajada":
                            query = query.OrderBy(b => b.lnk_bajada).ToList();
                            break;
                        case "lnk_web":
                            query = query.OrderBy(b => b.lnk_web).ToList();
                            break;
                        case "lnk_origen":
                            query = query.OrderBy(b => b.lnk_origen).ToList();
                            break;
                        case "lnk_estadoToString":
                            query = query.OrderBy(b => b.lnk_estadoToString).ToList();
                            break;
                        case "lnk_isPublicarToString":
                            query = query.OrderBy(b => b.lnk_isPublicarToString).ToList();
                            break;
                        default:
                            break;
                            
                    }
                }
            }
            return query;
        }
        public static List<cMensaje> GetMensajes(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro.ToLower();
            }
            var query = WebService.RecuperarTodosMensaje();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(x => x.tme_asunto.ToLower().Contains(filtro) || x.tme_mensaje.ToLower().Contains(filtro) || x.cli_nombre.ToLower().Contains(filtro)).ToList();
            }

            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "tme_asunto":
                            query = query.OrderBy(b => b.tme_asunto).ToList();
                            break;
                        case "tme_mensaje":
                            query = query.OrderBy(b => b.tme_mensaje).ToList();
                            break;
                        case "cli_nombre":
                            query = query.OrderBy(b => b.cli_nombre).ToList();
                            break;
                        case "est_nombre":
                            query = query.OrderBy(b => b.est_nombre).ToList();
                            break;
                        case "tme_fecha":
                            query = query.OrderBy(b => b.tme_fecha).ToList();
                            break;
                        case "tme_fechaToString":
                            query = query.OrderBy(b => b.tme_fechaToString).ToList();
                            break;
                        case "tme_fechaDesdeToString":
                            query = query.OrderBy(b => (DateTime)b.tme_fechaDesde).ToList();
                            break;
                        case "tme_fechaHastaToString":
                            query = query.OrderBy(b => (DateTime)b.tme_fechaHasta).ToList();
                            break;
                        case "tme_importanteToString":
                            query = query.OrderBy(b => b.tme_importanteToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "tme_asunto":
                            query = query.OrderByDescending(b => b.tme_asunto).ToList();
                            break;
                        case "tme_mensaje":
                            query = query.OrderByDescending(b => b.tme_mensaje).ToList();
                            break;
                        case "cli_nombre":
                            query = query.OrderByDescending(b => b.cli_nombre).ToList();
                            break;
                        case "est_nombre":
                            query = query.OrderByDescending(b => b.est_nombre).ToList();
                            break;
                        case "tme_fecha":
                            query = query.OrderByDescending(b => b.tme_fecha).ToList();
                            break;
                        case "tme_fechaToString":
                            query = query.OrderByDescending(b => b.tme_fechaToString).ToList();
                            break;
                        case "tme_fechaDesdeToString":
                            query = query.OrderByDescending(b => (DateTime)b.tme_fechaDesde).ToList();
                            break;
                        case "tme_fechaHastaToString":
                            query = query.OrderByDescending(b => (DateTime)b.tme_fechaHasta).ToList();
                            break;
                        case "tme_importanteToString":
                            query = query.OrderByDescending(b => b.tme_importanteToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<cHistorialProcesos> GetHistorialProcesos(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            var query = WebService.RecuperarTodasLasSincronizaciones();
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro.ToUpper();
                query = query.Where(x => x.his_Descripcion.ToUpper().Contains(filtro) || x.his_NombreProcedimiento.ToUpper().Contains(filtro) || x.his_FechaToString.ToUpper().Contains(filtro)).ToList();
            }

            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "his_FechaToString":
                            query = query.OrderBy(b => b.his_FechaToString).ToList();
                            break;
                        case "his_Descripcion":
                            query = query.OrderBy(b => b.his_Descripcion).ToList();
                            break;
                        case "his_NombreProcedimiento":
                            query = query.OrderBy(b => b.his_NombreProcedimiento).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "his_FechaToString":
                            query = query.OrderByDescending(b => b.his_FechaToString).ToList();
                            break;
                        case "his_Descripcion":
                            query = query.OrderByDescending(b => b.his_Descripcion).ToList();
                            break;
                        case "his_NombreProcedimiento":
                            query = query.OrderByDescending(b => b.his_NombreProcedimiento).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }


        public static List<cContacto> GetContacto(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);

            var query = WebService.RecuperarTodosContactos(pFiltro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                       
                        case "con_fecha":
                            query = query.OrderBy(b => b.con_fecha).ToList();
                            break;
                        case "con_nombre":
                            query = query.OrderBy(b => b.con_nombre).ToList();
                            break;
                        case "con_empresa":
                            query = query.OrderBy(b => b.con_empresa).ToList();
                            break;
                        case "con_comentario":
                            query = query.OrderBy(b => b.con_comentario).ToList();
                            break;
                        case "con_asunto":
                            query = query.OrderBy(b => b.con_asunto).ToList();
                            break;
                        case "con_mail":
                            query = query.OrderBy(b => b.con_mail).ToList();
                            break;
                        case "con_leido":
                            query = query.OrderBy(b => b.con_leido).ToList();
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        
                        case "con_fecha":
                            query = query.OrderBy(b => b.con_fecha).ToList();
                            break;
                        case "con_nombre":
                            query = query.OrderByDescending(b => b.con_nombre).ToList();
                            break;
                        case "con_empresa":
                            query = query.OrderByDescending(b => b.con_empresa).ToList();
                            break;
                        case "con_comentario":
                            query = query.OrderByDescending(b => b.con_comentario).ToList();
                            break;
                        case "con_asunto":
                            query = query.OrderByDescending(b => b.con_asunto).ToList();
                            break;
                        case "con_mail":
                            query = query.OrderByDescending(b => b.con_mail).ToList();
                            break;
                        case "con_leido":
                            query = query.OrderBy(b => b.con_leido).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<capaDatos.cUsuario> GetUsuariosDeCliente(string sortExpression,int pIdCliente, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = WebService.RecuperarUsuariosDeCliente(Constantes.cROL_OPERADORCLIENTE, pIdCliente, filtro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "usu_nombre":
                            query = query.OrderBy(b => b.usu_nombre).ToList();
                            break;
                        case "usu_apellido":
                            query = query.OrderBy(b => b.usu_apellido).ToList();
                            break;
                        case "usu_mail":
                            query = query.OrderBy(b => b.usu_mail).ToList();
                            break;
                        case "usu_login":
                            query = query.OrderBy(b => b.usu_login).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderBy(b => b.NombreYapellido).ToList();
                            break;
                        case "rol_Nombre":
                            query = query.OrderBy(b => b.rol_Nombre).ToList();
                            break;
                        case "usu_estadoToString":
                            query = query.OrderBy(b => b.usu_estadoToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "usu_nombre":
                            query = query.OrderByDescending(b => b.usu_nombre).ToList();
                            break;
                        case "usu_apellido":
                            query = query.OrderByDescending(b => b.usu_apellido).ToList();
                            break;
                        case "usu_mail":
                            query = query.OrderByDescending(b => b.usu_mail).ToList();
                            break;
                        case "usu_login":
                            query = query.OrderByDescending(b => b.usu_login).ToList();
                            break;
                        case "NombreYapellido":
                            query = query.OrderByDescending(b => b.NombreYapellido).ToList();
                            break;
                        case "rol_Nombre":
                            query = query.OrderByDescending(b => b.rol_Nombre).ToList();
                            break;
                        case "usu_estadoToString":
                            query = query.OrderByDescending(b => b.usu_estadoToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<cCatalogo> GetCatalogo(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro;
            }
            var query = string.IsNullOrEmpty(pFiltro) ? WebService.RecuperarTodosCatalogos() : WebService.RecuperarTodosCatalogos().Where(x => x.tbc_titulo.ToUpper().Contains(pFiltro.ToUpper().Trim()) || x.tbc_estadoToString.ToUpper().Contains(pFiltro.ToUpper().Trim())).ToList();
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "tbc_titulo":
                            query = query.OrderBy(b => b.tbc_titulo).ToList();
                            break;
                        case "tbc_descripcion":
                            query = query.OrderBy(b => b.tbc_descripcion).ToList();
                            break;
                        case "tbc_fechaToString":
                            query = query.OrderBy(b => b.tbc_fechaToString).ToList();
                            break;
                        case "tbc_orden":
                            query = query.OrderBy(b => b.tbc_orden).ToList();
                            break;
                        case "tbc_estadoToString":
                            query = query.OrderBy(b => b.tbc_estadoToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "tbc_titulo":
                            query = query.OrderByDescending(b => b.tbc_titulo).ToList();
                            break;
                        case "tbc_descripcion":
                            query = query.OrderByDescending(b => b.tbc_descripcion).ToList();
                            break;
                        case "tbc_fechaToString":
                            query = query.OrderByDescending(b => b.tbc_fechaToString).ToList();
                            break;
                        case "tbc_orden":
                            query = query.OrderByDescending(b => b.tbc_orden).ToList();
                            break;
                        case "tbc_estadoToString":
                            query = query.OrderByDescending(b => b.tbc_estadoToString).ToList();
                            break;
                        default:
                            break;      
                    }
                }
            }
            return query;
        }
        public static List<cCurriculumVitae> GetCurriculumVitae(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            var query = WebService.RecuperarTodosCurriculumVitae(pFiltro);
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "tcv_nombre":
                            query = query.OrderBy(b => b.tcv_nombre).ToList();
                            break;
                        case "tcv_mail":
                            query = query.OrderBy(b => b.tcv_mail).ToList();
                            break;
                        case "tcv_dni":
                            query = query.OrderBy(b => b.tcv_dni).ToList();
                            break;
                        case "tcv_comentario":
                            query = query.OrderBy(b => b.tcv_comentario).ToList();
                            break;
                        case "tcv_estado":
                            query = query.OrderBy(b => b.tcv_estado).ToList();
                            break;
                        case "tcv_estadoToString":
                            query = query.OrderBy(b => b.tcv_estadoToString).ToList();
                            break;
                        case "tcv_fecha":
                            query = query.OrderBy(b => b.tcv_fecha).ToList();
                            break;
                        case "tcv_fechaToString":
                            query = query.OrderBy(b => b.tcv_fechaToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "tcv_nombre":
                            query = query.OrderByDescending(b => b.tcv_nombre).ToList();
                            break;
                        case "tcv_mail":
                            query = query.OrderByDescending(b => b.tcv_mail).ToList();
                            break;
                        case "tcv_dni":
                            query = query.OrderByDescending(b => b.tcv_dni).ToList();
                            break;
                        case "tcv_comentario":
                            query = query.OrderByDescending(b => b.tcv_comentario).ToList();
                            break;
                        case "tcv_estado":
                            query = query.OrderByDescending(b => b.tcv_estado).ToList();
                            break;
                        case "tcv_estadoToString":
                            query = query.OrderByDescending(b => b.tcv_estadoToString).ToList();
                            break;
                        case "tcv_fecha":
                            query = query.OrderByDescending(b => b.tcv_fecha).ToList();
                            break;
                        case "tcv_fechaToString":
                            query = query.OrderByDescending(b => b.tcv_fechaToString).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<cTiposEnvios> GetTiposEnvios(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro.Trim().ToUpper();
            }
            var query = WebService.RecuperarTodosTiposEnvios().Where(x => x.env_codigo.ToUpper().Contains(filtro) || x.env_nombre.ToUpper().Contains(filtro)).ToList();//filtro
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "env_codigo":
                            query = query.OrderBy(b => b.env_codigo).ToList();
                            break;
                        case "env_nombre":
                            query = query.OrderBy(b => b.env_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "env_codigo":
                            query = query.OrderByDescending(b => b.env_codigo).ToList();
                            break;
                        case "env_nombre":
                            query = query.OrderByDescending(b => b.env_nombre).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
        public static List<cSucursalDependienteTipoEnviosCliente> GetTiposEnviosSucursal(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro.Trim().ToUpper();
            }
            var query = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.env_codigo != null? x.env_codigo.ToUpper().Contains(filtro) :false|| x.env_nombre!= null? x.env_nombre.ToUpper().Contains(filtro) :false || x.sde_sucursal.ToUpper().Contains(filtro) || x.sde_sucursalDependiente.ToUpper().Contains(filtro)).ToList();
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "env_codigo":
                            query = query.OrderBy(b => b.env_codigo).ToList();
                            break;
                        case "env_nombre":
                            query = query.OrderBy(b => b.env_nombre).ToList();
                            break;
                        case "sde_sucursal":
                            query = query.OrderBy(b => b.sde_sucursal).ToList();
                            break;
                        case "sde_sucursalDependiente":
                            query = query.OrderBy(b => b.sde_sucursalDependiente).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "env_codigo":
                            query = query.OrderByDescending(b => b.env_codigo).ToList();
                            break;
                        case "env_nombre":
                            query = query.OrderByDescending(b => b.env_nombre).ToList();
                            break;
                        case "sde_sucursal":
                            query = query.OrderByDescending(b => b.sde_sucursal).ToList();
                            break;
                        case "sde_sucursalDependiente":
                            query = query.OrderByDescending(b => b.sde_sucursalDependiente).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }

        //
        public static List<cCadeteriaRestricciones> GetCadeteriaRestricciones(string sortExpression, string pFiltro)
        {
            ordenamientoExpresion order = new ordenamientoExpresion(sortExpression);
            string filtro = string.Empty;
            if (pFiltro != null)
            {
                filtro = pFiltro.Trim().ToUpper();
            }
            var query = WebService.RecuperarTodosCadeteriaRestricciones().Where(x => x.tcr_codigoSucursal.ToUpper().Contains(filtro) || x.tcr_UnidadesMinimas.ToString().ToUpper().Contains(filtro) || x.tcr_UnidadesMaximas.ToString().ToUpper().Contains(filtro) || x.tcr_MontoMinimo.ToString().ToUpper().Contains(filtro) ||  x.tcr_MontoIgnorar.ToString().ToUpper().Contains(filtro)).ToList();
            if (order.isOrderBy)
            {
                if (order.OrderByAsc)
                {
                    switch (order.OrderByField)
                    {
                        case "tcr_codigoSucursal":
                            query = query.OrderBy(b => b.tcr_codigoSucursal).ToList();
                            break;
                        case "tcr_MontoIgnorar":
                            query = query.OrderBy(b => b.tcr_MontoIgnorar).ToList();
                            break;
                        case "tcr_MontoMinimo":
                            query = query.OrderBy(b => b.tcr_MontoMinimo).ToList();
                            break;
                        case "tcr_UnidadesMinimas":
                            query = query.OrderBy(b => b.tcr_UnidadesMinimas).ToList();
                            break;
                        case "tcr_UnidadesMaximas":
                            query = query.OrderBy(b => b.tcr_UnidadesMaximas).ToList();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (order.OrderByField)
                    {
                        case "tcr_codigoSucursal": 
                            query = query.OrderByDescending(b => b.tcr_codigoSucursal).ToList();
                            break;
                        case "tcr_MontoIgnorar":
                            query = query.OrderByDescending(b => b.tcr_MontoIgnorar).ToList();
                            break;
                        case "tcr_MontoMinimo":
                            query = query.OrderByDescending(b => b.tcr_MontoMinimo).ToList();
                            break;
                        case "tcr_UnidadesMinimas":
                            query = query.OrderByDescending(b => b.tcr_UnidadesMinimas).ToList();
                            break;
                        case "tcr_UnidadesMaximas":
                            query = query.OrderByDescending(b => b.tcr_UnidadesMaximas).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
    }

}

