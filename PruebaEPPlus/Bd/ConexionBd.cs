using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PruebaEPPlus
{
    class ConexionBd
    {
        #region Propiedades
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public DataTable DatosSolicitudes { get; set; }
        public DataTable DatosDocumento { get; set; }
        public DataRow RutaDocumento { get; set; }
        #endregion

        String Query = "";

        public DataTable ConsultarSolicitudes(string CadenaConexion, DateTime FechaActual)
        {
            Codigo = 1;
            Mensaje = "Exitoso";
            string Procedimiento = "dbo.PruebaPoliza";

            DatosSolicitudes = new DataTable();

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                try
                {
                    SqlCommand command = new SqlCommand(Procedimiento, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Fecha", FechaActual);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(DatosSolicitudes);
                }
                catch (Exception e)
                {
                    Codigo = 0;
                    Mensaje = e.Message;
                }
                connection.Close();
            }

            return DatosSolicitudes;
        }

        public DataTable ConsultarRutaArchivo(string CadenaConexion, string IdSolicitud)
        {
            string BaseDatosDocumentos = ConfigurationManager.AppSettings["BaseDatosDocumentos"];
            string IdTipoDocumento = ConfigurationManager.AppSettings["IdTipoDocumento"];
            Codigo = 1;
            Mensaje = "Exitoso";
            string Procedimiento = "dbo.spS_RutaDocumento";
            DatosDocumento = new DataTable();

            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                try
                {
                    SqlCommand command = new SqlCommand(Procedimiento, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                    command.Parameters.AddWithValue("@TipoDocumento", IdTipoDocumento);
                    command.Parameters.AddWithValue("@NombreBaseDocs", BaseDatosDocumentos);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(DatosDocumento);
                }
                catch (Exception e)
                {
                    Codigo = 0;
                    Mensaje = e.Message;
                }
                connection.Close();
            }

            return DatosDocumento;
        }

    }
}
