using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace blocknormalizer.Patches;

public class BlockPatch
{    
    
    public static void OnLoaded_Postfix(Block __instance, ICoreAPI api)
    {
        if (api.Side != EnumAppSide.Client) return;
        
        if (Global.Config.PatchPlantOffset && __instance is BlockPlant blockPlant)
        {
            float f = Global.Config.PatchedPlantReduceSelectionBoxSizeBy;
            Cuboidf b = blockPlant.SelectionBoxes[0];
            b.Set(
                b.X1 + f,
                b.Y1, 
                b.Z1 + f,
                b.X2 - f,
                b.Y2, 
                b.Z2 - f);
            __instance.RandomDrawOffset = 0;
        }

        if (Global.Config.PatchHenboxRotation && __instance is BlockHenbox blockHenbox)
        {
            __instance.RandomizeRotations = false;
        }

    }
    
}