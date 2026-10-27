namespace StudentScoreManagement.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();

    }
    public enum Gender
    {
        Male, Female
    }

}

