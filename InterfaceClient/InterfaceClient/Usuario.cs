using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Clientes
{   
    class Usuario
    {   
        private string nombre;
        private string clave;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Clave { get => clave; set => clave = value; }

        public Usuario(string usuario, string clave)
        {
            this.Nombre = usuario;
            this.clave = clave;
        }
        
        public string Cadena()
        {
            return Nombre + ";" + clave;
        }

        public void GuardarUsuario(string nombre, string clave) 
        {
            StreamWriter miDoc = File.AppendText("../Servidor/usuarios.txt");
            miDoc.WriteLine(Cadena());
            miDoc.Close();
            Console.WriteLine("Usuario registrado con exito.");
        }

        public bool ComprobarUsuario(Usuario u) 
        {
            string[] lineaLeida = File.ReadAllLines("../Servidor/usuarios.txt");
            
            foreach (var linea in lineaLeida)
            {
                //CREAR OBJETO PARA COMPARAR CON LA LISTA
                string[] separador = linea.Split(";");
                Usuario p =  new Usuario(separador[0],separador[1]);
                if (u.Nombre.Equals(p.Nombre) == false && u.clave.Equals(p.clave) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}