using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Reflection;

/// <summary>
/// Summary description for FuncionesPersonalizadas
/// </summary>
public class FuncionesPersonalizadas
{
    //public static void grabarLog_ant(string pNombreProcedimiento, Exception pException, DateTime pFechaActual)
    //{
    //    try
    //    {
    //        string path = SitioBase.Constantes.cRaizLog;
    //        if (Directory.Exists(path) == false)
    //        {
    //            Directory.CreateDirectory(path);
    //        }
    //        string nombreArchivo = DateTime.Now.Year.ToString("0000") + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + "_h_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + "_" + DateTime.Now.Millisecond.ToString("000") + ".txt";
    //        string FilePath = path + nombreArchivo;
    //        StreamWriter sw = null;
    //        if (!File.Exists(FilePath))
    //        {
    //            sw = File.CreateText(FilePath);
    //        }
    //        else
    //        {
    //            sw = File.AppendText(FilePath);
    //            sw.WriteLine(string.Empty);
    //        }
    //        sw.WriteLine("<NombreProcedimiento>");
    //        sw.WriteLine(pNombreProcedimiento);
    //        sw.WriteLine("</NombreProcedimiento>");
    //        sw.WriteLine(string.Empty);
    //        if (pException.Data != null)
    //        {
    //            sw.WriteLine("<Data>");
    //            sw.WriteLine(pException.Data.ToString());
    //            sw.WriteLine("</Data>");
    //        }
    //        if (pException.HelpLink != null)
    //        {
    //            sw.WriteLine("<HelpLink>");
    //            sw.WriteLine(pException.HelpLink.ToString());
    //            sw.WriteLine("</HelpLink>");
    //        }
    //        if (pException.InnerException != null)
    //        {
    //            sw.WriteLine("<InnerException>");
    //            sw.WriteLine(pException.InnerException.ToString());
    //            sw.WriteLine("</InnerException>");
    //        }
    //        if (pException.Message != null)
    //        {
    //            sw.WriteLine("<Message>");
    //            sw.WriteLine(pException.Message.ToString());
    //            sw.WriteLine("</Message>");
    //        }
    //        if (pException.Source != null)
    //        {
    //            sw.WriteLine("<Source>");
    //            sw.WriteLine(pException.Source.ToString());
    //            sw.WriteLine("</Source>");
    //        }
    //        if (pException.StackTrace != null)
    //        {
    //            sw.WriteLine("<StackTrace>");
    //            sw.WriteLine(pException.StackTrace.ToString());
    //            sw.WriteLine("</StackTrace>");
    //        }
    //        sw.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
  
    //private static string LogError(MethodBase method, Exception ex, params object[] values)
    //{
    //    ParameterInfo[] parms = method.GetParameters();
    //    object[] namevalues = new object[2 * parms.Length];

    //    string msg = "Error in " + method.Name + "(";
    //    for (int i = 0, j = 0; i < parms.Length; i++, j += 2)
    //    {
    //        msg += "{" + j + "}={" + (j + 1) + "}, ";
    //        namevalues[j] = parms[i].Name;
    //        if (i < values.Length) namevalues[j + 1] = values[i];
    //    }
    //    msg += "exception=" + ex.Message + ")";
    //    // Console.WriteLine(string.Format(msg, namevalues));
    //    return string.Format(msg, namevalues);
    //}

    public static List<cClientes> RecuperarTodosClientesActivosYSinClientes()
    {
        List<cClientes> resultado = new List<cClientes>();
        resultado.Add(new cClientes(0, "<< Sin clientes >>"));
        resultado.AddRange(WebService.RecuperarTodosClientes());
        return resultado;
    }
    public static List<cClientes> RecuperarTodosClientesParaCombo(bool pIsTodos)
    {
        List<cClientes> resultado = new List<cClientes>();
        //if (pIsTodos)
        //{
        //    resultado.Add(new cClientes(0, "<< Todos >>"));
        //}
        resultado.Add(new cClientes(0, "<< Todos >>"));
        resultado.AddRange(WebService.RecuperarTodosClientes());
        return resultado;
    }
    public static string GenerarWhereLikeConColumna(string pTxtBuscador, string pColumna)
    {
        string where = string.Empty;
        string[] palabras = pTxtBuscador.Split(new char[] { ' ' });
        bool isPrimerWhere = true;
        foreach (string item in palabras)
        {
            if (item != string.Empty)
            {
                if (isPrimerWhere)
                {
                    isPrimerWhere = false;
                }
                else
                {
                    where += " AND ";
                }
                where += " " + pColumna + " collate SQL_Latin1_General_Cp1_CI_AI like '%" + item + "%' ";
            }
        }
        return where;
    }
    public static string GenerarWhereLikeConColumna_EmpiezaCon(string pTxtBuscador, string pColumna)
    {
        string where = string.Empty;
        string[] palabras = pTxtBuscador.Split(new char[] { ' ' });
        //bool isPrimerWhere = true;
        foreach (string item in palabras)
        {
            if (item != string.Empty)
            {
                where += " " + pColumna + " collate SQL_Latin1_General_Cp1_CI_AI like '" + item + "%' ";
                break;
            }
        }
        return where;
    }
    public static string GenerarWhereLikeConVariasColumnas(string pTxtBuscador, List<string> pListaColumna)
    {
        string where = string.Empty;
        string[] palabras = pTxtBuscador.Split(new char[] { ' ' });
        bool isPrimerWhere = true;
        foreach (string item in palabras)
        {
            if (item != string.Empty)
            {
                if (isPrimerWhere)
                {
                    isPrimerWhere = false;
                    where += " ( ";
                }
                else
                {
                    where += " AND ( ";
                }
                for (int i = 0; i < pListaColumna.Count; i++)
                {
                    if (i != 0)
                    {
                        where += " OR ";
                    }
                    where += " " + pListaColumna[i] + " collate SQL_Latin1_General_Cp1_CI_AI like '%" + item + "%' ";
                }
                where += " ) ";
            }
        }
        return where;
    }
    public static List<string> RecuperarPalabrasYaBuscadaSinRepetir(int? pIdUsuario, string pNombreTabla)
    {
        var listaPalabras = WebService.RecuperarTodasPalabrasYaBuscada(pIdUsuario, pNombreTabla);
        var listaStrPalabrasSinRepetir = (from ob in listaPalabras
                                          select ob.hbp_Palabra.ToUpper()).Distinct().ToList();
        return listaStrPalabrasSinRepetir;
    }
    //public static decimal ObtenerPrecioFinal(cClientes pClientes, cProductos pProductos)
    //{
    //    //   pClientes.cli_deswebespmed // Cli_DesWebEspMed 
    //    decimal resultado = new decimal(0.0);
    //    if (pProductos.pro_neto)
    //    {   // Neto        
    //        switch (pProductos.pro_codtpopro)
    //        {
    //            case "M": // medicamento
    //                resultado = pProductos.pro_preciofarmacia;

    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesbetmed / Convert.ToDecimal(100)));
    //                if (pClientes.cli_deswebnetmed)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "P": // Perfumeria
    //                resultado = pProductos.pro_preciofarmacia;
    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                if (pClientes.cli_deswebnetperpropio)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "A": // Accesorio
    //                resultado = pProductos.pro_preciofarmacia;
    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                if (pClientes.cli_deswebnetacc)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "V": // Accesorio
    //                resultado = pProductos.pro_preciofarmacia;
    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                if (pClientes.cli_deswebnetacc)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "F": // Perfumería Cuenta y Orden
    //                resultado = pProductos.pro_preciofarmacia;
    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordescomperfcyo / Convert.ToDecimal(100)));
    //                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesfinperfcyo / Convert.ToDecimal(100)));
    //                if (pClientes.cli_deswebnetpercyo)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    else
    //    {  // No neto      
    //        resultado = pProductos.pro_preciofarmacia;
    //        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
    //        if (pClientes.cli_deswebespmed)
    //        {
    //            resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //        }
    //    }
    //    return resultado;
    //}
    public static decimal ObtenerPrecioFinal(cClientes pClientes, cProductos pProductos)
    {
        decimal resultado = new decimal(0.0);
        if (pProductos.pro_acuerdo == SitioBase.Constantes.cAcuerdo_SinAcuerdo)
        {
            resultado = ObtenerPrecioFinal_acuerdo_SinACUERDO(pClientes, pProductos);
        }
        else if (pProductos.pro_acuerdo == SitioBase.Constantes.cAcuerdo_GENOMA)
        {
            resultado = ObtenerPrecioFinal_acuerdo_GENOMA(pClientes, pProductos);
        }
        else if (pProductos.pro_acuerdo == SitioBase.Constantes.cAcuerdo_ADEM)
        {
            resultado = ObtenerPrecioFinal_acuerdo_ADEM(pClientes, pProductos);
        }
        else
        {
            // resultado = ObtenerPrecioFinal_acuerdo_SinACUERDO(pClientes, pProductos);
        }
        return resultado;
    }
    public static decimal ObtenerPrecioFinal_acuerdo_SinACUERDO(cClientes pClientes, cProductos pProductos)
    {
        decimal resultado = new decimal(0.0);
        if (pProductos.pro_neto)
        {   // Neto        
            switch (pProductos.pro_codtpopro)
            {
                case "M": // medicamento
                    resultado = pProductos.pro_preciofarmacia;

                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesbetmed / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetmed)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "P": // Perfumeria
                    resultado = pProductos.pro_preciofarmacia;
                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetperpropio)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "A": // Accesorio
                    resultado = pProductos.pro_preciofarmacia;
                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetacc)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "V": // Accesorio
                    resultado = pProductos.pro_preciofarmacia;
                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetacc)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "F": // Perfumería Cuenta y Orden
                    resultado = pProductos.pro_preciofarmacia;
                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordescomperfcyo / Convert.ToDecimal(100)));
                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesfinperfcyo / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetpercyo)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {  // No neto      
            resultado = pProductos.pro_preciofarmacia;
            resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
            if (pClientes.cli_deswebespmed)
            {
                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
            }
        }
        return resultado;
    }
    public static decimal ObtenerPrecioFinal_acuerdo_GENOMA(cClientes pClientes, cProductos pProductos)
    {
        //   pClientes.cli_deswebespmed // Cli_DesWebEspMed 
        decimal resultado = new decimal(0.0);
        if (pProductos.pro_neto)
        {   // Neto        
            switch (pProductos.pro_codtpopro)
            {
                case "M": // medicamento
                    resultado = pProductos.pro_preciofarmacia;

                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesbetmed / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetmed)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "P": // Perfumeria
                    resultado = pProductos.pro_preciofarmacia;
                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetperpropio)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "A": // Accesorio
                    resultado = pProductos.pro_preciofarmacia;
                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetacc)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "V": // Accesorio
                    resultado = pProductos.pro_preciofarmacia;
                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetacc)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "F": // Perfumería Cuenta y Orden
                    resultado = pProductos.pro_preciofarmacia;
                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordescomperfcyo / Convert.ToDecimal(100)));
                    //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesfinperfcyo / Convert.ToDecimal(100)));
                    if (pClientes.cli_deswebnetpercyo)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {  // No neto      
            resultado = pProductos.pro_preciofarmacia;
            //resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
            if (pClientes.cli_deswebespmed)
            {
                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
            }
        }
        return resultado;
    }
    public static decimal ObtenerPrecioFinal_acuerdo_ADEM(cClientes pClientes, cProductos pProductos)
    {
        decimal resultado = new decimal(0.0);
        // No neto      
        resultado = pProductos.pro_preciofarmacia;
        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
        if (pClientes.cli_deswebespmed)
        {
            resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
        }

        return resultado;
    }
    public static decimal ObtenerPrecioUnitarioConDescuentoOferta(decimal pPrecioFinal, cProductos pProductos)
    {
        decimal resultado = Convert.ToDecimal(0);
        if (pProductos.pro_ofeporcentaje == 0 || pProductos.pro_ofeunidades == 0)
        {
            resultado = pPrecioFinal;
        }
        else
        {
            resultado = (pPrecioFinal * (Convert.ToDecimal(1) - (pProductos.pro_ofeporcentaje / Convert.ToDecimal(100))));
        }
        return resultado;
    }
    public static decimal ObtenerPrecioFinalTransfer(cClientes pClientes, cTransfer pTransfer, cTransferDetalle pTransferDetalle)
    {
        return ObtenerPrecioFinalTransferBase(pClientes, pTransfer.tfr_deshab, pTransfer.tfr_pordesadi, pTransferDetalle.pro_neto, pTransferDetalle.pro_codtpopro, pTransferDetalle.pro_descuentoweb, (decimal)pTransferDetalle.tde_predescuento);
    }
    public static decimal ObtenerPrecioFinalTransferBase(cClientes pClientes, bool pTfr_deshab, decimal? pTfr_pordesadi, bool pPro_neto, string pPro_codtpopro, decimal pPro_descuentoweb, decimal pTde_predescuento)
    {
        decimal resultado = new decimal(0.0);
        resultado = pTde_predescuento;
        if (pTfr_deshab)
        {
            if (pPro_neto)
            {   // Neto        
                switch (pPro_codtpopro)
                {
                    case "M": // medicamento
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesbetmed / Convert.ToDecimal(100)));
                        break;
                    case "P": // Perfumeria
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                        break;
                    case "A": // Accesorio
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                        break;
                    case "V": // Accesorio
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
                        break;
                    case "F": // Perfumería Cuenta y Orden
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordescomperfcyo / Convert.ToDecimal(100)));
                        resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesfinperfcyo / Convert.ToDecimal(100)));
                        break;
                    default:
                        break;
                }
            }
            else
            {  // No neto      
                resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
            }
        }
        if (pPro_neto)
        {   // Neto        
            switch (pPro_codtpopro)
            {
                case "M": // medicamento
                    if (pClientes.cli_deswebnetmed)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pPro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "P": // Perfumeria
                    if (pClientes.cli_deswebnetperpropio)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pPro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "A": // Accesorio
                    if (pClientes.cli_deswebnetacc)
                    {
                    }
                    break;
                case "V": // Accesorio
                    if (pClientes.cli_deswebnetacc)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pPro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                case "F": // Perfumería Cuenta y Orden
                    if (pClientes.cli_deswebnetpercyo)
                    {
                        resultado = resultado * (Convert.ToDecimal(1) - (pPro_descuentoweb / Convert.ToDecimal(100)));
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {  // No neto      
            if (pClientes.cli_deswebespmed)
            {
                resultado = resultado * (Convert.ToDecimal(1) - (pPro_descuentoweb / Convert.ToDecimal(100)));
            }
        }
        //if (pTfr_deshab)
        //{
        if (pTfr_pordesadi != null)
        {
            resultado = resultado * (Convert.ToDecimal(1) - ((decimal)pTfr_pordesadi / Convert.ToDecimal(100)));
        }
        //}
        return resultado;
    }
    public static string LimpiarStringErrorPedido(string pValor)
    {
        string resultado = pValor;
        resultado = resultado.Replace("-", string.Empty);
        resultado = resultado.Replace("0", string.Empty);
        resultado = resultado.Replace("1", string.Empty);
        resultado = resultado.Replace("2", string.Empty);
        resultado = resultado.Replace("3", string.Empty);
        resultado = resultado.Replace("4", string.Empty);
        resultado = resultado.Replace("5", string.Empty);
        resultado = resultado.Replace("6", string.Empty);
        resultado = resultado.Replace("7", string.Empty);
        resultado = resultado.Replace("8", string.Empty);
        resultado = resultado.Replace("9", string.Empty);
        return resultado;
    }
    //public static decimal ObtenerPrecioFinal(cClientes pClientes, cProductos pProductos)
    //{
    //    decimal resultado = new decimal(0.0);
    //    if (pProductos.pro_neto)
    //    {   // Neto        
    //        switch (pProductos.pro_codtpopro)
    //        {
    //            case "M": // medicamento
    //                resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                break;
    //            case "P": // Perfumeria
    //                resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                break;
    //            case "A": // Accesorio
    //                resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                break;
    //            case "V": // Accesorio
    //                resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                break;
    //            case "F": // Perfumería Cuenta y Orden
    //                resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //                resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    else
    //    {  // No neto      
    //        resultado = ObtenerPrecioDescuentoFarmacia(pClientes, pProductos);
    //        resultado = resultado * (Convert.ToDecimal(1) - (pProductos.pro_descuentoweb / Convert.ToDecimal(100)));
    //    }
    //    return resultado;
    //}
    //public static decimal ObtenerPrecioDescuentoFarmacia(cClientes pClientes, cProductos pProductos)
    //{
    //    decimal resultado = new decimal(0.0);
    //    if (pProductos.pro_neto)
    //    {   // Neto        
    //        switch (pProductos.pro_codtpopro)
    //        {
    //            case "M": // medicamento
    //                resultado = pProductos.pro_preciofarmacia;
    //                if (pClientes.cli_deswebnetmed)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesbetmed / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "P": // Perfumeria
    //                resultado = pProductos.pro_preciofarmacia;
    //                if (pClientes.cli_deswebnetperpropio)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "A": // Accesorio
    //                resultado = pProductos.pro_preciofarmacia;
    //                if (pClientes.cli_deswebnetacc)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "V": // Accesorio
    //                resultado = pProductos.pro_preciofarmacia;
    //                if (pClientes.cli_deswebnetacc)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesnetos / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            case "F": // Perfumería Cuenta y Orden
    //                resultado = pProductos.pro_preciofarmacia;
    //                if (pClientes.cli_deswebnetpercyo)
    //                {
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordescomperfcyo / Convert.ToDecimal(100)));
    //                    resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesfinperfcyo / Convert.ToDecimal(100)));
    //                }
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    else
    //    {  // No neto      
    //        resultado = pProductos.pro_preciofarmacia;
    //        if (pClientes.cli_deswebespmed)
    //        {
    //            resultado = resultado * (Convert.ToDecimal(1) - (pClientes.cli_pordesespmed / Convert.ToDecimal(100)));
    //        }
    //    }
    //    return resultado;
    //}
    public static DataTable ConvertProductosAndCantidadToDataTable(List<cProductosAndCantidad> pListaProductosMasCantidad)
    {

        DataTable pTablaDetalle = new DataTable();
        pTablaDetalle.Columns.Add(new DataColumn("codProducto", System.Type.GetType("System.String")));
        pTablaDetalle.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
        foreach (cProductosAndCantidad item in pListaProductosMasCantidad)
        {
            DataRow fila = pTablaDetalle.NewRow();
            fila["codProducto"] = item.codProductoNombre;
            fila["cantidad"] = item.cantidad;
            pTablaDetalle.Rows.Add(fila);
        }
        return pTablaDetalle;
    }




    public static DataTable ConvertNombresSeccionToDataTable(List<string> pListaNombreSeccion)
    {
        DataTable pTablaDetalle = new DataTable();
        pTablaDetalle.Columns.Add(new DataColumn("NombreSeccion", System.Type.GetType("System.String")));
        foreach (string item in pListaNombreSeccion)
        {
            DataRow fila = pTablaDetalle.NewRow();
            fila["NombreSeccion"] = item;
            pTablaDetalle.Rows.Add(fila);
        }
        return pTablaDetalle;
    }
    public static DataTable ObtenerDataTableProductosCarritoArchivosPedidos()
    {
        DataTable pTablaDetalle = new DataTable();
        pTablaDetalle.Columns.Add(new DataColumn("codProducto", System.Type.GetType("System.String")));
        pTablaDetalle.Columns.Add(new DataColumn("codigobarra", System.Type.GetType("System.String")));
        pTablaDetalle.Columns.Add(new DataColumn("codigoalfabeta", System.Type.GetType("System.String")));
        pTablaDetalle.Columns.Add(new DataColumn("troquel", System.Type.GetType("System.String")));
        pTablaDetalle.Columns.Add(new DataColumn("cantidad", System.Type.GetType("System.Int32")));
        pTablaDetalle.Columns.Add(new DataColumn("nroordenamiento", System.Type.GetType("System.Int32")));
        return pTablaDetalle;
    }
    public static DataRow ConvertProductosCarritoArchivosPedidosToDataRow(DataTable pTabla, string pCodProducto, int pCantidad, string pCodigoBarra, string pCodigoAlfaBeta, string pTroquel, string pTipoArchivoPedidos)
    {
        DataRow fila = pTabla.NewRow();
        if (pTipoArchivoPedidos == "F")
        {
            fila["codProducto"] = pCodProducto;
            fila["cantidad"] = pCantidad;
        }
        else if (pTipoArchivoPedidos == "S")
        {
            fila["codigobarra"] = pCodigoBarra;
            fila["codigoalfabeta"] = pCodigoAlfaBeta;
            fila["troquel"] = pTroquel.Trim();
            fila["cantidad"] = pCantidad;
        }
        fila["nroordenamiento"] = pTabla.Rows.Count + 1;
        return fila;
    }
    public static void CargarMensajeActualizado(int pIdCliente)
    {
        bool isAgregar = true;
        if (HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] != null)
        {
            if (((DateTime)HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"]) < DateTime.Now.AddMinutes(-5))
            {
                isAgregar = false;
            }
        }
        if (isAgregar)
        {
            List<cMensaje> listaMensaje = WebService.RecuperarTodosMensajesPorIdCliente(pIdCliente);
            if (listaMensaje != null)
            {
                //List<cMensaje> lista = ((List<cMensaje>)(Session["clientesDefault_ListaMensaje"])).Where(x => x.tme_estado == Convert.ToInt32(SitioBase.Constantes.cESTADO_SINLEER)).ToList();
                HttpContext.Current.Session["clientesDefault_CantListaMensaje"] = listaMensaje.Where(x => (x.tme_estado == Convert.ToInt32(SitioBase.Constantes.cESTADO_SINLEER) && !x.tme_importante)).ToList().Count;
                HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] = DateTime.Now;
            }
        }
    }
    public static void CargarRecuperadorFaltaActualizado(int pIdCliente)
    {
        bool isAgregar = true;
        if (HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"] != null)
        {
            if (((DateTime)HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"]) < DateTime.Now.AddMinutes(-5))
            {
                isAgregar = false;
            }
        }
        if (isAgregar)
        {
            List<cFaltantesConProblemasCrediticiosPadre> listaRecuperador = WebService.RecuperarFaltasProblemasCrediticios(pIdCliente, 1, 14); ;
            if (listaRecuperador != null)
            {
                HttpContext.Current.Session["clientesDefault_CantRecuperadorFalta"] = listaRecuperador.Count;
                HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"] = DateTime.Now;
            }
        }
    }
    public static cjSonBuscadorProductos RecuperarProductosGeneral_OfertaTransfer(bool pIsOrfeta, bool pIsTransfer)
    {
        cjSonBuscadorProductos resultado = null;
        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {
            List<cProductosBuscador> listaProductosBuscador = null;
            listaProductosBuscador = WebService.RecuperarTodosProductosDesdeBuscador_OfertaTransfer(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo, pIsOrfeta, pIsTransfer, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov);
            // TIPO CLIENTE
            if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Perfumeria) // Solamente perfumeria
            {
                listaProductosBuscador = listaProductosBuscador.Where(x => x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria || x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
            }
            else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Todos) // Todos los productos
            {
                // Si el cliente no toma perfumeria
                if (!((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaPerfumeria)
                {
                    listaProductosBuscador = listaProductosBuscador.Where(x => x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria && x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
                }
                // fin Si el cliente no toma perfumeria
            }
            // FIN TIPO CLIENTE
            for (int iPrecioFinal = 0; iPrecioFinal < listaProductosBuscador.Count; iPrecioFinal++)
            {
                listaProductosBuscador[iPrecioFinal].PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]), listaProductosBuscador[iPrecioFinal]);
                listaProductosBuscador[iPrecioFinal].PrecioConDescuentoOferta = FuncionesPersonalizadas.ObtenerPrecioUnitarioConDescuentoOferta(listaProductosBuscador[iPrecioFinal].PrecioFinal, listaProductosBuscador[iPrecioFinal]);
            }
            //// Optimizar
            //List<string> ListaSucursal = new List<string>();
            //ListaSucursal.Add(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc);
            //List<cSucursal> listaSucursalesAUX = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc).ToList();
            //foreach (cSucursal itemSucursalesAUX in listaSucursalesAUX)
            //{
            //    if (itemSucursalesAUX.sde_sucursalDependiente != ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc)
            //    {
            //        ListaSucursal.Add(itemSucursalesAUX.sde_sucursalDependiente);
            //    }
            //}
            //// Fin Optimizar
            // Inicio 17/02/2016
            List<string> ListaSucursal = RecuperarSucursalesParaBuscadorDeCliente();
            listaProductosBuscador = ActualizarStockListaProductos(ListaSucursal, listaProductosBuscador);
            // Fin 17/02/2016
            cjSonBuscadorProductos ResultadoObj = new cjSonBuscadorProductos();
            ResultadoObj.listaSucursal = ListaSucursal;
            ResultadoObj.listaProductos = listaProductosBuscador;
            resultado = ResultadoObj;
        }
        return resultado;
    }
 
    public static List<string> RecuperarSucursalesParaBuscadorDeCliente()
    {
        // Optimizar
        List<string> ListaSucursal = new List<string>();
        ListaSucursal.Add(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc);

        if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")//((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codtpoenv == "R" && 
        {
            if (!ListaSucursal.Contains("SF"))
                ListaSucursal.Add("SF");
            if (!ListaSucursal.Contains("CC"))
                ListaSucursal.Add("CC");
        }
        //else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc == "CD" && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov != "ENTRE RIOS")// cli_codsuc == "CD" concordia y de la provincia de entre rios
        //{
        //    List<cSucursal> listaSucursalesAUX = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc).ToList();
        //    foreach (cSucursal itemSucursalesAUX in listaSucursalesAUX)
        //    {
        //        if (itemSucursalesAUX.sde_sucursalDependiente != ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc && itemSucursalesAUX.sde_sucursalDependiente != "CO") // CO = Concepción del Uruguay
        //        {
        //            ListaSucursal.Add(itemSucursalesAUX.sde_sucursalDependiente);
        //        }
        //    }
        //}
        else
        {
            List<cSucursal> listaSucursalesAUX = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc).ToList();
            foreach (cSucursal itemSucursalesAUX in listaSucursalesAUX)
            {
                if (itemSucursalesAUX.sde_sucursalDependiente != ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc)
                {
                    ListaSucursal.Add(itemSucursalesAUX.sde_sucursalDependiente);
                }
            }
            if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa != null &&
                !ListaSucursal.Contains(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa))
            {
                ListaSucursal.Add(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa);
            }
        }
        return ListaSucursal;
        // Fin Optimizar
    }
    //
    public static List<cProductosBuscador> ActualizarStockListaProductos(List<string> pListaSucursal, List<cProductosBuscador> pListaProductos)
    {
        bool isActualizar = false;
        if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")
            isActualizar = true;
        //else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc == "CD" && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov != "ENTRE RIOS")// cli_codsuc == "CD" concordia y de la provincia de entre rios        
        //    isActualizar = true;
        else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa != null)     
            isActualizar = true;
        if (isActualizar)
        {
            List<cProductosAndCantidad> listaProductos = new List<cProductosAndCantidad>();
            foreach (cProductosBuscador item in pListaProductos)
            {
                listaProductos.Add(new cProductosAndCantidad { codProductoNombre = item.pro_codigo });
            }
            DataTable table = capaProductos.RecuperarStockPorProductosAndSucursal(ConvertNombresSeccionToDataTable(pListaSucursal), ConvertProductosAndCantidadToDataTable(listaProductos));
            if (table != null)
                for (int i = 0; i < pListaProductos.Count; i++)
                {
                    pListaProductos[i].listaSucursalStocks = (from r in table.Select("stk_codpro = '" + pListaProductos[i].pro_codigo + "'").AsEnumerable()
                                                              select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();
                }
        }
        return pListaProductos;
    }
    public static List<cProductosBuscador> ActualizarStockListaProductos_SubirArchico(List<string> pListaSucursal, List<cProductosBuscador> pListaProductos, string pSucursalElegida)
    {
        pListaProductos = ActualizarStockListaProductos(pListaSucursal, pListaProductos);
        bool isActualizar = false;
        if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")
            isActualizar = true;
        //else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc == "CD" && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov != "ENTRE RIOS")// cli_codsuc == "CD" concordia y de la provincia de entre rios        
        //    isActualizar = true;
        else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa != null)
            isActualizar = true;
        if (isActualizar)
        {
            for (int i = 0; i < pListaProductos.Count; i++)
            {
                foreach (cSucursalStocks item in pListaProductos[i].listaSucursalStocks)
                {
                    if (item.stk_codsuc == pSucursalElegida)
                    {
                        item.cantidadSucursal = pListaProductos[i].cantidad;
                    }
                }
            }
        }
        return pListaProductos;
    }
    public static List<cSucursalStocks> ActualizarStockListaProductos_Transfer(string pro_codigo, List<cSucursalStocks> pSucursalStocks)
    {
        List<cSucursalStocks> result = pSucursalStocks;
        bool isActualizar = false;
        if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")
            isActualizar = true;
        //else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc == "CD" && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov != "ENTRE RIOS")// cli_codsuc == "CD" concordia y de la provincia de entre rios        
        //    isActualizar = true;
        else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa != null)
            isActualizar = true;
        if (isActualizar)
        {
            List<cProductosAndCantidad> listaProductos = new List<cProductosAndCantidad>();

            listaProductos.Add(new cProductosAndCantidad { codProductoNombre = pro_codigo });
            List<string> ListaSucursal = RecuperarSucursalesParaBuscadorDeCliente();
            DataTable table = capaProductos.RecuperarStockPorProductosAndSucursal(ConvertNombresSeccionToDataTable(ListaSucursal), ConvertProductosAndCantidadToDataTable(listaProductos));
            if (table != null)
                result = (from r in table.Select("stk_codpro = '" + pro_codigo + "'").AsEnumerable()
                          select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();

        }
        return result;
    }

    //
    //




    public static cjSonBuscadorProductos RecuperarProductosGeneral_V3(int? pIdOferta, string pTxtBuscador, List<string> pListaColumna, bool pIsOrfeta, bool pIsTransfer)
    {
        cjSonBuscadorProductos resultado = null;
        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {
            List<cProductosBuscador> listaProductosBuscador = null;

            listaProductosBuscador = WebService.RecuperarTodosProductosDesdeBuscadorV3(pIdOferta, pTxtBuscador, pListaColumna, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo, pIsOrfeta, pIsTransfer, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov);
            // TIPO CLIENTE
            if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Perfumeria) // Solamente perfumeria
            {
                listaProductosBuscador = listaProductosBuscador.Where(x => x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria || x.pro_codtpopro == SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
            }
            else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tipo == SitioBase.Constantes.cTipoCliente_Todos) // Todos los productos
            {
                // Si el cliente no toma perfumeria
                if (!((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaPerfumeria)
                {
                    listaProductosBuscador = listaProductosBuscador.Where(x => x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_Perfumeria && x.pro_codtpopro != SitioBase.Constantes.cTIPOPRODUCTO_PerfumeriaCuentaYOrden).ToList();
                }
                // fin Si el cliente no toma perfumeria
            }
            // FIN TIPO CLIENTE
            for (int iPrecioFinal = 0; iPrecioFinal < listaProductosBuscador.Count; iPrecioFinal++)
            {
                listaProductosBuscador[iPrecioFinal].PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]), listaProductosBuscador[iPrecioFinal]);
                listaProductosBuscador[iPrecioFinal].PrecioConDescuentoOferta = FuncionesPersonalizadas.ObtenerPrecioUnitarioConDescuentoOferta(listaProductosBuscador[iPrecioFinal].PrecioFinal, listaProductosBuscador[iPrecioFinal]);
            }

            List<cProductos> listaProductosConImagen = WebService.ObtenerProductosImagenes();
            for (int iImagen = 0; iImagen < listaProductosBuscador.Count; iImagen++)
            {
                cProductos objImagen = listaProductosConImagen.Where(x => x.pro_codigo == listaProductosBuscador[iImagen].pro_codigo).FirstOrDefault();
                if (objImagen != null)
                    listaProductosBuscador[iImagen].pri_nombreArchivo = objImagen.pri_nombreArchivo;
            }

            //// Optimizar
            //List<string> ListaSucursal = new List<string>();
            //ListaSucursal.Add(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc);
            //List<cSucursal> listaSucursalesAUX = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc).ToList();
            //foreach (cSucursal itemSucursalesAUX in listaSucursalesAUX)
            //{
            //    if (itemSucursalesAUX.sde_sucursalDependiente != ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc)
            //    {
            //        ListaSucursal.Add(itemSucursalesAUX.sde_sucursalDependiente);
            //    }
            //}
            //// Fin Optimizar
            // Inicio 17/02/2016
            List<string> ListaSucursal = RecuperarSucursalesParaBuscadorDeCliente();
            listaProductosBuscador = ActualizarStockListaProductos(ListaSucursal, listaProductosBuscador);
            // Fin 17/02/2016

            cjSonBuscadorProductos ResultadoObj = new cjSonBuscadorProductos();
            ResultadoObj.listaSucursal = ListaSucursal;
            ResultadoObj.listaProductos = listaProductosBuscador;
            resultado = ResultadoObj;
        }
        return resultado;
    }
    public static cjSonBuscadorProductos RecuperarProductosGeneralSubirPedidos(List<cProductosBuscador> pListaProveedor)
    {
        cjSonBuscadorProductos resultado = null;
        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {

            List<cProductosBuscador> listaProductosBuscador = pListaProveedor;
            // fin Si el cliente no toma perfumeria
            for (int iPrecioFinal = 0; iPrecioFinal < listaProductosBuscador.Count; iPrecioFinal++)
            {
                listaProductosBuscador[iPrecioFinal].PrecioFinal = FuncionesPersonalizadas.ObtenerPrecioFinal(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]), listaProductosBuscador[iPrecioFinal]);
                listaProductosBuscador[iPrecioFinal].PrecioConDescuentoOferta = FuncionesPersonalizadas.ObtenerPrecioUnitarioConDescuentoOferta(listaProductosBuscador[iPrecioFinal].PrecioFinal, listaProductosBuscador[iPrecioFinal]);
            }
            //// Optimizar
            //List<string> ListaSucursal = new List<string>();
            //ListaSucursal.Add(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc);
            //List<cSucursal> listaSucursalesAUX = WebService.RecuperarTodasSucursalesDependientes().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc).ToList();
            //foreach (cSucursal itemSucursalesAUX in listaSucursalesAUX)
            //{
            //    if (itemSucursalesAUX.sde_sucursalDependiente != ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc)
            //    {
            //        ListaSucursal.Add(itemSucursalesAUX.sde_sucursalDependiente);
            //    }
            //}
            //// Fin Optimizar
            // Inicio 17/02/2016
            List<string> ListaSucursal = RecuperarSucursalesParaBuscadorDeCliente();
            listaProductosBuscador = ActualizarStockListaProductos_SubirArchico(ListaSucursal, listaProductosBuscador, HttpContext.Current.Session["subirpedido_SucursalEleginda"].ToString());
            // Fin 17/02/2016
            cjSonBuscadorProductos ResultadoObj = new cjSonBuscadorProductos();
            ResultadoObj.listaSucursal = ListaSucursal;
            ResultadoObj.listaProductos = listaProductosBuscador;
            resultado = ResultadoObj;
        }
        return resultado;
    }
    //public static cjSonBuscadorProductos RecuperarProductosBase(string pTxtBuscador, List<string> pListaColumna)
    //{
    //    cjSonBuscadorProductos resultado = null;
    //    if (!string.IsNullOrEmpty(pTxtBuscador))
    //    {
    //        if (pTxtBuscador.Trim() != string.Empty)
    //        {
    //            if (HttpContext.Current.Session["clientesDefault_Usuario"] != null)
    //            {
    //                HttpContext.Current.Session["clientesPages_Buscador"] = WebService.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((SitioBase.capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, SitioBase.Constantes.cTABLA_PRODUCTO);
    //            }
    //            resultado = FuncionesPersonalizadas.RecuperarProductosGeneral(pTxtBuscador, pListaColumna, false, false);
    //        }
    //    }
    //    return resultado;
    //}
    public static cjSonBuscadorProductos RecuperarProductosBase_V3(int? pIdOferta, string pTxtBuscador, List<string> pListaColumna, bool pIsBuscarConOferta, bool pIsBuscarConTransfer)
    {
        cjSonBuscadorProductos resultado = null;
        if (!string.IsNullOrEmpty(pTxtBuscador) || pIdOferta != null)
        {
            if (!string.IsNullOrEmpty(pTxtBuscador) && pTxtBuscador.Trim() != string.Empty && HttpContext.Current.Session["clientesDefault_Usuario"] != null)
                HttpContext.Current.Session["clientesPages_Buscador"] = WebService.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((SitioBase.capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, SitioBase.Constantes.cTABLA_PRODUCTO);

            resultado = FuncionesPersonalizadas.RecuperarProductosGeneral_V3(pIdOferta, pTxtBuscador, pListaColumna, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaOfertas, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaTransfers);
            if (pIsBuscarConOferta || pIsBuscarConTransfer)
            {
                if (resultado != null)
                {
                    if (pIsBuscarConOferta && pIsBuscarConTransfer)
                    {
                        resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0 || x.isTieneTransfer || x.isProductoFacturacionDirecta).ToList();
                    }
                    else
                    {
                        if (pIsBuscarConOferta)
                        {
                            resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0).ToList();
                        }
                        else if (pIsBuscarConTransfer)
                        {
                            resultado.listaProductos = resultado.listaProductos.Where(x => x.isTieneTransfer || x.isProductoFacturacionDirecta).ToList();
                        }
                    }
                }
            }

        }
        return resultado;
    }
    //public static cjSonBuscadorProductos RecuperarProductosBase(string pTxtBuscador, List<string> pListaColumna, bool pIsBuscarConOferta, bool pIsBuscarConTransfer)
    //{
    //    cjSonBuscadorProductos resultado = null;
    //    if (!string.IsNullOrEmpty(pTxtBuscador))
    //    {
    //        if (pTxtBuscador.Trim() != string.Empty)
    //        {
    //            if (HttpContext.Current.Session["clientesDefault_Usuario"] != null)
    //            {
    //                HttpContext.Current.Session["clientesPages_Buscador"] = WebService.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((SitioBase.capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, SitioBase.Constantes.cTABLA_PRODUCTO);
    //            }
    //            resultado = FuncionesPersonalizadas.RecuperarProductosGeneral(pTxtBuscador, pListaColumna, false, false);
    //            if (pIsBuscarConOferta || pIsBuscarConTransfer)
    //            {
    //                if (resultado != null)
    //                {
    //                    if (pIsBuscarConOferta && pIsBuscarConTransfer)
    //                    {
    //                        resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0 || x.isTieneTransfer).ToList();
    //                    }
    //                    else
    //                    {
    //                        if (pIsBuscarConOferta)
    //                        {
    //                            resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0).ToList();
    //                        }
    //                        else if (pIsBuscarConTransfer)
    //                        {
    //                            resultado.listaProductos = resultado.listaProductos.Where(x => x.isTieneTransfer).ToList();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return resultado;
    //}

    public static string ObtenerHorarioCierre_base(string pSucursal, string pSucursalDependiente, string pCodigoReparto, DateTime fechaActual)
    {
        string resultado = null;
        List<cHorariosSucursal> listaHorariosSucursal = WebService.RecuperarTodosHorariosSucursalDependiente().Where(x => x.sdh_sucursal == pSucursal && x.sdh_sucursalDependiente == pSucursalDependiente && x.sdh_codReparto == pCodigoReparto).ToList();
        string day = string.Empty;
        bool isEncontroEnFechaHoy = false;
        switch (fechaActual.DayOfWeek)
        {
            case DayOfWeek.Monday:
                day = SitioBase.Constantes.cDIASEMANA_Lunes;
                break;
            case DayOfWeek.Tuesday:
                day = SitioBase.Constantes.cDIASEMANA_Martes;
                break;
            case DayOfWeek.Wednesday:
                day = SitioBase.Constantes.cDIASEMANA_Miercoles;
                break;
            case DayOfWeek.Thursday:
                day = SitioBase.Constantes.cDIASEMANA_Jueves;
                break;
            case DayOfWeek.Friday:
                day = SitioBase.Constantes.cDIASEMANA_Viernes;
                break;
            case DayOfWeek.Saturday:
                day = SitioBase.Constantes.cDIASEMANA_Sabado;
                break;
            case DayOfWeek.Sunday:
                day = SitioBase.Constantes.cDIASEMANA_Domingo;
                break;
            default:
                break;
        }
        if (day != string.Empty)
        {
            foreach (cHorariosSucursal itemHorariosSucursal in listaHorariosSucursal)
            {
                if (itemHorariosSucursal.sdh_diaSemana == day)
                {
                    if (itemHorariosSucursal.listaHorarios != null)
                    {
                        foreach (string itemHorarios in itemHorariosSucursal.listaHorarios)
                        {
                            string[] tiempo = itemHorarios.Split(':');
                            if (tiempo.Count() > 1)
                            {
                                DateTime fechaCierre = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, Convert.ToInt32(tiempo[0]), Convert.ToInt32(tiempo[1]), 30);
                                if (fechaCierre > fechaActual)
                                {
                                    isEncontroEnFechaHoy = true;
                                    resultado = itemHorarios + " hs. ";
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }

        }
        int SumaDia = 0;
        while (!isEncontroEnFechaHoy)
        {
            day = string.Empty;
            SumaDia += 1;
            //DateTime fechaOtroDia = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day + SumaDia, fechaActual.Hour, fechaActual.Minute, fechaActual.Second);
            DateTime fechaOtroDia = fechaActual.AddDays(SumaDia);
            switch (fechaOtroDia.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day = SitioBase.Constantes.cDIASEMANA_Lunes;
                    break;
                case DayOfWeek.Tuesday:
                    day = SitioBase.Constantes.cDIASEMANA_Martes;
                    break;
                case DayOfWeek.Wednesday:
                    day = SitioBase.Constantes.cDIASEMANA_Miercoles;
                    break;
                case DayOfWeek.Thursday:
                    day = SitioBase.Constantes.cDIASEMANA_Jueves;
                    break;
                case DayOfWeek.Friday:
                    day = SitioBase.Constantes.cDIASEMANA_Viernes;
                    break;
                case DayOfWeek.Saturday:
                    day = SitioBase.Constantes.cDIASEMANA_Sabado;
                    break;
                case DayOfWeek.Sunday:
                    day = SitioBase.Constantes.cDIASEMANA_Domingo;
                    break;
                default:
                    break;
            }
            if (day != string.Empty)
            {
                foreach (cHorariosSucursal itemHorariosSucursal in listaHorariosSucursal)
                {
                    if (itemHorariosSucursal.sdh_diaSemana == day)
                    {
                        if (itemHorariosSucursal.listaHorarios != null)
                        {
                            foreach (string itemHorarios in itemHorariosSucursal.listaHorarios)
                            {
                                string[] tiempo = itemHorarios.Split(':');
                                if (tiempo.Count() > 1)
                                {
                                    DateTime fechaCierre = new DateTime(fechaOtroDia.Year, fechaOtroDia.Month, fechaOtroDia.Day, Convert.ToInt32(tiempo[0]), Convert.ToInt32(tiempo[1]), 30);
                                    if (fechaCierre > fechaActual)
                                    {
                                        isEncontroEnFechaHoy = true;
                                        resultado = itemHorarios + " hs. " + day;
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
            if (SumaDia > 7)
            {
                isEncontroEnFechaHoy = true;
            }
        } // fin while (!isEncontroEnFechaHoy)

        // Inicio S7
        if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7" && pSucursalDependiente == "SF")
        {
            DateTime fechaCierre = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 22, 15, 30);
            resultado = "22:15" + " hs. ";
            switch (fechaActual.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (fechaCierre < fechaActual)
                        resultado = "22:15" + " hs. " + "MA";
                    break;
                case DayOfWeek.Tuesday:
                    if (fechaCierre < fechaActual)
                        resultado = "22:15" + " hs. " + "MI";
                    break;
                case DayOfWeek.Wednesday:
                    if (fechaCierre < fechaActual)
                        resultado = "22:15" + " hs. " + "JU";
                    break;
                case DayOfWeek.Thursday:
                    if (fechaCierre < fechaActual)
                        resultado = "22:15" + " hs. " + "VI";
                    break;
                case DayOfWeek.Friday:
                    if (fechaCierre < fechaActual)
                        resultado = "22:15" + " hs. " + "LU";
                    break;
                case DayOfWeek.Saturday:
                    resultado = "22:15" + " hs. " + "LU";
                    break;
                case DayOfWeek.Sunday:
                    resultado = "22:15" + " hs. " + "LU";
                    break;
                default:
                    break;
            }
        }
        // Fin S7
        return resultado;
    }

    public static string ObtenerHorarioCierre(string pSucursal, string pSucursalDependiente, string pCodigoReparto)
    {
        return ObtenerHorarioCierre_base(pSucursal, pSucursalDependiente, pCodigoReparto, DateTime.Now);
    }
    public static string ObtenerHorarioCierreAnterior(string pSucursal, string pSucursalDependiente, string pCodigoReparto, string pHorarioCierre)
    {
        if (string.IsNullOrEmpty(pHorarioCierre))
            return string.Empty;
        string result = string.Empty;
        try
        {


            DateTime fechaCuentaRegresiva;
            DateTime hoy = DateTime.Now;
            if (pHorarioCierre.Length == 12)
            {
                var diaSemana = pHorarioCierre.Substring(10, 2);
                var diaSemanaNro = -1;
                //Note: Sunday is 0, Monday is 1, and so on || from 0 to 6
                // LU = 1
                // MA = 2
                // MI = 3
                // JU = 4
                // VI = 5
                // SA = 6
                // DO = 0
                switch (diaSemana)
                {
                    case "LU":
                        diaSemanaNro = 1;
                        break;
                    case "MA":
                        diaSemanaNro = 2;
                        break;
                    case "MI":
                        diaSemanaNro = 3;
                        break;
                    case "JU":
                        diaSemanaNro = 4;
                        break;
                    case "VI":
                        diaSemanaNro = 5;
                        break;
                    case "SA":
                        diaSemanaNro = 6;
                        break;
                    case "DO":
                        diaSemanaNro = 0;
                        break;
                    default:
                        break;
                }
                pHorarioCierre = pHorarioCierre.Replace(" hs. " + diaSemana, "");
                var values = pHorarioCierre.Split(':');
                var d = new DateTime(hoy.Year, hoy.Month, hoy.Day, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 0);// mes 0 = enero
                                                                                                                              //var n = d.DayOfWeek;
                var sumaDia = 0;
                while ((int)d.DayOfWeek != diaSemanaNro)
                {
                    sumaDia++;
                    d = new DateTime(hoy.Year, hoy.Month, hoy.Day + sumaDia, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 50);// mes 0 = enero
                    if (sumaDia > 7 || (int)d.DayOfWeek == diaSemanaNro)
                        break;
                }
                fechaCuentaRegresiva = d;

            }
            else
            {
                pHorarioCierre = pHorarioCierre.Replace(" hs.", "");
                var values = pHorarioCierre.Split(':');
                fechaCuentaRegresiva = new DateTime(hoy.Year, hoy.Month, hoy.Day, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 50);// mes 0 = enero
            }
            result = ObtenerHorarioCierre_base(pSucursal, pSucursalDependiente, pCodigoReparto, fechaCuentaRegresiva);
        }
        catch (Exception ex)
        {

            var oo = 1;
        }
        return result;
    }
    public static void GenerarCSV()
    {
        List<string> lista = new List<string>();
        //nombreArchivoTXT = "Kell" + fechaArchivoTXT + "-" + pFactura + ".txt";
        //resultado = nombreArchivoTXT;
        System.IO.StreamWriter FAC_txt = new System.IO.StreamWriter("pRuta + nombreArchivoTXT", false, System.Text.Encoding.UTF8);
        // encabezado
        string encabezado = string.Empty;
        //Fecha;Comprobante;Numero Comprobante;Monto Exento;Monto Gravado;Monto Iva Inscipto;Monto Iva No Inscripto;Monto Percepciones DGR;Monto Total
        encabezado += "Fecha";
        encabezado += ";";
        encabezado += "Comprobante";
        encabezado += ";";
        encabezado += "Numero Comprobante";
        encabezado += ";";
        encabezado += "Monto Exento";
        encabezado += ";";
        encabezado += "Monto Gravado";
        encabezado += ";";
        encabezado += "Monto Iva Inscipto";
        encabezado += ";";
        encabezado += "Monto Iva No Inscripto";
        encabezado += ";";
        encabezado += "Monto Percepciones DGR";
        encabezado += ";";
        encabezado += "Monto Total";

        FAC_txt.WriteLine(encabezado);
        // fin encabezado


        foreach (string item in lista)
        {
            string fila = string.Empty;
            fila += DateTime.Now.ToString();
            fila += ";";
            fila += item;
            fila += ";";
            fila += item;
            FAC_txt.WriteLine(fila);
        }
        FAC_txt.Close();
    }
    public static List<string> RecuperarSinPermisosSecciones(int pIdUsuario)
    {
        List<string> resultado = null;
        List<cUsuarioSinPermisosIntranet> lista = WebService.RecuperarTodosSinPermisosIntranetPorIdUsuario(pIdUsuario);
        if (lista != null)
        {
            resultado = new List<string>();
            foreach (cUsuarioSinPermisosIntranet item in lista)
            {
                resultado.Add(item.usp_nombreSeccion.ToUpper());
            }
        }
        HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = resultado;
        return resultado;
    }
    public static bool IsPermisoSeccion(string pNombreSeccion)
    {
        bool resultado = true;
        if (HttpContext.Current.Session["master_ListaSinPermisoSecciones"] != null)
        {
            List<string> listaSinPermiso = (List<string>)HttpContext.Current.Session["master_ListaSinPermisoSecciones"];
            foreach (string item in listaSinPermiso)
            {
                if (item == pNombreSeccion)
                {
                    resultado = false;
                    break;
                }
            }
        }
        return resultado;
    }
    public static List<cTipoEnvioClienteFront> RecuperarTiposDeEnvios()
    {
        List<cTipoEnvioClienteFront> resultado = null;
        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {
            List<cSucursalDependienteTipoEnviosCliente> lista = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.sde_sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc && (x.tsd_idTipoEnvioCliente == null || x.env_codigo == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codtpoenv)).ToList();
            if (lista != null)
            {
                resultado = new List<cTipoEnvioClienteFront>();
                for (int i = 0; i < lista.Count; i++)
                {
                    cTipoEnvioClienteFront obj = new cTipoEnvioClienteFront();
                    obj.sucursal = lista[i].sde_sucursalDependiente;
                    obj.tipoEnvio = lista[i].env_codigo;
                    List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTiposEnvios = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == lista[i].tsd_id).ToList();
                    if (listaTiposEnvios != null)
                    {
                        obj.lista = new List<cTiposEnvios>();
                        foreach (cSucursalDependienteTipoEnviosCliente_TiposEnvios itemSucursalDependienteTipoEnviosCliente_TiposEnvios in listaTiposEnvios)
                        {
                            cTiposEnvios objTipoEnvio = new cTiposEnvios();
                            objTipoEnvio.env_codigo = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_codigo;
                            objTipoEnvio.env_nombre = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_nombre;
                            objTipoEnvio.env_id = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_id;
                            obj.lista.Add(objTipoEnvio);
                        }
                    }
                    //
                    resultado.Add(obj);
                }
            }
            // Inicio S7
            if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")
            {
                cTipoEnvioClienteFront obj = new cTipoEnvioClienteFront();
                obj.sucursal = "SF";
                obj.tipoEnvio = null;
                //List<cSucursalDependienteTipoEnviosCliente> lista_aux = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.sde_sucursal == "SF" && x.sde_sucursalDependiente == "SF" && x.tsd_idTipoEnvioCliente == null).ToList();
                //if (lista_aux.Count > 0)
                //{
                //    List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTiposEnvios_aux = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == lista_aux[0].tsd_id).ToList();
                //    foreach (cSucursalDependienteTipoEnviosCliente_TiposEnvios itemSucursalDependienteTipoEnviosCliente_TiposEnvios in listaTiposEnvios_aux)
                //    {
                //        cTiposEnvios objTipoEnvio = new cTiposEnvios();
                //        objTipoEnvio.env_codigo = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_codigo;
                //        objTipoEnvio.env_nombre = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_nombre;
                //        objTipoEnvio.env_id = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_id;
                //        obj.lista.Add(objTipoEnvio);
                //    }
                //}
                obj.lista = new List<cTiposEnvios>();
                cTiposEnvios objTipoEnvioR = new cTiposEnvios();
                objTipoEnvioR.env_codigo = "R";
                objTipoEnvioR.env_nombre = "Reparto";
                //objTipoEnvioR.env_id = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_id;
                obj.lista.Add(objTipoEnvioR);
                cTiposEnvios objTipoEnvioE = new cTiposEnvios();
                objTipoEnvioE.env_codigo = "E";
                objTipoEnvioE.env_nombre = "Encomienda";
                obj.lista.Add(objTipoEnvioE);
                cTiposEnvios objTipoEnvioM = new cTiposEnvios();
                objTipoEnvioM.env_codigo = "M";
                objTipoEnvioM.env_nombre = "Mostrador";
                obj.lista.Add(objTipoEnvioM);
                cTiposEnvios objTipoEnvioC = new cTiposEnvios();
                objTipoEnvioC.env_codigo = "C";
                objTipoEnvioC.env_nombre = "Cadeteria";
                obj.lista.Add(objTipoEnvioC);
                resultado.Add(obj);
            }
            // Fin S7
        }
        return resultado;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////
    public static List<int> CargarProductoCantidadDependiendoTransfer(cProductosBuscador pProducto, int pCantidad)
    {
        List<int> resultado = new List<int>();
        bool isPasarDirectamente = false;
        int cantidadCarritoTransfer = 0;
        int cantidadCarritoComun = 0;

        if (pProducto.isProductoFacturacionDirecta)
        { // facturacion directa
            // Combiene transfer o promocion                      
            decimal precioConDescuentoDependiendoCantidad = ObtenerPrecioUnitarioConDescuentoOfertaSiLlegaConLaCantidad(pProducto, pCantidad);
            if (precioConDescuentoDependiendoCantidad > pProducto.PrecioFinalTransfer)
            {
                var isSumarTransfer = false;
                if (pProducto.tde_muluni != null && pProducto.tde_unidadesbonificadas != null)
                {
                    /// UNIDAD MULTIPLO Y BONIFICADA
                    if ((pCantidad >= (int)pProducto.tde_muluni) && (pCantidad <= ((int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas)))
                    {
                        // es multiplo
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = (int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas;
                    }
                    else if (pCantidad > ((int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas))
                    {
                        isSumarTransfer = true;
                        int cantidadMultiplicar = Convert.ToInt32(Math.Truncate(Convert.ToDouble(pCantidad) / Convert.ToDouble(pProducto.tde_muluni)));
                        cantidadCarritoTransfer = cantidadMultiplicar * ((int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas);
                        //
                        for (int iCantMulti = 0; iCantMulti < cantidadMultiplicar; iCantMulti++)
                        {
                            int cantTemp = iCantMulti * ((int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas);
                            if (cantTemp >= pCantidad)
                            {
                                cantidadCarritoTransfer = cantTemp;
                                break;
                            }
                        }
                        //
                        if (cantidadCarritoTransfer == pCantidad)
                        {

                        }
                        else
                        {
                            if (pCantidad < cantidadCarritoTransfer)
                            {
                                cantidadCarritoComun = 0;
                            }
                            else
                            {
                                cantidadCarritoComun = pCantidad - cantidadCarritoTransfer;
                            }
                            if ((cantidadCarritoComun >= (int)pProducto.tde_muluni) && (cantidadCarritoComun <= ((int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas)))
                            {
                                cantidadCarritoTransfer += (int)pProducto.tde_muluni + (int)pProducto.tde_unidadesbonificadas;
                                cantidadCarritoComun = 0;
                            }
                        }
                    }
                    if (isSumarTransfer)
                    {

                    }
                    else
                    {
                        isPasarDirectamente = true;
                    }
                    /// FIN UNIDAD MULTIPLO Y BONIFICADA
                } // fin if (listaProductosBuscados[pFila].tde_muluni != null && listaProductosBuscados[pFila].tde_unidadesbonificadas != null){
                else if (pProducto.tde_fijuni != null)
                {
                    // UNIDAD FIJA
                    if (pCantidad == (int)pProducto.tde_fijuni)
                    {
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = (int)pProducto.tde_fijuni;
                    }
                    else if (pCantidad > (int)pProducto.tde_fijuni)
                    {
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = (int)pProducto.tde_fijuni;
                        cantidadCarritoComun = pCantidad - (int)pProducto.tde_fijuni;
                    }
                    if (isSumarTransfer)
                    {

                    }
                    else
                    {
                        isPasarDirectamente = true;
                    }
                    // FIN UNIDAD FIJA
                }
                else if (pProducto.tde_minuni != null && pProducto.tde_maxuni != null)
                {
                    // UNIDAD MAXIMA Y MINIMA
                    if ((int)pProducto.tde_minuni <= pCantidad && (int)pProducto.tde_maxuni >= pCantidad)
                    {
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = pCantidad;
                    }
                    else if ((int)pProducto.tde_maxuni < pCantidad)
                    {
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = (int)pProducto.tde_maxuni;
                        cantidadCarritoComun = pCantidad - (int)pProducto.tde_maxuni;
                    }
                    if (isSumarTransfer)
                    {

                    }
                    else
                    {
                        isPasarDirectamente = true;
                    }
                    // FIN UNIDAD MAXIMA Y MINIMA
                }
                else if (pProducto.tde_minuni != null)
                {
                    // UNIDAD MINIMA
                    if ((int)pProducto.tde_minuni <= pCantidad)
                    {
                        isSumarTransfer = true;
                        cantidadCarritoTransfer = pCantidad;
                    }
                    if (isSumarTransfer)
                    {

                    }
                    else
                    {
                        isPasarDirectamente = true;
                    }
                    // FIN UNIDAD MINIMA
                }
            } // fin if (listaProductosBuscados[pFila].PrecioConDescuentoOferta > listaProductosBuscados[pFila].PrecioFinalTransfer){
            else
            {
                isPasarDirectamente = true;
            }
        }
        else
        {
            isPasarDirectamente = true;
        }
        if (isPasarDirectamente)
        {
            cantidadCarritoComun = pCantidad;
        }
        resultado.Add(cantidadCarritoComun);
        resultado.Add(cantidadCarritoTransfer);
        return resultado;
    }
    public static decimal ObtenerPrecioUnitarioConDescuentoOfertaSiLlegaConLaCantidad(cProductosBuscador pProductos, int pCantidad)
    {
        decimal resultado = Convert.ToDecimal(0);
        bool isClienteTomaOferta = false;
        if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
        {
            isClienteTomaOferta = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaOfertas;
        }
        if (isClienteTomaOferta)
        {
            if (pProductos.pro_ofeunidades == 0 || pProductos.pro_ofeporcentaje == 0)
            {
                resultado = pProductos.PrecioFinal;
            }
            else
            {
                if (pProductos.pro_ofeunidades > pCantidad)
                {
                    resultado = pProductos.PrecioFinal;
                }
                else
                {
                    resultado = pProductos.PrecioFinal * (Convert.ToDecimal(1) - (pProductos.pro_ofeporcentaje / Convert.ToDecimal(100)));
                }
            }
        }
        else
        {
            // Cliente si permiso para tomar oferta
            resultado = pProductos.PrecioFinal;
        }
        return resultado;
    }

    public static string obtenerNombreMes(int pValue)
    {
        string result = string.Empty;
        switch (pValue)
        {
            case 1:
                result = "Enero";
                break;
            case 2:
                result = "Febrero";
                break;
            case 3:
                result = "Marzo";
                break;
            case 4:
                result = "Abril";
                break;
            case 5:
                result = "Mayo";
                break;
            case 6:
                result = "Junio";
                break;
            case 7:
                result = "Julio";
                break;
            case 8:
                result = "Agosto";
                break;
            case 9:
                result = "Septiembre";
                break;
            case 10:
                result = "Octubre";
                break;
            case 11:
                result = "Noviembre";
                break;
            case 12:
                result = "Diciembre";
                break;
            default:
                break;
        }
        return result;
    }

}
