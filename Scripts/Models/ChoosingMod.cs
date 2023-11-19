using App.Scripts.Models.Mods;

namespace App.Scripts.Models.Songs.Notes
{
    public static class ChoosingMod
    {
        public static IModOfVisual ModVisual { get; private set; } = new NormalMode();
        public static IModOfPlayability ModPlayability { get; private set; } = new PlayablityDefault();

        public static void SetModVisual(IModOfVisual visual)
        {
            ModVisual = visual;
        }
        
        public static void SetModPlayability(IModOfPlayability playability)
        {
            ModPlayability = playability;
        }
    }
}