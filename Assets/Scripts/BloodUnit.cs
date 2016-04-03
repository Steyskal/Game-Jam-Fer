using UnityEngine;
using System.Collections;

public class BloodUnit : MonoBehaviour {

    public string unitName;
    [TextAreaAttribute(1,5)]
    public string unitDescription;
    public Sprite unitSprite;

    [System.Serializable]
    public struct ScaleCriticalValues
    {
        public float minScale;
        public float maxScale;
    }

    [Tooltip("The minimum and maximum scale range in percentages.")]
    public ScaleCriticalValues scaleCriticalValues;

    private Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        Invoke("EnableUnitCollider", 1.0f);

        ApplyRandomScale();
    }

    void ApplyRandomScale()
    {
        float newScale = Random.Range(scaleCriticalValues.minScale, scaleCriticalValues.maxScale);
        newScale /= 100.0f;

        transform.localScale = new Vector3(transform.localScale.x * newScale, transform.localScale.y * newScale, transform.localScale.z * newScale);
    }

    void EnableUnitCollider()
    {
        _collider.enabled = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Spray")) && gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            Invoke("DestroyCell", 1.0f);
        }

        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("SpawnManager")))
            Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Spray")) && gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            Invoke("DestroyCell", 1.0f);
        }
    }

    void DestroyCell()
    {
        Destroy(gameObject);
    }
}
