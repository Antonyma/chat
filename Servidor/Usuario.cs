using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{   
    public class Usuario
    {   
        private string nombre;
        private string clave;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Clave { get => clave; set => clave = value; }

        public Usuario(string usuario, string clave)
        {
            nombre = usuario;
            clave = clave;
        }

        public bool ComprobarUsuario(string nombre, string clave)
        {
            string[] lineaLeida = File.ReadAllLines("./usuarios.txt");

            foreach (var linea in lineaLeida)
            {
                //CREAR OBJETO PARA COMPARAR CON LA LISTA
                string[] separador = linea.Split(";");
                Usuario p = new Usuario(separador[0], separador[1]);
                if (nombre.Equals(p.Nombre) == false || clave.Equals(p.clave) == false)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}