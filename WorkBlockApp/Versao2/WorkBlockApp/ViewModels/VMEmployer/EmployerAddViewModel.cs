using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkBlockApp.Models.ValueObjects;

namespace WorkBlockApp.ViewModels.VMEmployer;

public class EmployerAddViewModel
{
    public string Address { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ulong TaxId { get; set; }
    public AddressModel LegalAddress { get; set; } = null!;
}
