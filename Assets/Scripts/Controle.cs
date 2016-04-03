using UnityEngine;
using System.Collections;

public class Controle : MonoBehaviour {

    Rigidbody playerRigidbody;
    Vector3 movement;
    Vector3 rotation;

    private float moveH;
    private float moveV;
    private float moveLT_RT;

    public float moveSpeed;
    public float rotSpeed;

    // Joystick1Button4 LB
    // Joystick1Button5 RB
    // Joystick1Button9 donja gljivica kad se stisne
    // Joystick1Button8 gornja gljivica kad se stisne
    // Joystick1Button7 start butt
    // Joystick1Button6 stop butt

    void Start () {

        playerRigidbody = GetComponent<Rigidbody>();

    }
	
	void Update () {

        moveV = Input.GetAxis("Horizontal");
        moveH = Input.GetAxis("Vertical");

        moveLT_RT = Input.GetAxis("LTRT");
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
        
    }

    void Move()
    {
        playerRigidbody.MovePosition(transform.position + transform.forward * (moveSpeed / 10) * moveLT_RT * -1.0f);
        
    }

    void Rotate()
    {
        rotation.Set(moveH * rotSpeed, moveV * rotSpeed, 0.0f);
                
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);

    }
}
