using System.Collections.Generic;
using UnityEngine;

namespace Zelda.World
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private List<Room> _RoomPrefabs;
        [SerializeField] private EWorldLayer _CurrentLayer;
        public Room CurrentRoom { get; private set; }
        
        private Grid _grid;
        private Dictionary<EWorldLayer, Dictionary<Vector2Int, Room>> _rooms;
        
        public void Initialize()
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
            
            _rooms = new Dictionary<EWorldLayer, Dictionary<Vector2Int, Room>>();
            List<Room> rooms = new List<Room>(FindObjectsOfType<Room>(true));
            foreach (Room r in rooms)
            {
                if (!_rooms.ContainsKey(r.Layer))
                    _rooms.Add(r.Layer, new Dictionary<Vector2Int, Room>());
                
                if (_rooms[r.Layer].ContainsKey(r.Position))
                {
                    Debug.LogError("Multiple Rooms with the same keys are defined");
                    continue;
                }
                _rooms[r.Layer].Add(r.Position, r);

                r.gameObject.SetActive(r.Layer == _CurrentLayer);
            }

            if (_RoomPrefabs.Count <= 0) return;
            foreach (Room r in _RoomPrefabs)
            {
                if (r == null) continue;
                
                if (!_rooms.ContainsKey(r.Layer))
                    _rooms.Add(r.Layer, new Dictionary<Vector2Int, Room>());
                
                if (!_rooms[r.Layer].ContainsKey(r.Position))
                    _rooms[r.Layer].Add(r.Position, r);
            }
        }
        
        public void ActivateLayer(EWorldLayer pLayer)
        {
            _CurrentLayer = pLayer;
            foreach (KeyValuePair<EWorldLayer,Dictionary<Vector2Int,Room>> layer in _rooms)
            {
                bool active = layer.Key == pLayer;
                foreach (KeyValuePair<Vector2Int, Room> room in layer.Value)
                {
                    room.Value.gameObject.SetActive(active);
                }
            }
        }
        
        public void UpdateCurrentRoom(Vector2Int pNewPosition)
        {
            if (_rooms.ContainsKey(_CurrentLayer) && _rooms[_CurrentLayer].ContainsKey(pNewPosition))
                CurrentRoom = _rooms[_CurrentLayer][pNewPosition];
        }
        
        public Room GetRoom(EWorldLayer pLayer, Vector2Int pPosition)
        {
            if (_rooms.ContainsKey(pLayer) && _rooms[pLayer].ContainsKey(pPosition))
                return _rooms[pLayer][pPosition];
            return null;
        }
        
        public Room GetRoom(EWorldLayer pLayer, int pX, int pY) =>
            GetRoom(pLayer, new Vector2Int(pX, pY));
    }
}
