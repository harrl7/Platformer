using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public bool TrackPlayer;


    // Use this for initialization
    void Start ()
    {
       // transform.position = target.position;
	}

    // Track target
    void FixedUpdate()
    {
        if (target)
        {
            // Move camera
            Vector2 targeX = new Vector2(target.position.x - transform.position.x, 0);         
            Vector2 toTarget = target.position - transform.position;
            transform.Translate(targeX * 0.5f);
        }
    }

}
