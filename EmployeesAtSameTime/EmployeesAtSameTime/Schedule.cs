using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAtSameTime
{
    class Schedule
    {
        public string day { get; set; }

        public TimeSpan startTime { get; set; }

        public TimeSpan endTime { get; set; }
    }
}
