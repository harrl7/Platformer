using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //
    public Transform pointer;

    // Control
    private int index;
    private const int item_count = 2;

    // Input
    private float inp_yAxis;
    private bool inp_select;

    // Use this for initialization
    void Start ()
    {
        index = 0;

        getInput();
    }
	
	// Update is called once per frame
	void Update ()
    {
        getInput();

        index += (int) inp_yAxis;
        index %= item_count;

        float y = 0 - (index * -0.5f);

        pointer.position = new Vector2(pointer.position.x, y);


	}

    // === Get input ===
    void getInput()
    {
        inp_yAxis = Input.GetAxis("Vertical");

        inp_yAxis = (Input.GetKeyDown(KeyCode.UpArrow) ? 0:1) - (Input.GetKeyDown(KeyCode.DownArrow) ? 0 : 1);

      //  inp_yAxis = (Input.GetButtonDown("vertical");

        inp_select = Input.GetButtonDown("Jump");
    }

}
