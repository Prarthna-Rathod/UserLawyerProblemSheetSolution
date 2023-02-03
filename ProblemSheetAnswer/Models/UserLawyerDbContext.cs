using Microsoft.EntityFrameworkCore;

namespace ProblemSheetAnswer.Models
{
    public class UserLawyerDbContext: DbContext
    {
        public UserLawyerDbContext(DbContextOptions<UserLawyerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }

        public DbSet<Questions> questions { get; set; }

        public DbSet<Feedback> feedbacks { get; set; }

        public DbSet<OldConversation> oldConversations { get; set; }
    }
}
