using UnityEngine.EventSystems;

namespace Zelda.UI.Visualizers
{
    public abstract class AVisualizer<TContext> : UIBehaviour
    {
        public abstract void SetContext(TContext pContext);
    }

    public interface IInitializeUI
    {
        void Initialize();
    }
}
