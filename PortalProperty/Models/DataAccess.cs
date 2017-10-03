using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PortalProperty.Models
{
    public class DataAccess
    {
        public long User(long id, string firstanme)
        {

            using (SqlConnection connect = new SqlConnection("DatabaseEntities1"))
            {
                using (SqlCommand comma = new SqlCommand())
                {
                    comma.Connection = connect;
                    try
                    {
                        comma.Connection.Open();
                        comma.CommandText = "INSERT INTO [dbo].[tblUsers](firstname,lastname,email,password) VALUES(@firstname,@lastname,@email,@password)";
                        //comma.Parameters.AddWithValue("@Firstname", firstname);
                        //comma.Parameters.AddWithValue("@lastname", lastname);
                        //comma.Parameters.AddWithValue("@email", email);
                        //comma.Parameters.AddWithValue("@password", password);
                        comma.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
            }
            return id;
        }

        internal long User(Users id)
        {
            throw new NotImplementedException();
        }

        internal int User(tblUser value)
        {
            throw new NotImplementedException();
        }
      

        public Users getUser(string email, string password)
        {
            Users use = null;
            string query = "SELECT email,password WHERE email=@email AND password=@password";
            SqlDataReader read;

            using (SqlConnection connect = new SqlConnection("DatabaseEntities1"))
            {
                using (SqlCommand comma = new SqlCommand(query, connect))
                    try
                    {
                        connect.Open();
                        comma.CommandText = "INSERT INTO [dbo].[tblUsers](email,password) VALUES(@email,@password)";
                        comma.Parameters.AddWithValue("@email", email);
                        comma.Parameters.AddWithValue("@password", password);
                        comma.ExecuteNonQuery();
                    }
                    catch
                    {
                        connect.Close();
                    }

            }

            return use;

        }

    }

    class LoginDet
    {
    }
}