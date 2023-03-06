using System.Text;
using System.Net.Sockets;

namespace Clientes
{
    public class Conexion
    {
        public readonly int Port;
        private TcpClient cliente;
        public Conexion(int port)
        {        
            try
            {
                //TcpListener escucha y acepta solicitudes de conexion
                cliente = new TcpClient();
                cliente.Connect("localhost", port);

                if (cliente.Connected)
                {
                    Console.WriteLine("Esperando conexión...");
                    Mensajes();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void Mensajes()
        {    
            BinaryWriter writer = new BinaryWriter(cliente.GetStream(), Encoding.ASCII);
            BinaryReader reader = new BinaryReader(cliente.GetStream(), Encoding.ASCII);
            string data = null;
            
            //Crea hilo en segundo plano
            Task.Run(()=>
            {
             do
             {
                string mensajeRecibido = reader.ReadString();
                Console.WriteLine(mensajeRecibido); 
             } while (cliente.Connected);
            });

            do
            {
                if (data == null)
                {
                    //Se para 
                    data = Console.ReadLine();
                    writer.Write(data);
                    writer.Flush();
                    data = null;
                }
            }
            while (cliente.Connected);
        }
    }
}
