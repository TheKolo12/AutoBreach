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
using System.Collections.Generic;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Scp079;

namespace AutoBreach
{
    public class AutoBreachEventHandler
    {
        public static readonly Dictionary<RoleTypeId ,int > SCPCount = new()
    {
        { RoleTypeId.Scp049, 0 },
        { RoleTypeId.Scp079, 0 },
        { RoleTypeId.Scp096, 0 },
        { RoleTypeId.Scp106, 0 },
        { RoleTypeId.Scp173, 0 },
        { RoleTypeId.Scp939, 0 },
        { RoleTypeId.Scp3114, 0 },
    };
        public static readonly Dictionary<RoleTypeId, int> BlackoutCount = new()
    {
        { RoleTypeId.Scp106, 0 },
        { RoleTypeId.Scp939, 0 },
    };
        public static readonly Dictionary<DoorType, int> DoorAlredyOpen = new()
    {
        { DoorType.Scp096, 0},
        { DoorType.Scp173NewGate, 0},
        { DoorType.Scp049Gate, 0},
        { DoorType.GR18Inner, 0},

    };

        public static AutoBreachEventHandler Instance { get; private set; }

        public AutoBreachEventHandler()
        {
            Instance = this;
        }

        public void OnSpawned (SpawnedEventArgs ev)
        {
            if (ev.Player == null)
                return;

            RoleTypeId roleType = ev.Player.Role;

            switch (roleType)
            {
                case RoleTypeId.Scp049:
                case RoleTypeId.Scp3114:
                case RoleTypeId.Scp079:
                case RoleTypeId.Scp096:
                case RoleTypeId.Scp106:
                case RoleTypeId.Scp173:
                    if (SCPCount.ContainsKey(roleType))
                    {
                        SCPCount[roleType]++;
                        Log.Debug($"[AutoBreach] {roleType} spawned. Total count: {SCPCount[roleType]}");
                    }
                    break;

                default:
                    break;
            }
        }

        // Breaching SCP : 096, 173, 049, 3114
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if ((DoorAlredyOpen.ContainsKey(ev.Door.Type) && DoorAlredyOpen[ev.Door.Type] > 0)) 
                return;

            DoorType doorType = ev.Door.Type;

            switch (doorType)
            {
                case DoorType.Scp096:
                    if ((SCPCount.TryGetValue(RoleTypeId.Scp096, out int count096) && count096 >= 1) ||
                        Player.List.Any(p => p.Role.Type == RoleTypeId.Scp096))
                    {
                        ev.Player.ShowHint("[AutoBreach] SCP-096 is already breached.");
                        Log.Debug("[AutoBreach] SCP-096 is already breached.");
                        ev.IsAllowed = false;
                        return;
                    }
                    break;

                case DoorType.Scp049Gate:
                    if ((SCPCount.TryGetValue(RoleTypeId.Scp049, out int count049) && count049 >= 1) ||
                        Player.List.Any(p => p.Role.Type == RoleTypeId.Scp049))
                    {
                        ev.Player.ShowHint("[AutoBreach] SCP-049 is already breached.");
                        Log.Debug("[AutoBreach] SCP-049 is already breached.");
                        ev.IsAllowed = false;
                        return;
                    }
                    break;

                case DoorType.Scp173NewGate:
                    if ((SCPCount.TryGetValue(RoleTypeId.Scp173, out int count173) && count173 >= 1) ||
                        Player.List.Any(p => p.Role.Type == RoleTypeId.Scp173))
                    {
                        ev.Player.ShowHint("[AutoBreach] SCP-173 is already breached.");
                        Log.Debug("[AutoBreach] SCP-173 is already breached.");
                        ev.IsAllowed = false;
                        return;
                    }
                    break;


            }
            if (ev.Door.Type == Main.Instance.Config.Scp3114DoorType)
            {
                if ((SCPCount.TryGetValue(RoleTypeId.Scp3114, out int count3114) && count3114 >= 1) ||
                    Player.List.Any(p => p.Role.Type == RoleTypeId.Scp3114))
                {
                    ev.Player.ShowHint("[AutoBreach] SCP-3114 is already breached.");
                    Log.Debug("[AutoBreach] SCP-3114 is already breached.");
                    ev.IsAllowed = false;
                    return;
                }
            }
            if (ev == null || ev.Player == null)
            return;


            var player = ev.Player;

            var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();
            var spectatorVip = Player.List.Where(p => p.Role == RoleTypeId.Overwatch).ToList();

            if (spectators.Count > 0 || spectatorVip.Count > 0)
            {
                Player randomSpectator = spectators.Count > 0 ? spectators[UnityEngine.Random.Range(0, spectators.Count)] : null;
                Player randomSpectatorVip = spectatorVip.Count > 0 ? spectatorVip[UnityEngine.Random.Range(0, spectatorVip.Count)] : null;

                if (spectators.Count == 0 && spectatorVip.Count == 0)
                    return;

                // 096
                if (ev.Door.Type == DoorType.Scp096)
                {
                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp096);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-096!");
                        }
                        else 
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp096);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-096!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp096);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-096!");
                    }

                    DoorAlredyOpen[doorType]++;
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp096, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);

                    ev.Door.IsOpen = true;
                }

                // 173
                else if (ev.Door.Type == DoorType.Scp173NewGate)
                {
                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp173);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-173!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp173);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-173!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp173);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-173!");
                    }

                    DoorAlredyOpen[doorType]++;
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp173, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);


                    ev.Door.IsOpen = true;
                }

                // 049
                else if (ev.Door.Type == DoorType.Scp049Gate)
                {
                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp049);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-049!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp049);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-049!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp049);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-049!");
                    }

                    DoorAlredyOpen[doorType]++;
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp049, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);


                    ev.Door.IsOpen = true;
                }

                // 3114
                else if (ev.Door.Type == Main.Instance.Config.Scp3114DoorType)
                {
                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp3114);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-3114!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp3114);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-3114!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp3114);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-3114!");
                    }
                    DoorAlredyOpen[doorType]++;
                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp3114, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);


                    ev.Door.IsOpen = true;
                }
            }
        }
        // Breaching SCP079
        public void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (ev == null || ev.Player == null)
                return;
            if ((!SCPCount.TryGetValue(RoleTypeId.Scp079, out int count079) && count079 >= 1) || !Player.List.Any(p => p.Role.Type == RoleTypeId.Scp173))
                return;

            bool anyGeneratorActivated = Generator.List.Any(gen => gen.IsReady);

            if (anyGeneratorActivated)
            {
                var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();
                var spectatorVip = Player.List.Where(p => p.Role == RoleTypeId.Overwatch).ToList();

                if (spectators.Count > 0 || spectatorVip.Count > 0)
                {
                    Player randomSpectator = spectators.Count > 0 ? spectators[UnityEngine.Random.Range(0, spectators.Count)] : null;
                    Player randomSpectatorVip = spectatorVip.Count > 0 ? spectatorVip[UnityEngine.Random.Range(0, spectatorVip.Count)] : null;

                    if (spectators.Count == 0 && spectatorVip.Count == 0)
                        return;

                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp079);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-079!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp079);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-079!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp079);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-079!");
                    }

                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp079, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);
                }
            }
        }
        // Breaching SCP: 106, 939
        public void RoomBlackout(RoomBlackoutEventArgs ev)
        {
            if (ev == null || ev.Player == null)
                return;

            var spectators = Player.List.Where(p => p.Role == RoleTypeId.Spectator).ToList();
            var spectatorVip = Player.List.Where(p => p.Role == RoleTypeId.Overwatch).ToList();

            if (spectators.Count == 0 && spectatorVip.Count == 0)
                return;

            // 106
            if (ev.Room.Type == RoomType.Hcz106)
            {
                if (!BlackoutCount.ContainsKey(RoleTypeId.Scp106))
                    BlackoutCount[RoleTypeId.Scp106] = 0;

                BlackoutCount[RoleTypeId.Scp106]++;
                Log.Debug($"{ev.Scp079.Name} Did {BlackoutCount[RoleTypeId.Scp106]} in the cell of 106");

                if (BlackoutCount.TryGetValue(RoleTypeId.Scp106, out int blackoutcount106) && blackoutcount106 >= 3)
                {
                    if (SCPCount.TryGetValue(RoleTypeId.Scp106, out int count106) && count106 >= 1 || Player.List.Any(p => p.Role.Type == RoleTypeId.Scp106))
                    {
                        ev.Player.ShowHint("[AutoBreach] SCP-106 is already breached.");
                        Log.Debug("[AutoBreach] SCP-106 is already breached.");
                        return;
                    }
                    Player randomSpectator = spectators.Count > 0 ? spectators[UnityEngine.Random.Range(0, spectators.Count)] : null;
                    Player randomSpectatorVip = spectatorVip.Count > 0 ? spectatorVip[UnityEngine.Random.Range(0, spectatorVip.Count)] : null;

                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp106);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-106!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp106);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-106!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp106);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-106!");
                    }


                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp106, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);
                }
            }
            // 939
            if (ev.Room.Type == RoomType.Hcz939)
            {
                if (!BlackoutCount.ContainsKey(RoleTypeId.Scp939))
                    BlackoutCount[RoleTypeId.Scp939] = 0;

                BlackoutCount[RoleTypeId.Scp939]++;
                Log.Debug($"{ev.Scp079.Name} Did {BlackoutCount[RoleTypeId.Scp939]} in the cell of 106");

                if (BlackoutCount.TryGetValue(RoleTypeId.Scp939, out int blackoutcount939) && blackoutcount939 >= 3)
                {
                    if (SCPCount.TryGetValue(RoleTypeId.Scp939, out int count939) && count939 >= 1 || Player.List.Any(p => p.Role.Type == RoleTypeId.Scp939))
                    {
                        ev.Player.ShowHint("[AutoBreach] SCP-939 is already breached.");
                        Log.Debug("[AutoBreach] SCP-939 is already breached.");
                        return;
                    }
                    Player randomSpectator = spectators.Count > 0 ? spectators[UnityEngine.Random.Range(0, spectators.Count)] : null;
                    Player randomSpectatorVip = spectatorVip.Count > 0 ? spectatorVip[UnityEngine.Random.Range(0, spectatorVip.Count)] : null;

                    if (Main.Instance.Config.OverwatchPriority)
                    {
                        if (spectatorVip.Count > 0)
                        {
                            randomSpectatorVip.Role.Set(RoleTypeId.Scp939);
                            Log.Debug($"[AutoBreach] {randomSpectatorVip.Nickname} has become SCP-939!");
                        }
                        else
                        {
                            randomSpectator.Role.Set(RoleTypeId.Scp939);
                            Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-939!");
                        }
                    }
                    else
                    {
                        randomSpectator.Role.Set(RoleTypeId.Scp939);
                        Log.Debug($"[AutoBreach] {randomSpectator.Nickname} has become SCP-939!");
                    }


                    if (Main.CassieMessagesMap.TryGetValue(RoleTypeId.Scp939, out var cassieMsg))
                        Cassie.MessageTranslated(cassieMsg.Content, cassieMsg.Message, false, true, true);
                }
            }
        }
        public void RoundEnd(RoundEndedEventArgs ev)
        {
            SCPCount.Clear();
            BlackoutCount.Clear();
            DoorAlredyOpen.Clear();
        }


    } 
}
