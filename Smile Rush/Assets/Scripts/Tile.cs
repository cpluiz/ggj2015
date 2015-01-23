using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {


	public GameObject[] tiles = new GameObject[4]; //Array que contem os prefabs dos tiles.
	public float tileDistance = 0.64f; //Variavel da distancia entre um tile e outro nos eixos x e y.
	public float delay; //Delay para o funçao GeraTile iniciar ("Para Testes")
    private GameObject[] map;
    private ObjectManager manager; //Objeto que gerencia a comunicação entre os componentes do jogo
    private player playerRef;

	Vector2 spawnPosition;
	int xLength;
	int yLength;

	//Matriz que armazena o tilemap contendo numeros de acordo com o total de tiles.

	//0 = Grass
	//1 = Tree
	//2 = Inicio/Circle
	//3 = Fim/x


	float[,] tileMap = {
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
		{1, 1, 1, 1, 0, 1, 0, 3, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1}, 
		{1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1},
		{1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1},
		{1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1},
		{1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1}, 
		{1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1},
		{1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1}, 
		{1, 2, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1},
		{1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1},
		{1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1}, 
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
	};

	void Start()
	{
        verifyManager();
        playerRef = GameObject.FindWithTag("Player").GetComponent<player>();
		playerRef.DisablePlayer ();
        map = new GameObject[tileMap.Length];
		xLength = tileMap.GetLength(0);
		yLength = tileMap.GetLength(1);
		StartCoroutine (GeraTiles());
	}

    public void restart() {
        destroyAll();
        Start();
    }

    public void destroyAll() {
        for (int i = 0; i < map.Length; i++) {
            Destroy(map[i]);
        }
    }

	public IEnumerator GeraTiles() //Funçao que faz a leitura da matriz de tiles e gera o tileMap no jogo partindo do ponto 0,0
	{
        int count = 0;
        for (int i = xLength - 1, y = 0; i >= 0; i--, y++)
		{
			for (int j = 0, x = 0; j < yLength; j++,x++) 
			{
				//Instancia um tile determinado na determinada posiçao
				map[count] = Instantiate(tiles[(int)(tileMap[i,j])], new Vector2(x * tileDistance,y * tileDistance), transform.rotation) as GameObject;
				//Atribui o tile como um objeto filho, para melhor organizaçao no inspector.
				map[count].transform.parent = transform;

				if(tileMap[i,j] == 2)
				{
					spawnPosition = new Vector2(x * tileDistance,y * tileDistance);
					print(spawnPosition);
				}
                manager.loading(count+1, tileMap.Length);
                count++;
				yield return new WaitForSeconds(delay);
			}
		}
       
		playerRef.SpawnPlayer (spawnPosition); //Seta o player no tile inicio.
	}

    public void verifyManager(){
        if (manager == null) { manager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>(); manager.forceAwake(); }
    }
}
