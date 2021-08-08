using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [Header("Visual")]
    [SerializeField]
    GameObject Image;

    [Header("Value Objects")]
    [SerializeField]
    IntValue Health;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        ClearHealth();
     
        for (int i = 0; i < Health.Vlaue; i++)
        {
            Instantiate(Image, transform);
        }
    }

    public void ClearHealth()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnHealthChange()
    {
        UpdateHealth();
    }

}
