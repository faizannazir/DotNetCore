namespace CodeFIrstCRUD.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Student>? Students { get; set; }
        public Student? Student { get; set; }

    }
}
