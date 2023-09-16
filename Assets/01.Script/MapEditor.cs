using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private Transform mapParent;
    [SerializeField] private string mapId;
    [SerializeField] private BlockData[] blockDatas;
    [SerializeField] CustomButton addRowButton;
    [SerializeField] CustomButton addColButton;
    [SerializeField] CustomButton saveButton;
    GameObject blockPrefab;
    Block[,] map;
    private int curBlockType = 0;    
    private MapData mapData;
    public float dragSensitivity;
    private Vector3 prevPosition;
    private bool onDrag = false;
    private void Awake() 
    {
        GetBlockData();
        mapData = new MapData(mapId);
        blockPrefab = Resources.Load("BlockPrefab") as GameObject;
        InstantiateMap();
        CreateBlockButton();
        addRowButton.BindEvent(AddRow);
        addColButton.BindEvent(AddCol);
        saveButton.BindEvent(()=>mapData.Save());
    }
    private void Update() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(mousePosition,new Vector2(0,0)); 
            for(int i=0;i<hit2D.Length;++i)
            {
                if(hit2D[i].collider != null && hit2D[i].collider.gameObject.CompareTag("Block"))
                {
                    Block b = hit2D[i].collider.GetComponent<Block>();
                    UpdateBlock(b);
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(2))
            {
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0));
                for (int i = 0; i < hit2D.Length; ++i)
                {
                    if (hit2D[i].collider != null && hit2D[i].collider.gameObject.CompareTag("Map"))
                    {
                        prevPosition = mousePosition;
                        onDrag = true;
                    }
                }
            }
            else if(onDrag && Input.GetMouseButton(2))
            {
                Vector3 movingVector = mousePosition - prevPosition;
                prevPosition = mousePosition;
                mapParent.position += movingVector * dragSensitivity;
                float maxX = Mathf.Max(0,mapData.m - 16) * 0.5f;
                float maxY = Mathf.Max(0,mapData.n - 16) * 0.5f;
                mapParent.localPosition = new Vector3(Mathf.Clamp(mapParent.localPosition.x,-maxX,0),Mathf.Clamp(mapParent.localPosition.y,0,maxY),0);
            }
            else if(Input.GetMouseButtonUp(2))
            {
                onDrag = false;
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
    private void InstantiateBlock(int y, int x)
    {
        map[y, x] = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity).GetComponent<Block>();
        map[y, x].BindSprite(blockDatas[mapData.data[y, x]].sprite);
        map[y, x].BindPosition(y, x);
        map[y, x].transform.parent = mapParent;
        map[y, x].transform.localPosition = new Vector3(-3.5f + x * 0.5f,3.5f - y * 0.5f);
    }
    private void InstantiateMap()
    {
        map = new Block[mapData.n,mapData.m];
        for(int i=0;i<mapData.n;++i)
        {
            for(int j=0;j<mapData.m;++j)
            {
                InstantiateBlock(i,j);
            }
        }
    }
    private void AddRow()
    {
        mapData.AddRow();
        map = new Block[mapData.n,mapData.m];
        for(int i=0;i<mapData.m;++i)
        {
            InstantiateBlock(mapData.n-1,i);
        }
    }
    private void AddCol()
    {
        mapData.AddCol();
        map = new Block[mapData.n, mapData.m];
        for (int i = 0; i < mapData.n; ++i)
        {
            InstantiateBlock(i, mapData.m-1);
        }
    }

}
