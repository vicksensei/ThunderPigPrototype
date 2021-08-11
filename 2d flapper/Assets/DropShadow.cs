using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector2 ShadowOffset= new Vector2(0.1f,-.5f);
    public Material ShadowMaterial;

    SpriteRenderer spriteRenderer;
    GameObject shadowGameobject;
    SpriteRenderer shadowSpriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");

        //create a new SpriteRenderer for Shadow gameobject
        shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();

        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;

        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;

        //update the sorting layer of the shadow to always lie behind the sprite
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;

        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;
        shadowSpriteRenderer.flipX = spriteRenderer.flipX;
        shadowSpriteRenderer.flipY = spriteRenderer.flipY;

        //update the position and rotation of the sprite's shadow with moving sprite
        shadowGameobject.transform.SetParent(spriteRenderer.transform);
        shadowGameobject.transform.localPosition =  (Vector3)ShadowOffset;
        shadowGameobject.transform.localRotation = transform.localRotation;

    }

    void LateUpdate()
    {
        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowGameobject.transform.localPosition = new Vector3(ShadowOffset.x, ShadowOffset.y + (transform.localPosition.y/100));

    }
}