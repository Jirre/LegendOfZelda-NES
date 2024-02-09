using System.Collections;
using UnityEngine;
using Zelda.World;

namespace Zelda.Systems
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _TransitionBorderWidth;
        private Vector2Int _currentPosition;

        private bool _routineIsRunning;
        private Coroutine _routine;

        private Camera _camera;

        /// <summary>
        /// Navigate to a new Grid Position relative to the current Grid Position
        /// </summary>
        public void DeltaGoto(Vector2Int pDelta, float pDuration = 1f, float pPreWait = 0.2f)
        {
            Goto(_currentPosition + pDelta, pDuration, pPreWait);
        }
        
        /// <summary>
        /// Navigate to new Grid Position over the given time provided as duration
        /// </summary>
        public void Goto(Vector2Int pPosition, float pDuration = 1f, float pPreWait = 0.2f)
        {
            if (pPosition == _currentPosition)
                return;
            
            if (_routineIsRunning)
                StopCoroutine(_routine);

            if (pDuration > 0 || pPreWait > 0) 
                _routine = StartCoroutine(MovementEnumerator(pPosition, pDuration, pPreWait));
            else
            {
                _currentPosition = pPosition;
                transform.localPosition = (Vector3)GetRoomPosition(pPosition) + Vector3.back * 10f;
            }
        }

        private IEnumerator MovementEnumerator(Vector2Int pPosition, float pDuration = 1f, float pPreWait = 0.2f)
        {
            _routineIsRunning = true;
            
            float lerpProgress = 0;
            Vector2Int originPosition = _currentPosition;
            _currentPosition = pPosition;

            _routineIsRunning = false;

            yield return new WaitForSeconds(pPreWait);

            if (pDuration <= 0)
            {
                _currentPosition = pPosition;
                transform.localPosition = (Vector3)GetRoomPosition(pPosition) + Vector3.back * 10f;
                yield break;
            }
            
            while (true)
            {
                lerpProgress += Time.deltaTime;
                transform.localPosition = 
                    (Vector3)(GetRoomPosition(Vector2.Lerp(originPosition, _currentPosition, lerpProgress / pDuration))) +
                    Vector3.back * 10f;
                    
                if (lerpProgress >= pDuration)
                    yield break;

                yield return new WaitForEndOfFrame();
            }
        }

        public Vector3 WorldToViewportPoint(Vector3 pPosition)
        {
            _camera ??= GetComponent<Camera>();
            return _camera.WorldToViewportPoint(pPosition);
        }

        public bool IsInsideBounds(Vector2 pPosition)
        {
            Rect rect = new Rect((Vector2)transform.position - new Vector2(Room.WIDTH, Room.HEIGHT) * 0.5f + Vector2.one * _TransitionBorderWidth,
                new Vector2(Room.WIDTH, Room.HEIGHT) - Vector2.one * _TransitionBorderWidth * 2f);

            return rect.Contains(pPosition);
        }

        public Vector2Int GetBorderDelta(Vector2 pPosition)
        {
            Vector3 vp = WorldToViewportPoint(pPosition);
            switch (vp.x)
            {
                case > 0.95f:
                    return Vector2Int.right;
                case < 0.05f:
                    return Vector2Int.left;
            }
            return vp.y > 0.5 ? Vector2Int.up : Vector2Int.down;
        }

        private Vector2 GetRoomPosition(Vector2 pPosition)
        {
            return new Vector3(pPosition.x * Room.WIDTH, pPosition.y * Room.HEIGHT - 0.25f);
        }
    }
}
