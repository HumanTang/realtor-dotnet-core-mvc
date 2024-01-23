namespace realtor_dotnet_core_mvc.Models.Property
{
    public class PropertyLookUp
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Price { get; set; }
        public string? Address { get; set; }
        public string? Transport { get; set; }
        public string? ImagesUrl { get; set; }
        public string? ImagesFloorplan { get; set; }
        public int? YearBuilt { get; set; }
        public string? BuildArea { get; set; }
        public string? LandArea { get; set; }
        public string? Floor { get; set; }
        public int? LandTypeId { get; set; }
        public int? PropertyTypeId { get; set; }
        public string? Pid { get; set; }
        public int? Room { get; set; }
    }
}
