using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block",menuName = "ScriptableObejct/Block")]
public class BlockData : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public GameObject prefab;
}
