using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string memberId = TextBox1.Text;
            string password = TextBox2.Text;

            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Please enter both Member ID and Password.');</script>");
                return;
            }

            string connectionString = "Data Source=DESKTOP-B05IFKN\\SQLEXPRESS01;Initial Catalog=libruaryBD;Integrated Security=true;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM [dbo].[member_master_tb1] WHERE [member_id] = @memberId AND [password] = @password";
                    using (SqlCommand cmd1 = new SqlCommand(query, conn))
                    {
                        cmd1.Parameters.AddWithValue("@memberId", memberId);
                        cmd1.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    
                                    Session["username"] = reader.GetValue(8).ToString();
                                    Session["full_name"] = reader.GetValue(0).ToString();
                                    Session["role"] = "user";
                                    Session["status"] = reader.GetValue(10).ToString(); ;

                                    Response.Write("<script>alert('" + reader.GetValue(8).ToString() + "');</script>");
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("usersignup.aspx");
        }
    }
}
