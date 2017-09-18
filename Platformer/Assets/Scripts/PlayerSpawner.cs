using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner: MonoBehaviour
{
    // Level
    public Room room;

    public GameObject player;

    public int s;


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float scale = transform.localScale.x - 0.5f;
        transform.localScale = new Vector2(scale, scale);

        if (scale <= 1)
        {
            // Create player
            Player p = Instantiate(player, room.Respawn.position, Quaternion.identity).GetComponent<Player>();
            p.room = room;

            // Set new camera target
             Game.instance.cam.GetComponent<CamController>().target = p.GetComponent<Transform>();

            // Destroy respawn anim
            Destroy(gameObject);        
        }
	}
}
