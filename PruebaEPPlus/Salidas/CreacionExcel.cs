﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Data;

namespace PruebaEPPlus
{
    class CreacionExcel
    {
        public void ExcelXlSX(DirectoryInfo RutaExcel, DataTable Datos, string CadenaConexion)
        {
            ConexionBd conexion = new ConexionBd();

            var NuevoArchivo = new FileInfo(RutaExcel + @"\Excel1.xlsx");

            if (NuevoArchivo.Exists)
            {
                NuevoArchivo.Delete();
                NuevoArchivo = new FileInfo(RutaExcel + @"\Excel1.xlsx");
            }

            using (var package = new ExcelPackage(NuevoArchivo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Poliza");

                //Agregamos las Columnas
                for (int i = 0; i  < Datos.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = Datos.Columns[i].ColumnName;
                }

                for (int m = 0; m < Datos.Rows.Count; m++)
                {
                    for (int n = 0; n < Datos.Columns.Count; n++)
                    {
                        int Columna = n + 1;
                        int Fila = 2 + m;
                        string Dato = Datos.Rows[m].ItemArray[n].ToString();
                        worksheet.Cells[Fila, Columna].Value = Dato;

                        if (Datos.Columns[n].ColumnName == "Caso")
                        {
                            string Ruta =  conexion.ConsultarRutaArchivo(CadenaConexion, Dato).Rows[0]["RutaArchivo"].ToString();

                            ClienteFTP clienteFTP = new ClienteFTP();
                            clienteFTP.CargarArchivo();

                        }
                    }
                }

                //for (int m = 0; m < Datos.Columns.Count; m++)
                //{

                //    //if (Datos.Columns[m].ColumnName == "Caso")
                //    //{
                //    //    ClienteFTP clienteFTP = new ClienteFTP();
                //    //    clienteFTP.CargarArchivo();
                //    //}

                //    for (int n = 0; n < Datos.Rows.Count; n++)
                //    {
                //        int Columna = m + 1;
                //        int Fila = 2 + n;
                //        if (m == 0)
                //        {
                //            worksheet.Cells[Fila, Columna].Value = Convert.ToInt32(Datos.Rows[n].ItemArray[m]);
                //        }
                //        else
                //        {
                //            worksheet.Cells[Fila, Columna].Value = Datos.Rows[n].ItemArray[m];
                //        }
                //    }
                //}

                worksheet.Cells["A2:A4"].Style.Numberformat.Format = "0";
                worksheet.Cells["B2:B4"].Style.Numberformat.Format = "0";
                worksheet.Cells["D2:D4"].Style.Numberformat.Format = "0";
                worksheet.Cells["O2:O4"].Style.Numberformat.Format = "#,##0";
                worksheet.Cells["E2:E4"].Style.Numberformat.Format = "@";
                worksheet.Cells["M2:M4"].Style.Numberformat.Format = "yyyy/MM/dd";//"dd/MM/yyyy";


                //Add some items...
                //worksheet.Cells["A2"].Value = 12001;
                //worksheet.Cells["B2"].Value = "Nails";
                //worksheet.Cells["C2"].Value = 37;
                //worksheet.Cells["D2"].Value = 3.99;

                //worksheet.Cells["A3"].Value = 12002;
                //worksheet.Cells["B3"].Value = "Hammer";
                //worksheet.Cells["C3"].Value = 5;
                //worksheet.Cells["D3"].Value = 12.10;

                //worksheet.Cells["A4"].Value = 12003;
                //worksheet.Cells["B4"].Value = "Saw";
                //worksheet.Cells["C4"].Value = 12;
                //worksheet.Cells["D4"].Value = 15.37;

                ////Add a formula for the value-column
                ////worksheet.Cells["E2:E4"].Formula = "C2*D2";

                ////Ok now format the values;
                ////using (var range = worksheet.Cells[1, 1, 1, 5])
                ////{
                ////    range.Style.Font.Bold = true;
                ////    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                ////    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                ////    range.Style.Font.Color.SetColor(Color.White);
                ////}

                //worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                ////worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                ////worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                ////worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                ////Create an autofilter for the range
                ////worksheet.Cells["A1:E4"].AutoFilter = true;

                //worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                ////There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                ////For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                ////you want to use the result of a formula in your program.
                ////worksheet.Calculate();

                ////worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                //// lets set the header text 
                //worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                //// add the page number to the footer plus the total number of pages
                //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                //    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                //// add the sheet name to the footer
                //worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                //// add the file path to the footer
                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                //// Change the sheet view to show it in page layout mode
                //worksheet.View.PageLayoutView = true;

                //// set some document properties
                ////package.Workbook.Properties.Title = "Invertory";
                ////package.Workbook.Properties.Author = "Jan Källman";
                ////package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                //// set some extended property values
                ////package.Workbook.Properties.Company = "AdventureWorks Inc.";

                //// set some custom property values
                ////package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                ////package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                //// save our new workbook and we are done!
                package.Save();
            }
        }

        public void ExcelXLS()
        {

        }
    }
}
