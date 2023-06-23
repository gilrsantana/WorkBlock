using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBlockApp.ViewModels.VMEmployee;

public class EmployeeAddViewModel
{
    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ulong TaxId { get; set; }

    public uint BegginingWorkDay { get; set; }
    

    public uint EndWorkDay { get; set; }

    public string EmployerAddress { get; set; } = null!;

    public byte State { get; set; }
}
