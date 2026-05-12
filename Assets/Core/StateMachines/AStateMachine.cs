using System;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public abstract class AStateMachine<T> where T : AState
    {
        public Dictionary<Type, T> States = new Dictionary<Type, T>();
        public T Current { get; private set; }
        public virtual void SwitchState(Type Next)
        {
            Debug.Log(Next.ToString());
            if (States.ContainsKey(Next))
            {
                Current?.Exit();
                Current = States[Next];
                Current.Enter();
            }
        }
    }
}
