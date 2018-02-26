using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEPPlus
{
    class Salida
    {
        public void IniciarProcesoSalida()
        {
            #region Variables            
            var appSettings = ConfigurationManager.AppSettings;
           
            string ExtensionArchivo = appSettings["ExtensionExcel"];
            string ConexionBd = appSettings["CadenaConexion"];
            string Ip = appSettings["Ip"];
            string Puerto = appSettings["Puerto"];
            string CarpetaSalida = appSettings["CarpetaSalida"];
            DirectoryInfo RutaSalida = new DirectoryInfo(appSettings["RutaSalida"]);

            DateTime FechaActual = DateTime.Now;
            string Dia = FechaActual.ToString("dd");
            string Mes = FechaActual.ToString("MM");
            string Anio = FechaActual.ToString("yyyy");

            #endregion

            Uri RutaNuevaCarpeta = new Uri("ftp://" + Ip + ":" + Puerto + CarpetaSalida + Dia + Mes + Anio);
            DataTable DatosExcel = new DataTable();

            ClienteFTP Cliente = new ClienteFTP();
            ConexionBd bd = new ConexionBd();
            CreacionExcel creacionExcel = new CreacionExcel();
            

            //Cliente.CrearDirectorio(RutaNuevaCarpeta);

            DatosExcel = bd.ConsultarSolicitudes(ConexionBd, DateTime.Today);

            if (bd.Codigo == 0)
            {
                Console.WriteLine("Error ", bd.Mensaje);
            }
            else
            {
                creacionExcel.ExcelXlSX(RutaSalida, DatosExcel, ConexionBd);
            }
            
            
        }
    }
}
