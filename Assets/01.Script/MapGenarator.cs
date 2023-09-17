using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenarator : MonoBehaviour
{
    [SerializeField] private string mapId;
    [SerializeField] private BlockData[] blockDatas;
    [SerializeField] GameObject player;
    private MapData mapData;
    private void Awake() {
        mapData = new MapData(mapId);
        GetBlockData();
        CreateMap();
    }
    private void GetBlockData()
    {
        blockDatas = Resources.LoadAll("Block").Cast<BlockData>().ToArray();
    }
    private void CreateMap()
    {
        GameObject blockPrefab = Resources.Load("OriginBlock") as GameObject;
        float startX = ((float)mapData.m / 2) * -0.5f;
        float startY = ((float)mapData.n / 2) * 0.5f;
        player.transform.position = new Vector3(startX,startY,0);
        for(int i=0;i<mapData.n;++i)
        {
            for(int j=0;j<mapData.m;++j)
            {
                Block b = Instantiate(blockPrefab,new Vector3(startX + j * 0.5f,startY - i * 0.5f,0),Quaternion.identity).GetComponent<Block>();
                b.BindSprite(blockDatas[mapData.data[i,j]].sprite);
            }
        }
    }
}
