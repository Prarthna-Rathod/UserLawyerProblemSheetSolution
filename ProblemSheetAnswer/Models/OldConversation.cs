using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemSheetAnswer.Models
{
    public class OldConversation
    {
        public int Id { get; set; }
        [ForeignKey("Question")]
        public int QuesId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int solvedBy { get; set; }

    }
}
