using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MapData
{
    private string mapId;
    public int n,m;
    public int[,] data;
    public MapData(string mapId)
    {
        this.mapId = mapId;
        List<string> mapCSV = CSVImporter.Extract("Map/"+mapId.ToString());
        if(mapCSV == null)
        {
            Debug.Log("not found");
            n = m = 8;
            data = new int[8,8];
            for(int i=0;i<8;++i)for(int j=0;j<8;++j)data[i,j] = 0;
            return;
        }
        string[] tmp = mapCSV[0].Split(',');
        n = int.Parse(tmp[0]);
        m = int.Parse(tmp[1]);
        data = new int[n,m];
        for(int i=1;i<=n;++i)
        {
            tmp = mapCSV[i].Split(',');
            for(int j=0;j<m;++j)
            {
                data[i-1,j] = int.Parse(tmp[j]);
            }
        }
    }
    public void Save()
    {
        string[] saveData = new string[n+1];
        saveData[0] = n.ToString() +","+m.ToString();
        for(int i=0;i<n;++i)
        {
            for(int j=0;j<m - 1;++j)
            {
                saveData[i+1] += data[i,j].ToString()+",";
            }
            saveData[i+1] += data[i,m-1].ToString();
        }
        StreamWriter sw =  File.CreateText("Assets/Resources/Map/"+mapId.ToString()+".csv");
        for(int i=0;i<n+1;++i)
        {
            sw.WriteLine(saveData[i]);
        }
        sw.Close();
        UnityEditor.AssetDatabase.Refresh();
    }
    public void AddRow()
    {
        int[,] copy = data;
        n++;
        data = new int[n,m];
        for(int i=0;i<n-1;++i)
        {
            for(int j=0;j<m;++j)
            {
                data[i,j] = copy[i,j];
            }
        }
        for(int i=0;i<m;++i)
        {
            data[n-1,i] = 0;
        }
    }
    public void AddCol()
    {
        int[,] copy = data;
        m++;
        data = new int[n,m];
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < m - 1; ++j)
            {
                data[i, j] = copy[i, j];
            }
        }
        for(int i=0;i<n;++i)
        {
            data[i,m-1] = 0;
        }
    }
}
