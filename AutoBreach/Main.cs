using AutoBreach;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using Handler = Exiled.Events.Handlers;
//:)
namespace AutoBreach
{
    public class Main : Plugin<Config>
    {
        public override string Name => "AutoBreach";
        public override string Author => "Kolo";
        public override string Prefix => "AutoBreach";
        public override Version RequiredExiledVersion => new Version(9, 6, 0);
        public override Version Version => new Version(2, 0, 2);

        public static Main Instance { get; private set; }
        public static Dictionary<RoleTypeId, CassieMessage> CassieMessagesMap { get; private set; }

        private AutoBreachEventHandler _eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new AutoBreachEventHandler();
            CassieMessagesMap = Config.CassieMessages.ToDictionary(msg => msg.Role);
            RegisterEvent(); 
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnRegisterEvent();
            Instance = null;
            _eventHandlers = null;
            base.OnDisabled();
        }
        public void RegisterEvent()
        {
            Handler.Player.ActivatingGenerator += _eventHandlers.OnActivatingGenerator;
            Handler.Player.InteractingDoor += _eventHandlers.OnInteractingDoor;
            Handler.Scp079.RoomBlackout += _eventHandlers.RoomBlackout;
            Handler.Player.Spawned += _eventHandlers.OnSpawned;
            Handler.Server.RoundEnded += _eventHandlers.RoundEnd;

        }
        public void UnRegisterEvent()
        {
            Handler.Player.ActivatingGenerator -= _eventHandlers.OnActivatingGenerator;
            Handler.Player.InteractingDoor -= _eventHandlers.OnInteractingDoor;
            Handler.Player.Spawned -= _eventHandlers.OnSpawned;
            Handler.Server.RoundEnded -= _eventHandlers.RoundEnd;
            Handler.Scp079.RoomBlackout -= _eventHandlers.RoomBlackout;
            Handler.Server.RoundEnded-= _eventHandlers.RoundEnd;
        }
    }
}