using UnityEngine;
using UnityEngine.Events;

namespace SOEvents
{
    /**/
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour
, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;
        [SerializeField] private UER unityEventResponse;

        public E GameEvent { get => gameEvent; set => gameEvent = value; }
        public UER UnityEventResponse { get => unityEventResponse; set => unityEventResponse = value; }

        private void OnEnable()
        {
            if (gameEvent == null) { return; }
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) { return; }
            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }

}