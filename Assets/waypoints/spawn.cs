using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class spawn : MonoBehaviour {
    [HideInInspector]
    public int zrncaCount;
    int stara;
    public float zrncaSpawnSpeed;
    public string nextTarget;
    public int maxBrojZrnca;

    [System.Serializable]
    public struct BloodCell
    {
        public GameObject cell;
        [Tooltip("Time between spawned cells in seconds.")]
        public float spawnFrequency;
    }

    public float cellSpeed;
    public Transform parentTransform;
    public List<BloodCell> cellsToSpawn = new List<BloodCell>();


    void Awake()
    {
        foreach (BloodCell bloodCell in cellsToSpawn)
        {
            StartCoroutine(SpawnCell(bloodCell.cell, bloodCell.spawnFrequency));
        }
    }

    // Use this for initialization
    void Start () {

        zrncaCount = 0;
        stara = 0;

        //InvokeRepeating("Spawn", 0.0f, zrncaSpawnSpeed);

	}
	
	// Update is called once per frame
	void Update () {

        if (zrncaCount > stara)
        {
            stara = zrncaCount;
        }

        if (zrncaCount < stara)
        {
            stara = zrncaCount;
        }
        
    }
    /*
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
        
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("putGospodinov")))
        {
            nextTarget = other.gameObject.name;
        }
    }

    private IEnumerator SpawnCell(GameObject cell, float spawnFrequency)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            if (zrncaCount < maxBrojZrnca)
            {       
                Vector3 position = new Vector3(UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-8.0F, 4.0F));

                GameObject clone = Instantiate(cell,
                    new Vector3(UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-5.0F, 5.0F), UnityEngine.Random.Range(-8.0F, 24.0F)),
                    Quaternion.identity) as GameObject;
                clone.transform.SetParent(this.gameObject.transform, false);

                clone.GetComponent<pracenje>().target = GameObject.Find(nextTarget).transform;
                zrncaCount++;

                //clone.GetComponent<Rigidbody>().MovePosition(transform.forward * cellSpeed, ForceMode.Force);
            }
        }        
                    
    }

}
