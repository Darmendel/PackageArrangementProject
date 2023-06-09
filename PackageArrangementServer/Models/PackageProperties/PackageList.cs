namespace PackageArrangementServer.Models
{
    public class PackageList
    {
        private List<Package> _packages;
        //private readonly List<Package> packages;
        //public string id { get; set; }

        public PackageList(List<Package> packages)
        {
            this._packages = packages.ConvertAll(p => new Package(
                p.Id, p.DeliveryId, p.Width, p.Height, p.Length, p.Order));
        }

        public PackageList()
        {
            _packages = new List<Package>();
        }

        public List<Package> Packages { get { return _packages; } } // need to fix - return a copy

        //public bool IsEmpty { get { return packages.Count == 0; } }

        public int Count { get { return _packages.Count; } }

        public void Add(Package package)
        {
            if (package == null || _packages.Contains(package)) return;
            _packages.Add(package);
        }

        public void Extend(PackageList packages)
        {
            foreach (Package package in packages.Packages)
            {
                _packages.Add(package);
            }
        }

        public void Edit(Package package, string width = null, string height = null,
            string Length = null)
        {
            if (package == null) return;

            if (_packages.Contains(package))
            {
                int index = _packages.IndexOf(package);

                if (width != null) _packages[index].Width = width;
                if (height != null) _packages[index].Height = height;
                if (Length != null) _packages[index].Length = Length;
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
