using Evaluacion2.imz.Comunicacion;
using Modelo;
using Modelo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evaluacion2.imz
{

    public class Program
    {
        private static ModeloDAL mensajesDAL = ModeloDALArchivos.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Seleccione una de las opciones");
            Console.ResetColor();
            Console.WriteLine("1. Ingresar");
            Console.WriteLine("2. Mostrar");
            Console.WriteLine("0. Salir");
            Console.WriteLine("9. Limpiar");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                case "9":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opcion no encontrada, intentalo nuevamente........");
                    break;
            }
            return continuar;
        }

        static void IniciarServidor()
        {


        }
        static void Main(string[] args)
        {
            //Iniciar el Servidor Socket en el puerto 3000
            //El puerto tiene que ser configurable App.Config
            //IniciarServidor();
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            //1. ¿como atender mas de un cliente a las vez?
            //2. ¿como evito que dos clientes ingresen al archivo a la vez?
            //3. ¿como evitar el bloqueo mutuo?
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Ingresa el numero de tu medidor");
            Console.ResetColor();
            Console.WriteLine("Numero del medidor: ");
            string nromedidor = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Ingresa la fecha respetando el formato yyyy-MM-dd HH:mm:ss ");
            Console.ResetColor();
            Console.WriteLine("Fecha: ");
            string fecha = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Ingresa el valor de tu consumo actual kw/h utilizando coma decimal");
            Console.ResetColor();
            Console.WriteLine("Consumo: ");
            string valorconsumo = Console.ReadLine().Trim();
            Mensaje mensaje = new Mensaje()
            {
                NroMedidor = nromedidor,
                Fecha = fecha,
                ValorConsumo = valorconsumo
            };
            lock (mensajesDAL)
            {
                mensajesDAL.AgregarMensaje(mensaje);
            }
        }

        static void Mostrar()
        {
            List<Mensaje> mensajes = null;
            lock (mensajesDAL)
            {
                mensajes = mensajesDAL.ObtenerMensajes();
            }
            foreach (Mensaje mensaje in mensajes)
            {
                Console.WriteLine(mensaje);
            }

        }
    }
}
