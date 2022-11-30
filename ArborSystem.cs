using Arbour.Common.WorldGeneration;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Arbour
{
    internal class ArborSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {       
            tasks.Insert(tasks.Count - 6, new PassLegacy("Arbor Islands", ArborGeneration.SpawnIslands));
        }
    }
}
