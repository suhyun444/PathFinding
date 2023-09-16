using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int id;
    public int y,x;
    private SpriteRenderer spriteRenderer;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void BindSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void BindPosition(int y,int x)
    {
        this.y = y;
        this.x = x;
    }
}
