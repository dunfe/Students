using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Students
{
    public abstract class Student
    {
        public const int NumOfTests = 3;

        public string Name { get; set; }

        protected readonly int[] Test;

        public string CourseGrade { get; protected set; }

        public abstract void ComputeCourseGrade();

        protected Student()
        {
            Test = new int[NumOfTests];
            CourseGrade = "****";
        }

        public int GetTestScore(int testNumber)
        {
            return Test[testNumber];
        }

        public void SetTestScore(int testNumber, int testScore)
        {
            Test[testNumber] = testScore;
        }
    }

    public class GraduateStudent : Student
    {
        public override void ComputeCourseGrade()
        {
            var total = Test.Sum();
            CourseGrade = total / NumOfTests >= 80 ? "Pass" : "No Pass";
        }
    }

    public class UndergraduateStudent : Student
    {
        public override void ComputeCourseGrade()
        {
            var total = Test.Sum();
            CourseGrade = total / NumOfTests >= 70 ? "Pass" : "No Pass";
        }
    }

    public class ComputeGrades
    {
        private const string UnderGrad = "U";
        private const string Grad = "G";
        private Student[] _roster;

        public void ProcessData(string file)
        {
            try
            {
                BuildRoster(file);
                ComputeGrade();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void BuildRoster(string filename)
        {
            var studentCount = 0;
            var sr = new StreamReader(filename);
            string input;
            while ((sr.ReadLine()) != null) studentCount++;
            sr.Close();

            sr = new StreamReader(filename);
            _roster = new Student[studentCount];
            studentCount = 0;
            while ((input = sr.ReadLine()) != null)
            {
                var student = CreateStudent(input);
                if (student != null) _roster[studentCount++] = student;
            }

            sr.Close();
        }

        private void ComputeGrade()
        {
            foreach (var st in _roster)
            {
                st.ComputeCourseGrade();
            }
        }

        private static Student CreateStudent(string line)
        {
            string[] tokens = line.Split(' ');
            if (tokens.Length < Student.NumOfTests + 3)
            {
                return null;
            }

            Student student;
            if (tokens[0].Equals(UnderGrad))
            {
                student = new UndergraduateStudent();
            }
            else if (tokens[0].Equals(Grad))
            {
                student = new GraduateStudent();
            }
            else
            {
                return null;
            }

            //set the student name
            student.Name = (tokens[1] + " " + tokens[2]);
            try
            {
                //set the student test scores
                for (var testNum = 0;
                    testNum < Student.NumOfTests;
                    testNum++)
                {
                    student.SetTestScore(testNum, Convert.ToInt32(tokens[testNum + 3]));
                }
            }
            catch (Exception)
            {
                student = null;
            }

            return student;
        }

        public void PrintResult()
        {
            foreach (Student st in _roster)
            {
                //print one student
                Console.Write(st.CourseGrade.Equals("U") ? "U" : "G");
                Console.Write("\t" + st.Name);
                for (var testNum = 0;
                    testNum < Student.NumOfTests;
                    testNum++)
                {
                    Console.Write("\t" + st.GetTestScore(testNum));
                }

                Console.WriteLine("\t" + st.CourseGrade);
            }
        }

        public void SortByName()
        {
            Array.Sort(_roster, new NameComparer());
        }
    }


    class NameComparer : IComparer
    {
        // Test the name of each object.
        int IComparer.Compare(object o1, object o2)
        {
            var t1 = (Student) o1;
            var t2 = (Student) o2;
            return string.CompareOrdinal(t1?.Name, t2?.Name);
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            var gradeComputer = new ComputeGrades();
            gradeComputer.ProcessData("Data.txt");
            Console.WriteLine("The Student's list:");
            gradeComputer.PrintResult();
            Console.WriteLine("\nThe Student's list sorted by the Name order:");
            gradeComputer.SortByName();
            gradeComputer.PrintResult();

            Console.ReadKey();
        }
    }
}