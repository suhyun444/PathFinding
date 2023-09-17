using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AStarAlgorithm aStar;
    private Point position;
    private Coroutine movingCoroutine;
    float startX;
    float startY;
    public void Init(MapData mapData)
    {
        position = new Point(0, 0);
        startX = ((float)mapData.m / 2) * -0.5f;
        startY = ((float)mapData.n / 2) * 0.5f;
        aStar = new AStarAlgorithm(mapData);
    }
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0));
            for (int i = 0; i < hit2D.Length; ++i)
            {
                if (hit2D[i].collider != null && hit2D[i].collider.gameObject.CompareTag("Block"))
                {
                    Block b = hit2D[i].collider.GetComponent<Block>();
                    if (movingCoroutine != null) StopCoroutine(movingCoroutine);
                    movingCoroutine = StartCoroutine(Move(b.y, b.x));
                }
            }
        }
    }
    private IEnumerator Move(int y, int x)
    {
        List<Point> route = aStar.PathFinding(position, new Point(x, y));
        for (int i = route.Count - 1; i >= 0; --i)
        {
            Vector3 start = transform.position;
            Vector3 end = new Vector3(startX + route[i].x * 0.5f, startY - route[i].y * 0.5f, 0);
            float time = 0;
            float t = 0.2f;
            position = route[i];
            while(time <= 1)
            {
                time += Time.deltaTime / t;
                transform.position= Vector3.Lerp(start,end,time);
                yield return null;
            }
        }
    }
}
