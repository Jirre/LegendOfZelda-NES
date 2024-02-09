using System;
using System.Collections.Generic;
using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public class TransitionHandler
    {
        public event Action OnCompleteEvent;
        
        private Queue<ITransitionEvent> _events;
        private ITransitionEvent _currentEvent;
        private float _transitionDuration;

        private readonly GameManager _managerCache;
        private readonly PlayerController _playerCache;

        public bool IsRunning => _events.Count > 0 || _currentEvent != null;

        public TransitionHandler(GameManager pManager, PlayerController pPlayer)
        {
            _managerCache = pManager;
            _playerCache = pPlayer;

            _events = new Queue<ITransitionEvent>();
            _currentEvent = null;
        }
        
        public void Add(ITransitionEvent pEvent)
        {
            _events ??= new Queue<ITransitionEvent>();
            _events.Enqueue(pEvent);
        }

        public void Clear()
        {
            _events.Clear();
        }

        public void Update(float pDeltaTime)
        {
            if (!IsRunning) 
                return;

            _transitionDuration += pDeltaTime;

            if (_currentEvent != null && !_currentEvent.IsReady(_transitionDuration))
                return;

            if (_currentEvent is ITransitionCompleteEvent completeEvent)
                completeEvent.OnComplete(_managerCache, _playerCache);
            
            Pop();
        }

        private void Pop()
        {
            if (_events.Count <= 0)
            {
                _currentEvent = null;
                _transitionDuration = 0f;
                OnCompleteEvent?.Invoke();
                return;
            }

            _currentEvent = _events.Dequeue();
            _transitionDuration = 0f;
            _currentEvent.OnTrigger(_managerCache, _playerCache);
        }
    }
}
