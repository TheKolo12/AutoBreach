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
        public override Version RequiredExiledVersion => new Version(9, 5, 2);
        public override Version Version => new Version(1, 0, 3);

        public static Main Instance { get; private set; }
        public static Dictionary<RoleTypeId, CassieMessage> CassieMessagesMap { get; private set; }

        private AutoBreachEventHandler _eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new AutoBreachEventHandler();
            CassieMessagesMap = Config.CassieMessages.ToDictionary(msg => msg.Role);
            RegisterEvent(); 
            Log.Info("Autobreach Enabled");
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnRegisterEvent();
            Instance = null;
            _eventHandlers = null;
            Log.Info("Autobreach Disabled.");
            base.OnDisabled();
        }
        public void RegisterEvent()
        {
            Handler.Player.ActivatingGenerator += _eventHandlers.OnActivatingGenerator;
            Handler.Player.InteractingDoor += _eventHandlers.OnInteractingDoor;
        }
        public void UnRegisterEvent()
        {
            Handler.Player.ActivatingGenerator -= _eventHandlers.OnActivatingGenerator;
            Handler.Player.InteractingDoor -= _eventHandlers.OnInteractingDoor;
        }
    }
}
