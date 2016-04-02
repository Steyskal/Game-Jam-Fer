using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    public GameObject hologramPositioner;
    public GameObject hologramObject;
    public Text title;
    public Text description;

    public static GameManager getInstance()
    {
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        if (hologramObject != null)
        {
            setHologram(hologramObject);    
        }
    }

    void Update()
    {

    }

    public void setHologram(GameObject objectPrefab)
    {
        Destroy(hologramObject);
        hologramObject = Instantiate(objectPrefab);
        hologramObject.GetComponent<Rigidbody>().isKinematic = true;
        hologramObject.transform.SetParent(hologramPositioner.transform);
        hologramObject.transform.localPosition = Vector3.zero;

        Vector3 holoSize = hologramPositioner.GetComponent<BoxCollider>().size;
        Vector3 objectSize = objectPrefab.GetComponent<Collider>().bounds.size;

        float scaleX = holoSize.x / objectSize.x;
        float scaleY = holoSize.y / objectSize.y;
        //float scaleX = holoSize.x / hologramObject.transform.localScale.x;
        //float scaleY = holoSize.y / hologramObject.transform.localScale.y;

        float finalScale = scaleX < scaleY ? scaleX : scaleY;

        hologramObject.transform.localScale = new Vector3(finalScale, finalScale, finalScale);

        setInformationText(hologramObject.GetComponent<BloodUnit>().unitName, hologramObject.GetComponent<BloodUnit>().unitDescription);
    }

    public void setInformationText(string newTitle, string newDescription)
    {
        title.text = newTitle;
        description.text = newDescription;
    }
}
