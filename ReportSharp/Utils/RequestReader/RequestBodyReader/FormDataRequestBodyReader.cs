using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.RequestReader.RequestBodyReader
{
    public class FormDataRequestBodyReader : IRequestBodyReader
    {
        public HttpContext Context { get; set; }

        public string Read(HttpContext context)
        {
            Context = context;
            var bodyFiles = ReadFiles();
            var bodyForms = ReadForms();
            return $"{string.Join(",\n", bodyForms)},\n{string.Join(",\n", bodyFiles)}";
        }

        public IEnumerable<string> ReadFiles()
        {
            return Context.Request.Form.Files
                .Select(x =>
                    $"{x.Name} => {x.FileName} | {x.ContentDisposition} | {x.ContentType} | {x.Length} bytes"
                ).ToList();
        }

        public IEnumerable<string> ReadForms()
        {
            return Context.Request.Form
                .Select(x => $"{x.Key} => {x.Value.ToString()}")
                .ToList();
        }
    }
}