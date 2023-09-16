using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MapData
{
    public int n,m;
    public int[,] data;
    public MapData(string mapId)
    {
        List<string> mapCSV = CSVImporter.Extract("Map/"+mapId.ToString());
        if(mapCSV == null)
        {
            n = m = 8;
            data = new int[8,8];
            for(int i=0;i<8;++i)for(int j=0;j<8;++j)data[i,j] = 0;
            return;
        }
        string[] tmp = mapCSV[0].Split(',');
        n = int.Parse(tmp[0]);
        m = int.Parse(tmp[1]);
        for(int i=1;i<=n;++i)
        {
            tmp = mapCSV[i].Split(',');
            for(int j=0;j<m;++j)
            {
                data[i-1,j] = int.Parse(tmp[j]);
            }
        }
    }
}
