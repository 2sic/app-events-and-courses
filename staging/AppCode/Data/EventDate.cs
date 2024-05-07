using System.Collections.Generic;
using System.Linq;

namespace AppCode.Data
{
  public partial class EventDate
  {
    // Add your own properties and methods here

    public List<Registrations> Registrations => _registrations ??= Parents<Registrations>().ToList();
    private List<Registrations> _registrations;
  }
}