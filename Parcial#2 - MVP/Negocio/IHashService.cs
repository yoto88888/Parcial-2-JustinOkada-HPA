using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2___MVP.Negocio
{
    internal interface IHashService
    {
        (string Hash, string Salt) GenerarHash(string passwordTextoPlano);
    }
}
