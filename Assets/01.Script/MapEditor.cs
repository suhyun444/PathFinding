using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private string mapId;
    [SerializeField] private BlockData[] blockDatas;
    private int curBlockType = -1;    
    private MapData mapData;
    private void Awake() 
    {
        mapData = new MapData(mapId);
        GetBlockData();
        CreateBlockButton();
    }
    private void GetBlockData()
    {
        blockDatas = Resources.LoadAll("Block").Cast<BlockData>().ToArray();
    }
    private void SelectBlock(int id)
    {
        curBlockType = id;
    }
    private void CreateBlockButton()
    {
        GameObject customButton = Resources.Load("CustomButton") as GameObject;
        for(int i=0;i<blockDatas.Length;++i)
        {
            CustomButton cur = Instantiate(customButton,new Vector3(7.3f,1.5f * i,0),Quaternion.identity).GetComponent<CustomButton>();
            int id = i;
            cur.BindSprite(blockDatas[i].sprite);
            cur.BindEvent(()=>SelectBlock(id));
        }
    }

}
