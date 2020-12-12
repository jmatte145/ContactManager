using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactManager
{
    public sealed class ContactDB
    {
        private ContactDB() { }

        static readonly ContactDB instance = new ContactDB();
        List<Contact> contacts = new List<Contact>();
        public string connectionString = @"data source=localhost\SQLEXPRESS;database = ContactManager;Trusted_Connection=True";

        public static ContactDB CInstance
        {
            get
            {
                return instance;
            }
        }

        public void LoadDB()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cm = new SqlCommand("select Id, FirstName, LastName, Phone, Email from Contact",con);
            con.Open();
            SqlDataReader sdr = cm.ExecuteReader();

            while (sdr.Read())
            {
                Contact c = new Contact(
                    (int)sdr["Id"],
                    (string)sdr["FirstName"],
                    (string)sdr["LastName"],
                    (string)sdr["Phone"],
                    (string)sdr["Email"]
                    );
                contacts.Add(c);
            }
            con.Close();
        }

        public void UpdateDB()
        {
            foreach(Contact c in contacts.ToList())
            {
                contacts.Remove(c);
            }

            LoadDB();

        }

        public void AddContact(string fn, string ln, string p, string e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "insert into Contact (FirstName, LastName, Phone, Email) values(@fn,@ln,@p,@e)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fn",fn);
            cmd.Parameters.AddWithValue("@ln", ln);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.Parameters.AddWithValue("@e", e);
            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
            }
            catch(SqlException exception)
            {
                MessageBox.Show("Error! " + exception.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteContact(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "delete from contact where Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
            }
            catch(SqlException exception)
            {
                MessageBox.Show("Error! " + exception.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void EditContact(int id, string fn, string ln, string p, string e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "update Contact set FirstName=@fn, LastName=@ln, Phone=@p, Email=@e where Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            using(SqlConnection sidecon = new SqlConnection(connectionString))
            {
                if (fn == "")
                {
                    string FirstName;
                    SqlCommand specialcmd = new SqlCommand("select FirstName from Contact where Id=@id", sidecon);
                    sidecon.Open();
                    FirstName = (string)specialcmd.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@fn", FirstName);
                    sidecon.Close();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fn", fn);
                }

                if (ln == "")
                {
                    string LastName;
                    SqlCommand specialcmd = new SqlCommand("select LastName from Contact where Id=@id", sidecon);
                    sidecon.Open();
                    LastName = (string)specialcmd.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@ln", LastName);
                    sidecon.Close();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ln", ln);
                }

                if (p == "")
                {
                    string Phone;
                    SqlCommand specialcmd = new SqlCommand("select Phone from Contact where Id=@id", sidecon);
                    sidecon.Open();
                    Phone = (string)specialcmd.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@ln", Phone);
                    sidecon.Close();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@p", p);
                }

                if (e == "")
                {
                    string Email;
                    SqlCommand specialcmd = new SqlCommand("select Email from Contact where Id=@id", sidecon);
                    sidecon.Open();
                    Email = (string)specialcmd.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@ln", Email);
                    sidecon.Close();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@e", e);
                }
            }
            
            
            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                MessageBox.Show("Error! " + exception.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public List<Contact> getList()
        {
            return contacts.ToList();
        }
        
    }
}
