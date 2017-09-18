using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private const int duration = 10;
    private int durationTick = duration;

    public

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        durationTick--;

        if (durationTick == 0)
        {
            Destroy(this.gameObject);
        }

    }

    // === Delete enemies hit ===
    void OnCollisionEnter2D(Collision2D coll)
    {
        int x = 5;

        if(coll.gameObject.tag.Equals("Enemy"))
        {
            Destroy(coll.gameObject);
        }
    }
}
