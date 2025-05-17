using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Cassie;
using Exiled.API.Interfaces;
using Exiled.API.Features.Roles;
using PlayerRoles;
using System.Linq;
using MEC;
using static PlayerList;
using System;
using Exiled.API.Enums;
using System.Runtime.InteropServices.WindowsRuntime;

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
            if (ev.Door.Type == DoorType.Scp096 && Player.List.Any(p => p.Role.Type == RoleTypeId.Scp096) ||
                ev.Door.Type == DoorType.Scp049Gate && Player.List.Any(p => p.Role.Type == RoleTypeId.Scp049) ||
                ev.Door.Type == DoorType.Scp173Gate && Player.List.Any(p => p.Role.Type == RoleTypeId.Scp173))
            {
                ev.Player.ShowHint("[AutoBreach] That SCP is already breached");
                return;
            }


            if (ev == null || ev.Player == null)
            return;


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
                        Cassie.Message(cassieMsg.Message, false, true);

                    ev.Door.ChangeLock(DoorLockType.Isolation);

                    if (ev.Door is IDamageableDoor damageableDoor && !damageableDoor.IsDestroyed)
                    {
                        damageableDoor.IsDestroyed = true;
                        Log.Debug($"[AutoBreach] Door: {ev.Door.Name} Destroyed .");
                    }


                }

                if (ev.Door == scp173Door)
                {
                    randomSpectator.Role.Set(RoleTypeId.Scp173);
                    Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP173!");
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp173, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, cassieMsg.Subtiles);

                    ev.Door.ChangeLock(DoorLockType.Isolation);

                    if (ev.Door is IDamageableDoor damageableDoor && !damageableDoor.IsDestroyed)
                    {
                        damageableDoor.IsDestroyed = true;
                        Log.Debug($"[AutoBreach] Door: {ev.Door.Name} Destroyed .");
                    }


                }
                if (ev.Door == scp049Door)
                {
                    randomSpectator.Role.Set(RoleTypeId.Scp049);
                    Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP049!");
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp049, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, cassieMsg.Subtiles);

                    ev.Door.ChangeLock(DoorLockType.Isolation);

                    if (ev.Door is IDamageableDoor damageableDoor && !damageableDoor.IsDestroyed)
                    {
                        damageableDoor.IsDestroyed = true;
                        Log.Debug($"[AutoBreach] Door: {ev.Door.Name} Destroyed .");
                    }

                }

            }
        }
        // Breaching SCP079
        public void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (ev == null || ev.Player == null)
                return;

            
            bool anyGeneratorActivated = Generator.List.Any(gen => gen.IsReady);

            if (anyGeneratorActivated)
            {
                var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();

                if (spectators.Count > 0)
                {
                    var randomSpectator = spectators[UnityEngine.Random.Range(0, spectators.Count)];
                    randomSpectator.Role.Set(RoleTypeId.Scp079);

                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp079, out var cassieMsg))
                        Cassie.Message(cassieMsg.Message, false, cassieMsg.Subtiles);
                }
            }
        }
    } 
}
