using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace Test_webapplication
{
    public partial class Excel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable employeeTable = new DataTable("Employee");
            employeeTable.Columns.Add("Employee ID");
            employeeTable.Columns.Add("Employee Name");
            employeeTable.Columns.Add("Employee des");
            employeeTable.Rows.Add("1", "Praveen", "Tester");
            employeeTable.Rows.Add("2", "PraveenDEF", "Tester");
            employeeTable.Rows.Add("3", "PraveenPQR", "Tester1");
            employeeTable.Rows.Add("24", "XYZ", "Tester1");
            employeeTable.Rows.Add("21", "Praveen", "develop2er");
            employeeTable.Rows.Add("22", "PraveenDEF", "develope2r");
            employeeTable.Rows.Add("23", "PraveenPQR", "developer2");
            employeeTable.Rows.Add("34", "XYZ", "developer2");
            employeeTable.Rows.Add("31", "Praveen", "developer1");
            employeeTable.Rows.Add("32", "PraveenDEF", "BA");
            employeeTable.Rows.Add("33", "PraveenPQR", "BA1");
            employeeTable.Rows.Add("54", "XYZ", "eBA1"); ;

            //Create a Department Table
            DataTable departmentTable = new DataTable("Department");
            departmentTable.Columns.Add("Department ID");
            departmentTable.Columns.Add("Department Name");
            departmentTable.Rows.Add("1", "IT");
            departmentTable.Rows.Add("2", "HR");
            departmentTable.Rows.Add("3", "Finance");
            departmentTable.Rows.Add("31", "IT");
            departmentTable.Rows.Add("32", "HR");
            departmentTable.Rows.Add("33", "Finance");
            departmentTable.Rows.Add("41", "IT");
            departmentTable.Rows.Add("42", "HR");
            departmentTable.Rows.Add("43", "Finance");




            //Create a DataSet with the existing DataTables
            DataSet ds = new DataSet("Organization");
            ds.Tables.Add(employeeTable);
            ds.Tables.Add(departmentTable);



            ///ADDITIONAL HEADER OPTIONS 
            CustomExcelProperties objCustomExcelProperties = new CustomExcelProperties();
            objCustomExcelProperties.ColumnHeaderBackgroundColor = XLColor.Red;
            objCustomExcelProperties.TabColor = XLColor.Green;
            objCustomExcelProperties.ColumnHeaderAlignmentHorizontal = XLAlignmentHorizontalValues.Center;
            //specify the additional row header 
            objCustomExcelProperties.AddtionalHeaderRequired = true;
            objCustomExcelProperties.AddtionalHeaderRownumbers = new int[] { 4, 6 };


            ///CELL HIGHLIGHT OPTIONS 
            objCustomExcelProperties.HighlightCellRequired = true;
            CustomHighlightCellRownumber objCustomHighlightCellRownumber = new CustomHighlightCellRownumber();
            objCustomHighlightCellRownumber.HighlightCellColumnnumber = 2;
            objCustomHighlightCellRownumber.HighlightCellRownumber = 2;
            objCustomExcelProperties.HighlightCellRownumberList.Add(objCustomHighlightCellRownumber);

            CustomHighlightCellRownumber objCustomHighlightCellRownumber1 = new CustomHighlightCellRownumber();
            objCustomHighlightCellRownumber1.HighlightCellColumnnumber = 2;
            objCustomHighlightCellRownumber1.HighlightCellRownumber = 8;
            objCustomExcelProperties.HighlightCellRownumberList.Add(objCustomHighlightCellRownumber1);
            objCustomExcelProperties.HighlightCellColor = XLColor.OldGold;

            WriteExcel(ds, "test", objCustomExcelProperties);
        }
        /// <summary>
        /// Generic excel creation using data set as input 
        /// </summary>
        /// <param name="dsSource"></param>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public string WriteExcel(DataSet dsSource, string strFilename, CustomExcelProperties objCustomExcelProperties)
        {
            string strMessage = string.Empty;
            try
            {

                var workbook = new XLWorkbook();
                foreach (DataTable dataTable in dsSource.Tables)
                {
                    string sheetName = dataTable.TableName;
                    var worksheet = workbook.Worksheets.Add(sheetName);
                    worksheet.TabColor = objCustomExcelProperties.TabColor;
                    foreach (DataRow datarow in dataTable.Rows)
                    {

                        for (int i = 1; i <= dataTable.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i).Value = dataTable.Columns[i - 1].ColumnName.ToString();
                            worksheet.Cell(1, i).Style.Font.Bold = objCustomExcelProperties.ColumnHeaderFontBold;
                            worksheet.Cell(1, i).Style.Font.FontColor = objCustomExcelProperties.ColumnHeaderFontColor;
                            worksheet.Cell(1, i).Style.Fill.BackgroundColor = objCustomExcelProperties.ColumnHeaderBackgroundColor;
                            worksheet.Cell(1, i).Style.Font.Italic = objCustomExcelProperties.ColumnHeaderFontItalic;
                            worksheet.Cell(1, i).Style.Font.Strikethrough = objCustomExcelProperties.ColumnHeaderStrikeFont;
                            worksheet.Cell(1, i).Style.Font.FontName = objCustomExcelProperties.ColumnHeaderFontStyle;
                            worksheet.Cell(1, i).Style.Alignment.WrapText = objCustomExcelProperties.ColumnHeaderWrapText;
                            worksheet.Cell(1, i).Style.Alignment.Horizontal = objCustomExcelProperties.ColumnHeaderAlignmentHorizontal;

                            for (int j = 0; j < dataTable.Rows.Count; j++)
                            {
                                for (int k = 0; k < dataTable.Columns.Count; k++)
                                {

                                    worksheet.Cell(j + 2, k + 1).Value = dataTable.Rows[j].ItemArray[k].ToString();
                                    worksheet.Cell(j + 2, k + 1).Style.Font.FontColor = objCustomExcelProperties.RowFontColor;
                                    worksheet.Cell(j + 2, k + 1).Style.Fill.BackgroundColor = objCustomExcelProperties.RowBackgroundColor;
                                    worksheet.Cell(j + 2, k + 1).Style.Font.Italic = objCustomExcelProperties.RowFontItalic;
                                    worksheet.Cell(j + 2, k + 1).Style.Font.Bold = objCustomExcelProperties.RowFontBold;
                                    worksheet.Cell(j + 2, k + 1).Style.Font.Strikethrough = objCustomExcelProperties.RowStrikeFont;
                                    worksheet.Cell(j + 2, k + 1).Style.Font.FontName = objCustomExcelProperties.RowFontStyle;
                                    worksheet.Cell(j + 2, k + 1).Style.Alignment.WrapText = objCustomExcelProperties.RowWrapText;
                                    worksheet.Cell(j + 2, k + 1).Style.Alignment.Horizontal = objCustomExcelProperties.RowAlignmentHorizontal;

                                }
                            }

                        }
                    }

                    ////if highlight cell is true then proceed 
                    if (objCustomExcelProperties.HighlightCellRequired)
                    {
                        for (int intHighlight = 0; intHighlight < objCustomExcelProperties.HighlightCellRownumberList.Count(); intHighlight++)
                        {
                            if (objCustomExcelProperties.HighlightCellRownumberList[intHighlight].HighlightCellRownumber <= dataTable.Rows.Count && objCustomExcelProperties.HighlightCellRownumberList[intHighlight].HighlightCellColumnnumber <= dataTable.Columns.Count)
                            {
                                worksheet.Cell(objCustomExcelProperties.HighlightCellRownumberList[intHighlight].HighlightCellRownumber, objCustomExcelProperties.HighlightCellRownumberList[intHighlight].HighlightCellColumnnumber).Style.Fill.BackgroundColor = objCustomExcelProperties.HighlightCellColor;
                            }
                        }

                    }

                    //if additional header is defined 
                    if (objCustomExcelProperties.AddtionalHeaderRequired)
                    {
                        for (int introw = 0; introw < objCustomExcelProperties.AddtionalHeaderRownumbers.Count(); introw++)
                        {
                            for (int intcolumn = 1; intcolumn <= dataTable.Columns.Count; intcolumn++)
                            {
                                if (objCustomExcelProperties.AddtionalHeaderRownumbers[introw] <= dataTable.Rows.Count)
                                {
                                    worksheet.Cell(objCustomExcelProperties.AddtionalHeaderRownumbers[introw], intcolumn).Style.Fill.BackgroundColor = objCustomExcelProperties.ColumnHeaderBackgroundColor;

                                }
                            }

                        }
                    }
                }
                //change needed in the BAS code 
                string root = @"C:\Users\N.SANGAVI\Documents\visual studio 2010\Projects\Test_webapplication\tmp\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(root);
                foreach (FileInfo file in di.GetFiles())
                {
                    if (file.Length > 0)
                        file.Delete();
                }

                strFilename = strFilename + DateTime.Now.ToString("_yyyMMdd_HHmmss") + ".xlsx";
                workbook.SaveAs(Path.Combine(root + strFilename));

                // Response.ContentType = ContentType;
                // Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Path.Combine(root + strFilename)));
                // Response.WriteFile(Path.Combine(root + strFilename));
                // Response.End();
                return strMessage = "SUCCESS";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {

            }


        }
    }

}