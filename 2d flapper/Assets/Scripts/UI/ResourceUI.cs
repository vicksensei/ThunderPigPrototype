using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [Header("Images")]
    [SerializeField]
    Image[] images;

    [Header("Values")]
    [SerializeField]
    IntValue maxCharges;
    [SerializeField]
    IntValue curCharges;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < curCharges.Value)
            {
                images[i].gameObject.SetActive(true);
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}
