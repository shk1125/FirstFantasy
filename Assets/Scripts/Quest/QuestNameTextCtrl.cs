using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNameTextCtrl : MonoBehaviour
{

    public int questKillNum;


    public void OnNPCQuestPanel()
    {
        GameManager.instance.OnNPCQuestPanel(questKillNum);
    }


}
