using UnityEngine;
using System.Collections;
using System;

public class spawn : MonoBehaviour {
    [HideInInspector]
    public int zrncaCount;
    int stara;
    public float zrncaSpawnSpeed;
    public string nextWaypoint;
    public GameObject zrncePrefab;
    public int maxBrojZrnca;

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
        if (zrncaCount < maxBrojZrnca)
        {
            GameObject clone = Instantiate(zrncePrefab,
                new Vector3(UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-8.0F, 4.0F)), Quaternion.identity) as GameObject;
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
