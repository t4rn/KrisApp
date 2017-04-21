using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KrisApp.Common.Serialization
{
    public static class Serializer
    {
        /// <summary>
        /// Serializuje obiekt do pliku XML o ścieżce przekazanej w parametrze
        /// </summary>
        public static void SerializeToXML<T>(T obj, string filePath)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                xmlSer.Serialize(sw, obj);
            }
        }

        /// <summary> 
        /// Serializuje obiekt do zwracanego stringa
        /// </summary>
        public static string SerializeToXML(object obj)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xmlSerializer.Serialize(writer, obj, ns);
            }

            return sb.ToString();
        }

        /// <summary> 
        /// Serializuje obiekt do zwracanego stringa
        /// </summary>
        public static string SerializeToXMLWithNamespaces(object obj)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                xmlSerializer.Serialize(writer, obj);
            }

            return sb.ToString();
        }

        /// <summary> 
        /// Deserializuje przekazany XML do obiektu
        /// </summary>
        internal static T DeserializeFromXML<T>(string xml)
        {
            var reader = new StringReader(xml);
            var serializer = new XmlSerializer(typeof(T));
            T response = (T)serializer.Deserialize(reader);

            return response;
        }

        /// <summary>
        /// Serializuje przekazany obiekt do stringa
        /// </summary>
        public static string SerializeToJson(object obj)
        {
            if (obj != null)
                return JsonConvert.SerializeObject(obj);
            else
                return null;
        }

        /// <summary>
        /// Deserializuje przekazany string do obiektu
        /// </summary>
        public static T DeserializeFromJson<T>(string json)
        {
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
