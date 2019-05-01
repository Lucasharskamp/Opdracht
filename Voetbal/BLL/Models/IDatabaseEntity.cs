using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    public interface IDatabaseEntity
    {
        Guid Id { get; set; }
    }
}
