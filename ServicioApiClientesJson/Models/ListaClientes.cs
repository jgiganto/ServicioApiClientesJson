using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ServicioApiClientesJson.Models
{
    public class ListaClientes
    {
        [JsonProperty("clientes")]
        public List<Clientes> Clientes { get; set; }
    }
}