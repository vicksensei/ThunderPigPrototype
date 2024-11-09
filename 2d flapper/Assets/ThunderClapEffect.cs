using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderClapEffect : MonoBehaviour
{
    Image flash;

    private void Awake()
    {
        flash = GetComponent<Image>();
    }
    public void PlayEffect()
    {
        StartCoroutine("LightningAnim");
    }

    IEnumerator LightningAnim()
    {
        flash.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.01f);
        flash.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(.01f);
        flash.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.01f);
        flash.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(.01f);
        flash.color = new Color(1, 1, 1, 1);

        yield return CoroutineAnims.ILerp(1f, flashtime =>
        {
            float colorScaler = Mathf.Lerp(1, 0, flashtime);
            flash.color = new Color(1, 1, 1, colorScaler);
        });

        flash.color = new Color(1, 1, 1, 0);
        yield return null;
    }
}
