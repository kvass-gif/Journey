namespace Journey.Entities
{
    public class FacilityPlace : Record
    {
        public int FacilityId { get; set; }
        public int PlaceId { get; set; }
        public Facility Facility { get; set; }
        public Place Place { get; set; }
    }
}
