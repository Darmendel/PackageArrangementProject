namespace PackageArrangementServer.Models
{
    public class StaticData
    {
        private static UserList users = SetUsers();
        private static DeliveryList deliveries = SetDeliveries();
        private static PackageList packages = SetPackages();

        private static UserList SetUsers()
        {
            UserList userList = new UserList();
            userList.Add(new User { Id = "1", Name = "A", Password = "12345", Deliveries = new List<Delivery>() });
            userList.Add(new User { Id = "2", Name = "B", Password = "12345", Deliveries = new List<Delivery>() });
            userList.Add(new User { Id = "3", Name = "C", Password = "12345", Deliveries = new List<Delivery>() });
            return userList;
        }

        private static DeliveryList SetDeliveries()
        {
            DeliveryList deliveryList = new DeliveryList();
            DeliveryList d1 = new DeliveryList();
            DeliveryList d2 = new DeliveryList();
            DeliveryList d3 = new DeliveryList();

            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime t1 = tomorrow.AddDays(1);
            DateTime t2 = tomorrow.AddDays(2);
            DateTime t3 = tomorrow.AddDays(3);
            DateTime t4 = tomorrow.AddDays(4);

            d1.Add(new Delivery("1", "1", tomorrow, new List<Package>()));
            d1.Add(new Delivery("2", "1", t1, new List<Package>()));
            EditUser(users, "1", d1);

            d2.Add(new Delivery("3", "2", t4, new List<Package>()));
            EditUser(users, "2", d2);

            d3.Add(new Delivery("4", "2", t2, new List<Package>()));
            d3.Add(new Delivery("5", "3", t3, new List<Package>()));
            EditUser(users, "3", d3);

            deliveryList.Extend(d1);
            deliveryList.Extend(d2);
            deliveryList.Extend(d3);

            return deliveryList;
        }

        private static PackageList SetPackages()
        {
            PackageList packageList = new PackageList();

            for (int i = 1; i < 6; i++)
            {
                //int one = 0 + i, two = 1 + i, three = 2 + i;
                string deliveryId = i.ToString();
                PackageList p = new PackageList();

                p.Add(new Package(1.ToString(), deliveryId, "Clothing", "2", "100", "50", "70", "5", "200", "Fifth avn."));
                p.Add(new Package(2.ToString(), deliveryId, "Lamps", "1", "300", "150", "200", "100", "700", "Fifth avn."));
                p.Add(new Package(3.ToString(), deliveryId, "Books", "3", "50", "50", "50", "400", "500", "Fifth avn."));

                EditDelivery(deliveries, deliveryId, p);
                packageList.Extend(p);
            }

            return packageList;
        }

        private static void EditUser(UserList users, string id, DeliveryList deliveries)
        {
            User user = users.Users.Find(x => x.Id == id);
            List<Delivery> list = deliveries.Deliveries;
            users.Edit(user, deliveries: list);
        }

        private static void EditDelivery(DeliveryList deliveries, string id, PackageList packages)
        {
            Delivery delivery = deliveries.Deliveries.Find(x => id == x.Id);
            List<Package> list = packages.Packages;
            deliveries.Edit(delivery, packages: list);
        }

        public static UserList GetUsers()
        {
            return users;
        }

        public static DeliveryList GetDeliveries()
        {
            return deliveries;
        }

        public static PackageList GetPackages()
        {
            return packages;
        }
    }
}
