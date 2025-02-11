namespace blocknormalizer.Patches;

public class CratePatch
{
    public static bool rndScale_Prefix(ref float __result)
    {
        __result = 1f;
        return false;
    }
}