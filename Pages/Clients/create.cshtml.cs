using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class createModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string err = "";
        public string str = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                err = "All the Fields are Required";
                return;
            }


            try
            {
                string connectionString = "Data Source=.;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO clients (name, email, phone, address) VALUES " +
                                  "(@name ,@email, @phone, @address);";

                    using (SqlCommand cmd1 = new SqlCommand(sql, conn))
                    {
                        cmd1.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd1.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd1.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd1.Parameters.AddWithValue("@address", clientInfo.address);

                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return;
            }
            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";

            str = "Client Added successfully";
            Response.Redirect("/Clients/Index");

        }
    }
}
