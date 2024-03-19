using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceStandings : MonoBehaviour
{
    public Text standingsText;
    public GameObject standingsPanel;
    private string[] standings;
    
    // Start is called before the first frame update
    void Start()
    {
        standings = GameController.Instance.standings;
    }

    // Update is called once per frame
    void Update()
    {
                
        if (GameController.Instance.endRace)
        {
            standingsText.text = "";
            for (int i = 0; i < standings.Length; i++)
            {
                standingsText.text += "[" + (i+1).ToString() + "]: " + standings[i] + "\n";
            }

            standingsPanel.SetActive(true);
            StartCoroutine(Delay());
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

    }
    
}
