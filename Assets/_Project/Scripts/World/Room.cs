using UnityEngine;

namespace Zelda.World
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2Int _Position;
        [SerializeField] private EWorldLayer _Layer;
        public Vector2Int Position => _Position;
        public EWorldLayer Layer => _Layer;
        public Rect WorldRect { get; private set; }

        private void Awake()
        {
            WorldRect = new Rect(_Position - new Vector2(Constants.ROOM_WIDTH, Constants.ROOM_HEIGHT) * 0.5f,
                new Vector2(Constants.ROOM_WIDTH, Constants.ROOM_HEIGHT));

            transform.position = new Vector3(Constants.ROOM_WIDTH * _Position.x, Constants.ROOM_HEIGHT * _Position.y);
        }

        public static Vector2 GetRoomPosition(Vector2Int pPosition) =>
            new Vector2(Constants.ROOM_WIDTH * pPosition.x, Constants.ROOM_HEIGHT * pPosition.y);

        public static Vector2 GetRoomPosition(Vector2Int pPosition, Vector2 pOffset) =>
            new Vector2(Constants.ROOM_WIDTH * pPosition.x, Constants.ROOM_HEIGHT * pPosition.y) + pOffset;
    }
}
