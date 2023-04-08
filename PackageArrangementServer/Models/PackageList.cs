namespace PackageArrangementServer.Models
{
    public class PackageList
    {
        private List<Package> _packages;
        private readonly List<Package> packages;
        //public string id { get; set; }

        public PackageList(List<Package> packages)
        {
            this._packages = packages.ConvertAll(p => new Package(
                p.Id, p.DeliveryId, p.Type, p.Amount, p.Width, p.Height, p.Depth, p.Weight, p.Cost, p.Address));
        }

        public PackageList()
        {
            for (int i = 0; i < 4; i++)
            {
                int one = 1 + i, two = 2 + i, three = 3 + i;
                this.Add(new Package(one.ToString(), i.ToString(), "Clothing", "2", "100", "50", "70", "5", "200", "Fifth avn."));
                this.Add(new Package(two.ToString(), i.ToString(), "Lamps", "1", "300", "150", "200", "100", "700", "Fifth avn."));
                this.Add(new Package(three.ToString(), i.ToString(), "Books", "3", "50", "50", "50", "400", "500", "Fifth avn."));
            }
        }

        public List<Package> Packages { get { return packages; } } // need to fix - return a copy

        //public bool IsEmpty { get { return packages.Count == 0; } }

        public int Count { get { return packages.Count; } }

        public void Add(Package package)
        {
            if (package == null || _packages.Contains(package)) return;
            _packages.Add(package);
        }

        public void Edit(Package package, string type = null, string amount = null,
            string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null)
        {
            if (package == null) return;

            if (_packages.Contains(package))
            {
                int index = _packages.IndexOf(package);

                if (type != null) _packages[index].Type = type;
                if (amount != null) _packages[index].Amount = amount;
                if (width != null) _packages[index].Width = width;
                if (height != null) _packages[index].Height = height;
                if (depth != null) _packages[index].Depth = depth;
                if (weight != null) _packages[index].Weight = weight;
                //if (isFragile != null) _packages[index].IsFragile = (bool) isFragile;
                if (cost != null) _packages[index].Cost = cost;
                if (address != null) _packages[index].Address = address;
            }
        }

        public void Remove(Package package)
        {
            if (package == null) return;
            if (_packages.Contains(package)) _packages.Remove(package);
        }

        public static implicit operator List<object>(PackageList v)
        {
            throw new NotImplementedException();
        }
    }
}
