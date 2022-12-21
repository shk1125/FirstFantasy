using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKillFormat : QuestFormat
{
    public int enemyNum;
    public int targetKillCount;
    public int currentKillCount;


    public QuestKillFormat(string questName, string questContent, int questRewardMoney, int enemyNum, int targetKillCount, int currentKillCount, bool isCleared,bool  isAccepted)
    {
        this.questName = questName;
        this.questContent = questContent;
        this.questRewardMoney = questRewardMoney;
        this.enemyNum = enemyNum;
        this.targetKillCount = targetKillCount;
        this.currentKillCount = currentKillCount;
        this.isCleared = isCleared;
        this.isAccepted = isAccepted;
    }
}
