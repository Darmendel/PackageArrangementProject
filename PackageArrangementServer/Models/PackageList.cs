namespace PackageArrangementServer.Models
{
    public class PackageList
    {
        private List<Package> _packages;
        public string id { get; set; }

        public PackageList(List<Package> packages)
        {
            this._packages = packages.ConvertAll(p => new Package(
                p.id, p.type, p.amount, p.width, p.height, p.depth, p.weight, p.isFragile, p.cost, p.address));
        }

        public List<Package> Packages { get { return _packages; } }

        public void Add(Package package)
        {
            if (package == null || _packages.Contains(package)) return;
            _packages.Add(package);
        }

        public void Edit(Package package, string? type = null, int? amount = null,
            int? width = null, int? height = null, int? depth = null, int? weight = null,
            bool? isFragile = null, int? cost = null, string? address = null)
        {
            if (package == null) return;
            if (_packages.Contains(package))
            {
                int index = _packages.IndexOf(package);
                if (index != -1)
                {
                    if (type != null) _packages[index].type = type;
                    if (amount != null) _packages[index].amount = (int) amount;
                    if (width != null) _packages[index].width = (int) width;
                    if (height != null) _packages[index].height = (int) height;
                    if (depth != null) _packages[index].depth = (int) depth;
                    if (weight != null) _packages[index].weight = (int) weight;
                    if (isFragile != null) _packages[index].isFragile = (bool) isFragile;
                    if (cost != null) _packages[index].cost = (int) cost;
                    if (address != null) _packages[index].address = address;
                }
            }
        }

        public void Remove(Package package)
        {
            if (package == null || !_packages.Contains(package)) return;
            _packages.Remove(package);
        }
    }
}
