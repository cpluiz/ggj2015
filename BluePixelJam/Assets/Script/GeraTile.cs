using UnityEngine;
using System.Collections;

public class GeraTile : MonoBehaviour {
	
	
	public GameObject[] tiles = new GameObject[7];
	public Sprite[] tilesImg;
	public float tileDistance = 0.7f;
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

	private GameObject[,] tileObjMap;
	
	int[,] tileMap = {
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,2},
		{2,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,2},
		{2,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,2},
		{2,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,2},
		{2,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,2},
		{2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
	};
	
	IEnumerator Start()
	{
		xLength = tileMap.GetLength (0);
		yLength = tileMap.GetLength (1);
		
		GeraMap();

		yield return new WaitForEndOfFrame();

		ChangeGroundTextures();
	}
	
	public void GeraMap() {
		tileObjMap = new GameObject[xLength, yLength];

		mapBounds = new Bounds();

		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) {
			for (int j = 0, x = 0; j < yLength; j++, x++) {
				if(tileMap[i,j] != 0) {
					tileObjMap[i,j] = Instantiate(tiles[tileMap[i,j]],new Vector2(x * tileDistance,y * tileDistance), transform.rotation) as GameObject;
					tileObjMap[i,j].transform.parent = transform;
					
					mapBounds.Encapsulate(tileObjMap[i,j].transform.GetComponent<SpriteRenderer>().bounds);
				}
			}
		}

		GameObject.FindWithTag("Display").GetComponent<Display>().StartDisplay(timer);
		GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().setBounds(mapBounds, tileDistance);
	}
	
	public void ChangeGroundTextures() {
		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) {
			for (int j = 0, x = 0; j < yLength; j++, x++) {
				if(tileObjMap[i,j] != null && tileObjMap[i,j].name == "Ground(Clone)") {
					//Tem N combinaçoes de tiles, sendo 16 checagens para ver em qual posiçao o tile se encontra, mais N checagens para ver qual a rotaçao que o tile ficara.
				}
			}
		}
	}
}
