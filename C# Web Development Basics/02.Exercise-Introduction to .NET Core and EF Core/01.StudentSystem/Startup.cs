namespace _01.StudentSystem
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemDbContext())
            {
                context.Database.Migrate();

                //SeedData(context);

                //PrintStudentsWithHomeworks(context);
                //PrintCoursesWithResources(context);
                //PrintCoursesWithMoreThanFiveResources(context);
                //PrintCoursesThatWereActiveOnDate(context, new DateTime(2018, 2, 1));
                //PrintStudentsWithCources(context);

                //PrintCoursesWithresourcesAndLicenses(context);
                //PrintStudentWithFullInfo(context);

            }
        }
        
        private static void SeedData(StudentSystemDbContext context)
        {
            var date = DateTime.Now;
            var rnd = new Random();

            //Students
            string[] studentsNames =
            {
                "Pesho",
                "Dimitrichka",
                "Ginka",
                "Hose",
                "Ivan",
                "Hostansa"
            };

            string[] phones =
            {
                "0889323245",
                "0889323243",
                "0889323565",
                "0889329645",
                "0789323245",
                "0824323245"
            };
            
            for (int i = 0; i < studentsNames.Length; i++)
            {
                var currentStudent = new Student()
                {
                    Name = studentsNames[i],
                    PhoneNumber = phones[i],
                    RegistrationDate = date.AddMonths(-1 * (i + 1)),
                    BirthDate = date.AddYears(-1 * (i + 1) * 5)
                };

                context.Students.Add(currentStudent);
            }

            context.SaveChanges();

            //Cources 
            var allStudentsIds = context.Students
                .Select(s => s.Id)
                .ToList();

            string[] coursesNames =
            {
                "MathForProgrammers",
                "LearnWithNakov",
                "LearnWhatYouDontKnow"
            };

            for (int i = 0; i < coursesNames.Length; i++)
            {
                var currentCourse  = new Course()
                {
                    Name = coursesNames[i],
                    StartDate = date.AddDays(-1 * (i + 10)),
                    EndDate = date.AddMonths(i + 3),
                    Price = (i + 1) * 100
                };

                for (int k = 0; k < allStudentsIds.Count; k++)
                {
                    currentCourse
                        .Students
                        .Add(new StudentCourse(){ StudentId = allStudentsIds[k]});
                }

                context.Courses.Add(currentCourse);
            }

            context.SaveChanges();

            //Resources

            string[] resourcesNames =
            {
                "C#Advanced-Lab",
                "C#Advanced-Exercise",
                "C#Basic-Lab",
                "DbBasic-Exercise",
                "DbAdvanced-Lab",
                "JsAdvanced-Exercise",
            };

            var url = "softuni.bg";
            var allCoursesIds = context.Courses
                .Select(c => c.Id)
                .ToList();

            for (int i = 0; i < resourcesNames.Length; i++)
            {
                var currentResource = new Resource()
                {
                    Name = resourcesNames[i],
                    ResourceType = (ResourceType)rnd.Next(0, 2),
                    Url = url,
                    CourseId = allCoursesIds[rnd.Next(0, allCoursesIds.Count - 1)]
                };

                context.Resources.Add(currentResource);
            }

            context.SaveChanges();

            //Homeworks
            string[] homeworkContents =
            {
                "here is all i know",
                "task ready",
                "give me A",
                "ready to play"
            };

            for (int i = 0; i < homeworkContents.Length; i++)
            {
                var currentHomework = new Homework()
                {
                    Content = homeworkContents[i],
                    CourseId = allCoursesIds[rnd.Next(0, allCoursesIds.Count - 1)],
                    StudentId = allStudentsIds[rnd.Next(0, allStudentsIds.Count - 1)],
                    SubmissionDate = date.AddDays(-1 * i),
                    Type = (ContentType)rnd.Next(0, 2)

                };

                context.Homeworks.Add(currentHomework);
            }

            context.SaveChanges();

            //Liccenses
            var allResourcesIds = context.Resources
                .Select(r => r.Id)
                .ToList();

            string[] licensesNames =
            {
                "Licence1",
                "Licence2",
                "Licence3",
                "Licence4",
                "Licence5",
                "Licence6"
            };


            foreach (var license in licensesNames)
            {
                var currentLicense = new License()
                {
                    Name = license,
                    ResourceId = allResourcesIds[rnd.Next(0, allResourcesIds.Count - 1)]
                };

                context.Licenses.Add(currentLicense);
            }

            context.SaveChanges();
        }

        private static void PrintStudentsWithHomeworks(StudentSystemDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks
                        .Select(h => new
                        {
                            h.Content,
                            h.Type
                        })
                })
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}");

                if (student.Homeworks.Count() == 0)
                {
                    Console.WriteLine("Homeworks: None");
                }
                else
                {
                    Console.WriteLine("Homeworks: ");

                    foreach (var homework in student.Homeworks)
                    {
                        Console.WriteLine($"---{homework.Content}-{homework.Type}");
                    }
                }

                Console.WriteLine("=========================");
            }
        }

        private static void PrintCoursesWithResources(StudentSystemDbContext context)
        {
            var courses = context.Courses
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    c.Resources,
                    c.StartDate,
                    c.EndDate
                })
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Description: {course.Description}");

                if (course.Resources.Count == 0)
                {
                    Console.WriteLine("Resources: None");
                }
                else
                {
                    Console.WriteLine("Reources:");

                    var count = 0;
                    foreach (var resource in course.Resources)
                    {
                        Console.WriteLine($"--Name: {resource.Name}");
                        Console.WriteLine($"--Type: {resource.ResourceType}");
                        Console.WriteLine($"--Url: {resource.Url}");

                        count++;
                        if (count != course.Resources.Count)
                        {
                            Console.WriteLine("+++++++++++++++++++");
                        }
                    }
                }

                Console.WriteLine("=========================");
            }
        }

        private static void PrintCoursesWithMoreThanFiveResources(StudentSystemDbContext context)
        {
            var courses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .Select(c => new
                {
                    c.Name,
                    ResourcesCount = c.Resources.Count,
                    c.StartDate
                })
                .OrderByDescending(c => c.ResourcesCount)
                .ThenByDescending(c => c.StartDate);

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Resources count: {course.ResourcesCount}");
                Console.WriteLine("===========================");
            }
        }

        private static void PrintCoursesThatWereActiveOnDate(StudentSystemDbContext context, DateTime date)
        {
            var courses = context.Courses
                .Where(c => c.StartDate <= date && c.EndDate >= date)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    StudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsEnrolled)
                .ThenByDescending(c => (c.EndDate - c.StartDate).TotalDays)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Start date: {course.StartDate}");
                Console.WriteLine($"Start date: {course.EndDate}");
                Console.WriteLine($"Duration: {course.EndDate.Subtract(course.StartDate)}");
                Console.WriteLine($"Students enrolled: {course.StudentsEnrolled}");
                Console.WriteLine("=======================");
            }
        }

        private static void PrintStudentsWithCources(StudentSystemDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(sc => sc.Course.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}");
                Console.WriteLine($"Number of courses: {student.NumberOfCourses}");
                Console.WriteLine($"Total price: {student.TotalPrice}");
                Console.WriteLine($"Average price per course: {student.TotalPrice / student.NumberOfCourses}");
                Console.WriteLine("=========================");
            }
        }

        private static void PrintCoursesWithresourcesAndLicenses(StudentSystemDbContext context)
        {
            var courses = context.Courses
                .Select(c => new
                {
                    c.Name,
                    Resourses = c.Resources
                        .Select(r => new
                        {
                            r.Name,
                            r.Licenses
                        })
                })
                .OrderByDescending(c => c.Resourses.Count())
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course: {course.Name}");
                if (!course.Resourses.Any())
                {
                    Console.WriteLine($"Resources: None");
                }
                else
                {
                    Console.WriteLine($"Resources: ");

                    foreach (var resourse in course.Resourses
                        .OrderByDescending(r => r.Licenses.Count())
                        .ThenBy(r => r.Name))
                    {
                        Console.WriteLine("+++++++++++++++++");
                        Console.WriteLine($"Resource name: {resourse.Name}");

                        if (!resourse.Licenses.Any())
                        {
                            Console.WriteLine("Licenses: None");
                        }
                        else
                        {
                            Console.WriteLine("Licenses:");

                            foreach (var license in resourse.Licenses)
                            {
                                Console.WriteLine($"--{license.Name}");
                            }
                        }
                    }
                }

                Console.WriteLine("==========================");
            }
        }

        private static void PrintStudentWithFullInfo(StudentSystemDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    Resources = s.Courses.Sum(sc => sc.Course
                    .Resources
                    .Count()),
                    Licenses = s.Courses.Sum(sc => sc.Course
                        .Resources
                        .Sum(r => r.Licenses.Count))
                })
                .OrderByDescending(s => s.Courses)
                .ThenByDescending(s => s.Resources)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}");
                Console.WriteLine($"Courses: {student.Courses}");
                Console.WriteLine($"Resources: {student.Resources}");
                Console.WriteLine($"Licenses: {student.Licenses}");
                Console.WriteLine("============================");
            }
        }
    }
}
