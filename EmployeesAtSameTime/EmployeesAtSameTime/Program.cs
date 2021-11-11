using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace EmployeesAtSameTime
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            path = ConfigurationManager.AppSettings.Get("path");

            String line;
            try
            {
                StreamReader sr = new StreamReader(path);
                line = sr.ReadLine();

                List<Employee> listEmployees = new List<Employee>();
                int ide = 0;

                while (line != null)
                {
                    Employee employee = new Employee();                    
                    int equal = line.IndexOf('=', 0, line.Length);
                    string name = line.Substring(0, equal);
                    employee.id = ide;
                    employee.name = name;
                   
                    int commaTotal = Regex.Matches(line, ",").Count;

                    List<Schedule> listEmployeeHours = new List<Schedule>();

                    string shortLine = line.Substring((equal + 1), (line.Length- equal) -1);

                    for(int i=0; i<= commaTotal; i++)
                    {
                        Schedule employeeHour = new Schedule();
                        int findComma = 0;
                        if(i == commaTotal)
                        {
                            findComma = shortLine.Length;
                        } else
                        {
                            findComma = shortLine.IndexOf(',', 0);
                        }
                         
                        string hourSection = shortLine.Substring(0, findComma);

                        string day = hourSection.Substring(0, 2).Trim();
                        employeeHour.day = day;

                        TimeSpan startTime = new TimeSpan(Convert.ToInt32(hourSection.Substring(2, 2)), Convert.ToInt32(hourSection.Substring(5, 2)), 0);
                        employeeHour.startTime = startTime;

                        TimeSpan endTime = new TimeSpan(Convert.ToInt32(hourSection.Substring(8, 2)), Convert.ToInt32(hourSection.Substring(11, 2)), 0);
                        employeeHour.endTime = endTime;

                        listEmployeeHours.Add(employeeHour);
                        if (i != commaTotal)
                        {
                            shortLine = shortLine.Substring(findComma + 1, (shortLine.Length - findComma) - 1);
                        }
                    }

                    employee.timeDetails = (listEmployeeHours);
                    listEmployees.Add(employee);

                    ide++;

                    line = sr.ReadLine();
                }

                sr.Close();
                
                for (int i = 0; i < listEmployees.Count; i++)
                {
                    for (int j = i + 1; j < listEmployees.Count; j++)
                    {
                        int coincidences = 0;

                        int uno = listEmployees[i].id;
                        int dos = listEmployees[j].id;

                        string name1 = listEmployees[i].name;
                        string name2 = listEmployees[j].name;

                        foreach (var tim1 in listEmployees[i].timeDetails)
                        {
                            foreach (var tim2 in listEmployees[j].timeDetails)
                            {
                                if(tim1.day == tim2.day)
                                {
                                    TimeSpan t_start_1 = tim1.startTime;
                                    TimeSpan t_end_1 = tim1.endTime;

                                    TimeSpan t_start_2 = tim2.startTime;
                                    TimeSpan t_end_2 = tim2.endTime;

                                    if ((t_start_2 >= t_start_1 && t_start_2 <= t_end_1) ||
                                        (t_start_1 >= t_start_2 && t_start_1 <= t_end_2))
                                    {
                                        coincidences++;
                                    }
                                }
                            }                            
                        }

                        Console.WriteLine(name1 + "-" + name2 + ": " + coincidences);
                    }
                }
                
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadLine();
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
                Console.ReadLine();
            }
        }
    }
}
