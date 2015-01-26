using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class FaseObject
{
    public int lvl;
    public int[,] tiles;
    public float time;
}

public class AlwaysAlive : MonoBehaviour
{

    public int fase = 0;
    public int faseMax = 0;
    public JSONObject fases;
    public FaseObject[] fasesObj;
    public string patch;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Start();
    }

    IEnumerator Start()
    {
        TextAsset textFile = Resources.Load <TextAsset>("fase");
        string text = textFile.ToString();
        yield return new WaitForSeconds(0.01f);
        LoadTiles(text);
    }
    private void LoadTiles(string stuff)
    {
        JSONObject j = new JSONObject(stuff);
        fases = j;

        //Debug.Log(fases);
        //Debug.Log(fases["mapas"]);

        fasesObj = new FaseObject[fases["mapas"].Count];
        int count = 0;
        int x = 0;

        foreach (var member in fases["mapas"].list)
        {
            fasesObj[count] = new FaseObject();
            fasesObj[count].lvl = (int)member["fase"].n;
            fasesObj[count].time = (float)member["time"].n;
            int[] line = new int[member["tiles"].Count];
            int columns = member.list[1].list[0].str.Length;
            x = 0;
            fasesObj[count].tiles = new int[line.Length,columns];
            foreach (var tiles in member["tiles"].list)
            {
               line = Array.ConvertAll(tiles.str.ToCharArray(), c => (int)Char.GetNumericValue(c));
               for (int i = 0; i < line.Length; i++) {
                   fasesObj[count].tiles[x,i] = line[i];
                }
                x++;
            }

            count++;
            //Debug.Log(fases["mapas"].keys[i]);
        }
        faseMax = count;
    }

    public FaseObject getFase(int fase)
    {
        return fasesObj[fase - 1];
    }
}
