using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    private BlockData[] blockDatas;
    int curBlockType = -1;    
    private void Awake() 
    {
        GetBlockData();
        CreateBlockButton();
    }
    private void GetBlockData()
    {
        blockDatas = (BlockData[])Resources.LoadAll("Block",typeof(BlockData));
    }
    private void SelectBlock(int id)
    {
        curBlockType = id;
    }
    private void CreateBlockButton()
    {
        GameObject customButton = (GameObject)Resources.Load("CustomButton",typeof(GameObject));
        for(int i=0;i<blockDatas.Length;++i)
        {
            CustomButton cur = Instantiate(customButton,new Vector3(7.3f,3 * i,0),Quaternion.identity).GetComponent<CustomButton>();
            int id = i;
            cur.BindSprite(blockDatas[i].sprite);
            cur.BindEvent(()=>SelectBlock(id));
        }
    }

}
