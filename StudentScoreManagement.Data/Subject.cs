using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Version2
namespace StudentScoreManagement.Data
{
    public class Subject
    {
        public int Id { get; set; }
        public int databaseScore { get; set; }
        public int englishScore { get; set; }
        public int datastureScore { get; set; }
        public int aiScore { get; set; }
        public int appScore { get; set; }
        public int javaScore { get; set; }
        public double AverageScore
        {
            get
            {
                return (databaseScore + englishScore + datastureScore + aiScore + appScore + javaScore) / 6.0;
            }
        }
        public char Grade => AverageScore switch
        {
            < 70 => 'F',
            <= 82 => 'C',
            <= 94 => 'B',
            <= 100 => 'A',
            _ => 'N'
        };
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
