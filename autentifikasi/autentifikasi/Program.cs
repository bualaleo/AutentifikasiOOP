using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Autentifikasi
{
    class Program : User
    {
        static void Main(string[] args)
        {
            int keluar = 0;

            List<User> ListUser = new List<User>();

            do
            {
                Console.WriteLine("————————————————————————————————————————");
                Console.WriteLine("Basic Autentifikasi");
                Console.WriteLine("————————————————————————————————————————");
                Console.WriteLine("1. Create user");
                Console.WriteLine("2. Show user");
                Console.WriteLine("3. Search User");
                Console.WriteLine("4. Update User");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Login User");
                Console.WriteLine("7. Exit");
                Console.WriteLine("————————————————————————————————————————");
                Console.WriteLine("\n");
                
                int c;
                Console.Write("Input = ");
                
                try
                {
                c = int.Parse(Console.ReadLine());

                    switch (c)
                    {
                        case 1:
                            Console.Clear();
                            CreateUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            ShowUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("————————————————————————————————————————");
                            Console.WriteLine("Menu Search User");
                            Console.WriteLine("————————————————————————————————————————");
                            Console.Write("Username : ");
                            string usern = Console.ReadLine();
                            ShowUser(ListUser, usern);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            UpdateUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            DeleteUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 6:
                            Console.Clear();
                            LoginUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 7:
                            keluar = 7;
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Input Kembali");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Input Kembali");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            while (keluar != 7);
        }

        public static void CreateUser(List<User> ListUser)
        {
            Console.WriteLine("————————————————————————————————————————");
            Console.WriteLine("Menu Create User");
            Console.WriteLine("————————————————————————————————————————");

            bool cekFirstLast = false;
            string namaDepan, namaBlkg;

            do
            {
                Console.Write("First Name = ");
                namaDepan = Console.ReadLine();

                Console.Write("Last Name = ");
                namaBlkg = Console.ReadLine();

                if (namaDepan == "" || namaBlkg == "")
                {
                    
                    Console.WriteLine("Input not valid");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.Clear();
                }
                else
                {
                    cekFirstLast = true;
                }
            } while (cekFirstLast == false);

            Random rnd = new Random();
            int rPertama = rnd.Next(1, 99);
            int rKedua = rnd.Next(100);
            string usern = namaDepan.Substring(0, 2) + namaBlkg.Substring(0, 2) + rPertama + rKedua;

            bool cekPass = false;
            string pass;
            do
            {
                z:
                Console.Write("Password = ");
                pass = Console.ReadLine();

                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");

                var isValidated = hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass);

                if (pass == "")
                {
                    Console.WriteLine();
                    Console.WriteLine("Input not valid");
                }
                else if (!isValidated)
                {
                    Console.WriteLine("Password harus mengandung huruf besar, angka dan simbol");
                    goto z;
                }
                else
                {
                    cekPass = true;
                }

            } while (cekPass == false);

            pass = BCrypt.Net.BCrypt.HashPassword(pass);

            ListUser.Add(new User(namaDepan, namaBlkg, usern, pass));

            Console.WriteLine("\n");
            Console.WriteLine("Terimakasih");
        }

        public static void ShowUser(List<User> ListUser)
        { 
            Console.WriteLine("————————————————————————————————————————");
            Console.WriteLine("Menu Show User");
            Console.WriteLine("————————————————————————————————————————");

            if (ListUser.Count == 0)
            {
                Console.WriteLine("Belum ada user terdaftar");
                return;
            }

            foreach (var p in ListUser)
            {
                Console.WriteLine($"Nama : {(p.FirstName)} {p.LastName}");
                Console.WriteLine($"username : {p.UserName}");
                Console.WriteLine($"Password :  {p.Password}");
                Console.WriteLine();
                Console.WriteLine("————————————————————————————————————————");
            }
        }

        public static void ShowUser(List<User> ListUser, string usern)
        {
            User c = ListUser.FirstOrDefault(x => x.UserName == usern);
            if (c == null)
            {
                Console.WriteLine("Username Tidak Ditemukan");
                return;
            }
            else
            {
                foreach (var p in ListUser)
                {
                    if (usern == p.UserName)
                    {
                        Console.WriteLine($"Nama : {(p.FirstName)} {p.LastName}");
                        Console.WriteLine($"username : {p.UserName}");
                        Console.WriteLine($"Password :  {p.Password}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        public static void UpdateUser(List<User> ListUser)
        {
            Console.WriteLine("————————————————————————————————————————");
            Console.WriteLine("Menu Update User");
            Console.WriteLine("————————————————————————————————————————");

            Console.Write("username : ");
            string a = Console.ReadLine();

            User c = ListUser.FirstOrDefault(x => x.UserName == a);
            if (c == null)
            {
                Console.WriteLine("Username Tidak Ditemukan");
                return;
            }
            else
            {
                foreach (var p in ListUser)
                {
                    if (a == p.UserName)
                    {
                        Console.WriteLine($"Nama : {(p.FirstName)} {p.LastName}");
                        Console.WriteLine($"username : {p.UserName}");
                        Console.WriteLine($"Password :  {p.Password}");

                        var hasNumber = new Regex(@"[0-9]+");
                        var hasUpperChar = new Regex(@"[A-Z]+");
                        var hasMinimum8Chars = new Regex(@".{8,}");

                        z:
                        Console.Write("Input ganti password = ");
                        string passbaru = Console.ReadLine();

                        var isValidated = hasNumber.IsMatch(passbaru) && hasUpperChar.IsMatch(passbaru) && hasMinimum8Chars.IsMatch(passbaru);

                        if (isValidated)
                        {
                            p.Password = BCrypt.Net.BCrypt.HashPassword(passbaru);
                            Console.WriteLine($"Password baru = {p.Password}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Password harus mengandung huruf besar, angka dan simbol");
                            goto z;
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        public static void DeleteUser(List<User> ListUser)
        {
            Console.WriteLine("————————————————————————————————————————");
            Console.WriteLine("Menu Delete User");
            Console.WriteLine("————————————————————————————————————————");

            Console.Write("Username : ");
            string a = Console.ReadLine();

            User c = ListUser.FirstOrDefault(x => x.UserName == a);
            if (c == null)
            {
                Console.WriteLine("Username Tidak Ditemukan");
                return;
            }
            else
            {
                foreach (var p in ListUser)
                {
                    if (a == p.UserName)
                    {
                        ListUser.Remove(p);
                        Console.WriteLine("Data user sudah dihapus");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        public static void LoginUser(List<User> ListUser)
        {
            Console.Write("Username : ");
            string a = Console.ReadLine();

            Console.Write("Password : ");
            string b = Console.ReadLine();

            if (ListUser.Exists(item => item.UserName == a && BCrypt.Net.BCrypt.Verify(b, item.Password)))
            {
                Console.WriteLine("Login Berhasil!!");
                Console.ReadKey();
                Console.Clear();
            }
            else if (ListUser.Exists(item => item.UserName != a))
            {
                Console.WriteLine("username salah");
                Console.ReadKey();
            }
            else if (ListUser.Exists(item => item.UserName == a && BCrypt.Net.BCrypt.Verify(b, item.Password) == false))
            {
                Console.WriteLine("password salah");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Username Tidak DItemukan");
            }
            Console.WriteLine();
        }
    }
}