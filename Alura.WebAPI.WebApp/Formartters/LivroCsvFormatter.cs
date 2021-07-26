using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Formartters
{
    public class LivroCsvFormatter : TextOutputFormatter
    {
        public LivroCsvFormatter()
        {
            var csvMediaType = MediaTypeHeaderValue.Parse("text/csv");
            var appCsvMediaType = MediaTypeHeaderValue.Parse("application/csv");
            SupportedMediaTypes.Add(csvMediaType);
            SupportedMediaTypes.Add(appCsvMediaType);
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LivroApi);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {

            var livroEmCsv = "";
            if (context.Object is LivroApi)
            {
                var livro = context.Object as LivroApi;
                livroEmCsv = $"{livro.Titulo};{livro.Subtitulo};{livro.Autor};{livro.Lista}";
            }

            using (var escritor = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
                return escritor.WriteAsync(livroEmCsv);
            }
            
        }
    }
}
