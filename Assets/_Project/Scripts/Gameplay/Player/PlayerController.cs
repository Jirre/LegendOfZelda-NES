using UnityEngine;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _MovementSpeed;
        [SerializeField] private float _AttackDuration;
        [SerializeField] private Transform _WeaponRoot;
        [SerializeField] private SpriteRenderer _WeaponRenderer;
        
        private Rigidbody2D _rigidbody;

        private Vector2 _transitionStartPosition;
        private Vector2 _transitionEndPosition;

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
