using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTextCtrl : MonoBehaviour
{
    public List<TextAsset> loadingTextAssets;

    Text loadingText;

    void Start()
    {
        loadingText = GetComponent<Text>();
        loadingText.text = loadingTextAssets[Random.Range(0, loadingTextAssets.Count)].text;
    }


}
