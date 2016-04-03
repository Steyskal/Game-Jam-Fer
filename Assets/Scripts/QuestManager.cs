using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

    public List<GameObject> cellsToDestroy;
    public int cellsLeft = 47;
    public bool win = false;

    private static QuestManager _instance = null;

    public static QuestManager getInstance()
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
    }
    void Update()
    {
        if(checkCells())
        {
            GameManager.getInstance().WinGame();
        }
    }

    bool checkCells()
    {
        foreach (GameObject c in cellsToDestroy)
        {
            if (c != null)
                return false;
        }

        return true;
    }
}
