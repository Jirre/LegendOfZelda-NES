using UnityEngine;
using Zelda.World;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class TeleporterBehaviour : MonoBehaviour
    {
        [SerializeField] private Vector2Int _Room;
        public Vector2Int Room => _Room;
        [SerializeField] private EWorldLayer _Layer;
        public EWorldLayer Layer => _Layer;
        [SerializeField] private Vector2 _Position;
        public Vector2 Position => _Position;
        [SerializeField] private Vector2 _WalkInOffset;
        public Vector2 WalkInOffset => _WalkInOffset;
    }
}
