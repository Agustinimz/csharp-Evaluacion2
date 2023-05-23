using System;
using System.Collections.Generic;
using Modelo;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DAL
{
    public class ModeloDALArchivos : ModeloDAL
    {
        //implementar Singleton:
        //1. Contructor tiene que ser private
        private ModeloDALArchivos() { }

        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static ModeloDALArchivos instancia;
        //3. tener un metodo getIntance, que de vuelva una referencia al atributo
        public static ModeloDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ModeloDALArchivos();
            }
            return instancia;
        }
        //como vamos a hacer para que 2 hebras no accedan a la vez a este archivo?????


        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";
        public void AgregarMensaje(Mensaje mensaje)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(mensaje.NroMedidor + ";" + mensaje.Fecha + ";" + mensaje.ValorConsumo);
                    write.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Mensaje> ObtenerMensajes()
        {
            List<Mensaje> lista = new List<Mensaje>();
            try
            {
                using (StreamReader read = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = read.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Mensaje mensaje = new Mensaje()
                            {
                                NroMedidor = arr[0],
                                Fecha = arr[1],
                                ValorConsumo = arr[2]
                            };
                            lista.Add(mensaje);
                        }

                    } while (texto != null);
                }

            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }


    }
}

