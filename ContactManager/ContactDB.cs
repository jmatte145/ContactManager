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

        public static ContactDB CInstance
        {
            get
            {
                return instance;
            }
        }

        public void LoadDB()
        {
            SqlConnection con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = ContactManager;Trusted_Connection=True");
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
            SqlConnection con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = ContactManager;Trusted_Connection=True");
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
            SqlConnection con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = ContactManager;Trusted_Connection=True");
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

        public List<Contact> getList()
        {
            return contacts.ToList();
        }
        
    }
}
