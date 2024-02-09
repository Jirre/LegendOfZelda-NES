using System.Collections.Generic;
using UnityEngine;
using Zelda.Gameplay;
using Zelda.Systems.Transitions;
using Zelda.UI;
using Zelda.World;

namespace Zelda.Systems
{
    public partial class GameManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int _SpawnRoom;
        [SerializeField] private Vector2 _SpawnPosition;
        [Space]
        
        [SerializeField] private PlayerController _PlayerPrefab;

        private UIManager _uiManager;
        private WorldManager _worldManager;

        private CameraController _camera;
        private PlayerController _player;

        private void Awake()
        {
            InitStates();
        }

        private void Update()
        {
            _states.Update(Time.deltaTime);
        }

        private void InitPlayer()
        {
            Vector2 position = new Vector2(
                _SpawnRoom.x * Room.WIDTH + _SpawnPosition.x,
                _SpawnRoom.y * Room.HEIGHT + _SpawnPosition.y);

            _player = Instantiate(_PlayerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerController>();
            _player.Initialize(this);
            
            _worldManager.UpdateCurrentRoom(Room.PositionToRoomIndex(position));
        }

        private void InitCamera()
        {
            _camera ??= FindObjectOfType<CameraController>();
            _camera.Goto(_SpawnRoom, 0f, 0f);
        }

        public void SetLayer(EWorldLayer pLayer)
        {
            _worldManager.ActivateLayer(pLayer);
        }
    }
}
