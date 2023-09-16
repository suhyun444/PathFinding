using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private Transform mapParent;
    [SerializeField] private string mapId;
    [SerializeField] private BlockData[] blockDatas;
    GameObject blockPrefab;
    Block[,] map;
    private int curBlockType = 0;    
    private MapData mapData;
    private void Awake() 
    {
        GetBlockData();
        mapData = new MapData(mapId);
        blockPrefab = Resources.Load("BlockPrefab") as GameObject;
        InstantiateMap();
        CreateBlockButton();
    }
    private void Update() {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("AS");
            RaycastHit2D hit2D = Physics2D.Raycast(mousePosition,new Vector2(0,0)); 
            if(hit2D.collider != null)
            {
                if(hit2D.collider.gameObject.CompareTag("Block"))
                {
                    Block b = hit2D.collider.GetComponent<Block>();
                    UpdateBlock(b);
                }
            }
        }
    }
    private void UpdateBlock(Block block)
    {
        if(mapData.data[block.y,block.x] == curBlockType)return;
        block.BindSprite(blockDatas[curBlockType].sprite);
        mapData.data[block.y,block.x] = curBlockType;
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
            CustomButton cur = Instantiate(customButton,new Vector3(7.3f,3.0f - 1.5f * i,0),Quaternion.identity).GetComponent<CustomButton>();
            int id = i;
            cur.BindSprite(blockDatas[i].sprite);
            cur.BindEvent(()=>SelectBlock(id));
        }
    }
    private void InstantiateMap()
    {
        map = new Block[mapData.n,mapData.m];
        for(int i=0;i<mapData.n;++i)
        {
            for(int j=0;j<mapData.m;++j)
            {
                map[i,j] = Instantiate(blockPrefab,new Vector3(-7f + i * 1,4f - j * 1,0),Quaternion.identity).GetComponent<Block>();
                map[i,j].BindSprite(blockDatas[mapData.data[i,j]].sprite);
                map[i,j].BindPosition(i,j);
                map[i,j].transform.parent = mapParent;
            }
        }
    }

}
