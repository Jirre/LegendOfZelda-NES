using UnityEngine;
using Zelda.Systems;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _MovementSpeed;
        [SerializeField] private float _AttackDuration;

        [SerializeField] private Transform _ModelRoot;
        
        [SerializeField] private Transform _WeaponRoot;
        [SerializeField] private SpriteRenderer _WeaponRenderer;

        private GameManager _gameManager;
        private Rigidbody2D _rigidbody;

        private Vector2 _lerpStartPosition;
        private Vector2 _lerpEndPosition;
        private float _lerpDuration;

        public void Initialize(GameManager pManager)
        {
            _gameManager = pManager;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _WeaponRoot.gameObject.SetActive(false);
            
            InitStates();
            InitAnimation();
            SubscribeInput();
        }

        private void OnDestroy()
        {
            UnsubscribeInput();
        }

        private void Update()
        {
            UpdateState();
        }

        private void FixedUpdate()
        {
            UpdateAnimation();
        }
    }
}
