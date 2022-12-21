using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNameTextCtrl_Player : MonoBehaviour
{
    public int questKillNum;

    public void OnPlayerQuestPanel()
    {
        GameManager.instance.OnPlayerQuestPanel(questKillNum);
    }
}
