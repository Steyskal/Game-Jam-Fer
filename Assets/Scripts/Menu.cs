using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Application.LoadLevel("konacna scena");
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Application.Quit();
        }

    }
}
