using AutoBreach;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using System;
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
        public override Version Version => new Version(1, 0, 0);

        public static Main Instance { get; private set; }
        private AutoBreachEventHandler _eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new AutoBreachEventHandler();
            Log.Info("Autobreach Enabled");
            base.OnEnabled();
            RegisterEvent();
        }
        public override void OnDisabled()
        {
            Instance = null;
            _eventHandlers = null;
            Log.Info("Autobreach Disabled.");
            base.OnDisabled();
            UnRegisterEvent();
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