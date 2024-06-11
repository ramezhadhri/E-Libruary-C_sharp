



using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libruary
{
    public partial class author : System.Web.UI.Page
    {
        string connectionString = "Data Source=DESKTOP-B05IFKN\\SQLEXPRESS01;Initial Catalog=libruaryBD;Integrated Security=true;";



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM author_master_tb1 ";

                    using (SqlCommand cmd1 = new SqlCommand(query, conn))
                    {

                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd1))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gridview1.DataSource = dt;
                            gridview1.DataBind();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions
                Response.Write($"<script>alert('SQL Error: {sqlEx.Message}');</script>");

            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");

            }
        }
        //add button
        protected void Button2_Click(object sender, EventArgs e)
        {

            string id = TextBox1.Text;
            string name = TextBox2.Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                Response.Write("<script>alert('Please enter all required fields.');</script>");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO author_master_tb1 (author_id, author_name) VALUES (@id, @name)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                Response.Write("<script>alert('Registration Successful');</script>");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = TextBox1.Text;


            if (string.IsNullOrEmpty(id))
            {
                Response.Write("<script>alert('Please enter all required fields.');</script>");
                return;
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM author_master_tb1 WHERE author_id = @id";

                    using (SqlCommand cmd1 = new SqlCommand(query, conn))
                    {
                        cmd1.Parameters.AddWithValue("@id", id);
                        conn.Open();

                        using (SqlDataReader reader = cmd1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    TextBox2.Text = reader.GetValue(1).ToString(); ;
                                }

                            }
                            else
                            {
                                Response.Write("<script>alert( 'no found ID ');</script>");
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions
                Response.Write($"<script>alert('SQL Error: {sqlEx.Message}');</script>");

            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string id = TextBox1.Text;
            string name = TextBox2.Text;

            if (string.IsNullOrEmpty(id))
            {
                Response.Write("<script>alert('Please enter all required fields.');</script>");
                return;
            }

           

            try
            {
               
                
                
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE author_master_tb1 SET author_name = @name WHERE author_id = @id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@id", id);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    // Display a success message or redirect to another page
                    Response.Write("<script>alert('Update Successful');</script>");
                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string id = TextBox1.Text;
            

            if (string.IsNullOrEmpty(id))
            {
                Response.Write("<script>alert('Please enter all required fields.');</script>");
                return;
            }
            try
            {



                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE from author_master_tb1  WHERE author_id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                // Display a success message or redirect to another page
                Response.Write("<script>alert('DELETE Successful');</script>");

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }

        }
    }
}