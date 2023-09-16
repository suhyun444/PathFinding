using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler 
{
    public delegate void OnClickEvent();
    private OnClickEvent clickCallBack;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void BindSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void BindEvent(OnClickEvent clickEvent)
    {
        clickCallBack = clickEvent;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        clickCallBack.Invoke();
    }
}
