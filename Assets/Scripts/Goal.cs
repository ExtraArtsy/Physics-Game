using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Goal : MonoBehaviour
{

    private GameObject victoryUIHolder;
    private bool hasTriggered = false;


    private void Start()
    {
        GameObject mainCanvas = GameObject.Find("VictoryCanvas");

        if (mainCanvas != null)
        {
           Transform holderTransform = mainCanvas.transform.Find("VictoryUIHolder");
           if (holderTransform != null)
           {
               victoryUIHolder = holderTransform.gameObject;
           }
        }

        if(victoryUIHolder == null)
        {
            Debug.LogWarning("Victory UI Holder is not found in the scene. Please ensure it exists and is named correctly.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Game Won!");

        if(victoryUIHolder != null)
        {
            victoryUIHolder.SetActive(true);
        }
       
        Time.timeScale = 0f;
    }
}
