using UnityEngine;

namespace Zelda.Systems
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2Int _Position;
        public Vector2Int Position => _Position;
        public Rect WorldRect { get; private set; }

        private void Awake()
        {
            WorldRect = new Rect(_Position - new Vector2(Constants.ROOM_WIDTH, Constants.ROOM_HEIGHT) * 0.5f,
                new Vector2(Constants.ROOM_WIDTH, Constants.ROOM_HEIGHT));

            transform.position = new Vector3(Constants.ROOM_WIDTH * _Position.x, Constants.ROOM_HEIGHT * _Position.y);
        }
    }
}
