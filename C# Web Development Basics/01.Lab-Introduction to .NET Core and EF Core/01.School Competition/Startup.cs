namespace _01.School_Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var scores = new Dictionary<string, int>();
            var subjects = new Dictionary<string, SortedSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var splitted = line.Split(' ');
                var studentName = splitted[0];
                var currentSubject = splitted[1];
                var score = int.Parse(splitted[2]);

                if (!scores.ContainsKey(studentName))
                {
                    scores[studentName] = 0;
                }

                if (!subjects.ContainsKey(studentName))
                {
                    subjects[studentName] = new SortedSet<string>();
                }

                scores[studentName] += score;
                subjects[studentName].Add(currentSubject);
            }

            var orderedStudents = scores
                .OrderByDescending(s => s.Value)
                .ThenBy(s => s.Key);

            foreach (var student in orderedStudents)
            {
                var studentSubjectsText = string.Join(", ", subjects[student.Key]);

                Console.WriteLine($"{student.Key}: {student.Value} [{studentSubjectsText}]");
            }
        }
    }
}
