using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Libruary
{
    public partial class usersignup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fullName = TextBox1.Text;
            string dob = TextBox2.Text;
            string contactNo = TextBox3.Text;
            string email = TextBox4.Text;
            string state = DropDownList1.SelectedValue;
            string city = TextBox6.Text;
            string pincode = TextBox7.Text;
            string fullAddress = TextBox5.Text;
            string memberId = TextBox8.Text;
            string password = TextBox9.Text;
            string accountStatus = "Pending"; // Assuming a default value for new sign-ups

            string connectionString = "Data Source=DESKTOP-B05IFKN\\SQLEXPRESS01;Initial Catalog=libruaryBD;Integrated Security=true;";

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(memberId))
            {
                Response.Write("<script>alert('Please enter all required fields.');</script>");
                return;
            }
            bool CheckMember()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT * FROM member_master_tb1 WHERE member_id = @MemberId";

                        using (SqlCommand cmd1 = new SqlCommand(query, conn))
                        {
                            cmd1.Parameters.AddWithValue("@MemberId", memberId);
                            conn.Open();

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd1))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                return dt.Rows.Count >= 1;
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions
                    Response.Write($"<script>alert('SQL Error: {sqlEx.Message}');</script>");
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                    return false;
                }
            }

            try
            {
             if (CheckMember())
            {
                Response.Write("<script>alert('user existe deja');</script>");
            }
            else
            {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO member_master_tb1 (full_name, dob, contact_no, email, state, city, pincode, full_addresse, member_id, password, account_status) " +
                                       "VALUES (@FullName, @DOB, @ContactNo, @Email, @State, @City, @Pincode, @FullAddress, @MemberId, @Password, @AccountStatus)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@FullName", fullName);
                            cmd.Parameters.AddWithValue("@DOB", dob);
                            cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@State", state);
                            cmd.Parameters.AddWithValue("@City", city);
                            cmd.Parameters.AddWithValue("@Pincode", pincode);
                            cmd.Parameters.AddWithValue("@FullAddress", fullAddress);
                            cmd.Parameters.AddWithValue("@MemberId", memberId);
                            cmd.Parameters.AddWithValue("@Password", password);
                            cmd.Parameters.AddWithValue("@AccountStatus", accountStatus);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    // Display a success message or redirect to another page
                    Response.Write("<script>alert('Registration Successful');</script>");
            }
        }
               
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }
    }
}
