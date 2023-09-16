using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVImporter
{
    public static List<string> Extract(string path)
    {
        TextAsset textAsset = Resources.Load(path) as TextAsset;
        if(textAsset == null)return null;
        StringReader sr = new StringReader(textAsset.text);
        List<string> text = new List<string>();
        while(sr.Peek() != -1)
        {
            text.Add(sr.ReadLine());
        }
        return text;
    }
}
