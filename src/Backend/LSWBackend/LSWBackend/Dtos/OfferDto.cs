namespace Backend.Dtos
{
    public class OfferDto
    {
        public int OfferId { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public int OfferDateId { get; set; }
		public int Size { get; set; }
		public List<int> TeacherIdsn { get; set; }
    }
}
