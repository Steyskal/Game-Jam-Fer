﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    public GameObject informationCanvas;
    public Text title;
    public Text description;
    public Image cellSprite;
    public GameObject newScanCanvas;

    public float scanInterval;
    public AudioClip newInfo;
    public List<GameObject> cellsToScan;

    private GameObject _scannedCell;
    private float _timer = 0.0f;
    private int _scanCounter = 0;
    private bool _cellScanned = false;

    private AudioSource _audioSource;

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

        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_cellScanned)
        {
            _timer += Time.deltaTime;

            if(_timer >= scanInterval)
            {
                if (_scanCounter < cellsToScan.Count) {
                    _audioSource.pitch = 1.0f;
                    _audioSource.PlayOneShot(newInfo);
                    newScanCanvas.SetActive(true);
                    informationCanvas.SetActive(false);
                    updateScanInfo(cellsToScan[_scanCounter++]);
                    _timer = 0.0f;
                }
                else
                {
                    if (_cellScanned != null)
                        Destroy(_scannedCell);

                    //Destroy(cellSprite.gameObject);
                    //setInformationText("", "<color=orange>Final Mision</color>\nSave the heart!");
                    
                }

            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button0) && (_scanCounter <= cellsToScan.Count))
        {
            _audioSource.pitch = 0.6f;
            _audioSource.PlayOneShot(newInfo);
            newScanCanvas.SetActive(false);
            informationCanvas.SetActive(true);
            setInformationText(_scannedCell.GetComponent<BloodUnit>().unitName, _scannedCell.GetComponent<BloodUnit>().unitDescription);
            setCellSprite(_scannedCell);
            _cellScanned = false;
        }
    }

    public void updateScanInfo(GameObject objectPrefab)
    {
        _scannedCell = objectPrefab;
        _cellScanned = true;
    }

    public void setCellSprite(GameObject objectPrefab)
    {
        cellSprite.sprite = objectPrefab.GetComponent<BloodUnit>().unitSprite;
    }

    public void setInformationText(string newTitle, string newDescription)
    {
        title.text = newTitle;
        description.text = newDescription;
    }
}
