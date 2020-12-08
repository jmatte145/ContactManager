using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Contact(int id, string fn, string ln, string phone, string email)
        {
            this.Id = id;
            this.FirstName = fn;
            this.LastName = ln;
            this.Phone = phone;
            this.Email = email;
        }

        public Contact( string fn, string ln, string phone, string email)
        {
            this.FirstName = fn;
            this.LastName = ln;
            this.Phone = phone;
            this.Email = email;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Id);
            sb.Append(" ");
            sb.Append(this.FirstName);
            sb.Append(" ");
            sb.Append(this.LastName);
            sb.Append(" ");
            sb.Append(this.Phone);
            sb.Append(" ");
            sb.Append(this.Email);
            sb.Append(" ");

            return sb.ToString();
        }
    }
}
