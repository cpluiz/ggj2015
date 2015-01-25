using UnityEngine;
using System.Collections;

public class GeraTile : MonoBehaviour {


	public GameObject[] tiles = new GameObject[7];
	public float tileDistance = 0.7f;
    [SerializeField]
    private Bounds mapBounds;

	private float timer;

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

	int[,] tileMap = {
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2},
		{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,0,1,1,1,0,0,1,1,0,1,1,1,0,0,1,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,2},
		{2,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,2},
		{2,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,2},
		{2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
		{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
	};

	void Start()
	{
        GameObject controller = GameObject.FindWithTag("GameController");
        FaseObject map = controller.GetComponent<AlwaysAlive>().getFase(controller.GetComponent<GameConfig>().getFase());
        tileMap = map.tiles;
        timer = map.time;
		xLength = tileMap.GetLength (0);
		yLength = tileMap.GetLength (1);

		GeraMap ();
	}

	public void GeraMap()
	{
        mapBounds = new Bounds();
		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) 
		{
			for (int j = 0, x = 0; j < yLength; j++, x++) 
			{
				if(tileMap[i,j] != 0)
				{
					GameObject tile = Instantiate(tiles[tileMap[i,j]],new Vector2(x * tileDistance,y * tileDistance), transform.rotation) as GameObject;
					tile.transform.parent = transform;

                    mapBounds.Encapsulate(tile.transform.GetComponent<SpriteRenderer>().bounds);
				}
			}
		}
        GameObject.FindWithTag("Display").GetComponent<Display>().StartDisplay(timer);
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().setBounds(mapBounds);
	}
}
