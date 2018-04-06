using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServicioApiClientesJson.Models
{
    public class ModeloClientes
    {
        private String Path { get; set; }
        public ModeloClientes(String path)
        {
            this.Path = path;
        }

        public List<Clientes> GetClientes()
        {
            //usamos sistema de ficheros para leer el contenido del doc Json
            String json = File.ReadAllText(this.Path);
            //trabajamos con la clase jsonObject para almacenar
            //todo el contenido o parte de un Json
            var jsonobject = JObject.Parse(json);//parseo json a traves de un texto
            //a partir de un object puedo extraer el contenido de una determinada etiqueta, como si fuese un XML
            //y nos devuelve un conjunto llamado jarray(en este caso los clientes)
            JArray clientes = (JArray)jsonobject["clientes"];
            //realizamos un foreach para extraer todos los clientes de forma manual
            List<Clientes> lista = new List<Clientes>();
            foreach(var c in clientes)
            {
                Clientes cliente = new Clientes();
                cliente.IdCliente = int.Parse(c["idcliente"].ToString());
                cliente.Nombre = c["nombre"].ToString();
                cliente.PaginaWeb = c["paginaweb"].ToString();
                cliente.Imagen = c["imagencliente"].ToString();
                lista.Add(cliente);
            }
            return lista; 
        }

        public Clientes BuscarCliente(int idcliente)
        {
            String json = File.ReadAllText(this.Path);
            //recuperamos todos los clientes
            ListaClientes lista = JsonConvert.DeserializeObject<ListaClientes>(json);
            //buscamos el cliente con el id mediante lambda
            Clientes cliente = lista.Clientes.Where(z => z.IdCliente == idcliente).FirstOrDefault();
            return cliente;
        }

        public void InsertarCliente(int idcliente,String nombre, String paginaweb,String imagen)
        {
            //creamos un nuevo objeto cliente 
            Clientes cliente = new Clientes
            {
                IdCliente = idcliente,Nombre=nombre,PaginaWeb=paginaweb,Imagen=imagen
            };
            //leemos todo el contenido json
            String json = File.ReadAllText(this.Path);
            //extraemos todos los clientes a objetos
            ListaClientes lista =
                JsonConvert.DeserializeObject<ListaClientes>(json);
            //añadimos nuestro nuevo cliente 
            lista.Clientes.Add(cliente);
            //convertimos la clase clientes a contenido json 
            //serializando su contenido a dicho formato
            String newjson = JsonConvert.SerializeObject(lista, Formatting.Indented);
            //sobreescribimos el documento Json
            File.WriteAllText(this.Path, newjson);         
           
        }
        public void ModificarCliente(int idcliente, String nombre, String paginaweb, String imagen)
        {
            String json = File.ReadAllText(this.Path);
            ListaClientes lista =
                JsonConvert.DeserializeObject<ListaClientes>(json);
            Clientes cliente =
                lista.Clientes.Where(z => z.IdCliente == idcliente).FirstOrDefault();
            cliente.Nombre = nombre;
            cliente.PaginaWeb = paginaweb;
            cliente.Imagen = imagen;
            String newjson = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(this.Path, newjson);            
        }

        public void EliminarCliente(int idcliente)
        {
            String json = File.ReadAllText(this.Path);
            ListaClientes lista = JsonConvert.DeserializeObject<ListaClientes>(json);
            Clientes cliente = lista.Clientes.Where(z => z.IdCliente == idcliente).FirstOrDefault();
            lista.Clientes.Remove(cliente);
            String newjson = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(this.Path, newjson);
        }
    }
}