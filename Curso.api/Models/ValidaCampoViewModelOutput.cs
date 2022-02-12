using System.Collections.Generic;

namespace Curso.api.Models
{
    class ValidaCampoViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }
        public ValidaCampoViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}