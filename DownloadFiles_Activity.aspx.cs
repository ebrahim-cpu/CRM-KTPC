using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class DownloadFiles_Activity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Document_Id"] != null)
                {
                    int fileId = Convert.ToInt32(Request.QueryString["Document_Id"]);
                    DownloadFromDatabase(fileId);
                }
            }
        }
        private void DownloadFromDatabase(int fileId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT FileName, FileData, ContentType FROM Document_IActivity WHERE Document_Id = @FileId", connection);
                cmd.Parameters.AddWithValue("@FileId", fileId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string fileName = reader["FileName"].ToString();
                        byte[] fileData = (byte[])reader["FileData"];
                        string contentType = reader["ContentType"].ToString();
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                        Response.BinaryWrite(fileData);
                        Response.End();
                    }
                }
            }
        }
    }
}