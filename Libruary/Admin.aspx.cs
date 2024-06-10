using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libruary
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            string full_name = TextBox1.Text;
            string password = TextBox2.Text;

            if (string.IsNullOrEmpty(full_name) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Please enter both full_name and Password.');</script>");
                return;
            }

            string connectionString = "Data Source=DESKTOP-B05IFKN\\SQLEXPRESS01;Initial Catalog=libruaryBD;Integrated Security=true;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM [dbo].[admin_login_tb1] WHERE [full_name] = @full_name AND [password] = @password";
                    using (SqlCommand cmd1 = new SqlCommand(query, conn))
                    {
                        cmd1.Parameters.AddWithValue("@full_name", full_name);
                        cmd1.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Session["role"] = "admin";
                                }
                                Response.Redirect("homepage.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('No matching records found.');</script>");
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log detailed SQL exception (consider using a logging framework)
                Response.Write("<script>alert('Database error occurred. Please try again later.');</script>");
            }
            catch (Exception ex)
            {
                // Log detailed exception (consider using a logging framework)
                Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
            }
        }
    }
}