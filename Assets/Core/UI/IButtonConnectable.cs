using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public interface IButtonConnectable
    {
        void ConnectToButton(UnityAction action);
        void DissconnectFromButton(UnityAction action);
    }
}