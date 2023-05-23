using Modelo.DAL;
using ServidorSocket;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evaluacion2.imz.Comunicacion
{
    public class HebraServidor
    {
        private ModeloDAL mensajesDAL = ModeloDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("S: Servidor iniciado en puerto {0}", puerto);
            Console.ResetColor();
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("S: Esperando cliente....");
                    Console.ResetColor();
                    Socket cliente = servidor.ObtenerCliente();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("S: Cliente recibido");
                    Console.ResetColor();
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("Error fatal {0}", puerto);
            }
        }
    }
}
