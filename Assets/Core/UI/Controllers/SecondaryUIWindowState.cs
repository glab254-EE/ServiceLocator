using System;

namespace Core.UI.States
{
    public class SecondaryUIWindowState : AUIWindowState
    {
        public SecondaryUIWindowState(WindowStateMachine parentMachine, IUIView targetView, IButtonConnectable targetButtonConnectable, Type next) : base(parentMachine, targetView, targetButtonConnectable, next)
        {
        }
    }
}
