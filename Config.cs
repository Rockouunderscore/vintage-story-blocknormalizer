
namespace blocknormalizer;

public class Config
{
    public bool PatchPlantOffset { get; set; } = true;
    public float PatchedPlantReduceSelectionBoxSizeBy { get; set; } = 0.05f;
    public bool PatchCrateSize { get; set; } = true;
    public bool PatchHenboxRotation { get; set; } = true;
}