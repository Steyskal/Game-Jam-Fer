using UnityEngine;
using System.Collections;

public class pracenje : MonoBehaviour
{

    public Transform target;
    float f_RotSpeed = 3.0f, f_MoveSpeed = 3.0f;

    private Vector3 velocity = Vector3.zero;

    private spawn playerSpawn;
    int waypointNum;

    void Start()
    {
        waypointNum = 1;
        target = GameObject.Find("waypoint_" + 1).transform;

        playerSpawn = GameObject.Find("player").GetComponent<spawn>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), f_RotSpeed * Time.deltaTime);

        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("player")))
        {
            Debug.Log("Unutra sam");
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("waypoint")))
        {
            waypointNum++;
            try
            {                
                target = GameObject.Find("waypoint_" + waypointNum).transform;
            }
            catch (System.Exception)
            {
                target = GameObject.Find("waypoint_1").transform;
            }
            
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("player")))
        {
            playerSpawn.zrncaCount--;
            Destroy(this.gameObject, 0);
        }
        
    }
}
