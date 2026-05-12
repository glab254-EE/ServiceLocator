using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

namespace Core.UI.States
{
    public abstract class AUIWindowState : AState
    {
        private WindowStateMachine AStateMachine;
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
                AStateMachine.SwitchState(nextState);
            }
        }
        public AUIWindowState(WindowStateMachine parentMachine, IUIView targetView, IButtonConnectable targetButtonConnectable,System.Type next)
        {
            this.AStateMachine = parentMachine;
            this.targetView = targetView;
            this.targetButtonConnectable = targetButtonConnectable;
            nextState = next;
        }
    }
}
