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
        patch = "http://bpixel.com.br/dev/ggj/ggj15/fase.json";
#if (UNITY_EDITOR || UNITY_EDITOR_WIN)
        //patch = "file://" + Application.dataPath.ToString() + "/Resources/fase.json";
#endif
        Start();
    }

    IEnumerator Start()
    {
#if (UNITY_STANDALONE || UNITY_ANDROID)
        TextAsset fases = Resources.Load<TextAsset>("fase");
        yield return new WaitForSeconds(0.002f);
        LoadTiles(fases.ToString());
#else
        WWW www = new WWW(patch);
        yield return www;
        if (www.error == null)
        {
            LoadTiles(www.text);
        }
        else
        {
            Debug.Log("Error: " + www.error);
            TextAsset fases = Resources.Load<TextAsset>("fase");
            yield return new WaitForSeconds(0.002f);
            LoadTiles(fases.ToString());
        }
#endif
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
