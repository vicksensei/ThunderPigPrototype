using UnityEngine;
using UnityEngine.UI;

public class ToggleHealthDisplay : MonoBehaviour
{
    public Slider slider;
    bool curactive;
    private void Awake()
    {
        curactive = slider.gameObject.activeSelf;
    }
    public void ToggleHealthBar()
    {
        slider.gameObject.SetActive(!curactive);
    }
}
