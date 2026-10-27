using Microsoft.EntityFrameworkCore;
using StudentScoreManagement.Data;
using System.Diagnostics;

bool exit = false;
while (!exit)
{
    Console.Clear();
    Console.WriteLine("Welcome to the Student Score Management System!");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Remove Student");
    Console.WriteLine("3. Student List");
    Console.WriteLine("4. Add Student Score");
    Console.WriteLine("5. Update Student Score");
    Console.WriteLine("6. List Student Score");
    Console.WriteLine("7. Exit");
    Console.Write("Please select an option: ");
    var option = Console.ReadLine();
    switch (option)
    {
        case "1":
            Console.Write("Enter First Name: ");
            String firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            String lastName = Console.ReadLine();
            Console.Write("Enter Gender(0:Male, 1:Female): ");
            int genderInput = int.Parse(Console.ReadLine());
            Console.Write("Enter Address: ");
            String address = Console.ReadLine();

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = (Gender)genderInput,
                Address = address
            };

            var db = new StudentDbContext();
            db.Students.Add(student);
            db.SaveChanges();

            Console.WriteLine("Student Created.Press Enter to continue...");
            Console.ReadLine();
            db.Dispose();

            break;
        case "2":
            Console.Write("Enter Student ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int idRemove))
            {
                Console.WriteLine("Invalid ID. Press Enter to continue...");
                Console.ReadLine();
                break;
            }
            using (var dbCheckNotFound = new StudentDbContext())
            {
                if (!dbCheckNotFound.Students.Any(s => s.Id == idRemove))
                {
                    Console.WriteLine("Student not found. Press Enter to continue...");
                    Console.ReadLine();
                    break;
                }
            }

            using (var dbRemove = new StudentDbContext())
            {
                var studentRemove = dbRemove.Students.Find(idRemove);
                if (studentRemove != null)
                {
                    dbRemove.Students.Remove(studentRemove);
                    dbRemove.SaveChanges();
                    Console.WriteLine("Student removed successfully. Press Enter to continue...");
                }
                else
                {
                    Console.WriteLine("Student not found. ");
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            break;
        case "3":
            using (var dbList = new StudentDbContext())
            {
                var students = dbList.Students.ToList();
                Console.WriteLine("\nStudent List:");
                Console.WriteLine("  Id\t | \tName\t |    Gender\t|  Address");
                foreach (var s in students)
                {

                    Console.WriteLine($"  {s.Id}\t | {s.LastName} {s.FirstName}\t | " +
                                      $"   {s.Gender}\t| " +
                                      $" {s.Address}");
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            break;
        case "4":
            Console.Write("Enter Student ID to add score: ");
            if (!int.TryParse(Console.ReadLine(), out int idAddScore))
            {
                Console.WriteLine("Invalid ID. Press Enter to continue...");
                Console.ReadLine();
                break;
            }

            using (var dbCheckNotFound = new StudentDbContext())
            {
                if (!dbCheckNotFound.Students.Any(s => s.Id == idAddScore))
                {
                    Console.WriteLine("Student not found. Press Enter to continue...");
                    Console.ReadLine();
                    break;
                }
            }

            
            using (var dbCheckDouble = new StudentDbContext())
            {
                if (dbCheckDouble.Subjects.Any(sub => sub.StudentID == idAddScore))
                {
                    Console.WriteLine("Student score already added. Press Enter to continue...");
                    Console.ReadLine();
                    break;
                }
            }

            Console.WriteLine("\t=====Enter Score Of Subject=====");
                Console.Write("Database Score: ");
                int databaseScore = int.Parse(Console.ReadLine());
                if (databaseScore < 0 || databaseScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                Console.Write("English Score: ");
                int englishScore = int.Parse(Console.ReadLine());
                if (englishScore < 0 || englishScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                Console.Write("Data Structure Score: ");
                int datastureScore = int.Parse(Console.ReadLine());
                if (datastureScore < 0 || datastureScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                Console.Write("AI Score: ");
                int aiScore = int.Parse(Console.ReadLine());
                if (aiScore < 0 || aiScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                Console.Write("Appliction Score: ");
                int appScore = int.Parse(Console.ReadLine());
                if (appScore < 0 || appScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                Console.Write("Java Score: ");
                int javaScore = int.Parse(Console.ReadLine());
                if (javaScore < 0 || javaScore > 100)
                {
                    Console.WriteLine("Invalid score. Please enter a score between 0 and 100.");
                    Console.ReadLine();
                    break;
                }
                var subject = new Subject
                {
                    StudentID = idAddScore,
                    databaseScore = databaseScore,
                    englishScore = englishScore,
                    datastureScore = datastureScore,
                    aiScore = aiScore,
                    appScore = appScore,
                    javaScore = javaScore
                };
            
                using (var dbAddScore = new StudentDbContext())
                {
                    dbAddScore.Subjects.Add(subject);
                    dbAddScore.SaveChanges();
                }
            
            break;
        case "5":
            Console.WriteLine("\t=====Update Score Of Subject=====");
            Console.Write("Enter Subject ID to update score: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid ID. Press Enter to continue...");
                Console.ReadLine();
                break;
            }
            

            using (var dbUpdate = new StudentDbContext())
            {
                var subjectUpdate = dbUpdate.Subjects.FirstOrDefault(s => s.StudentID == studentId);

                if (!dbUpdate.Students.Any(s => s.Id == studentId))
                {
                    Console.WriteLine("Student Id not found. Press Enter to continue...");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Press Enter without typing a number to keep the current score.");

                Console.Write($"Database Score ({subjectUpdate.databaseScore}): ");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newDbScore))
                    subjectUpdate.databaseScore = newDbScore;

                Console.Write($"English Score ({subjectUpdate.englishScore}): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newEngScore))
                    subjectUpdate.englishScore = newEngScore;

                Console.Write($"Data Structure Score ({subjectUpdate.datastureScore}): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newDSScore))
                    subjectUpdate.datastureScore = newDSScore;

                Console.Write($"AI Score ({subjectUpdate.aiScore}): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newAIScore))
                    subjectUpdate.aiScore = newAIScore;

                Console.Write($"Application Score ({subjectUpdate.appScore}): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newAppScore))
                    subjectUpdate.appScore = newAppScore;

                Console.Write($"Java Score ({subjectUpdate.javaScore}): ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int newJavaScore))
                    subjectUpdate.javaScore = newJavaScore;

                var subjectScores = new Subject
                {
                    StudentID= studentId
                };

                dbUpdate.SaveChanges();
                Console.WriteLine("Scores updated successfully. Press Enter to continue...");
                Console.ReadLine();
            }
            break;
        case "6":
            using (var dbScore = new StudentDbContext())
            {
                var subjects = dbScore.Subjects
                    .Include(s => s.Student)
                    .ToList();
                Console.WriteLine("\t=====List Student Score=====");
                Console.WriteLine("  \tName\t | Database | English | Data Structure | AI | Appliction | Java | Average | Grade |");
                
                foreach (var c in subjects)
                {   
                    Console.WriteLine($"  {c.Student.LastName} {c.Student.FirstName}\t |    {c.databaseScore}    |   {c.englishScore}    |       {c.datastureScore}       | " +
                        $"{c.aiScore} |     {c.appScore}     |  {c.javaScore}  |  {c.AverageScore:F2}  |   {c.Grade}   |");
                }

            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            break;
        case "7":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            Console.ReadLine();
            break;
    }
}

