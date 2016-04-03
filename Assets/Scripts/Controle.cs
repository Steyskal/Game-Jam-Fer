using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controle : MonoBehaviour {

    Rigidbody playerRigidbody;
    Vector3 movement;
    Vector3 rotation;

    private float moveH;
    private float moveV;
    private float moveLT_RT;

    public List<GameObject> flashlights;

    public float moveSpeed;
    public float rotSpeed;

    public GameObject sprayParticle;
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

        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            foreach (GameObject f in flashlights)
            {
                f.SetActive(!f.activeSelf);   
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            sprayParticle.GetComponent<Collider>().enabled = true;
            sprayParticle.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            sprayParticle.GetComponent<Collider>().enabled = false;
            sprayParticle.GetComponent<ParticleSystem>().Stop();
        }

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

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.GetComponent<BloodUnit>());

        if (other.gameObject.GetComponent<BloodUnit>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BloodUnit>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
