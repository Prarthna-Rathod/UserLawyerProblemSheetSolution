using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemSheetAnswer.Models
{
    public class Questions
    {
        //Filled by User
        public int Id { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile MediaDetails { get; set; }
        public string MediaFile { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }


        //Fill by Lawyer
        public Boolean Assign { get; set; }
        public string? Answer { get; set; }
        public Boolean Picked { get; set; }

        [ForeignKey("Lawyer")]
        public int? LawyerId { get; set; }
        public Lawyer? Lawyer { get; set; }

        
        public int? AssignTo { get; set; }
        public Lawyer? AssignToLawyer { get; set; }

        public int? PickedBy { get; set; }
        public Lawyer? PickedByLawyer { get; set; }

        public Boolean? IsUserSatisfied { get; set; }
    }
}
