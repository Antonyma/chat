using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Servidor
{
    public class Mensajes
    {
        public readonly int Port;
        public readonly string Nombre;
        private ConcurrentDictionary<string, TcpClient> listaConectados = new ConcurrentDictionary<string, TcpClient>();
        private TcpListener _server;
        private BinaryWriter _writer;
        private BinaryReader _reader;

        public Mensajes(TcpListener server)
        {
            _server = server;
            try
            {
                server.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void HiloMensajes()
        {            
            //TcpListener escucha y acepta solicitudes de conexion
            TcpClient nuevoCliente = _server.AcceptTcpClient();
            _reader = new BinaryReader(nuevoCliente.GetStream(), Encoding.ASCII);

            string user = _reader.ReadString();
            string password = _reader.ReadString();
            
            //Comprueba que esté registrado
            if (ComprobarUsuario(user,password))
            {
                listaConectados.TryAdd(user, nuevoCliente);

                foreach (var kvp in listaConectados)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
                Console.WriteLine(user + " se ha conectado");
                var thread = new System.Threading.Thread(() => ControlarMensajes(nuevoCliente, user));
                thread.Start();
            }
            // Thread thread = new Thread(() => ControlarMensajes(nuevoCliente, user));
            // thread.Start();
        }

        public void ControlarMensajes(TcpClient sock, string nombre)
        {
            _reader = new BinaryReader(sock.GetStream(), Encoding.ASCII);
            string data;

            while (true)
            {
                data = _reader.ReadString();
                Console.WriteLine(data);

                try
                {
                    foreach (var kvp in listaConectados)
                    {
                        _writer = new BinaryWriter(kvp.Value.GetStream(), Encoding.ASCII);
                        _writer.Write($"- {nombre}: {data}");
                        _writer.Flush();
                    }
                    data = null;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public bool ComprobarUsuario(string nombre, string clave)
        {
            string[] lineaLeida = File.ReadAllLines("./usuarios.txt");

            foreach (var linea in lineaLeida)
            {
                string[] separador = linea.Split(";");
                Usuario p = new Usuario(separador[0], separador[1]);
                if ((nombre.Equals(separador[0]) && clave.Equals(separador[1])) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}