using UnityEngine;
using System.Collections;

public class pracenje : MonoBehaviour
{

    public Transform target;
    float f_RotSpeed = 3.0f, f_MoveSpeed = 3.0f;

    private Vector3 velocity = Vector3.zero;

    private spawn playerSpawn;
    int waypointNum;

    char[] wayList = new char[5];
    bool waypointMetnuo;

    void Start()
    {
        waypointMetnuo = false;

        wayList[0] = 'a';
        wayList[1] = 'b';
        wayList[2] = 'c';
        wayList[3] = 'd';

        waypointNum = 1; // ALO!!! trebalo bude promjenit dok budes radit finali level
        target = GameObject.Find("waypoint_" + 1 + wayList[Random.Range(0, 3)]).transform;

        playerSpawn = GameObject.Find("player").GetComponent<spawn>();
                
        Debug.Log("Waypoint_" + 1 + wayList[1]);
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

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("waypoint")) && waypointMetnuo == false)
        {
            waypointMetnuo = true;
            Invoke("IzvadiGa", 3.0f);
            waypointNum++;
            try
            {                
                target = GameObject.Find("waypoint_" + waypointNum + wayList[Random.Range(0,3)]).transform;
            }
            catch (System.Exception)
            {
                target = GameObject.Find("waypoint_1" + wayList[Random.Range(0, 3)]).transform;  // ALO!!! trebalo bude promjenit dok budes radit finali level
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

    void IzvadiGa()
    {
        waypointMetnuo = false;
    }
}
