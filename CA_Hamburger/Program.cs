using CA_Hamburger.Concrete;
using CA_Hamburger.Data;
using System.ComponentModel.Design;

while (true)
{
    Console.WriteLine("Hamburger Otomasyona Hoşgeldiniz!");

    #region Account Authentication
    while (true)
    {
        Console.WriteLine("***********************************************");
        Console.Write("1-)Oturum aç\n2-)Hesap oluştur\nYapmak istediğiniz işlemi seçin :");
        int islemSecim;

        while (true)
        {
            islemSecim = int.Parse(Console.ReadLine());
            if (islemSecim > 2 || islemSecim < 1)
            { Console.WriteLine("Lütfen geçerli bir seçim yapın!"); }
            else
            {
                break;
            }
        }

        bool doesNickExist = false;
        int accountReference = 0;

        if (islemSecim == 1)
        {
            while (true)
            {
                Console.Write("Hesap nick :");
                string enteredNick = Console.ReadLine();

                for (int i = 0; i < Data.accounts.Count; i++)
                {
                    doesNickExist = Data.accounts[i].Nickname == enteredNick;

                    if (doesNickExist)
                    {
                        accountReference = i;
                        break;
                    }
                }

                if (doesNickExist)
                {
                    while (true)
                    {
                        Console.Write("Şifre :");
                        string enteredPassword = Console.ReadLine();

                        if (enteredPassword == Data.accounts[accountReference].Password)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Şifre yanlış!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Lütfen geçerli bir hesap adı giriniz.");
                    continue;
                }
                break;
            }
        }
        else if (islemSecim == 2)
        {
            Account.CreateAccount();
            continue;
        }
        #endregion

        //Account name and balance
        Console.WriteLine($"Merhaba {Data.accounts[accountReference].Nickname}! Bakiyeniz :{Data.accounts[accountReference].Balance}");

        #region Creating An Order
        while (true)
        {
            //Order instance oluşturma

            Order order = new Order();


            //Menüler listelenicek 

            for (int i = 0; i < Data.menus.Count(); i++)
            {
                Console.WriteLine($"{Data.menus[i].ID}-){Data.menus[i].Name}");
            }
            int menuLength = Data.menus.Count() + 2;
            Console.WriteLine($"{menuLength-1}-)Siparişler");
            Console.WriteLine($"{menuLength}-)Bakiye ekleme");

            int menuPicked;
            int amountOfMenus = 1;

            //Seçilen menü alınıcak 

            while (true)
            {
                try
                {
                    Console.Write("Menü Seçimi :");
                    menuPicked = int.Parse(Console.ReadLine());

                    if (menuPicked < menuLength - 1)
                    {
                        Console.Write("Adet :");
                        amountOfMenus = int.Parse(Console.ReadLine());
                    }

                    if (menuPicked <= 0 || menuPicked > menuLength)
                    { Console.WriteLine("Lütfen geçerli bir seçim yapın"); }
                    else if (amountOfMenus <= 0)
                    { Console.WriteLine("Lütfen geçerli bir seçim yapın"); }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            order.AmountOfMenus = amountOfMenus;

            //Alınan menü siparişe fiyatıyla kaydedilicek

            if (menuPicked < menuLength - 1)
            {
                order.Menu = Data.menus[menuPicked - 1].Name;
                order.OrderPrice += Data.menus[menuPicked - 1].Price * amountOfMenus;

                Data.orders.Add(order);
            }
            else if (menuPicked == menuLength - 1)
            {
                foreach (Order o in Data.orders)
                {
                    Console.WriteLine("***********************************************");

                    string extras = String.Join(',', o.OrderedExtras);
                    string format = $"Sipariş Oluşturma Tarihi :{o.OrderDate}\nSeçili Menü :{o.Menu} Adet :{o.AmountOfMenus}\nSipariş fiyatı :{o.OrderPrice}\nEkstralar :{extras}";
                    Console.WriteLine(format);
                }
                Console.WriteLine("***********************************************");
                continue;
            }
            else if (menuPicked == menuLength)
            {
                try
                {
                    while (true)
                    {
                        Console.Write("Hesabınıza eklemek istediğiniz tutarı girin :");
                        Data.accounts[accountReference].Balance += decimal.Parse(Console.ReadLine());
                        Console.WriteLine($"Güncel bakiye :{Data.accounts[accountReference].Balance}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                continue;
            }

            //Ekstralar yazılıcak 

            for (int i = 0; i < Data.extras.Count(); i++)
            {
                Console.WriteLine($"{Data.extras[i].ID}-){Data.extras[i].Name}");
            }

            //Seçilen ekstralar splitle arraye alınıcak ve siparişe atılcak 

            int[] extrasPickedINT = new int[20];

            while (true)
            {
                Console.Write("Ekstra seçimlerinizi yazın (1,2,2 şeklinde):");
                string[] extrasPickedSTR = Console.ReadLine().Split(',');

                try
                {
                    for (int i = 0; i < extrasPickedSTR.Length; i++)
                    {
                        extrasPickedINT[i] = int.Parse(extrasPickedSTR[i]);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //Seçilen ekstraların fiyatları döngüyle hesaplanıcak ve siparişe atılcak 

            for (int i = 0; i < Data.extras.Count(); i++)
            {
                for (int z = 0; z < extrasPickedINT.Length; z++)
                {
                    if (Data.extras[i].ID == extrasPickedINT[z])
                    {
                        order.OrderPrice += Data.extras[i].Price * amountOfMenus;
                        order.OrderedExtras.Add(Data.extras[i].Name);
                    }
                }
            }

            if (order.OrderPrice > Data.accounts[accountReference].Balance)
            {
                Data.orders.Remove(order);
                Console.WriteLine("Bu siparişi oluşturmak için yeterli bakiyeniz yok!");
                continue;
            }


            //Ödenecek tutar 

            Data.accounts[accountReference].Balance -= order.OrderPrice;
            Console.WriteLine($"Siparişiniz Oluşturuldu! Ödenecek tutar :{order.OrderPrice}");

        }
        #endregion

    }
}
