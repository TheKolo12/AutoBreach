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

        [Description("Cassie for their respective SCP. Format: SCP, Content,Message ")]
        public List<CassieMessage> CassieMessages { get; set; } = new()
        {
            new CassieMessage { Role = RoleTypeId.Scp096, Content = "SCP-096 Has broke the containment!", Message = "SCP-096 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp173, Content = "SCP-173 Has broke the containment!", Message = "SCP-173 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp049, Content = "SCP-049 Has broke the containment", Message = "SCP-049 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp079, Content = "SCP-079 has breached the facility!", Message = "SCP-079 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp939, Content = "SCP-939 has breached the facility!", Message = "SCP-939 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp106, Content = "SCP-106 has breached the facility!", Message = "SCP-106 Has broke the containment!" },
            new CassieMessage { Role = RoleTypeId.Scp3114, Content = "SCP-3114 has breached the facility!", Message = "SCP-3114 Has broke the containment!" },

        };
        [Description("Configs")]
        public bool OverwatchPriority { get; set; } = false;
        public DoorType Scp3114DoorType { get; set; } = DoorType.GR18Inner;

    }
}
