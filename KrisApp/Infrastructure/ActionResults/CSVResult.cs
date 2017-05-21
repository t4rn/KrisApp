using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KrisApp.Infrastructure
{
    public class CSVResult : FileResult
    {
        private readonly IEnumerable _data;

        public CSVResult(IEnumerable data, string fileName) : base("text/csv")
        {
            _data = data;
            FileDownloadName = fileName;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            foreach (object item in _data)
            {
                PropertyInfo[] properteis = item.GetType().GetProperties();

                foreach (PropertyInfo prop in properteis)
                {
                    sw.Write(GetValue(item, prop.Name));
                    sw.Write("| ");
                }
                sw.WriteLine();
            }

            response.Write(sb);
        }

        private string GetValue(object item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null)?.ToString() ?? "";
        }
    }
}