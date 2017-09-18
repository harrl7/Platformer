using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform Respawn;
    public Transform Exit;

    public GameObject Respawner;
    public GameObject MainCam;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnPlayer()
    {
        Game.instance.cam.GetComponent<CamController>().target = Respawn;
        GameObject r = Instantiate(Respawner, Respawn.position, Quaternion.identity);
        PlayerSpawner mything = r.GetComponent<PlayerSpawner>();        
        mything.room = this;
    }
}
