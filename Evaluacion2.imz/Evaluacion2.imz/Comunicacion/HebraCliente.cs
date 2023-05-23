using Modelo;
using Modelo.DAL;
using ServidorSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.imz.Comunicacion
{
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private ModeloDAL mensajesDAL = ModeloDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            //ahora traemos el codigo que atiende al cliente
            clienteCom.Escribir("Ingrese nro medidor: ");
            string nromedidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese fecha: ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese valor consumido: ");
            string valorconsumo = clienteCom.Leer();
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

            clienteCom.Desconectar();
        }
    }
}

