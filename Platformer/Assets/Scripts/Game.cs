using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;

    public GameObject oMainCamera;
    public GameObject oPlayer;
    public GameObject oLevel;

    public CamController cam;
    public Player player = null;
    public Room room;


    // Awake is always called before any Start functions
    void Awake()
    {
        // Single instance of Game Manager
        if (instance == null)       instance = this;
        else if (instance != this)  Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
       // InitGame();
    }

    // Use this for initialization
    void Start ()
    {
        player = Instantiate(oPlayer, new Vector2(0,0), Quaternion.identity).GetComponent<Player>();
        //cam.target = player.transform;

        // Create Main Camera
       // cam = Instantiate(oMainCamera).GetComponent<CamController>();
        cam.target = player.transform;

       // room = oLevel.GetComponent<Room>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnPlayer()
    {
        room.SpawnPlayer();
    }

    public void ChangeRoom(RoomExit roomExit)
    {
        Destroy(room.gameObject);
        room = Instantiate(roomExit.room, new Vector2(0, 0), Quaternion.identity).GetComponent<Room>();
    }
}
