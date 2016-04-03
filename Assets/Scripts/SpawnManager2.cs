using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager2 : MonoBehaviour {
    
    [System.Serializable]
    public struct SpawnPoints
    {
        public Transform topLeft;
        public Transform topRight;
        public Transform bottomLeft;
        public Transform bottomRigh;
    }

    [System.Serializable]
    public struct BloodCell
    {
        public GameObject cell;
        [Tooltip("Time between spawned cells in seconds.")]
        public float spawnFrequency;
    }

    public float cellSpeed;
    public Transform parentTransform;
    public SpawnPoints spawnPoints;
    public List<BloodCell> cellsToSpawn = new List<BloodCell>();

    public bool isActive = false;

    void Awake()
    {
        if(isActive)
            foreach (BloodCell bloodCell in cellsToSpawn)
            {
                StartCoroutine(SpawnCell(bloodCell.cell, bloodCell.spawnFrequency));
            }
    }

    private IEnumerator SpawnCell(GameObject cell, float spawnFrequency)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            float x = Random.Range(spawnPoints.topLeft.position.x, spawnPoints.topRight.position.x);
            float y = Random.Range(spawnPoints.topLeft.position.y, spawnPoints.bottomLeft.position.y);

            Vector3 position = new Vector3(x, y, transform.position.z);

            
            GameObject clone = (GameObject)Instantiate(cell, position, Quaternion.identity);
            clone.transform.SetParent(parentTransform);

            clone.GetComponent<Rigidbody>().AddForce(transform.forward * cellSpeed, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("player")))
        {
            isActive = true;

            foreach (BloodCell bloodCell in cellsToSpawn)
            {
                StartCoroutine(SpawnCell(bloodCell.cell, bloodCell.spawnFrequency));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("player")))
        {
            isActive = false;

            foreach (BloodCell bloodCell in cellsToSpawn)
            {
                StopAllCoroutines();
            }
        }
    }
}
