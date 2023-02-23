using System;
using System.Collections.Generic;
using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems
{
    public partial class GameManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int _SpawnRoom;
        [SerializeField] private Vector2 _SpawnPosition;
        [Space]
        
        [SerializeField] private PlayerController _PlayerPrefab;
        [SerializeField] private List<Room> _RoomPrefabs;
        
        private Grid _grid;
        private CameraController _camera;
        private PlayerController _player;

        private Dictionary<Vector2Int, Room> _rooms;

        private void Awake()
        {
            InitStates();
        }

        private void Update()
        {
            _states.Update(Time.deltaTime);
        }

        private void InitRooms()
        {
            _grid ??= FindObjectOfType<Grid>();
            if (_grid == null)
            {
                GameObject obj = new GameObject("Grid");
                _grid = obj.AddComponent<Grid>();
                _grid.cellLayout = GridLayout.CellLayout.Rectangle;
                _grid.cellSize = new Vector3(1, 1, 0);
                _grid.cellGap = Vector3.zero;
                _grid.cellSwizzle = GridLayout.CellSwizzle.XYZ;
            }
            
            _rooms = new Dictionary<Vector2Int, Room>();
            List<Room> rooms = new List<Room>(FindObjectsOfType<Room>());
            foreach (Room r in rooms)
            {
                if (_rooms.ContainsKey(r.Position))
                    Debug.LogError("Multiple Rooms with the same position are defined");
                _rooms.Add(r.Position, r);
            }
            
            foreach (Room r in _RoomPrefabs)
            {
                if (!_rooms.ContainsKey(r.Position))
                {
                    Instantiate(r.gameObject, _grid.transform);
                    _rooms.Add(r.Position, r);
                }
            }
        }

        private void InitPlayer()
        {
            Vector2 position = new Vector2(
                _SpawnRoom.x * Constants.ROOM_WIDTH + _SpawnPosition.x,
                _SpawnRoom.y * Constants.ROOM_HEIGHT + _SpawnPosition.y);

            _player = Instantiate(_PlayerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerController>();
        }

        private void InitCamera()
        {
            _camera ??= FindObjectOfType<CameraController>();
            _camera.Goto(_SpawnRoom, 0f, 0f);
        }
    }
}
