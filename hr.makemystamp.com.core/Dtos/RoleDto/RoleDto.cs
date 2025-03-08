using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.core.Dtos.RoleDto
{
    public record RoleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
