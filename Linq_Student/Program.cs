using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Student
{
    class Program
    {

        static void Main(string[] args)
        {
            IEnumerable<Student> studentQuery =
            from student in Student.students
            where student.Scores[0] > 90 && student.Scores[3] < 80
            orderby student.Scores[0] descending
            select student;

            var studentQuery2 =
            from student in Student.students
            group student by student.Last[0];

            var studentQuery3 =
            from student in Student.students
            group student by student.Last[0];

            var studentQuery4 =
            from student in Student.students
            group student by student.Last[0] into studentGroup
            orderby studentGroup.Key
            select studentGroup;

            var studentQuery5 =
            from student in Student.students
            let totalScore = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            where totalScore / 4 < student.Scores[0]
            select student.Last + " " + student.First;

            var studentQuery6 =
            from student in Student.students
            let totalScore = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            select totalScore;

            double averageScore = studentQuery6.Average();
            Console.WriteLine("Class average score = {0}", averageScore);

            IEnumerable<string> studentQuery7 =
                from student in Student.students
                where student.Last == "Garcia"
                select student.First;

            var studentQuery8 =
            from student in Student.students
            let x = student.Scores[0] + student.Scores[1] +
            student.Scores[2] + student.Scores[3]
            where x > averageScore
            select new { id = student.ID, score = x };

            foreach (var item in studentQuery8)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score);
            }


            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }


            foreach (string s in studentQuery5)
            {
                Console.WriteLine(s);
            }


            foreach (var groupOfStudents in studentQuery4)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine("   {0}, {1}",
                        student.Last, student.First);
                }
            }


            foreach (var groupOfStudents in studentQuery3)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine("   {0}, {1}",
                        student.Last, student.First);
                }
            }


            foreach (var studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}",
                              student.Last, student.First);
                }
            }


            

        }
    }

}

