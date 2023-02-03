using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemSheetAnswer.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        [ForeignKey("Lawyer")]
        public int? LawyerId { get; set; }
        public Lawyer? Lawyer { get; set; }
    }
}
