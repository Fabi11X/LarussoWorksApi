using System;
using System.Collections.Generic;

#nullable disable

namespace LarussoWorksApi.Domain.entities
{
    public partial class Contactano
    {
        public int IdContacto { get; set; }
        public string AsuntoContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string MensajeContacto { get; set; }
    }
}
