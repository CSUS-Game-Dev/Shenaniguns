using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    private List<GameObject> players = new List<GameObject>();
    public List<GameObject> cameras = new List<GameObject>();

    //splitscreen viewports
    private Rect full = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
    private Rect top = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
    private Rect bot = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
    private Rect topLeft = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
    private Rect topRight = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
    private Rect botLeft = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
    private Rect botRight = new Rect(0.5f, 0.0f, 0.5f, 0.5f);

    //this pretty much just enforces singletons
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    void StartGame(int numPlayers, string arena) {

        SceneManager.LoadScene(arena, LoadSceneMode.Single);
    }

    void Update()
    {
        if (Input.GetButtonDown("Player1Start"))
        {
            StartGame(1, "testArena");
        }
        if (Input.GetButtonDown("Player2Start"))
        {
            Debug.Log("trying");
            CreatePlayer(1);
            CreatePlayer(2);
        }
    }

    public GameObject CreatePlayer(int playerNum)
    {
        Object prefab = Resources.Load("meshes/ThaylenLightSworn"); 
        GameObject player = (GameObject)Instantiate(prefab, new Vector3(playerNum*5, 0, 0), Quaternion.identity);
        player.AddComponent(System.Type.GetType("Player"));
        player.GetComponent<Rigidbody>().freezeRotation = true;
        player.GetComponent<Player>().Init(playerNum);
        BuildPlayerCamera(player, playerNum);
        return player;
    }

    public void BuildPlayerCamera(GameObject player,int playerNum) {
        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.GetComponent<Camera>().transform.position = new Vector3(playerNum*5, 10, -10);
        camera.transform.LookAt(player.transform);
        camera.transform.parent = player.transform;
        instance.cameras.Add(camera);
        CalcSplitScreens();
    }

    public void CalcSplitScreens() {

        switch (instance.cameras.Count) {
            case 1:
                instance.cameras[0].GetComponent<Camera>().rect = full;
                break;
            case 2:
                instance.cameras[0].GetComponent<Camera>().rect = top;
                instance.cameras[1].GetComponent<Camera>().rect = bot;
                break;
            case 3:
                instance.cameras[0].GetComponent<Camera>().rect = top;
                instance.cameras[1].GetComponent<Camera>().rect = botLeft;
                instance.cameras[2].GetComponent<Camera>().rect = botRight;
                break;
            case 4:
                instance.cameras[0].GetComponent<Camera>().rect = topLeft;
                instance.cameras[1].GetComponent<Camera>().rect = topRight;
                instance.cameras[2].GetComponent<Camera>().rect = botLeft;
                instance.cameras[3].GetComponent<Camera>().rect = botRight;
                break;
            default:
                break;
        }
        
    }
}
