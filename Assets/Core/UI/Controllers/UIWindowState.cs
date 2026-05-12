using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

namespace Core.UI.States
{
    public abstract class AUIWindowState : AState
    {
        private WindowStateMachine parentStateMachine;
        private IUIView targetView;
        private IButtonConnectable targetButtonConnectable;
        private System.Type nextState;
        public override void Enter()
        {
            targetView.ToggleEnabled(true);
            targetButtonConnectable.ConnectToButton(OnButtonPress);
        }
        public override void Exit()
        {
            targetView.ToggleEnabled(false);
            targetButtonConnectable.DissconnectFromButton(OnButtonPress);
        }
        public void OnButtonPress()
        {
            if (nextState != null)
            {
                parentStateMachine.SwitchState(nextState);
            }
        }
        public AUIWindowState(WindowStateMachine _parentMachine, IUIView _targetView, IButtonConnectable _targetButtonConnectable,System.Type next)
        {
            parentStateMachine = _parentMachine;
            targetView = _targetView;
            targetButtonConnectable = _targetButtonConnectable;
            nextState = next;
        }
    }
}
