using AdventureAssembly.Units.Heroes;
using TinyTools.Extensions;

namespace AdventureAssembly.Debug
{
    public class DebugKillRandomHero : DebugKeyPress
    {
        public override void DebugKeyPressed()
        {
            HeroManager.Instance.Units.Random().Die();
        }
    }
}