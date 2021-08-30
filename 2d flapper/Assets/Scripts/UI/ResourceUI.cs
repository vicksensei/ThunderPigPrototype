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
    ProgressionObject saveFile;
    public enum ResourceType
    {
        Charge,
        HP
    }
    [SerializeField]
    ResourceType RT;

    int currentVal;
    
    // Start is called before the first frame update

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (RT == ResourceType.HP) {currentVal = saveFile.CurHP; }
        else { currentVal = saveFile.CurCharge; }
        for (int i = 0; i < images.Length; i++)
        {
            if (i < currentVal)
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
