using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineAnims
{

    public static IEnumerator growShrink(Transform transform, float initialScale)
    {
        float growScale = initialScale; //1 + (saveFile.CurCombo / 50f);
        transform.localScale = new Vector3(growScale, growScale, growScale);
        yield return ILerp(.5f, growTime =>
        {
            float scaler = Mathf.Lerp(growScale, 1, growTime);
            transform.localScale = new Vector3(scaler, scaler, scaler);
        });
    }

    /// <summary>
    /// provides a util to easily control the timing of a lerp over a duration
    /// </summary>
    /// <param name="duration">How long our lerp will take</param>
    /// <param name="action">The action to perform per frame of the lerp, is given the progress t in [0,1]</param>
    public static IEnumerator ILerp(float duration, Action<float> action)
    {
        float time = 0;
        while (time < duration)
        {
            float delta = Time.deltaTime;
            float t = (time + delta > duration) ? 1 : (time / duration);
            action(t);
            time += delta;
            yield return null;
        }
        // handle the last frame
        action(1);
    }
}
