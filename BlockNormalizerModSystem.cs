using System.Reflection;
using blocknormalizer.Patches;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;
using Vintagestory.Common;
using Vintagestory.GameContent;

namespace blocknormalizer;

public class BlockNormalizerModSystem : ModSystem
{
    public const string ModID = "blocknormalizer";
    private const string ConfigFileName = ModID + "/config.json";
    
    private Harmony harmony;

    private void Init(ICoreAPI api)
    {
        Global.Api = api;
        try
        {
            Global.Config = Global.Api.LoadModConfig<Config>(ConfigFileName);
            Global.Api.StoreModConfig(Global.Config, ConfigFileName); // update the config file
        }
        finally
        {
            if (Global.Config == null)
            {
                Global.Config = new Config();
                Global.Api.StoreModConfig(Global.Config, ConfigFileName);
            }
        }
        
        // Harmony.DEBUG = true;
        if (!Harmony.HasAnyPatches(ModID))
        {
            harmony = new Harmony(ModID);
            if (Global.Config.PatchHenboxRotation || Global.Config.PatchPlantOffset)
            {
                MethodInfo original = typeof(Block).GetMethod(nameof(Block.OnLoaded));
                HarmonyMethod postfix = typeof(BlockPatch).GetMethod(nameof(BlockPatch.OnLoaded_Postfix));
                harmony.Patch(original, postfix: postfix);
            }
            if (Global.Config.PatchCrateSize)
            {
                MethodBase original = typeof(BlockEntityCrate).PropertyGetter("rndScale");
                HarmonyMethod prefix = typeof(CratePatch).GetMethod(nameof(CratePatch.rndScale_Prefix));
                harmony.Patch(original, prefix: prefix);
            }
        }
    }
    
    public override void StartClientSide(ICoreClientAPI api)
    {
        Mod.Logger.Notification($"{nameof(BlockNormalizerModSystem)}.{nameof(StartClientSide)} {ModID}");
        
        Init(api);
    }
    
    public override void Dispose()
    {
        Mod.Logger.Notification($"{nameof(BlockNormalizerModSystem)}.{nameof(Dispose)} {ModID}");
        harmony?.UnpatchAll(ModID);
    }
    
}