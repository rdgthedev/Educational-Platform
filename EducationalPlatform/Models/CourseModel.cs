namespace EducationalPlatform.Models
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public Guid CourseId { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
