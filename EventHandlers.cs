using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Cassie;
using PlayerRoles;
using System.Linq;
using static PlayerList;
using System;

namespace AutoBreach
{
    public class AutoBreachEventHandler
    {
        public static AutoBreachEventHandler Instance { get; private set; }

        public AutoBreachEventHandler()
        {
            Instance = this;
        }



    // Breaching SCP by Opening doors
    public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev == null || ev.Player == null)
                return;

            if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp096, out var msg))
            {
                Cassie.Message(msg.Message, false, false);
            }


            var player = ev.Player;

            var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();


            if (spectators.Count > 0)
            {
                var randomSpectator = spectators[UnityEngine.Random.Range(0, spectators.Count)];

                // Doors
                var scp096Door = Door.Get(Exiled.API.Enums.DoorType.Scp096);
                var scp173Door = Door.Get(Exiled.API.Enums.DoorType.Scp173NewGate);
                var scp049Door = Door.Get(Exiled.API.Enums.DoorType.Scp049Gate);

                if (ev.Door == scp096Door)
                {
                    randomSpectator.Role.Set(RoleTypeId.Scp096);
                    Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP096!");
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp096, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, false);
                }
                if (ev.Door == scp173Door)
                {
                    randomSpectator.Role.Set(RoleTypeId.Scp173);
                    Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP173!");
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp173, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, false);
                }
                if (ev.Door == scp049Door)
                {
                    randomSpectator.Role.Set(RoleTypeId.Scp049);
                    Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP049!");
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp049, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, false);
                }

            }
        }
        // Breaching SCP079
        public void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (ev == null || ev.Player == null)
                return;

            var player = ev.Player;

            var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();


            if (spectators.Count > 0)
            {
                var randomSpectator = spectators[UnityEngine.Random.Range(0, spectators.Count)];
                randomSpectator.Role.Set(RoleTypeId.Scp079);
                if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp079, out var cassieMsg))
                    Cassie.Message(cassieMsg.Message, false, false);
            }
        }   
    } 
}
