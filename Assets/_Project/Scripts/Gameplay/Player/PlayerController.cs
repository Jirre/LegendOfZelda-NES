using UnityEngine;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _MovementSpeed;
        
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
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
