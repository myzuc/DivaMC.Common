using System.Collections.Concurrent;
using System.Reflection;

namespace DivaMC.Protocol.World.Tiles
{
    public abstract record Tile
    {
        private static readonly ConcurrentDictionary<int, Tile> IdToState = new();
        private static readonly ConcurrentDictionary<Tile, int> StateToId = new();
        
        static Tile()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(Tile)) && !t.IsAbstract);
            foreach (Type type in types)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }
        
        public abstract bool IsAir { get; }
        public abstract bool HasFluid { get; }
        protected internal Tile()
        {
            
        }

        public static int Map(Tile tile)
        {
            if (!StateToId.TryGetValue(tile, out int id)) throw new InvalidDataException();
            return id;
        }
        public static Tile Unmap(int id)
        {
            if (!IdToState.TryGetValue(id, out Tile? tile)) throw new InvalidDataException();
            return tile;
        }
        internal static void Register(Tile state, int id)
        {
            if (!IdToState.TryAdd(id, state)) throw new ArgumentException();
            if (!StateToId.TryAdd(state, id)) throw new ArgumentException();
        }
    }
}