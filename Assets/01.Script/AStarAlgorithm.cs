using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Point
{
    public int x,y;
    public Point(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}
public class Node : IComparable
{
    public int f,g;
    public Point pos;
    public Node(int f,int g,Point pos)
    {
        this.f = f;
        this.g = g;
        this.pos = pos;
    }
    int IComparable.CompareTo(object obj)
    {
        Node other = obj as Node;
        if(other == null)
            throw new Exception("Not Match Type");
        return g - other.g;
    }
}
public class AStarAlgorithm
{
    public static readonly int[] dirY = {0,1,0,-1,-1,-1,1,1};
    public static readonly int[] dirX = {1,0,-1,0,-1,1,1,-1};
    public static readonly int[] cost = {10,10,10,10,14,14,14,14};
    MapData mapData;
    private int n,m;
    public AStarAlgorithm(MapData mapData)
    {
        this.mapData = mapData;
        this.n = mapData.n;
        this.m = mapData.m;
    }
    public List<Point>  PathFinding(Point start,Point end)
    {
        bool[,] closed = new bool[n,m];
        int[,] open = new int[n,m];
        Point[,] parent = new Point[n,m];
        PriorityQueue<Node> pq = new PriorityQueue<Node>();
        for(int i=0;i<n;++i)
        {
            for(int j=0;j<m;++j)
            {
                open[i,j] = Int32.MaxValue;
            }
        }
        
        int startF = 10 * (Mathf.Abs(end.x - start.x) + Mathf.Abs(end.y - start.y));
        open[start.y,start.x] = startF;
        parent[start.y,start.x] = start;
        pq.Add(new Node(startF,0,start));

        while(!pq.IsEmpty())
        {
            Node cur = pq.Pop();
            if(closed[cur.pos.y,cur.pos.x])
                continue;
            closed[cur.pos.y,cur.pos.x] = true;
            if(cur.pos.y == end.y && cur.pos.x == end.x)
            {
                break;
            }
            for(int i=0;i<8;++i)
            {
                int nextY = cur.pos.y + dirY[i];
                int nextX = cur.pos.x + dirX[i];
                if(nextY < 0 || nextY >= n || nextX < 0 || nextX >= m)
                    continue;
                if(mapData.data[nextY,nextX] != 1)
                    continue;
                if(closed[nextY,nextX])
                    continue;
                int g = cur.g + cost[i];
                int h = 10 * (Mathf.Abs(end.y - nextY) + Math.Abs(end.x - nextX));

                if(open[nextY,nextX] < g + h)
                    continue;
                
                open[nextY,nextX] = g + h;
                pq.Add(new Node(g + h,g,new Point(nextX,nextY)));
                parent[nextY,nextX] = new Point(cur.pos.x,cur.pos.y);
            }
        }
        List<Point> result = new List<Point>();
        Point curPos = end;
        while(curPos.x != start.x || curPos.y != start.y)
        {
            result.Add(curPos);
            curPos = parent[curPos.y,curPos.x];
        }
        return result;
    }
}
