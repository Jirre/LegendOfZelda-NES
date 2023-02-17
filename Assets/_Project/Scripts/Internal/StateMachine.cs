using System;
using System.Collections.Generic;

namespace Zelda.Internal
{
    public class StateMachine<T> 
        where T : Enum
    {
        public T CurrentState { get; private set; }
        private Dictionary<T, State<T>> _states;

        public void AddState(T pID, State<T>.UpdateEvent pUpdate)
        {
            _states ??= new Dictionary<T, State<T>>();
            _states.Add(pID, new State<T>(pUpdate));
        }

        public void Goto(T pID)
        {
            if (_states.ContainsKey(pID))
            {
                if (CurrentState.Equals(pID)) return;
                _states[CurrentState].IsFirstFrame = true;
                _states[CurrentState].ActiveTime = 0f;
                
                CurrentState = pID;
                _states[pID].IsFirstFrame = true;
                _states[pID].ActiveTime = 0f;
            }
        }
        
        public void Update(float pDeltaTime)
        {
            if (_states.ContainsKey(CurrentState))
                _states[CurrentState].Update(pDeltaTime);
        }
    }

    public class State<T>
        where T : Enum
    {
        public float ActiveTime { get; internal set; }
        public bool IsFirstFrame { get; internal set; }

        public delegate void UpdateEvent(State<T> pState);

        private readonly UpdateEvent _event;

        public State(UpdateEvent pEvent)
        {
            _event = pEvent;
            ActiveTime = 0f;
            IsFirstFrame = false;
        }
        
        public void Update(float pDeltaTime)
        {
            _event.Invoke(this);
            ActiveTime += pDeltaTime;
            IsFirstFrame = false;
        }
    }
}
