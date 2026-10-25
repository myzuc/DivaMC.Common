using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Jukeboxes
{
    public sealed record JukeboxSong : INbtSerializable<JukeboxSong>
    {
        public static readonly Identifier RegistryName = "minecraft:jukebox_song";

        public static readonly JukeboxSong Stub = new()
        {
            Description = string.Empty,
            Sound = SoundEvent.None,
            Length = TimeSpan.FromSeconds(1),
            SignalStrength = 0
        };
        public static readonly Registry<JukeboxSong> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:11"] = Stub,
                ["minecraft:13"] = Stub,
                ["minecraft:5"] = Stub,
                ["minecraft:blocks"] = Stub,
                ["minecraft:bounce"] = Stub,
                ["minecraft:cat"] = Stub,
                ["minecraft:chirp"] = Stub,
                ["minecraft:creator"] = Stub,
                ["minecraft:creator_music_box"] = Stub,
                ["minecraft:far"] = Stub,
                ["minecraft:lava_chicken"] = Stub,
                ["minecraft:mall"] = Stub,
                ["minecraft:mellohi"] = Stub,
                ["minecraft:otherside"] = Stub,
                ["minecraft:pigstep"] = Stub,
                ["minecraft:precipice"] = Stub,
                ["minecraft:relic"] = Stub,
                ["minecraft:stal"] = Stub,
                ["minecraft:strad"] = Stub,
                ["minecraft:tears"] = Stub,
                ["minecraft:wait"] = Stub,
                ["minecraft:ward"] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required ChatComponent Description { get; set; }
        public required SoundEvent Sound { get; set; }
        public required TimeSpan Length { get; set; }
        public required int SignalStrength { get; set; }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Description.ToNbt().Rename("description").AddTo(nbt);
            Sound.ToNbt().Rename("sound_event").AddTo(nbt);
            NbtFloat.FromF32((float)Length.TotalSeconds).Rename("length_in_seconds").AddTo(nbt);
            NbtInt.FromS32(SignalStrength).Rename("comparator_output").AddTo(nbt);
            return nbt;
        }
        public static JukeboxSong FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}