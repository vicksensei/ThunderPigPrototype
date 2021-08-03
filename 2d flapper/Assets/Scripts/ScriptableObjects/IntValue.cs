using UnityEngine;

[CreateAssetMenu(fileName = "New Int", menuName = "SOValues/Int")]
public class IntValue : ScriptableObject
{

    [SerializeField]
    int value;

    public int Vlaue { get => value; set => this.value = value; }
}
