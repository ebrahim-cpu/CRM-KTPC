<%@ Page Language="C#" %>
    <%@ Import Namespace="System.Data" %>
        <%@ Import Namespace="System.Data.SqlClient" %>
            <%@ Import Namespace="System.Configuration" %>

                <!DOCTYPE html>
                <html>

                <head>
                    <title>Trigger Check</title>
                </head>

                <body>
                    <form id="form1" runat="server">
                        <div>
                            <h1>Trigger Diagnostic</h1>
                            <asp:GridView ID="gvTriggers" runat="server"></asp:GridView>
                        </div>
                    </form>
                    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
                        {
                            try {
                string connStr = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                                using(SqlConnection con = new SqlConnection(connStr))
                                {
                                    con.Open();
                    string sql = "SELECT name, is_instead_of_trigger FROM sys.triggers WHERE parent_id = OBJECT_ID('Investors_Profile')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    gvTriggers.DataSource = dt;
                                    gvTriggers.DataBind();
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Error: " + ex.Message);
                            }
                        }
                    </script>
                </body>

                </html>
