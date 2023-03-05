using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearBrowser : MonoBehaviour
{
    public GameObject endtext;
    public MeshRenderer screen1;
    public MeshRenderer screen2;
    public void OnClear()
    {
        screen1.gameObject.SetActive(false);
        screen2.gameObject.SetActive(true);
        endtext.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}
