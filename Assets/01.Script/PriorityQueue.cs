using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : System.IComparable
{
    private T[] tree;
    private int capacity = 2;
    private int length = 1;
    public PriorityQueue()
    {
        tree = new T[capacity];
    }
    public void Add(T node)
    {
        if(length == capacity)
        {
            capacity *= 2;
            System.Array.Resize(ref tree,capacity);
        }
        tree[length] = node;
        for(int i=length;i != 1;i/=2)
        {
            if(tree[i / 2].CompareTo(tree[i]) > 0)
            {
                T tmp = tree[i/2];
                tree[i/2] = tree[i];
                tree[i] = tmp;
            }
        }
        length++;
    }
    public T Pop()
    {
        T ret = tree[1];
        length--;
        tree[1] = tree[length];
        int curIndex = 1;
        while(true)
        {
            bool changed = false;
            if(length >= curIndex * 2 + 1)
            {
                if(tree[curIndex * 2].CompareTo(tree[curIndex * 2 + 1]) < 0 )
                {
                    if(tree[curIndex * 2].CompareTo(tree[curIndex]) < 0)
                    {
                        T tmp = tree[curIndex * 2];
                        tree[curIndex * 2] = tree[curIndex];
                        tree[curIndex] = tmp;
                        curIndex = curIndex * 2;
                        changed = true;
                    }
                }
                else
                {
                    if (tree[curIndex * 2 + 1].CompareTo(tree[curIndex]) < 0)
                    {
                        T tmp = tree[curIndex * 2 + 1];
                        tree[curIndex * 2 + 1] = tree[curIndex];
                        tree[curIndex] = tmp;
                        curIndex = curIndex * 2 + 1;
                        changed = true;
                    }
                }
            }
            if(length == curIndex * 2)
            {
                if (tree[curIndex * 2].CompareTo(tree[curIndex * 2 + 1]) < 0)
                {
                    if (tree[curIndex * 2].CompareTo(tree[curIndex]) < 0)
                    {
                        T tmp = tree[curIndex * 2];
                        tree[curIndex * 2] = tree[curIndex];
                        tree[curIndex] = tmp;
                        curIndex = curIndex * 2;
                        changed = true;
                    }
                }
            }
            if(!changed)break;
        }
        return ret;
    }
    public bool IsEmpty()
    {
        return length == 1;
    }
}
