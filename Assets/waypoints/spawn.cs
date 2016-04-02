using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

    public int zrncaCount;
    int stara;
    public float zrncaSpawnSpeed;
    public string nextWaypoint;
    public GameObject zrncePrefab;

	// Use this for initialization
	void Start () {

        zrncaCount = 0;
        stara = 0;

        InvokeRepeating("Spawn", 0.0f, zrncaSpawnSpeed);

	}
	
	// Update is called once per frame
	void Update () {

        if (zrncaCount > stara)
        {
            stara = zrncaCount;
            Debug.Log("Dodan je novi!");
        }

        if (zrncaCount < stara)
        {
            stara = zrncaCount;
            Debug.Log("Maknut je jedan!");
        }
        
    }

    void Spawn()
    {
        if (zrncaCount < 10)
        {
            GameObject clone = Instantiate(zrncePrefab,
                new Vector3(0, 0, -5), Quaternion.identity) as GameObject;
            clone.transform.SetParent(this.gameObject.transform, false);
            clone.GetComponent<pracenje>().target= GameObject.Find(nextWaypoint).transform;
            zrncaCount++;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("waypoint")))
        {
            nextWaypoint = other.gameObject.name;
        }
    }

}
