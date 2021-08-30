using UnityEngine;

[CreateAssetMenu(fileName = "New Int", menuName = "SOValues/Int")]
public class IntValue : ScriptableObject
{


    [SerializeField]
    int value;

    public int Value { get => value; set => this.value = value; }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
