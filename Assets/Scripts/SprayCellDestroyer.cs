using UnityEngine;
using System.Collections;

public class SprayCellDestroyer : MonoBehaviour {

    Collider cell;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            QuestManager.getInstance().cellsLeft--;
            if (QuestManager.getInstance().cellsLeft == 0)
                QuestManager.getInstance().win = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
           
        }
    }

    void DestroyCell()
    {
        QuestManager.getInstance().cellsLeft--;
        if (QuestManager.getInstance().cellsLeft == 0)
            QuestManager.getInstance().win = true;

        Destroy(cell.gameObject);
    }
}
