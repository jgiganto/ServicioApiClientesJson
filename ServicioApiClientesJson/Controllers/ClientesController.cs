using ServicioApiClientesJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ServicioApiClientesJson.Controllers
{
    public class ClientesController : ApiController
    {
        ModeloClientes modelo;
        //creamos un metodo para instanciar al modelo con la ruta fisica al servidor
        private void CrearModelo()
        {
            //capturamos la ruta fisica a nuestro server
            String path =
                HttpContext.Current.Server.MapPath("~/Documentos/clientes.json");
            this.modelo = new ModeloClientes(path);
        }

        // GET: api/Clientes
        public List<Clientes> Get()
        {
            this.CrearModelo();
            return modelo.GetClientes();
        }

        // GET: api/Clientes/5
        public Clientes Get(int id)
        {
            this.CrearModelo();
            return modelo.BuscarCliente(id);
        }

        // POST: api/Clientes
        public void Post(Clientes cliente)
        {
            this.CrearModelo();
            this.modelo.InsertarCliente(cliente.IdCliente, cliente.Nombre, cliente.PaginaWeb, cliente.Imagen);
        }

        // PUT: api/Clientes/5
        public void Put(int id,Clientes cliente)
        {
            this.CrearModelo();
            this.modelo.ModificarCliente(id, cliente.Nombre, cliente.PaginaWeb, cliente.Imagen);
        }

        // DELETE: api/Clientes/5
        public void Delete(int id)
        {
            this.CrearModelo();
            this.modelo.EliminarCliente(id);
        }
    }
}
