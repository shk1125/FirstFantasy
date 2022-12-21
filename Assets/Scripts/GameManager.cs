using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public PlayerCtrl playerCtrl;
    public GameObject npcTalkPanel;
    public GameObject npcQuestListPanel;
    public GameObject npcQuestPanel;
    public GameObject npcQuestClearPanel;
    public GameObject npcStorePanel;
    public GameObject playerQuestListPanel;
    public GameObject playerQuestPanel;
    public GameObject playerInventoryPanel;   
    public Text npcTalkText;
    public Text npcQuestText;
    public Text playerQuestText;
    public Text playerMoneyText;
    public Button npcQuestButton;
    public Button npcStoreButton;
    public Button npcQuestClearButton;
    public GameObject playerQuestButtonPrefab;
    public GameObject playerItemButtonPrefab;
    public GameObject storeBuyButtonPrefab;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Transform npcStoreScrollview, npcQuestListScrollview, playerQuestListScrollview, playerInventoryScrollview, npcQuestClearListScrollview;
    public GameObject questName_NPCPrefab;
    public GameObject questName_PlayerPrefab;
    
    Transform npcTransform;
    NPCQuestCtrl npcQuestCtrl;
    NPCStoreFormat npcStoreFormat;
    PlayerStat playerStat;
    AudioSource backgroundMusic;
    int questKillNum;
    PlayerQuests playerQuests;





    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Application.targetFrameRate = 60;

        
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.Play();
    }




    public void NPCStartTalk(Transform npcTransform)
    {
        
        this.npcTransform = npcTransform;

        npcTalkText.text = this.npcTransform.GetComponent<NPCTalkFormat>().talkAsset.text;

        playerQuests = playerCtrl.GetComponent<PlayerQuests>();
        npcQuestCtrl = this.npcTransform.GetComponent<NPCQuestCtrl>();
        if (npcQuestCtrl != null)
        {
            for (int i = 0; i < npcQuestCtrl.questList_Kill.Count; i++)
            {
                if (!npcQuestCtrl.questList_Kill[i].isCleared && !npcQuestCtrl.questList_Kill[i].isAccepted)
                {
                    npcQuestButton.gameObject.SetActive(true);
                    break;
                }
            }

            for(int j = 0; j < npcQuestCtrl.questList_Kill.Count; j++)
            {
                for(int k = 0; k < playerQuests.questList_Kill.Count; k++)
                {
                    if(npcQuestCtrl.questList_Kill[j].questName == playerQuests.questList_Kill[k].questName)
                    { 
                        if(playerQuests.questList_Kill[k].currentKillCount == playerQuests.questList_Kill[k].targetKillCount)
                        {
                            npcQuestClearButton.gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }





        npcStoreFormat = this.npcTransform.GetComponent<NPCStoreFormat>();
        if (npcStoreFormat != null)
        {
            npcStoreButton.gameObject.SetActive(true);
        }

        npcTalkPanel.SetActive(true);
        playerCtrl.isWithNPC = true;
    }

    public void OnNPCQuestListPanel()
    {
        npcQuestCtrl = npcTransform.GetComponent<NPCQuestCtrl>(); 

        for(int j = 0; j < npcQuestListScrollview.transform.childCount; j++)
        {
            Destroy(npcQuestListScrollview.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < npcQuestCtrl.questList_Kill.Count; i++)
        {
            if (!npcQuestCtrl.questList_Kill[i].isCleared && !npcQuestCtrl.questList_Kill[i].isAccepted)
            {

                GameObject questName = Instantiate(questName_NPCPrefab, npcQuestListScrollview);
                questName.GetComponent<Text>().text = npcQuestCtrl.questList_Kill[i].questName;
                questName.GetComponent<QuestNameTextCtrl>().questKillNum = i;
            }
        }




        npcTalkPanel.SetActive(false);
        npcQuestListPanel.SetActive(true);

    }

    public void OffNPCQuestListPanel()
    {
        npcQuestListPanel.SetActive(false);
        npcTalkPanel.SetActive(true);
    }

    public void OnNPCQuestPanel(int questKillNum)
    {
        this.questKillNum = questKillNum;
        npcQuestText.text = npcQuestCtrl.questList_Kill[questKillNum].questContent;
        npcQuestListPanel.SetActive(false);
        npcQuestPanel.SetActive(true);
    }

    public void OnNPCQuestClearPanel()
    {
        for (int j = 0; j < npcQuestCtrl.questList_Kill.Count; j++)
        {
            for (int k = 0; k < playerQuests.questList_Kill.Count; k++)
            {
                if (npcQuestCtrl.questList_Kill[j].questName == playerQuests.questList_Kill[k].questName)
                {
                    GameObject questName = Instantiate(questName_NPCPrefab, npcQuestClearListScrollview);
                    questName.GetComponent<Text>().text = npcQuestCtrl.questList_Kill[j].questName;
                }
            }
        }
        npcQuestClearPanel.SetActive(true);
    }

    public void OnNPCStorePanel()
    {
        playerStat = playerCtrl.GetComponent<PlayerStat>();
        playerMoneyText.text = "가진 돈 : " + playerStat.Money.ToString();
        if (npcStoreFormat.itemList.Count != 0)
        {
            Transform[] formerItems = npcStoreScrollview.GetComponentsInChildren<Transform>();
            if (formerItems != null)
            {
                for (int i = 1; i < formerItems.Length; i++)
                {
                    if (formerItems[i] != npcStoreScrollview)
                    {
                        Destroy(formerItems[i].gameObject);
                    }
                }
            }
            for (int i = 0; i < npcStoreFormat.itemList.Count; i++)
            {
                GameObject storeBuyButton = Instantiate(storeBuyButtonPrefab, npcStoreScrollview);
                StoreBuyButtonCtrl storeBuyButtonCtrl = storeBuyButton.GetComponent<StoreBuyButtonCtrl>();
                storeBuyButtonCtrl.itemNumber = i;
                storeBuyButtonCtrl.itemPrice = npcStoreFormat.itemList[i].Price;


                storeBuyButton.transform.GetChild(1).GetComponent<Image>().sprite = npcStoreFormat.itemList[i].itemIcon;
                storeBuyButton.transform.GetChild(0).GetComponent<Text>().text = npcStoreFormat.itemList[i].Name +
                   "\t\t\t\t\t\t" + npcStoreFormat.itemList[i].Price;
            }
        }
        npcTalkPanel.SetActive(false);
        npcStorePanel.SetActive(true);
    }

    



    public void OnPlayerQuestListPanel(PlayerQuests playerQuests)
    {
        playerQuestListPanel.SetActive(!playerQuestListPanel.activeSelf);
    }

    public void OnPlayerQuestPanel(int questKillNum)
    {
        PlayerQuests playerQuests = playerCtrl.GetComponent<PlayerQuests>();
        playerQuestText.text = playerQuests.questList_Kill[questKillNum].questName +
            "\n" + playerQuests.questList_Kill[questKillNum].questContent +
            "\n현재 : " + playerQuests.questList_Kill[questKillNum].currentKillCount +
              "\n목표 : " + playerQuests.questList_Kill[questKillNum].targetKillCount;
        playerQuestPanel.SetActive(true);
    }

    

    public void OnPlayerInventoryPanel(PlayerItems playerItems)
    {
        playerInventoryPanel.SetActive(!playerInventoryPanel.activeSelf);
    }

    public void AddItem(ItemFormat itemFormat)
    {
        GameObject playerItemButton = Instantiate(playerItemButtonPrefab, playerInventoryScrollview);
        playerItemButton.transform.GetChild(0).GetComponent<Text>().text = itemFormat.Name;
        playerItemButton.transform.GetChild(1).GetComponent<Image>().sprite = itemFormat.itemIcon;
    }


    public void PlayerCloseQuestPanel()
    {
        playerQuestPanel.SetActive(false);
    }

    public void PlayerCloseInventoryPanel()
    {
        playerInventoryPanel.SetActive(false);
    }

    public void UseItem(int itemNumber)
    {
        playerCtrl.UseItem(itemNumber);
    }


    public void NPCCloseTalkPanel()
    {

        npcTalkPanel.SetActive(false);
        npcQuestButton.gameObject.SetActive(false);
        npcStoreButton.gameObject.SetActive(false);
        npcQuestClearButton.gameObject.SetActive(false);
        playerCtrl.isWithNPC = false;
    }

    public void NPCCloseStorePanel()
    {
        npcStorePanel.SetActive(false);
        npcStoreButton.gameObject.SetActive(false);
        playerCtrl.isWithNPC = false;
    }

    public void NPCDenyQuest()
    {
        npcQuestPanel.SetActive(false);
        npcQuestListPanel.SetActive(true);
    }

    public void NPCAcceptQuest()
    {
        npcQuestCtrl.questList_Kill[questKillNum].isAccepted = true;
        playerCtrl.GetComponent<PlayerQuests>().questList_Kill.Add(npcQuestCtrl.questList_Kill[questKillNum]);

        GameObject questName = Instantiate(questName_PlayerPrefab, playerQuestListScrollview);
        questName.GetComponent<Text>().text = npcQuestCtrl.questList_Kill[questKillNum].questName;
        questName.GetComponent<QuestNameTextCtrl_Player>().questKillNum = questKillNum;


        npcQuestButton.gameObject.SetActive(false);
        npcStoreButton.gameObject.SetActive(false);
        npcQuestPanel.SetActive(false);
        playerCtrl.isWithNPC = false;

    }


    public void NPCBuyItem(int itemNumber, int itemPrice)
    {
        if (playerStat.Money < itemPrice)
        {
            return;
        }

        playerCtrl.GetComponent<PlayerItems>().AddItem(npcStoreFormat.itemList[itemNumber]);
        playerStat.Money -= itemPrice;
        playerMoneyText.text = "가진 돈 : " + playerStat.Money.ToString();

    }


    public void GameOver(string gameOverText, bool isGameOver)
    {
        this.gameOverText.text = gameOverText;
        switch (isGameOver)
        {
            case true:
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                break;
            case false:
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }
                gameOverPanel.SetActive(!gameOverPanel.activeSelf);
                break;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void SwitchToTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGameScene()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
