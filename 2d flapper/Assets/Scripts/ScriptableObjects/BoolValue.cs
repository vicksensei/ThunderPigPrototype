using UnityEngine;

[CreateAssetMenu(fileName = "New Bool", menuName = "SOValues/Bool")]
public class BoolValue : ScriptableObject
{
    [SerializeField]
    bool value;

    public bool Value { get => value; set => this.value = value; }
}
