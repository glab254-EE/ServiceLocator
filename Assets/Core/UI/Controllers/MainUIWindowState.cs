using System;

namespace Core.UI.States
{
    public class MainUIWindowState : AUIWindowState
    {
        public MainUIWindowState(WindowStateMachine parentMachine, IUIView targetView, IButtonConnectable targetButtonConnectable, Type next) : base(parentMachine, targetView, targetButtonConnectable, next)
        { 
        }
    }
}
