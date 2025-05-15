using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace AutoBreach
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Cassie for their respective SCP. Format: SCP, Cassie, Duration")]
        public List<CassieMessage> CassieMessages { get; set; } = new()
        {
            new CassieMessage { Role = RoleTypeId.Scp096, Message = "SCP-096 Has broke the containment!", Duration = 10f },
            new CassieMessage { Role = RoleTypeId.Scp173, Message = "SCP-173 Has broke the containment!", Duration = 10f },
            new CassieMessage { Role = RoleTypeId.Scp049, Message = "SCP-049 Has broke the containment", Duration = 10f },
            new CassieMessage { Role = RoleTypeId.Scp079, Message = "SCP-079 has breached the facility!", Duration = 10f },
        };
    }
}
