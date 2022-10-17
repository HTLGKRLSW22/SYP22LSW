namespace Backend.Dtos
{
    public class OfferDto
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MinStudents { get; set; }
        public int MaxStudents { get; set; }
        public string MeetingPoint { get; set; }
        public decimal Costs { get; set; }
        public string Location { get; set; }
        public int? TeacherId { get; set; }
    }
}
