using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SitioBase.capaDatos;


public class cClientes
{
    public cClientes()
    {
        cli_tipo = string.Empty;
    }
    public cClientes(int pCli_codigo, string pCli_nombre)
    {
        cli_codigo = pCli_codigo;
        cli_nombre = pCli_nombre;
        cli_tipo = string.Empty;
    }
    public int cli_codigo { get; set; }
    public string cli_nombre { get; set; }
    public string cli_dirección { get; set; }
    public string cli_estado { get; set; }
    public string cli_telefono { get; set; }
    public string cli_codprov { get; set; }
    public string cli_localidad { get; set; }
    public string cli_email { get; set; }
    public string cli_password { get; set; }
    public string cli_paginaweb { get; set; }
    public string cli_codsuc { get; set; }
    public decimal cli_pordesespmed { get; set; }
    public decimal cli_pordesbetmed { get; set; }
    public decimal cli_pordesnetos { get; set; }
    public decimal cli_pordesfinperfcyo { get; set; }
    public decimal cli_pordescomperfcyo { get; set; }
    public bool cli_deswebespmed { get; set; }
    public bool cli_deswebnetmed { get; set; }
    public bool cli_deswebnetacc { get; set; }
    public bool cli_deswebnetperpropio { get; set; }
    public bool cli_deswebnetpercyo { get; set; }
    public bool cli_mostraremail { get; set; }
    public bool cli_mostrartelefono { get; set; }
    public bool cli_mostrardireccion { get; set; }
    public bool cli_mostrarweb { get; set; }
    public decimal cli_destransfer { get; set; }
    public string cli_login { get; set; }
    public string cli_codtpoenv { get; set; }
    public string cli_codrep { get; set; }
    public bool cli_isGLN { get; set; }
    public bool cli_tomaOfertas { get; set; }
    public bool cli_tomaPerfumeria { get; set; }
    public bool cli_tomaTransfers { get; set; }
    public string cli_tipo { get; set; }
    public string cli_IdSucursalAlternativa { get; set; }
}
public class cSucursal
{
    public cSucursal()
    {
    }
    public cSucursal(int pSde_codigo, string pSde_sucursal, string pSde_sucursalDependiente)
    {
        sde_codigo = pSde_codigo;
        sde_sucursal = pSde_sucursal;
        sde_sucursalDependiente = pSde_sucursalDependiente;
    }
    public int sde_codigo { get; set; }
    public string sde_sucursal { get; set; }
    public decimal suc_montoMinimo { get; set; }
    public string suc_codigo { get; set; }
    public string suc_nombre { get; set; }
    public string sde_sucursalDependiente { get; set; }
    public string sucursal_sucursalDependiente { get; set; }
}
public class cHorariosSucursal
{
    public cHorariosSucursal()
    {
    }
    public int sdh_SucursalDependienteHorario { get; set; }
    public string sdh_sucursal { get; set; }
    public string sdh_sucursalDependiente { get; set; }
    public string sdh_codReparto { get; set; }
    public string sdh_diaSemana { get; set; }
    public string sdh_horario { get; set; }
    public List<string> listaHorarios { get; set; }
    //public string sde_sucursal { get; set; }
    //public string sde_sucursalDependiente { get; set; }
}
/// <summary>
/// Summary description for capaClientes
/// </summary>
public class capaClientes
{
    public static DataTable RecuperarClienteAdministradorPorIdUsuarios(int usu_codigo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Recursos.spRecuperarClienteAdministradorPorIdUsuarios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paUsu_codigo = cmdComandoInicio.Parameters.Add("@usu_codigo", SqlDbType.Int);
        paUsu_codigo.Value = usu_codigo;
 
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

    public static DataSet RecuperarUsuariosDeCliente(int usu_codRol, int usu_codCliente, string filtro)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Recursos.spRecuperarUsuariosPorIdCliente", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
        SqlParameter paUsu_codRol = cmdComandoInicio.Parameters.Add("@usu_codRol", SqlDbType.Int);
        SqlParameter paUsu_codCliente = cmdComandoInicio.Parameters.Add("@usu_codCliente", SqlDbType.Int);
        paUsu_codRol.Value = usu_codRol;
        paUsu_codCliente.Value = usu_codCliente;


        if (filtro == null)
        {
            paFiltro.Value = DBNull.Value;
        }
        else
        {
            paFiltro.Value = filtro;
        }

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "UsuariosCliente");
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
    public static DataSet MostrarProvincia(string filtro)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spProvincias", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);


        if (filtro == null)
        {
            paFiltro.Value = DBNull.Value;
        }
        else
        {
            paFiltro.Value = filtro;
        }

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Clientes");
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
    public static DataTable RecuperarTodosCodigoReparto()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosCodigoReparto", Conn);
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
    public static DataTable RecuperarTodosClientes()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosClientes", Conn);
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
    public static DataTable RecuperarClientePorId(int pIdCliente)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarClientePorId", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@cli_codigo", SqlDbType.Int);
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
    public static DataSet GestiónSucursal(int? sde_codigo, string sde_sucursal, string sde_sucursalDependiente, string accion)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spGestionSucursal", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSde_codigo = cmdComandoInicio.Parameters.Add("@sde_codigo", SqlDbType.Int);
        SqlParameter paSde_sucursal = cmdComandoInicio.Parameters.Add("@sde_sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSde_sucursalDependiente = cmdComandoInicio.Parameters.Add("@sde_sucursalDependiente", SqlDbType.NVarChar, 2);
        SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        if (sde_codigo == null)
        {
            paSde_codigo.Value = DBNull.Value;
        }
        else
        {
            paSde_codigo.Value = sde_codigo;
        }

        if (sde_sucursal == null)
        {
            paSde_sucursal.Value = DBNull.Value;
        }
        else
        {
            paSde_sucursal.Value = sde_sucursal;
        }

        if (sde_sucursalDependiente == null)
        {
            paSde_sucursalDependiente.Value = DBNull.Value;
        }
        else
        {
            paSde_sucursalDependiente.Value = sde_sucursalDependiente;
        }
        paAccion.Value = accion;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "Sucursal");
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
    public static DataTable RecuperarTodasSucursales()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodasSucursal", Conn);
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
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="sdh_codigo"></param>
    ///// <param name="sdh_sucursal"></param>
    ///// <param name="sdh_horario"></param>
    ///// <param name="accion"></param>
    ///// <returns></returns>    
    //public static DataSet GestiónSucursalHorario(int? sdh_codigo, string sdh_sucursal, string sdh_horario, string accion)
    //{
    //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
    //    SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spGestionSucursalHorario", Conn);
    //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

    //    SqlParameter paSdh_codigo = cmdComandoInicio.Parameters.Add("@sdh_codigo", SqlDbType.Int);
    //    SqlParameter paSdh_sucursal = cmdComandoInicio.Parameters.Add("@sdh_sucursal", SqlDbType.NVarChar, 2);
    //    SqlParameter paSdh_horario = cmdComandoInicio.Parameters.Add("@sdh_horario", SqlDbType.NVarChar, 500);
    //    SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

    //    if (sdh_codigo == null)
    //    {
    //        paSdh_codigo.Value = DBNull.Value;
    //    }
    //    else
    //    {
    //        paSdh_codigo.Value = sdh_codigo;
    //    }

    //    if (sdh_sucursal == null)
    //    {
    //        paSdh_sucursal.Value = DBNull.Value;
    //    }
    //    else
    //    {
    //        paSdh_sucursal.Value = sdh_sucursal;
    //    }

    //    if (sdh_horario == null)
    //    {
    //        paSdh_horario.Value = DBNull.Value;
    //    }
    //    else
    //    {
    //        paSdh_horario.Value = sdh_horario;
    //    }
    //    paAccion.Value = accion;

    //    try
    //    {
    //        Conn.Open();
    //        DataSet dsResultado = new DataSet();
    //        SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
    //        da.Fill(dsResultado, "Sucursal");
    //        return dsResultado;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //    finally
    //    {
    //        if (Conn.State == ConnectionState.Open)
    //        {
    //            Conn.Close();
    //        }
    //    }
    //}



    public static DataSet MostrarClientes(int cli_codigo, string tipo, string filtro)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spClientes", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter pacli_codigo = cmdComandoInicio.Parameters.Add("@cli_codigo", SqlDbType.Int);
        SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@tipo", SqlDbType.NVarChar, 50);
        SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);

        if (cli_codigo == null)
        {
            pacli_codigo.Value = DBNull.Value;
        }
        else
        {
            pacli_codigo.Value = cli_codigo;
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
            da.Fill(dsResultado, "Clientes");
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
    public static decimal? RecuperarLimiteSaldo()
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarLimiteSaldo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        try
        {
            Conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
            dt.Load(LectorSQLdata);
            decimal? resultado = null;
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("tls_limiteSaldo"))
                {
                    resultado = Convert.ToDecimal(dt.Rows[0]["tls_limiteSaldo"]);
                }
            }

            return resultado;
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
    public static DataSet GestiónSucursalDependienteHorarios(int? sdh_SucursalDependienteHorario, string sdh_sucursal, string sdh_sucursalDependiente, string sdh_codReparto, string sdh_diaSemana, string sdh_horario, string accion)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spGestionSucursalHorarios", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSdh_SucursalDependienteHorario = cmdComandoInicio.Parameters.Add("@sdh_SucursalDependienteHorario", SqlDbType.Int);
        SqlParameter paSdh_sucursal = cmdComandoInicio.Parameters.Add("@sdh_sucursal", SqlDbType.NVarChar, 2);
        SqlParameter paSdh_sucursalDependiente = cmdComandoInicio.Parameters.Add("@sdh_sucursalDependiente", SqlDbType.NVarChar, 2);
        SqlParameter paSdh_codReparto = cmdComandoInicio.Parameters.Add("@sdh_codReparto", SqlDbType.NVarChar, 2);
        SqlParameter paSdh_diaSemana = cmdComandoInicio.Parameters.Add("@sdh_diaSemana", SqlDbType.NVarChar, 2);
        SqlParameter paSdh_horario = cmdComandoInicio.Parameters.Add("@sdh_horario", SqlDbType.NVarChar, 500);
        SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        if (sdh_SucursalDependienteHorario == null)
        {
            paSdh_SucursalDependienteHorario.Value = DBNull.Value;
        }
        else
        {
            paSdh_SucursalDependienteHorario.Value = sdh_SucursalDependienteHorario;
        }
        if (sdh_sucursal == null)
        {
            paSdh_sucursal.Value = DBNull.Value;
        }
        else
        {
            paSdh_sucursal.Value = sdh_sucursal;
        }
        if (sdh_sucursalDependiente == null)
        {
            paSdh_sucursalDependiente.Value = DBNull.Value;
        }
        else
        {
            paSdh_sucursalDependiente.Value = sdh_sucursalDependiente;
        }
        if (sdh_codReparto == null)
        {
            paSdh_codReparto.Value = DBNull.Value;
        }
        else
        {
            paSdh_codReparto.Value = sdh_codReparto;
        }

        if (sdh_diaSemana == null)
        {
            paSdh_diaSemana.Value = DBNull.Value;
        }
        else
        {
            paSdh_diaSemana.Value = sdh_diaSemana;
        }
        if (sdh_horario == null)
        {
            paSdh_horario.Value = DBNull.Value;
        }
        else
        {
            paSdh_horario.Value = sdh_horario;
        }
        paAccion.Value = accion;

        try
        {
            Conn.Open();
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
            da.Fill(dsResultado, "SucursalHorario");
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
    public static bool AgregarMontoMinimo(string suc_codigo, decimal suc_montoMinimo)
    {
        SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spAgregarMontoMinimo", Conn);
        cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        SqlParameter paSuc_codigo = cmdComandoInicio.Parameters.Add("@suc_codigo", SqlDbType.NVarChar, 2);
        SqlParameter paSuc_montoMinimo = cmdComandoInicio.Parameters.Add("@suc_montoMinimo", SqlDbType.Decimal, 9);

        paSuc_codigo.Value = suc_codigo;
        paSuc_montoMinimo.Value = suc_montoMinimo;


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