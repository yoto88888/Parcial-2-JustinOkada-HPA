using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2___MVP.Negocio
{
    internal interface IVerificarHashService
    {
        bool Verificar(string passwordTextoPlano, string hashAlmacenado, string saltAlmacenado);
    }
}
