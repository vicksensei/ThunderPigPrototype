using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "SOValues/Float")]
public class FloatValue : ScriptableObject
{
    [SerializeField]
    float value;

    public float Value { get => value; set => this.value = value; }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
