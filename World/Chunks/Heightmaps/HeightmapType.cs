namespace DivaMC.Protocol.World.Chunks.Heightmaps
{
    public enum HeightmapType : int
    {
        /// <summary>
        /// All blocks other than air, cave air and void air.
        /// To determine if a beacon beam is obstructed.
        /// </summary>
        WorldSurface = 1,
        /// <summary>
        /// "Solid" blocks, except bamboo saplings and cacti; fluids.
        /// To determine where to display rain and snow. 
        /// </summary>
        MotionBlocking = 4,
        /// <summary>
        /// Same as MOTION_BLOCKING, excluding leaf blocks. 
        /// </summary>
        MotionBlackingNoLeaves = 5,
    }
}