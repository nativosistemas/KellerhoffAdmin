using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
namespace SitioBase.clases
{
    /// <summary>
    /// Summary description for Serializador
    /// </summary>
    public static class Serializador
    {
        public static string SerializarToXml(object obj)
        {
            try
            {
                StringWriter strWriter = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                serializer.Serialize(strWriter, obj);
                string resultXml = strWriter.ToString();
                strWriter.Close();

                return resultXml;
            }
            catch
            {
                return string.Empty;
            }
        }
        //Deserializar un XML a un objeto T 
        public static T DeserializarToXml<T>(string xmlSerializado)
        {
            try
            {
                XmlSerializer xmlSerz = new XmlSerializer(typeof(T));

                using (StringReader strReader = new StringReader(xmlSerializado))
                {
                    object obj = xmlSerz.Deserialize(strReader);
                    return (T)obj;
                }
            }
            catch { return default(T); }
        }
        /// <summary> 
        /// Método extensor para serializar JSON cualquier objeto 
        /// </summary> 
        public static string SerializarAJson(this object objeto)
        {
            string jsonResultado = string.Empty;
            try
            {
                DataContractJsonSerializer jsonSerializar = new DataContractJsonSerializer(objeto.GetType());
                MemoryStream ms = new MemoryStream();
                jsonSerializar.WriteObject(ms, objeto);
                jsonResultado = Encoding.UTF8.GetString(ms.ToArray());
            }
            catch { throw; }
            return jsonResultado;
        }
        public static T DeserializarJson<T>(this string jsonSerializado)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonSerializado));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
            catch { return default(T); }
        }
    }
}