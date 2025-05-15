using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;
// :)
namespace AutoBreach
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Spawn locations with their respective chances. Format: Location: Chance")]
        public static Dictionary<RoleTypeId, (string message, float duration)> CassieMessages { get; set; } = new Dictionary<RoleTypeId, (string, float)>
        {       
            { RoleTypeId.Scp096, ("SCP-096 Has broke the containment!", 10f ) },
            { RoleTypeId.Scp173, ("SCP-173 Has broke the containment!", 10f ) },
            { RoleTypeId.Scp049, ("SCP-049 Has broke the containment", 10f ) },
            { RoleTypeId.Scp079, ("SCP-079 has breached the facility!", 10f ) },
        };
    };
}