using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;


/// <summary>
/// Provides linq querying functionality towards Excel (xls) files
/// </summary>
public class LinqToExcelProvider
{
    /// <summary>
    /// Gets or sets the Excel filename
    /// </summary>
    private string FileName { get; set; }

    /// <summary>
    /// Template connectionstring for Excel connections
    /// </summary>
    private const string ConnectionStringTemplate = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="fileName">The Excel file to process</param>
    public LinqToExcelProvider(string fileName)
    {
        FileName = fileName;
    }

    /// <summary>
    /// Returns a worksheet as a linq-queryable enumeration
    /// </summary>
    /// <param name="sheetName">The name of the worksheet</param>
    /// <returns>An enumerable collection of the worksheet</returns>
    public EnumerableRowCollection<DataRow> GetWorkSheet(string sheetName)
    {
        // Build the connectionstring
        string connectionString = string.Format(ConnectionStringTemplate, FileName);

        // Query the specified worksheet
        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", sheetName), connectionString);

        // Fill the dataset from the data adapter
        DataSet myDataSet = new DataSet();
        dataAdapter.Fill(myDataSet, "ExcelInfo");

        // Initialize a data table which we can use to enumerate the contents based on the dataset
        DataTable dataTable = myDataSet.Tables["ExcelInfo"];

        // Return the data table contents as a queryable enumeration
        return dataTable.AsEnumerable();
    }
}
///http://elblogdelbeto.com/como-pasar-de-excel-a-sql-con-linq-en-visual-studio-y-c/
 //LinqToExcelProvider provider = new LinqToExcelProvider(@"c:\Employees.xls");
 
 //           // Query the worksheet
 //           var query = from p in provider.GetWorkSheet("Hoja1")
 //                       select new
 //                       {
 //                           Id = Convert.ToInt32(p.Field<object>("Id")),
 //                           FirstName = Convert.ToString(p.Field<object>("FirstName")),
 //                           LastName = Convert.ToString(p.Field<object>("LastName")),
 //                           Age = Convert.ToInt32(p.Field<object>("Age"))
 //                       };
 
 //           // Display the query result ordered by age
 //           foreach (var row in query.OrderBy(p => p.Age))
 //           {
 //               Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", row.Id, row.FirstName, row.LastName, row.Age));
 //           }
 
