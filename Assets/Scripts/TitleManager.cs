using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{

    public GameObject howToPlayPanel;
    public Text howToPlayText;
    public TextAsset howToPlayTextAsset;

    private void Start()
    {
        Application.targetFrameRate = 60;
        howToPlayText.text = howToPlayTextAsset.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOffHowToPlayPanel()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
    }

    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
