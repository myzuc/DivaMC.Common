using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data.Variants.EntitySounds
{
    public sealed record CatSoundAssets : INbtSerializable<CatSoundAssets>
    {
        public static readonly CatSoundAssets Stub = new()
        {
            Ambient = SoundEvent.None,
            StrayAmbient = SoundEvent.None,
            Eat = SoundEvent.None,
            FoodBeg = SoundEvent.None,
            Purr = SoundEvent.None,
            Purreow = SoundEvent.None,
            Hiss = SoundEvent.None,
            Hurt = SoundEvent.None,
            Death = SoundEvent.None
        };
        
        public required SoundEvent Ambient { get; set; }
        public required SoundEvent StrayAmbient { get; set; }
        
        public required SoundEvent Eat { get; set; }
        public required SoundEvent FoodBeg { get; set; }
        
        public required SoundEvent Purr { get; set; }
        public required SoundEvent Purreow { get; set; }
        public required SoundEvent Hiss { get; set; }
        public required SoundEvent Hurt { get; set; }
        public required SoundEvent Death { get; set; }

        public CatSoundAssets()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Ambient.ToNbt().Rename("ambient_sound").AddTo(nbt);
            StrayAmbient.ToNbt().Rename("stray_ambient_sound").AddTo(nbt);
            Eat.ToNbt().Rename("eat_sound").AddTo(nbt);
            FoodBeg.ToNbt().Rename("beg_for_food_sound").AddTo(nbt);
            Purr.ToNbt().Rename("purr_sound").AddTo(nbt);
            Purreow.ToNbt().Rename("purreow_sound").AddTo(nbt);
            Hiss.ToNbt().Rename("hiss_sound").AddTo(nbt);
            Hurt.ToNbt().Rename("hurt_sound").AddTo(nbt);
            Death.ToNbt().Rename("death_sound").AddTo(nbt);
            return nbt;
        }
        public static CatSoundAssets FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}