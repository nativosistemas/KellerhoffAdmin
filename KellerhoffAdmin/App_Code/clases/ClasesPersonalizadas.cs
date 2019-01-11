using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SitioBase.capaDatos;

/// <summary>
/// Summary description for ClasesPersonalizadas
/// </summary>
public class ResultTransfer
{
    public bool isNotError { get; set; }
    public List<cSucursalCarritoTransfer> listSucursalCarritoTransfer { get; set; }
    public List<cProductosAndCantidad> listProductosAndCantidadError { get; set; }
}
public class cPageClientes : System.Web.UI.Page
{
    [WebMethod(EnableSession = true)]
    public static bool SetearNroSemana(int pNroSemana)
    {
        HttpContext.Current.Session["FichaDebeHaberSaldo_NroSemana"] = pNroSemana;
        return true;
    }
    [WebMethod(EnableSession = true)]
    public static bool IsBanderaUsarDllSucursal(string pSucursal)
    {
        bool resultado = WebService.IsBanderaCodigo(pSucursal);
        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public static bool IsBanderaUsarDll()
    {
        bool resultado = WebService.IsBanderaCodigo(SitioBase.Constantes.cBAN_SERVIDORDLL);
        return resultado;
    }
    /// <summary>
    /// 0 si
    /// 1 no permite usar dll
    /// 2 cliente inhabilitado
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static int IsHacerPedidos()
    {
        int resultado = 0;
        if (!IsBanderaUsarDll())
        {
            resultado = 1;
        }
        else
        {
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes objCliente = WebService.RecuperarClientePorId(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
                if (objCliente != null)
                {
                    ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado = objCliente.cli_estado;
                }
                if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado.ToUpper() == SitioBase.Constantes.cESTADO_INH)
                {
                    resultado = 2;
                }
            }
        }
        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public static int IsHacerPedidos(string pSucursal)
    {
        int resultado = 0;
        if (!IsBanderaUsarDllSucursal(pSucursal))
        {
            resultado = 1;
        }
        else
        {
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes objCliente = WebService.RecuperarClientePorId(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
                if (objCliente != null)
                {
                    ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado = objCliente.cli_estado;
                }
                if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado.ToUpper() == SitioBase.Constantes.cESTADO_INH)
                {
                    resultado = 2;
                }
            }
        }
        return resultado;
    }
}
public class cGrillaHorarioSucursal
{
    public string dia { get; set; }
    public string hora { get; set; }
}
public class cProductosAndCantidad
{
    public string codSucursal { get; set; }
    public string codProducto { get; set; }
    public string codProductoNombre { get; set; }
    public int cantidad { get; set; }
    public bool isTransferFacturacionDirecta { get; set; }
    public int tde_codtfr { get; set; }
}
public class cjSonBuscadorProductos
{
    public List<string> listaSucursal { get; set; }
    public List<cProductosBuscador> listaProductos { get; set; }
    public int CantidadRegistroTotal { get; set; }
}
public class cjSonBuscadorProductosTransfer
{
    public List<string> listaSucursal { get; set; }
    public List<cTransfer> listaProductos { get; set; }
    public int CantidadRegistroTotal { get; set; }
}
public class cCarrito
{
    public cCarrito() { listaProductos = new List<cProductosEnCarrito>(); }
    public int lrc_id { get; set; }
    public string codSucursal { get; set; }
    public string proximoHorarioEntrega { get; set; }
    public List<cProductosEnCarrito> listaProductos { get; set; }
}
public class cCarritoTransfer
{
    public cCarritoTransfer() { listaProductos = new List<cProductosEnCarrito>(); }
    public int ctr_id { get; set; }
    public string ctr_codSucursal { get; set; }
    public int tfr_codigo { get; set; }
    public string tfr_nombre { get; set; }
    public bool tfr_deshab { get; set; }
    public decimal? tfr_pordesadi { get; set; }
    public string tfr_tipo { get; set; }
    //public string proximoHorarioEntrega { get; set; }
    public List<cProductosEnCarrito> listaProductos { get; set; }
}
public class cSucursalCarritoTransfer
{
    public string Sucursal { get; set; }
    public string proximoHorarioEntrega { get; set; }
    public List<cCarritoTransfer> listaTransfer { get; set; }
}
public class cProductosEnCarrito : cTransferDetalle
{
    public string codProducto { get; set; }
    public int cantidad { get; set; }
    public int idUsuario { get; set; }
    //public string tde_codpro { get; set; }
    //public decimal tde_predescuento { get; set; }
    //public decimal tde_prepublico { get; set; }
    public string stk_stock { get; set; }
    //public decimal PrecioFinalTransfer { get; set; }
    public bool isProductoFacturacionDirecta { get; set; }
    //public void CargarTransferDetalle(cTransferDetalle pValor)
    //{
    //    base.tde_codpro = pValor.tde_codpro;
    //    base.tde_codtfr = pValor.tde_codtfr;
    //    base.tde_descripcion = pValor.tde_descripcion;
    //    base.tde_fijuni = pValor.tde_fijuni;
    //    base.tde_maxuni = pValor.tde_maxuni;
    //    base.tde_minuni = pValor.tde_minuni;
    //    base.tde_muluni = pValor.tde_muluni;
    //    base.tde_predescuento = pValor.tde_predescuento;
    //    base.tde_prepublico = pValor.tde_prepublico;
    //    base.tde_proobligatorio = pValor.tde_proobligatorio;
    //    base.tde_unidadesbonificadas = pValor.tde_unidadesbonificadas;
    //    base.tde_unidadesbonificadasdescripcion = pValor.tde_unidadesbonificadasdescripcion;
    //}
    public void CargarTransferYTransferDetalle(cTransferDetalle pValor)
    {
        base.tde_codpro = pValor.tde_codpro;
        base.tde_codtfr = pValor.tde_codtfr;
        base.tde_descripcion = pValor.tde_descripcion;
        base.tde_fijuni = pValor.tde_fijuni;
        base.tde_maxuni = pValor.tde_maxuni;
        base.tde_minuni = pValor.tde_minuni;
        base.tde_muluni = pValor.tde_muluni;
        base.tde_predescuento = pValor.tde_predescuento;
        base.tde_prepublico = pValor.tde_prepublico;
        base.tde_proobligatorio = pValor.tde_proobligatorio;
        base.tde_unidadesbonificadas = pValor.tde_unidadesbonificadas;
        base.tde_unidadesbonificadasdescripcion = pValor.tde_unidadesbonificadasdescripcion;
        base.tfr_codigo = pValor.tfr_codigo;
        base.tfr_accion = pValor.tfr_accion;
        base.tfr_nombre = pValor.tfr_nombre;
        base.tfr_deshab = pValor.tfr_deshab;
        base.tfr_pordesadi = pValor.tfr_pordesadi;
        base.tfr_tipo = pValor.tfr_tipo;
        base.tfr_mospap = pValor.tfr_mospap;
        base.tfr_minrenglones = pValor.tfr_minrenglones;
        base.tfr_minunidades = pValor.tfr_minunidades;
        base.tfr_maxunidades = pValor.tfr_maxunidades;
        base.tfr_mulunidades = pValor.tfr_mulunidades;
        base.tfr_fijunidades = pValor.tfr_fijunidades;
        base.tfr_facturaciondirecta = pValor.tfr_facturaciondirecta;
        base.tfr_descripcion = pValor.tfr_descripcion;
    }

}
//public class cPermisoSecciones
//{
//    public string nombreSeccion { get; set; }
//    public bool isPermiso { get; set; }
//}
public class cUsuarioSinPermisosIntranet
{
    public int usp_id { get; set; }
    public int usp_codUsuario { get; set; }
    public string usp_nombreSeccion { get; set; }
}
public class cGaleria
{
    public cNoticia noticia { get; set; }
    public List<cArchivo> listaArchivo { get; set; }
}
public class cTipoEnvioClienteFront
{
    public string sucursal { get; set; }
    public string tipoEnvio { get; set; }
    public List<cTiposEnvios> lista { get; set; }
}
public class htmlArchivo
{
    public int codRecurso { get; set; }
    public string titulo { get; set; }
    public string descr { get; set; }
    public string arc_nombre { get; set; }
    public string tipo { get; set; }
    public int id { get; set; }
    public int ancho { get; set; }
    public int alto { get; set; }
    public cArchivo objArchivo { get; set; }

}
public class cPdfComprobante
{
    public int index { get; set; }
    public string nombreArchivo { get; set; }
    public bool isOk { get; set; }

}