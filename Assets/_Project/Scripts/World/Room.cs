using UnityEngine;

namespace Zelda.World
{
    public class Room : MonoBehaviour
    {
        public const float WIDTH = 16f;
        public const float HEIGHT = 10.5f;
        
        [SerializeField] private Vector2Int _Position;
        [SerializeField] private EWorldLayer _Layer;
        [SerializeField] private bool _BorderTransition = true;
        public Vector2Int Position => _Position;
        public EWorldLayer Layer => _Layer;
        public bool BorderTransition => _BorderTransition;
        public Rect WorldRect { get; private set; }

        private void Awake()
        {
            WorldRect = new Rect(_Position - new Vector2(WIDTH, HEIGHT) * 0.5f,
                new Vector2(WIDTH, HEIGHT));

            transform.position = new Vector3(WIDTH * _Position.x, HEIGHT * _Position.y);
        }

        public static Vector2 GetRoomPosition(Vector2Int pPosition) =>
            new Vector2(WIDTH * pPosition.x, HEIGHT * pPosition.y);

        public static Vector2 GetRoomPosition(Vector2Int pPosition, Vector2 pOffset) =>
            new Vector2(WIDTH * pPosition.x, HEIGHT * pPosition.y) + pOffset;

        public static Vector2Int PositionToRoomIndex(Vector2 pPosition) =>
            Vector2Int.RoundToInt(pPosition / new Vector2(WIDTH, HEIGHT));
    }
}
