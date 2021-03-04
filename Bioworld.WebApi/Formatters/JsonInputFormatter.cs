using System.Threading.Tasks;

namespace Bioworld.WebApi.Formatters
{
    using Microsoft.AspNetCore.Mvc.Formatters;

    public class JsonInputFormatter : IInputFormatter
    {
        public bool CanRead(InputFormatterContext context) => true;

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}