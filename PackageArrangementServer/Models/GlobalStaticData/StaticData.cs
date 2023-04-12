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
            userList.Add(new User { Id = "1", Name = "A", Email = "u1@gmail.com", Password = "12345", Deliveries = new List<Delivery>() });
            //userList.Add(new User { Id = "2", Name = "B", Email = "u2@gmail.com", Password = "12345", Deliveries = new List<Delivery>() });
            //userList.Add(new User { Id = "3", Name = "C", Email = "u3@gmail.com", Password = "12345", Deliveries = new List<Delivery>() });
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

            IContainer small = new SmallContainer();
            IContainer medium = new MediumContainer();
            IContainer big = new BigContainer();

            Delivery del1 = new Delivery("1", "1", tomorrow, new List<Package>(), small);
            Delivery del2 = new Delivery("2", "1", t1, new List<Package>(), medium);

            d1.Add(del1);
            d1.Add(del2);

            d1.Edit(del1, cost: 2100.ToString());
            d1.Edit(del2, cost: 2250.ToString());
            EditUser(users, "1", d1);

            /*d2.Add(new Delivery("3", "2", t4, new List<Package>(), small));
            EditUser(users, "2", d2);

            d3.Add(new Delivery("4", "2", t2, new List<Package>(), big));
            d3.Add(new Delivery("5", "3", t3, new List<Package>(), medium));
            EditUser(users, "3", d3);*/

            deliveryList.Extend(d1);
            //deliveryList.Extend(d2);
            //deliveryList.Extend(d3);

            return deliveryList;
        }

        private static PackageList SetPackages()
        {
            PackageList packageList = new PackageList();

            for (int i = 1; i < 3; i++)
            {
                string id1 = i.ToString() + 1.ToString();
                string id2 = i.ToString() + 2.ToString();
                string id3 = i.ToString() + 3.ToString();
                string deliveryId = i.ToString();
                PackageList p = new PackageList();

                p.Add(new Package(id1, deliveryId, "Clothing", "2", "100", "50", "70", "5", "200", "Fifth avn."));
                p.Add(new Package(id2, deliveryId, "Lamps", "1", "300", "150", "200", "100", "700", "Fifth avn."));
                p.Add(new Package(id3, deliveryId, "Books", "3", "50", "50", "50", "400", "500", "Fifth avn."));

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
