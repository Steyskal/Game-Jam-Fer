using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    public GameObject informationCanvas;
    public Text title;
    public Text description;
    public Image cellSprite;
    public GameObject newScanCanvas;
    public Sprite endScanSprite;

    public float scanInterval;
    public AudioClip newInfo;
    public List<GameObject> cellsToScan;

    public GameObject questLog;
    public Text quest;
    public GameObject controls;

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
                    setInformationText("Skeniranje završeno", "Ne postaje druge stanice u ovom pacijentu.");
                    cellSprite.sprite = endScanSprite;
                }

            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button3) && (_scanCounter <= cellsToScan.Count))
        {
            _audioSource.pitch = 0.6f;
            _audioSource.PlayOneShot(newInfo);
            newScanCanvas.SetActive(false);
            informationCanvas.SetActive(true);
            setInformationText(_scannedCell.GetComponent<BloodUnit>().unitName, _scannedCell.GetComponent<BloodUnit>().unitDescription);
            setCellSprite(_scannedCell);
            _cellScanned = false;
        }


        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            _audioSource.pitch = 0.6f;
            _audioSource.PlayOneShot(newInfo);
            questLog.SetActive(!questLog.activeSelf);
            controls.SetActive(!controls.activeSelf);
        }

        if (!QuestManager.getInstance().win)
            quest.text = "<color=orange>Spasite pacijenta!</color>\n- doðite do srca i uništite " + QuestManager.getInstance().cellsLeft + "/20 neprijateljskih stanica.\n\nPritisnite <color=green>Tipku A</color> da ubrizgate lijek.";
        else
        {
            WinGame();
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
        title.text = "<color=yellow>"+newTitle+"</color>";
        description.text = newDescription;
    }

    public void WinGame()
    {
        _audioSource.pitch = 1.0f;
        _audioSource.PlayOneShot(newInfo);
        quest.text = "<color=orange>Bravo!</color>\nDoktore, spasili ste pacijenta!";
        //Time.timeScale = 0.0f;

        Invoke("GoToCredits", 5.0f);
    }

    public void GoToCredists()
    {
        //EditorSceneManager.LoadScene("Credits");
    }
}
