using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class NPCQuestCtrl : MonoBehaviour
{
    public TextAsset quests_Kill;

    

    public List<QuestKillFormat> questList_Kill;

    void Start()
    {
        if(quests_Kill)
        {
            SetQuest_Kill(quests_Kill);
        }
    }

    
   

    void SetQuest_Kill(TextAsset quests)
    {
        string questName;
        string questContent;
        int questRewardMoney;
        int enemyNum;
        int targetKillCount;
        int currentKillCount;
        bool isCleared;
        bool isAccepted;
        questList_Kill = new List<QuestKillFormat>();
        List<Dictionary<string, object>> questsDictionaryList = CSVReader.Read(quests_Kill);

        
        for(int i = 0; i < questsDictionaryList.Count; i++)
        {
            questName = questsDictionaryList[i]["questName"].ToString();
            questContent = questsDictionaryList[i]["questContent"].ToString();
            questRewardMoney = int.Parse(questsDictionaryList[i]["questRewardMoney"].ToString());
            enemyNum = int.Parse(questsDictionaryList[i]["enemyNum"].ToString());
            targetKillCount = int.Parse(questsDictionaryList[i]["targetKillCount"].ToString());
            currentKillCount = int.Parse(questsDictionaryList[i]["currentKillCount"].ToString());
            bool.TryParse(questsDictionaryList[i]["isCleared"].ToString(), out isCleared);
            bool.TryParse(questsDictionaryList[i]["isAccepted"].ToString(), out isAccepted);

            QuestKillFormat questKill = new QuestKillFormat(questName, questContent, questRewardMoney, enemyNum, targetKillCount, currentKillCount, isCleared, isAccepted);
            questList_Kill.Add(questKill);

        }
        
    }

}


public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(TextAsset file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = file;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}


