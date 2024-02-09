using UnityEngine;
using Zelda.UI.Visualizers;

namespace Zelda.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ScreenFadeVisualizer _ScreenScreenFade;
        public ScreenFadeVisualizer ScreenScreenFade => _ScreenScreenFade;
    }
}
