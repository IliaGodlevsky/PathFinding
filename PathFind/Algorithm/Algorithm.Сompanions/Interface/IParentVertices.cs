using GraphLib.Interfaces;

namespace Algorithm.Сompanions.Interface
{
    public interface IParentVertices
    {
        void Add(IVertex child, IVertex parent);

        IVertex GetParent(IVertex child);

        void Clear();
    }
}
