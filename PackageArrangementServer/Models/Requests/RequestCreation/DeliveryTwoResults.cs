namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class DeliveryTwoResults
    {
        public string Id { get; set; }
        public GeneralContainer Container { get; set; }
        public List<Package> FirstPackages { get; set; }
        public List<Package> SecondPackages { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //public string UserId { get; set; }


        public DeliveryTwoResults(string id, GeneralContainer container, List<Package> fPackages, List<Package> sPackages)
        {
            Id = id;
            Container = container;
            FirstPackages = fPackages;
            SecondPackages = sPackages;
            //UserId = userId;
        }
    }
}
