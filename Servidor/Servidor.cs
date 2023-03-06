
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    class Servidor
    {
       static void Main(string[] args)
        {
            Console.Clear();
            // Crea servidor
            int port = 13000;
            Conexion m = new Conexion(port);
        } 
    }
}