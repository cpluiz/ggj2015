using UnityEngine;
using System.Collections;
using System;

public class GeraTile : MonoBehaviour {
	
	
	public Tile[] tiles = new Tile[7];
	public Sprite[] tilesImg;
	public float tileDistance;
    private string tilesetName = "new_tilemap";
    private Sprite[] tileset;
	[SerializeField]
	private Bounds mapBounds;
	
	public float timer = 60f;
	
	public GameObject holder;
	
	int xLength;
	int yLength;
	//
	
	//1 - chao e teto
	//2 - parede invisivel
	//3 - plataforma
	//4 - portal
	//5 - plataforma-meia-top
	//6 - plataforma-meia-bot

	private Tile[,] tileObjMap;

    int[,] tileMap;
	
	IEnumerator Start()
	{
        AlwaysAlive fases = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlwaysAlive>();
        try{
            tileMap = fases.fasesObj[fases.fase - 1].tiles;
        }catch{
            Debug.Log("Algo errado que não está certo - tentou acessar o índice "+(fases.fase-1));
        }
        timer = fases.fasesObj[fases.fase-1].time;
        tileDistance = tiles[0].getTileDistance()-0.19f;

        xLength = tileMap.GetLength(0);
        yLength = tileMap.GetLength(1);

		GeraMap();

        yield return new WaitForEndOfFrame();

		ChangeGroundTextures();
	}
	
	public void GeraMap() {
		tileObjMap = new Tile[xLength, yLength];
        tileset = Resources.LoadAll<Sprite>("Tiles/" + tilesetName);

		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) {
			for (int j = 0, x = 0; j < yLength; j++, x++) {
				if(tileMap[i,j] != 0) {
					tileObjMap[i,j] = Instantiate(tiles[tileMap[i,j]],new Vector2(x * tileDistance,y * tileDistance), transform.rotation) as Tile;
					tileObjMap[i,j].transform.parent = transform;
                    tileObjMap[i,j].GetComponent<Tile>().changeTile(tileset,tileDistance);
					mapBounds.Encapsulate(tileObjMap[i,j].transform.GetComponent<SpriteRenderer>().bounds);
				}
			}
		}
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().setBounds(mapBounds, tileDistance);
		GameObject.FindWithTag("Display").GetComponent<Display>().StartDisplay(timer);
		
	}
	
	public void ChangeGroundTextures() {
        mapBounds = new Bounds();
		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) {
			for (int j = 0, x = 0; j < yLength; j++, x++) {
				if(tileObjMap[i,j] != null) {
                    bool acima, abaixo, esquerda, direita;
                    acima = abaixo = esquerda = direita = false;
                    //0 = borda acima e abaixo
                    //1 = borda acima, a esquerda e a direita
                    //2 = borda acima e a esquerda
                    //3 = borda acima
                    //4 = sem borda
                    acima = comparaAcima(tileObjMap[i, j], i, j);
                    abaixo = comparaAbaixo(tileObjMap[i, j], i, j);
                    esquerda = comparaAnterior(tileObjMap[i, j], i, j);
                    direita = comparaProximo(tileObjMap[i, j], i, j);
                    //código ainda sem as rotações
                    if (acima && abaixo && esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,4);
                    }else if(!acima && !abaixo && esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,0);
                    }else if(acima && abaixo && !esquerda && !direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,0);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                    }else if(!acima && !esquerda && direita && abaixo){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,2);
                    }else if(acima && esquerda && !direita && !abaixo){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,2);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                    }else if(!acima && abaixo && esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,3);
                    }else if(acima && abaixo && !esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,3);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                    }else if(acima && !abaixo && esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,3);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                    }else if(acima && abaixo && esquerda && !direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance,3);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                    }else if(!acima && !abaixo && !esquerda && direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                    }else if(acima && !abaixo && !esquerda && !direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                    }else if(!acima && !abaixo && esquerda && !direita){
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance);
                        tileObjMap[i, j].transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                    }else{
                        tileObjMap[i, j].GetComponent<Tile>().changeTile(tileset, tileDistance);
                    }
				}
			}
		}
	}
    private bool comparaAcima(Tile a, int x, int y) {
        bool retorno = false;
        if (x - 1 > 0 && tileMap[x-1,y] !=0 ) { retorno = comparaTiles(a, tileObjMap[x - 1, y]); }
        return retorno;
    }
    private bool comparaAbaixo(Tile a, int x, int y) {
        bool retorno = false;
        if (x + 1 < xLength && tileMap[x+1, y] != 0) { retorno = comparaTiles(a, tileObjMap[x + 1, y]); }
        return retorno;
    }
    private bool comparaAnterior(Tile a, int x, int y) {
        bool retorno = false;
        if (y - 1 > 0 && tileMap[x, y-1] != 0) { retorno = comparaTiles(a, tileObjMap[x, y - 1]); }
        return retorno;
    }
    private bool comparaProximo(Tile a, int x, int y) {
        bool retorno = false;
        if (y + 1 < yLength && tileMap[x, y+1] != 0) { retorno = comparaTiles(a, tileObjMap[x, y + 1]); }
        return retorno;
    }
    private bool comparaTiles(Tile a, Tile b){
        return a.tileImage == b.tileImage;
    }
}
