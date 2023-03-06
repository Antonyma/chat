using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Servidor
{
    public class Conexion
    {
        private TcpListener servidor;
        public readonly int port;

        public Conexion(int port)
        {
            try
            {
                servidor = new TcpListener(IPAddress.Any, port);
                Console.WriteLine("Esperando conexión...");
                //Bucle para aceptar conexiones infinitas
                Mensajes m = new Mensajes(servidor);
                while (true)
                {
                    HiloCliente(m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void HiloCliente(Mensajes m)
        {
            try
            {
                m.HiloMensajes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);            
            }        
        }

        public bool ComprobarUsuario(string nombre)
        {
            string[] lineaLeida = File.ReadAllLines("./usuarios.txt");

            foreach (var linea in lineaLeida)
            {
                //CREAR OBJETO PARA COMPARAR CON LA LISTA
                string[] separador = linea.Split(";");
                Usuario p = new Usuario(separador[0], separador[1]);
                if (nombre.Equals(p.Nombre) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
