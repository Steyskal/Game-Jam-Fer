using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour {

    public int underwaterLevel = 7;
    public Color fogColor = Color.red;
    public float fogDensity = 0.04f;

    private Material noSkybox;

    void Start()
    {
        GetComponent<Camera>().backgroundColor = fogColor;
    }

    void Update()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.skybox = noSkybox;
    }
}
