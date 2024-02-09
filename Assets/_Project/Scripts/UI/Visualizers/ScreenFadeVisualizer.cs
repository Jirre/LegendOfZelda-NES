using System.Collections;
using UnityEngine;

namespace Zelda.UI.Visualizers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenFadeVisualizer : AVisualizer<ScreenFadeContext>, IInitializeUI
    {
        private CanvasGroup _group;
        private Coroutine _routine;

        protected override void Awake()
        {
            base.Awake();
            _group = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            _group.alpha = 1;
        }
        
        public override void SetContext(ScreenFadeContext pContext)
        {
            if (_routine != null) 
                StopCoroutine(_routine);
            
            _routine = StartCoroutine(CountdownEnumerator(pContext.Duration));
            _group.alpha = 0;
        }

        private IEnumerator CountdownEnumerator(float pDuration)
        {
            yield return new WaitForSeconds(pDuration);
            _group.alpha = 1;
        }
    }

    public readonly struct ScreenFadeContext
    {
        public readonly float Duration;
        public ScreenFadeContext(float pDuration) => Duration = pDuration;
    }
}
