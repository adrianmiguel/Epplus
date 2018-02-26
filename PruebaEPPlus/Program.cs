using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Configuration;

namespace PruebaEPPlus
{
    class Program
    {
        static void Main(string[] args)
        {

            //Salida salida = new Salida();
            //salida.IniciarProcesoSalida();


            var appSettings = ConfigurationManager.AppSettings;

            string Ip = appSettings["Ip"];
            string Puerto = appSettings["Puerto"];
            string CarpetaSalida = appSettings["CarpetaSalida"];

            DateTime FechaActual = DateTime.Now;

            string Dia = FechaActual.ToString("dd");
            string Mes = FechaActual.ToString("MM");
            string Anio = FechaActual.ToString("yyyy");

            Uri NuevaCarpeta = new Uri("ftp://" + Ip + ":" + Puerto + CarpetaSalida + Dia + Mes + Anio);

            ClienteFTP Cliente = new ClienteFTP();
            //Cliente.Conectarse();
            //Cliente.cmdBorrar();
            //Cliente.EliminarDiretorio(NuevaCarpeta);
            //Cliente.CrearDirectorio(NuevaCarpeta);
            //Cliente.CargarArchivo();

            Salida salida = new Salida();
            salida.IniciarProcesoSalida();

            //try
            //{
            //    ExcelPackage Excel = new ExcelPackage(new FileInfo(@"C:\Users\adria\Documents\Visual Studio 2017\Projects\PruebaEPPlus\Salidas\Excel.xlsx"));
            //    Excel.Workbook.Worksheets.Add("Hoja1");
            //    Excel.Save();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
    }
}
