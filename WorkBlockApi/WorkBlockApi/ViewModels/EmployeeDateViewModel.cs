using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBlockApi.ViewModels;

public class EmployeeDateViewModel
{
    public List<ulong>? Date { get; set; } 
    public List<List<string>>? Address { get; set; }
    public List<List<ulong>>? StartWork { get; set; }
    public List<List<ulong>>? EndWork { get; set; }
    public List<List<ulong>>? BreakStartTime { get; set; }
    public List<List<ulong>>? BreakEndTime { get; set; }
}
