using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraUpdater : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    SpriteRenderer AuraSpriteRenderer;
    [SerializeField]
    Animator aura;
    [SerializeField]
    BoolValue playerIsImmune;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AuraSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AuraOff();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        AuraSpriteRenderer.sprite = spriteRenderer.sprite;
    }

    public void AuraOn()
    {
        playerIsImmune.Value = true;
    }
    public void AuraOff()
    {
        playerIsImmune.Value = false;
        aura.Play("PlayerAuraOff");
    }

    public void getHit()
    {
        aura.Play("PlayerAuraOn");
    }
}
