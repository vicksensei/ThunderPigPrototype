using UnityEngine;
namespace SOEvents
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "GameEvents/Void")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
