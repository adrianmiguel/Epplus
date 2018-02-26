using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEPPlus
{
    class ClienteFTP
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        Uri uri;
        FtpWebRequest clienteRequest;
        NetworkCredential credenciales;
        string Ip = "192.168.0.21";
        string Puerto = "2221";

        public void CrearDirectorio(Uri uri)
        {
            try
            {
                //uri = new Uri("ftp://192.168.0.21:2221/ServidorFtp/Salidas/20180224");
                clienteRequest = (FtpWebRequest)WebRequest.Create(uri);

                credenciales = new NetworkCredential("adrian", "adrian9110");
                clienteRequest.Credentials = credenciales;

                clienteRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)clienteRequest.GetResponse();
                Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Conectarse()
        {
            try
            {
                //uri = new Uri("ftp://" + Ip + ":" + Puerto);
                //uri = new Uri("ftp://" + Ip + ":" + Puerto + "/PrurbaFtp/");
                uri = new Uri("ftp://" + Ip + ":" + Puerto + "/ServidorFtp/Entradas/20180224");

                clienteRequest = (FtpWebRequest)WebRequest.Create(uri);

                credenciales = new NetworkCredential("adrian", "adrian9110");

                clienteRequest.Credentials = credenciales;
                clienteRequest.EnableSsl = false;
                clienteRequest.Method = WebRequestMethods.Ftp.ListDirectory;// .ListDirectoryDetails;
                //clienteRequest.Method = WebRequestMethods.Http.Get;
                clienteRequest.KeepAlive = true;
                clienteRequest.UsePassive = true;

                FtpWebResponse respuesta = (FtpWebResponse)clienteRequest.GetResponse();

                StreamReader sr = new StreamReader(respuesta.GetResponseStream(), Encoding.UTF8);

                string resultado = sr.ReadToEnd();
                string mensaje = respuesta.WelcomeMessage;
                respuesta.Close();

                //List<Archivo> archivos = ObtieneLsita(resultado);

                //foreach (Archivo item in archivos)
                //{
                //    //string lista = item.nombre;
                //    Console.WriteLine(item.nombre);
                //    Console.ReadKey();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EliminarDiretorio(Uri uri)
        {
            try
            {
                //string archivocarga = "cover.jpg";
                //uri = new Uri("ftp://" + Ip + ":" + Puerto + "/PrurbaFtp/");
                //uri = new Uri("ftp://192.168.0.21:2221/ServidorFtp/Salidas/20180224");
                clienteRequest = (FtpWebRequest)WebRequest.Create(uri);

                credenciales = new NetworkCredential("adrian", "adrian9110");
                clienteRequest.Credentials = credenciales;

                clienteRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse response = (FtpWebResponse)clienteRequest.GetResponse();
                Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void CargarArchivo()
        {
            string archivocarga = "Excel1.xlsx";
            uri = new Uri("ftp://192.168.0.21:2221/ServidorFtp/Salidas/24022018/Excel1.xlsx");
            clienteRequest = (FtpWebRequest)WebRequest.Create(uri);

            credenciales = new NetworkCredential("adrian", "adrian9110");
            clienteRequest.Credentials = credenciales;

            clienteRequest.Method = WebRequestMethods.Ftp.UploadFile;

            Stream destino = clienteRequest.GetRequestStream();
            FileStream origen = new FileStream(@"C:\Users\adria\Documents\Visual Studio 2017\Projects\PruebaEPPlus\SalidaArchivos\24022018\" + archivocarga, FileMode.Open, FileAccess.Read);
            crearArhivo(origen, destino);
        }

        private void crearArhivo(Stream origen, Stream destino)
        {
            byte[] buffer = new byte[1024];
            int bytesLeidos = origen.Read(buffer, 0, 1024);
            while (bytesLeidos != 0)
            {
                destino.Write(buffer, 0, bytesLeidos);
                bytesLeidos = origen.Read(buffer, 0, 104);
            }
            origen.Close();
            destino.Close();
        }
    }
}
