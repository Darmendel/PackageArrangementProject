namespace PackageArrangementServer.Models
{
    public class StaticData
    {
        private UserList users;
        private DeliveryList deliveries;
        private PackageList packages;

        public StaticData()
        {
            users = SetUsers();
            deliveries = SetDeliveries();
            packages = SetPackages();
        }

        private UserList SetUsers()
        {
            UserList userList = new UserList();
            //List<string> ids = new List<string>() { "1", "2", "3", "4" };
            //List<string> names = new List<string>() { "A", "B", "C", "D" };
            userList.Add(new User { Id = "1", Name = "A", Password = "12345", Deliveries = new List<Delivery>() });
            userList.Add(new User { Id = "2", Name = "B", Password = "12345", Deliveries = new List<Delivery>() });
            userList.Add(new User { Id = "3", Name = "C", Password = "12345", Deliveries = new List<Delivery>() });
            userList.Add(new User { Id = "4", Name = "D", Password = "12345", Deliveries = new List<Delivery>() });
            return userList;
        }

        private DeliveryList SetDeliveries()
        {
            DeliveryList deliveryList = new DeliveryList();
            DeliveryList d1 = new DeliveryList();
            DeliveryList d2 = new DeliveryList();
            DeliveryList d3 = new DeliveryList();
            DeliveryList d4 = new DeliveryList();

            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime t1 = tomorrow.AddDays(1);
            DateTime t2 = tomorrow.AddDays(2);
            DateTime t3 = tomorrow.AddDays(3);
            DateTime t4 = tomorrow.AddDays(4);

            d1.Add(new Delivery("1", "1", tomorrow, new List<Package>()));
            d1.Add(new Delivery("2", "1", t1, new List<Package>()));
            EditUser("1", d1);

            d2.Add(new Delivery("3", "2", t4, new List<Package>()));
            d2.Add(new Delivery("4", "2", tomorrow, new List<Package>()));
            d2.Add(new Delivery("5", "2", t3, new List<Package>()));
            d2.Add(new Delivery("6", "2", t2, new List<Package>()));
            EditUser("2", d2);

            deliveryList.Add(new Delivery("7", "3", t2, new List<Package>()));
            EditUser("3", d3);

            deliveryList.Add(new Delivery("8", "4", tomorrow, new List<Package>()));
            deliveryList.Add(new Delivery("9", "4", t4, new List<Package>()));
            EditUser("4", d4);

            deliveryList.Extend(d1);
            deliveryList.Extend(d2);
            deliveryList.Extend(d3);
            deliveryList.Extend(d4);

            return deliveryList;
        }

        private PackageList SetPackages()
        {
            PackageList packageList = new PackageList();

            for (int i = 0; i < 4; i++)
            {
                int one = 1 + i, two = 2 + i, three = 3 + i;
                packageList.Add(new Package(one.ToString(), i.ToString(), "Clothing", "2", "100", "50", "70", "5", "200", "Fifth avn."));
                packageList.Add(new Package(two.ToString(), i.ToString(), "Lamps", "1", "300", "150", "200", "100", "700", "Fifth avn."));
                packageList.Add(new Package(three.ToString(), i.ToString(), "Books", "3", "50", "50", "50", "400", "500", "Fifth avn."));
            }

            return packageList;
        }

        private void EditUser(string id, DeliveryList deliveryList)
        {
            User user = users.Users.Find(x => x.Id == id);
            List<Delivery> list = deliveryList.Deliveries;
            users.Edit(user, Deliveries: list);
        }
    }
}
