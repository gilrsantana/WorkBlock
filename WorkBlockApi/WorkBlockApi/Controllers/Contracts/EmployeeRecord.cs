using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace WorkBlockApi.Controllers.Contracts;

public class EmployeeRecord
{
    public DateTime StartWork { get; set; }

    public DateTime BreakStartTime { get; set; }

    public DateTime BreakEndTime { get; set; }

    public DateTime EndWork { get; set; }

}