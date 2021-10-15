using System;
using System.Collections.Generic;
using System.Text;

namespace Autentifikasi
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        { 
        
        }

        public User(string namaDepan, string namaBlkg, string usern, string pass)
        {
            this.FirstName = namaDepan;
            this.LastName = namaBlkg;
            this.UserName = usern;
            this.Password = pass;
        }

    }
}
