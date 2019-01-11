using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SitioBase.capaDatos
{
    public class cNoticia
    {
        public cNoticia()
        {

        }
        public int not_codNoticia { get; set; }
        public DateTime? not_fechaDesde { get; set; }
        public DateTime? not_fechaHasta { get; set; }
        public string not_fechaDesdeReducido { get; set; }
        public string not_fechaHastaReducido { get; set; }
        public string not_titulo { get; set; }
        public string not_bajada { get; set; }
        public string not_descripcion { get; set; }
        public string not_destacado { get; set; }
        public int not_codTipoNoticia { get; set; }
        public bool? not_isPublicar { get; set; }
        public string not_isPublicarToString { get; set; }
        public int not_estado{ get; set; }
        public string not_estadoToString { get; set; }
        public int not_accion { get; set; }
        public int not_codUsuarioUltMov { get; set; }
        public DateTime? not_fechaUltMov { get; set; }

        public int hom_codNoticia1 { get; set; }
        public int hom_codNoticia2 { get; set; }
        public int hom_codNoticia3 { get; set; }

        public int lnk_codLinks { get; set; }
        public string lnk_titulo { get; set; }
        public string lnk_bajada { get; set; }
        public string lnk_descripcion { get; set; }
        public string lnk_web { get; set; }
        public string lnk_origen { get; set; }
        public int lnk_codTipo { get; set; }
        public bool? lnk_isPublicar { get; set; }
        public string lnk_isPublicarToString { get; set; }
        public int lnk_estado { get; set; }
        public string lnk_estadoToString { get; set; }
        public int lnk_accion { get; set; }
        public int lnk_codUsuarioUltMov { get; set; }
        public DateTime? lnk_fechaUltMov { get; set; }
    }
    public class capaNoticia
    {
        public static DataSet GestiónNoticia(int? not_codNoticia, DateTime? not_fechaDesde, DateTime? not_fechaHasta, string not_titulo, string not_bajada, string not_descripcion, string not_destacado, int? not_codTipoNoticia, bool? not_isPublicar, int? not_estado, int? not_accion, int? not_codUsuarioUltMov, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Noticias.spGestionNoticias", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paNot_codNoticia = cmdComandoInicio.Parameters.Add("@not_codNoticia", SqlDbType.Int);
            SqlParameter paNot_fechaDesde = cmdComandoInicio.Parameters.Add("@not_fechaDesde", SqlDbType.DateTime);
            SqlParameter paNot_fechaHasta = cmdComandoInicio.Parameters.Add("@not_fechaHasta", SqlDbType.DateTime);
            SqlParameter paNot_titulo = cmdComandoInicio.Parameters.Add("@not_titulo", SqlDbType.NVarChar,300);
            SqlParameter paNot_bajada = cmdComandoInicio.Parameters.Add("@not_bajada", SqlDbType.NVarChar, -1);
            SqlParameter paNot_descripcion = cmdComandoInicio.Parameters.Add("@not_descripcion", SqlDbType.NVarChar, -1);
            SqlParameter paNot_destacado = cmdComandoInicio.Parameters.Add("@not_destacado", SqlDbType.NVarChar, -1);
            SqlParameter paNot_codTipoNoticia = cmdComandoInicio.Parameters.Add("@not_codTipoNoticia", SqlDbType.Int);
            SqlParameter paNot_isPublicar = cmdComandoInicio.Parameters.Add("@not_isPublicar", SqlDbType.Bit); 
            SqlParameter paNot_accion = cmdComandoInicio.Parameters.Add("@not_accion", SqlDbType.Int);
            SqlParameter paNot_estado = cmdComandoInicio.Parameters.Add("@not_estado", SqlDbType.Int);
            SqlParameter paNot_codUsuarioUltMov = cmdComandoInicio.Parameters.Add("@not_codUsuarioUltMov", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (not_codNoticia == null)
            {
                paNot_codNoticia.Value = DBNull.Value;
            }
            else
            {
                paNot_codNoticia.Value = not_codNoticia;
            }
            if (not_fechaDesde == null)
            {
                paNot_fechaDesde.Value = DBNull.Value;
            }
            else
            {
                paNot_fechaDesde.Value = not_fechaDesde;
            }
            if (not_fechaHasta == null)
            {
                paNot_fechaHasta.Value = DBNull.Value;
            }
            else
            {
                paNot_fechaHasta.Value = not_fechaHasta;
            }
            if (not_titulo == null)
            {
                paNot_titulo.Value = DBNull.Value;
            }
            else
            {
                paNot_titulo.Value = not_titulo;
            }
            if (not_bajada == null)
            {
                paNot_bajada.Value = DBNull.Value;
            }
            else
            {
                paNot_bajada.Value = not_bajada;
            }
            if (not_descripcion == null)
            {
                paNot_descripcion.Value = DBNull.Value;
            }
            else
            {
                paNot_descripcion.Value = not_descripcion;
            }
            if (not_destacado == null)
            {
                paNot_destacado.Value = DBNull.Value;
            }
            else
            {
                paNot_destacado.Value = not_destacado;
            }
            if (not_codTipoNoticia == null)
            {
                paNot_codTipoNoticia.Value = DBNull.Value;
            }
            else
            {
                paNot_codTipoNoticia.Value = not_codTipoNoticia;
            }
            if (not_isPublicar == null)
            {
                paNot_isPublicar.Value = DBNull.Value;
            }
            else
            {
                paNot_isPublicar.Value = not_isPublicar;
            }
            if (not_accion == null)
            {
                paNot_accion.Value = DBNull.Value;
            }
            else
            {
                paNot_accion.Value = not_accion;
            }
            if (not_estado == null)
            {
                paNot_estado.Value = DBNull.Value;
            }
            else
            {
                paNot_estado.Value = not_estado;
            }
            if (not_codUsuarioUltMov == null)
            {
                paNot_codUsuarioUltMov.Value = DBNull.Value;
            }
            else
            {
                paNot_codUsuarioUltMov.Value = not_codUsuarioUltMov;
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
                da.Fill(dsResultado, "Noticia");
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

        public static DataSet GestiónLinks(int? lnk_codLinks, string lnk_titulo, string lnk_bajada, string lnk_descripcion, string lnk_web, string lnk_origen, int? lnk_codTipo, bool? lnk_isPublicar, int? lnk_estado, int? lnk_accion, int? lnk_codUsuarioUltMov, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Noticias.spGestionLinks", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter palnk_codLinks = cmdComandoInicio.Parameters.Add("@lnk_codLinks", SqlDbType.Int);
            SqlParameter palnk_titulo = cmdComandoInicio.Parameters.Add("@lnk_titulo", SqlDbType.NVarChar, 300);
            SqlParameter palnk_bajada = cmdComandoInicio.Parameters.Add("@lnk_bajada", SqlDbType.NVarChar, -1);
            SqlParameter palnk_descripcion = cmdComandoInicio.Parameters.Add("@lnk_descripcion", SqlDbType.NVarChar, -1);
            SqlParameter palnk_web = cmdComandoInicio.Parameters.Add("@lnk_web", SqlDbType.NVarChar, 300);
            SqlParameter palnk_origen = cmdComandoInicio.Parameters.Add("@lnk_origen", SqlDbType.NVarChar, 300);
            SqlParameter palnk_codTipo = cmdComandoInicio.Parameters.Add("@lnk_codTipo", SqlDbType.Int);
            SqlParameter palnk_isPublicar = cmdComandoInicio.Parameters.Add("@lnk_isPublicar", SqlDbType.Bit);
            SqlParameter palnk_accion = cmdComandoInicio.Parameters.Add("@lnk_accion", SqlDbType.Int);
            SqlParameter palnk_estado = cmdComandoInicio.Parameters.Add("@lnk_estado", SqlDbType.Int);
            SqlParameter palnk_codUsuarioUltMov = cmdComandoInicio.Parameters.Add("@lnk_codUsuarioUltMov", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (lnk_codLinks == null)
            {
                palnk_codLinks.Value = DBNull.Value;
            }
            else
            {
                palnk_codLinks.Value = lnk_codLinks;
            }
           
            if (lnk_titulo == null)
            {
                palnk_titulo.Value = DBNull.Value;
            }
            else
            {
                palnk_titulo.Value = lnk_titulo;
            }
            if (lnk_bajada == null)
            {
                palnk_bajada.Value = DBNull.Value;
            }
            else
            {
                palnk_bajada.Value = lnk_bajada;
            }
            if (lnk_descripcion == null)
            {
                palnk_descripcion.Value = DBNull.Value;
            }
            else
            {
                palnk_descripcion.Value = lnk_descripcion;
            }
            if (lnk_web == null)
            {
                palnk_web.Value = DBNull.Value;
            }
            else
            {
                palnk_web.Value = lnk_web;
            }
            if (lnk_origen == null)
            {
                palnk_origen.Value = DBNull.Value;
            }
            else
            {
                palnk_origen.Value = lnk_origen;
            }
            if (lnk_codTipo == null)
            {
                palnk_codTipo.Value = DBNull.Value;
            }
            else
            {
                palnk_codTipo.Value = lnk_codTipo;
            }
            if (lnk_isPublicar == null)
            {
                palnk_isPublicar.Value = DBNull.Value;
            }
            else
            {
                palnk_isPublicar.Value = lnk_isPublicar;
            }
            if (lnk_accion == null)
            {
                palnk_accion.Value = DBNull.Value;
            }
            else
            {
                palnk_accion.Value = lnk_accion;
            }
            if (lnk_estado == null)
            {
                palnk_estado.Value = DBNull.Value;
            }
            else
            {
                palnk_estado.Value = lnk_estado;
            }
            if (lnk_codUsuarioUltMov == null)
            {
                palnk_codUsuarioUltMov.Value = DBNull.Value;
            }
            else
            {
                palnk_codUsuarioUltMov.Value = lnk_codUsuarioUltMov;
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
                da.Fill(dsResultado, "Links");
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

        public static DataSet MostrarNoticias(int not_codNoticia, int tipo, string filtro)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Noticias.spNoticias", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paNot_codNoticia = cmdComandoInicio.Parameters.Add("@not_codNoticia", SqlDbType.Int);
            SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@tipo", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);

            if (not_codNoticia == null)
            {
                paNot_codNoticia.Value = DBNull.Value;
            }
            else
            {
                paNot_codNoticia.Value = not_codNoticia;
            }
           


            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }


            paTipo.Value = tipo;
            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Noticia");
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

        public static DataSet MostrarLinks(int lnk_codLinks, int tipo, string filtro)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Noticias.spLinks", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter palnk_codLinks = cmdComandoInicio.Parameters.Add("@lnk_codLinks", SqlDbType.Int);
            SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@tipo", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);

            if (lnk_codLinks == null)
            {
                palnk_codLinks.Value = DBNull.Value;
            }
            else
            {
                palnk_codLinks.Value = lnk_codLinks;
            }

            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }


            paTipo.Value = tipo;
            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Links");
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

        public static DataSet MostrarNoticiaHome(int? hom_codNoticia1, int? hom_codNoticia2, int? hom_codNoticia3, string filtro, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("spNoticiasHome", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter pahom_codNoticia1 = cmdComandoInicio.Parameters.Add("@hom_codNoticia1", SqlDbType.Int);
            SqlParameter pahom_codNoticia2 = cmdComandoInicio.Parameters.Add("@hom_codNoticia2", SqlDbType.Int);
            SqlParameter pahom_codNoticia3 = cmdComandoInicio.Parameters.Add("@hom_codNoticia3", SqlDbType.Int);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);


            if (hom_codNoticia1 == null)
            {
                pahom_codNoticia1.Value = DBNull.Value;
            }
            else
            {
                pahom_codNoticia1.Value = hom_codNoticia1;
            }

            if (hom_codNoticia2 == null)
            {
                pahom_codNoticia2.Value = DBNull.Value;
            }
            else
            {
                pahom_codNoticia2.Value = hom_codNoticia2;
            }
            if (hom_codNoticia3 == null)
            {
                pahom_codNoticia3.Value = DBNull.Value;
            }
            else
            {
                pahom_codNoticia3.Value = hom_codNoticia3;
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
                da.Fill(dsResultado, "Home");
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
}