using Assets.Scripts.Interfaces;

namespace Abstracts
{
    public abstract class Rune : SpawnElement, IPointable
    {
        public virtual float GetPoints()
        {
            return 0;
        }
    }
}
