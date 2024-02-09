using UnityEngine;
using Zelda.World;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class TeleporterBehaviour : MonoBehaviour
    {
        [Header("Target")] 
        [SerializeField] private bool _WalkTargetX = true;
        [SerializeField] private bool _WalkTargetY = true;
        public bool WalkTargetX => _WalkTargetX;
        public bool WalkTargetY => _WalkTargetY;
        
        [SerializeField] private EWorldLayer _Layer;
        public EWorldLayer Layer => _Layer;
        
        [SerializeField] private Vector2Int _Room;
        public Vector2Int Room => _Room;
        
        [SerializeField] private Vector2 _RoomPosition;
        public Vector2 RoomPosition => _RoomPosition;
        
        [SerializeField] private Vector2 _WalkInOffset;
        public Vector2 WalkInOffset => _WalkInOffset;

        [Header("Transition Order")]
        [SerializeField] private bool _Invert;
        public bool Invert => _Invert;
    }
}
