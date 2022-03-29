using System;
using System.Collections.Generic;
using System.Text;

namespace LarussoWorksApi.Domain.entities{

    public class Paginacion
    {
        public int pagina {get; set;} = 1;
        public int CantidadAMostrar {get; set;} = 6;
    }
}