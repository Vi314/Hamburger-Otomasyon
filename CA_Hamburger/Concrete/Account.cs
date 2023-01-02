using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Hamburger.Concrete
{
    internal class Account
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public decimal Balance { get; set; }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length < 8)
                {
                    Console.WriteLine("Şifre sekiz karakterden kısa olamaz!");
                }
                else if (value.Any(c => char.IsAsciiLetter(c)) && value.Any(c => char.IsAsciiDigit(c)))
                {
                    _password = value;
                }
                else
                {
                    Console.WriteLine("Şifre en az bir harf veya bir sayı içermeli!");
                }
            }
        }

        public static void CreateAccount()
        {

            Account account = new Account();

            account.ID = Data.Data.accounts.Count() + 1;
            bool isNickUsed = false;
            string enteredNick;

            do
            {
                isNickUsed = false;
                Console.Write("Kullanıcı adı :");
                enteredNick = Console.ReadLine();

                foreach (Account acc in Data.Data.accounts)
                {
                    if (acc.Nickname == enteredNick)
                    {
                        Console.WriteLine("Bu isim zaten kullanılıyor ! Lütfen farklı bir isim yazın.");
                        isNickUsed = true;
                    }
                }
            }
            while (isNickUsed);

            account.Nickname = enteredNick;

            Console.Write("İsim :");
            account.FirstName = Console.ReadLine();

            Console.Write("Soyisim :");
            account.LastName = Console.ReadLine();

            bool isPasswordEligible = false;

            do
            {
                Console.Write("Şifre :");
                account.Password = Console.ReadLine();

                if (account.Password != null)
                {
                    isPasswordEligible = true;
                }
            }
            while(!isPasswordEligible);

            account.Balance = 0;

            Data.Data.accounts.Add(account);
            
        }

    }
}
