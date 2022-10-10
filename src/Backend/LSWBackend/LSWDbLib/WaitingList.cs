using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib
{
    public class WaitingList
    {
        public int WaitingListId { get; set; }
        public DateTime WaitingSince { get; set; }
        public int StudentId { get; set; }
        public int OfferId { get; set; }

        public Student Student { get; set; }
        public Offer Offer { get; set; }
    }
}
