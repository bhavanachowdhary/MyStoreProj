using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public string err = "";
        public string str = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                clientInfo.id = "" + dr.GetInt32(0);
                                clientInfo.name = dr.GetString(1);
                                clientInfo.email = dr.GetString(2);
                                clientInfo.phone = dr.GetString(3);
                                clientInfo.address = dr.GetString(4);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                err = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                err = "All the Fields are Required";
                return;
            }

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=mystore;Integrated Security=True;Pooling=False";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE clients " +
                                    "SET name=@name, email=@email, phone=@phone, address=@address " + 
                                    "WHERE id=@id";

                    using (SqlCommand cmd1 = new SqlCommand(sql, conn))
                    {
                        cmd1.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd1.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd1.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd1.Parameters.AddWithValue("@address", clientInfo.address);
                        cmd1.Parameters.AddWithValue("@id", clientInfo.id);
                        cmd1.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) 
            {
                err = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
