using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SitioBase.capaDatos;

namespace SitioBase.clases
{
    public class Seguridad
    {
        public static Usuario Login(string pLogin, string pPassword, string pIp, string pHostName, string pUserAgent)
        {
            DataSet dsResultado = capaSeguridad.Login(pLogin, pPassword, pIp, pHostName, pUserAgent);
            if (dsResultado != null)
            {
                if (dsResultado.Tables["Login"].Rows.Count == 0)
                {
                    Usuario usSin = new Usuario();
                    usSin.id = -1;
                    return usSin;
                }
                else
                {
                    Usuario us = new Usuario();
                    us.id = Convert.ToInt32(dsResultado.Tables["Login"].Rows[0]["usu_codigo"]);
                    us.idRol = Convert.ToInt32(dsResultado.Tables["Login"].Rows[0]["usu_codRol"]);
                    us.NombreYApellido = Convert.ToString(dsResultado.Tables["Login"].Rows[0]["NombreYapellido"]).Trim();
                    us.idUsuarioLog = Convert.ToInt32(dsResultado.Tables["Login"].Rows[0]["ulg_codUsuarioLog"]);
                    if (dsResultado.Tables["Login"].Rows[0]["usu_estado"] != DBNull.Value)
                    {
                        us.usu_estado = Convert.ToInt32(dsResultado.Tables["Login"].Rows[0]["usu_estado"]);
                    }
                    if (dsResultado.Tables["Login"].Rows[0]["usu_codCliente"] == DBNull.Value)
                    {
                        us.usu_codCliente = null;
                    }
                    else
                    {
                        us.usu_codCliente = Convert.ToInt32(dsResultado.Tables["Login"].Rows[0]["usu_codCliente"]);
                    }
                    if (dsResultado.Tables["Login"].Rows[0]["usu_pswDesencriptado"] != DBNull.Value)
                    {
                        us.usu_pswDesencriptado = Convert.ToString(dsResultado.Tables["Login"].Rows[0]["usu_pswDesencriptado"]);
                    }
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public static void CerrarSession(int pIdUsuarioLog)
        {
            capaSeguridad.CerrarSession(pIdUsuarioLog);
        }
        public static int InsertarActualizarRol(int rol_codRol, string rol_Nombre)
        {
            string accion = rol_codRol == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            DataSet dsResultado = capaSeguridad.GestiónRol(rol_codRol, rol_Nombre, null, accion);
            int resultado = -1;
            if (rol_codRol == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Rol"].Rows[0]["rol_codRol"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Rol"].Rows[0]["rol_codRol"]);
                    }
                }
            }
            else
            {
                resultado = rol_codRol;
            }
            return resultado;
        }
        public static List<capaDatos.cRol> RecuperarTodasRoles(string pFiltro)
        {
            List<capaDatos.cRol> lista = new List<capaDatos.cRol>();
            DataSet dsResultado = capaSeguridad.GestiónRol(null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Rol"].Rows)
                {
                    capaDatos.cRol obj = new capaDatos.cRol();
                    if (item["rol_codRol"] != DBNull.Value)
                    {
                        obj.rol_codRol = Convert.ToInt32(item["rol_codRol"]);
                    }
                    if (item["rol_Nombre"] != DBNull.Value)
                    {
                        obj.rol_Nombre = item["rol_Nombre"].ToString();
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        public static capaDatos.cRol RecuperarRolPorId(int pIdRol)
        {
            capaDatos.cRol resultado = new capaDatos.cRol();
            DataSet dsResultado = capaSeguridad.GestiónRol(pIdRol, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Rol"].Rows)
                {
                    if (item["rol_codRol"] != DBNull.Value)
                    {
                        resultado.rol_codRol = Convert.ToInt32(item["rol_codRol"]);
                    }
                    if (item["rol_Nombre"] != DBNull.Value)
                    {
                        resultado.rol_Nombre = item["rol_Nombre"].ToString();
                    }
                }
            }
            return resultado;
        }
        public static List<capaDatos.cRol> RecuperarTodasRoles_Combo()
        {
            List<capaDatos.cRol> lista = new List<capaDatos.cRol>();
            DataSet dsResultado = capaSeguridad.GestiónRol(null, null, null, Constantes.cSQL_COMBO);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Rol"].Rows)
                {
                    capaDatos.cRol obj = new capaDatos.cRol();
                    if (item["rol_codRol"] != DBNull.Value)
                    {
                        obj.rol_codRol = Convert.ToInt32(item["rol_codRol"]);
                    }
                    if (item["rol_Nombre"] != DBNull.Value)
                    {
                        obj.rol_Nombre = item["rol_Nombre"].ToString();
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        public static void EliminarRol(int rol_codRol)
        {
            DataSet dsResultado = capaSeguridad.GestiónRol(rol_codRol, null, null, Constantes.cSQL_DELETE);
        }
        public static void EliminarRegla(int rgl_codRegla)
        {
            DataSet dsResultado = capaSeguridad.GestiónRegla(rgl_codRegla, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        }
        public static void EliminarUsuario(int usu_codigo)
        {
            DataSet dsResultado = capaSeguridad.GestiónUsuario(usu_codigo, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        }
        public static int InsertarActualizarRegla(int rgl_codRegla, string rgl_Descripcion, string rgl_PalabraClave, bool rgl_IsAgregarSoporta, bool rgl_IsEditarSoporta, bool rgl_IsEliminarSoporta, int? rgl_codReglaPadre)
        {
            string accion = rgl_codRegla == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            DataSet dsResultado = capaSeguridad.GestiónRegla(rgl_codRegla, rgl_Descripcion, rgl_PalabraClave, rgl_IsAgregarSoporta, rgl_IsEditarSoporta, rgl_IsEliminarSoporta, rgl_codReglaPadre, null, accion);
            int resultado = -1;
            if (rgl_codRegla == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Regla"].Rows[0]["rgl_codRegla"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Regla"].Rows[0]["rgl_codRegla"]);
                    }
                }
            }
            else
            {
                resultado = rgl_codRegla;
            }
            return resultado;
        }
        public static List<capaDatos.cRegla> RecuperarTodasReglas(string pFiltro)
        {
            List<capaDatos.cRegla> lista = new List<capaDatos.cRegla>();
            DataSet dsResultado = capaSeguridad.GestiónRegla(null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
                {
                    capaDatos.cRegla obj = new capaDatos.cRegla();
                    if (item["rgl_codRegla"] != DBNull.Value)
                    {
                        obj.rgl_codRegla = Convert.ToInt32(item["rgl_codRegla"]);
                    }
                    if (item["rgl_Descripcion"] != DBNull.Value)
                    {
                        obj.rgl_Descripcion = item["rgl_Descripcion"].ToString();
                    }
                    if (item["rgl_PalabraClave"] != DBNull.Value)
                    {
                        obj.rgl_PalabraClave = item["rgl_PalabraClave"].ToString();
                    }
                    if (item["rgl_IsAgregarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsAgregarSoporta = Convert.ToBoolean(item["rgl_IsAgregarSoporta"]);
                    }
                    if (item["rgl_IsEditarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsEditarSoporta = Convert.ToBoolean(item["rgl_IsEditarSoporta"]);
                    }
                    if (item["rgl_IsEliminarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsEliminarSoporta = Convert.ToBoolean(item["rgl_IsEliminarSoporta"]);
                    }
                    if (item["rgl_codReglaPadre"] != DBNull.Value)
                    {
                        obj.rgl_codReglaPadre = Convert.ToInt32(item["rgl_codReglaPadre"]);
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        public static capaDatos.cRegla RecuperarReglaPorId(int pIdRegla)
        {
            capaDatos.cRegla obj = null;
            DataSet dsResultado = capaSeguridad.GestiónRegla(pIdRegla, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
                {
                    obj = new capaDatos.cRegla();
                    if (item["rgl_codRegla"] != DBNull.Value)
                    {
                        obj.rgl_codRegla = Convert.ToInt32(item["rgl_codRegla"]);
                    }
                    if (item["rgl_Descripcion"] != DBNull.Value)
                    {
                        obj.rgl_Descripcion = item["rgl_Descripcion"].ToString();
                    }
                    if (item["rgl_PalabraClave"] != DBNull.Value)
                    {
                        obj.rgl_PalabraClave = item["rgl_PalabraClave"].ToString();
                    }
                    if (item["rgl_IsAgregarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsAgregarSoporta = Convert.ToBoolean(item["rgl_IsAgregarSoporta"]);
                    }
                    if (item["rgl_IsEditarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsEditarSoporta = Convert.ToBoolean(item["rgl_IsEditarSoporta"]);
                    }
                    if (item["rgl_IsEliminarSoporta"] != DBNull.Value)
                    {
                        obj.rgl_IsEliminarSoporta = Convert.ToBoolean(item["rgl_IsEliminarSoporta"]);
                    }
                    if (item["rgl_codReglaPadre"] != DBNull.Value)
                    {
                        obj.rgl_codReglaPadre = Convert.ToInt32(item["rgl_codReglaPadre"]);
                    }
                    break;
                }
            }
            return obj;
        }
        public static List<clases.cCombo> RecuperarTodasReglas_Combo()
        {
            List<clases.cCombo> lista = new List<clases.cCombo>();
            DataSet dsResultado = capaSeguridad.GestiónRegla(null, null, null, null, null, null, null, null, Constantes.cSQL_COMBO);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
                {
                    clases.cCombo obj = new clases.cCombo();
                    if (item["rgl_codRegla"] != DBNull.Value)
                    {
                        obj.id = Convert.ToInt32(item["rgl_codRegla"]);
                    }
                    if (item["rgl_Descripcion"] != DBNull.Value)
                    {
                        obj.nombre = item["rgl_Descripcion"].ToString();
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        public static capaDatos.ListaAcccionesRol RecuperarTodasAccionesPorIdRol(int pIdRol)
        {
            DataTable tablaAcciones = capaDatos.capaSeguridad.RecuperarTodasAccionesRol(pIdRol);
            capaDatos.ListaAcccionesRol listaAcciones = new capaDatos.ListaAcccionesRol();
            foreach (DataRow item in tablaAcciones.Rows)
            {
                capaDatos.AcccionesRol acRol = new capaDatos.AcccionesRol();

                acRol.idRegla = Convert.ToInt32(item["rgl_codRegla"]);
                acRol.palabraClave = item["rgl_PalabraClave"].ToString();

                if (DBNull.Value.Equals(item["rrr_codRelacionRolRegla"]))
                {
                    acRol.idReglaRol = null;
                }
                else
                {
                    acRol.idReglaRol = Convert.ToInt32(item["rrr_codRelacionRolRegla"]);
                }

                if (DBNull.Value.Equals(item["rrr_IsActivo"]))
                {
                    acRol.isActivo = false;
                }
                else
                {
                    acRol.isActivo = Convert.ToBoolean(item["rrr_IsActivo"]);
                }

                if (DBNull.Value.Equals(item["rrr_IsAgregar"]))
                {
                    acRol.isAgregar = false;
                }
                else
                {
                    acRol.isAgregar = Convert.ToBoolean(item["rrr_IsAgregar"]);
                }

                if (DBNull.Value.Equals(item["rrr_IsEditar"]))
                {
                    acRol.isEditar = false;
                }
                else
                {
                    acRol.isEditar = Convert.ToBoolean(item["rrr_IsEditar"]);
                }

                if (DBNull.Value.Equals(item["rrr_IsEliminar"]))
                {
                    acRol.isEliminar = false;
                }
                else
                {
                    acRol.isEliminar = Convert.ToBoolean(item["rrr_IsEliminar"]);
                }

                listaAcciones.Agregar(acRol);
            }
            return listaAcciones;
        }

        public static void InsertarActualizarRelacionRolRegla(int pIdRol, string pXML)
        {
            DataSet dsResultado = capaSeguridad.GestiónRoleRegla(pIdRol, null, pXML, Constantes.cSQL_UPDATE);
        }

        public static List<capaDatos.ReglaPorRol> RecuperarRelacionRolReglasPorRol(int pIdRol)
        {
            List<capaDatos.ReglaPorRol> listaResultado = new List<capaDatos.ReglaPorRol>();
            DataSet dsResultado = capaSeguridad.GestiónRoleRegla(pIdRol, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["RelacionRoleRegla"].Rows)
                {
                    capaDatos.ReglaPorRol obj = new capaDatos.ReglaPorRol();
                    obj.idRegla = Convert.ToInt32(item["rrr_codRegla"]);
                    obj.idRelacionReglaRol = Convert.ToInt32(item["rrr_codRelacionRolRegla"]);
                    obj.isActivo = Convert.ToBoolean(item["rrr_IsActivo"]);
                    if (item["rrr_IsAgregar"] is DBNull)
                    {
                        obj.isAgregar = null;
                    }
                    else
                    {
                        obj.isAgregar = Convert.ToBoolean(item["rrr_IsAgregar"]);
                    }
                    if (item["rrr_IsEditar"] is DBNull)
                    {
                        obj.isEditar = null;
                    }
                    else
                    {
                        obj.isEditar = Convert.ToBoolean(item["rrr_IsEditar"]);
                    }
                    if (item["rrr_IsEliminar"] is DBNull)
                    {
                        obj.isEliminar = null;
                    }
                    else
                    {
                        obj.isEliminar = Convert.ToBoolean(item["rrr_IsEliminar"]);
                    }
                    listaResultado.Add(obj);
                }
            }
            return listaResultado;
        }
        //
        public static List<int> RecuperarTodosIdReglasHijas(int pIdRegla, List<capaDatos.cRegla> pListaRegla)
        {
            List<int> listaHijas = new List<int>();
            if (pListaRegla != null)
            {
                List<capaDatos.cRegla> listaRegla = pListaRegla.Where(x => x.rgl_codReglaPadre == pIdRegla).ToList();
                foreach (capaDatos.cRegla item in listaRegla)
                {
                    listaHijas.Add(item.rgl_codRegla);
                }
            }
            return listaHijas;
        }
        public static List<capaDatos.ListaCheck> RecuperarTodasReglasPorNivel()
        {
            List<capaDatos.cRegla> listaReglaParametro = RecuperarTodasReglas(string.Empty);
            List<capaDatos.ListaCheck> listaResultado = new List<capaDatos.ListaCheck>();
            DataTable tabla = capaDatos.capaSeguridad.RecuperarTodasReglasPorNivel();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                ListaCheck obj = new ListaCheck();

                obj.id = Convert.ToInt32(tabla.Rows[i]["rgl_codRegla"]);
                obj.descripcion = tabla.Rows[i]["rgl_Descripcion"].ToString();
                obj.palabra = tabla.Rows[i]["rgl_PalabraClave"].ToString();
                obj.Nivel = Convert.ToInt32(tabla.Rows[i]["Level"]);
                if (tabla.Rows[i]["rgl_codReglaPadre"] is DBNull)
                {
                    obj.idPadreRegla = null;
                }
                else
                {
                    obj.idPadreRegla = Convert.ToInt32(tabla.Rows[i]["rgl_codReglaPadre"]);
                }
                obj.listaIdHijas = RecuperarTodosIdReglasHijas(Convert.ToInt32(tabla.Rows[i]["rgl_codRegla"]), listaReglaParametro);
                obj.isGraficada = false;
                //
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsAgregarSoporta"]))
                {
                    obj.checkAgregar = 0;
                }
                else
                {
                    obj.checkAgregar = -1;
                }
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsEditarSoporta"]))
                {
                    obj.checkEditar = 0;
                }
                else
                {
                    obj.checkEditar = -1;
                }
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsEliminarSoporta"]))
                {
                    obj.checkEliminar = 0;
                }
                else
                {
                    obj.checkEliminar = -1;
                }

                listaResultado.Add(obj);

            }

            return listaResultado;
        }
        public static int InsertarActualizarUsuario(int usu_codigo, int usu_codRol, int? usu_codCliente, string usu_nombre, string usu_apellido, string usu_mail, string usu_login, string usu_psw, string usu_observacion, int? usu_codUsuarioUltMov)
        {
            string accion = usu_codigo == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
            int codigoAccion = usu_codigo == 0 ? Constantes.cACCION_ALTA : Constantes.cACCION_MODIFICACION;
            int? codigoEstado = usu_codigo == 0 ? Constantes.cESTADO_ACTIVO : (int?)null;
            DataSet dsResultado = capaSeguridad.GestiónUsuario(usu_codigo, usu_codRol, usu_codCliente, usu_nombre, usu_apellido, usu_mail, usu_login, usu_psw, usu_observacion, usu_codUsuarioUltMov, codigoAccion, codigoEstado, null, accion);
            int resultado = -1;
            if (usu_codigo == 0)
            {
                if (dsResultado != null)
                {
                    if (dsResultado.Tables["Usuario"].Rows[0]["usu_codigo"] != DBNull.Value)
                    {
                        resultado = Convert.ToInt32(dsResultado.Tables["Usuario"].Rows[0]["usu_codigo"]);
                    }
                }
            }
            else
            {
                resultado = usu_codigo;
            }
            return resultado;
        }
        public static capaDatos.cUsuario ConvertToUsuario(DataRow pItem)
        {
            capaDatos.cUsuario obj = new capaDatos.cUsuario();
            if (pItem["usu_codigo"] != DBNull.Value)
            {
                obj.usu_codigo = Convert.ToInt32(pItem["usu_codigo"]);
            }
            if (pItem["usu_codRol"] != DBNull.Value)
            {
                obj.usu_codRol = Convert.ToInt32(pItem["usu_codRol"]);
            }
            if (pItem["usu_codCliente"] != DBNull.Value)
            {
                obj.usu_codCliente = Convert.ToInt32(pItem["usu_codCliente"]);
                if (pItem.Table.Columns.Contains("cli_nombre"))
                {
                    obj.cli_nombre = pItem["cli_nombre"].ToString();
                }
            }
            else
            {
                obj.usu_codCliente = null;
            }
            if (pItem["usu_nombre"] != DBNull.Value)
            {
                obj.usu_nombre = pItem["usu_nombre"].ToString();
            }
            if (pItem["usu_apellido"] != DBNull.Value)
            {
                obj.usu_apellido = pItem["usu_apellido"].ToString();
            }
            if (pItem["NombreYapellido"] != DBNull.Value)
            {
                obj.NombreYapellido = pItem["NombreYapellido"].ToString();
            }
            if (pItem["usu_login"] != DBNull.Value)
            {
                obj.usu_login = pItem["usu_login"].ToString();
            }
            if (pItem["usu_mail"] != DBNull.Value)
            {
                obj.usu_mail = pItem["usu_mail"].ToString();
            }
            if (pItem["usu_pswDesencriptado"] != DBNull.Value)
            {
                obj.usu_pswDesencriptado = pItem["usu_pswDesencriptado"].ToString();
            }
            if (pItem["rol_Nombre"] != DBNull.Value)
            {
                obj.rol_Nombre = pItem["rol_Nombre"].ToString();
            }
            if (pItem["usu_observacion"] != DBNull.Value)
            {
                obj.usu_observacion = pItem["usu_observacion"].ToString();
            }
            if (pItem["usu_estado"] != DBNull.Value)
            {
                obj.usu_estado = Convert.ToInt32(pItem["usu_estado"]);
                obj.usu_estadoToString = SitioBase.capaDatos.capaSeguridad.obtenerStringEstado(obj.usu_estado);
            }
            return obj;
        }
        public static List<capaDatos.cUsuario> RecuperarTodosUsuarios(string pFiltro)
        {
            List<capaDatos.cUsuario> lista = new List<capaDatos.cUsuario>();
            DataSet dsResultado = capaSeguridad.GestiónUsuario(null, null, null, null, null, null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Usuario"].Rows)
                {
                    lista.Add(ConvertToUsuario(item));
                }
            }
            return lista;
        }
        public static capaDatos.cUsuario RecuperarUsuarioPorId(int pIdUsuario)
        {
            capaDatos.cUsuario obj = null;
            DataSet dsResultado = capaSeguridad.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Usuario"].Rows)
                {
                    obj = ConvertToUsuario(item);
                    break;
                }
            }
            return obj;
        }
        public static void CambiarEstadoUsuarioPorId(int pIdUsuario, int pIdEstado, int pIdUsuarioEnSession)
        {
            capaSeguridad.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, null, null, pIdUsuarioEnSession, Constantes.cACCION_CAMBIOESTADO, pIdEstado, null, Constantes.cSQL_ESTADO);
        }
        public static void CambiarContraseñaUsuario(int pIdUsuario, string pConstraseña, int? pIdUsuarioEnSession)
        {
            //List<capaDatos.cUsuario> lista = new List<capaDatos.cUsuario>();
            DataSet dsResultado = capaSeguridad.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, pConstraseña, null, pIdUsuarioEnSession, Constantes.cACCION_CAMBIOCONTRASEÑA, null, null, Constantes.cSQL_CAMBIOCONTRASEÑA);
        }
        //public static void CambiarContraseñaUsuario(int pIdUsuario, string pConstraseña, int? pIdUsuarioEnSession)
        //{
        //    List<capaDatos.cUsuario> lista = new List<capaDatos.cUsuario>();
        //    DataSet dsResultado = capaSeguridad.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, null, pIdUsuarioEnSession, null, null, null, Constantes.cSQL_CAMBIOCONTRASEÑA);
        //}  
        public static bool IsRepetidoLogin(int pIdUsuario, string pLogin)
        {
            bool resultado = false;
            DataTable dtResultado = capaSeguridad.IsRepetidoLogin(pIdUsuario, pLogin);
            if (dtResultado != null)
            {
                if (dtResultado.Rows.Count > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
    }
}