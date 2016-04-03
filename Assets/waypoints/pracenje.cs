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
        target = GameObject.Find("PuteviGospodnji_" + 1).transform;      // pocetni target dok se instancira !!!!
        target = target.GetChild(Random.Range(1, 4)).transform;

        playerSpawn = GameObject.Find("Player").GetComponent<spawn>();
                
        //Debug.Log("Waypoint_" + 1 + wayList[1]);
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
           // Debug.Log("Unutra sam");
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("putGospodinov")) && waypointMetnuo == false)
        {
            waypointMetnuo = true;
            Invoke("IzvadiGa", 2.0f);
            waypointNum++;

            switch (waypointNum)
            {
                case 8:
                    waypointNum = Random.Range(1, 2);
                    waypointNum = waypointNum == 1 ? 59 : 9;
                    target = GameObject.Find("PuteviGospodnji_" + waypointNum).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                case 9:
                    waypointNum = Random.Range(1, 2);
                    waypointNum = waypointNum == 1 ? 43 : 10;
                    target = GameObject.Find("PuteviGospodnji_" + waypointNum).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                case 15:
                    waypointNum = Random.Range(1, 2);
                    waypointNum = waypointNum == 1 ? 34 : 16;
                    target = GameObject.Find("PuteviGospodnji_" + waypointNum).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                case 42:                    
                    target = GameObject.Find("PuteviGospodnji_" + 26).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                case 58:
                    target = GameObject.Find("PuteviGospodnji_" + 31).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                 case 33:
                    target = GameObject.Find("PuteviGospodnji_" + 7).transform;
                    target = target.GetChild(Random.Range(1, 4)).transform;

                    break;

                default:
                    try
                    {
                        target = GameObject.Find("PuteviGospodnji_" + waypointNum).transform;
                        target = target.GetChild(Random.Range(1, 4)).transform;
                    }
                    catch (System.Exception)
                    {
                        target = GameObject.Find("PuteviGospodnji_" + 1).transform;  // ALO!!! trebalo bude promjenit dok budes radit finali level
                        target = target.GetChild(Random.Range(1, 4)).transform;
                        waypointNum = 1;
                    }
                    break;
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
