using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

    public int countdownTime;
    public Text countdownDisplay;
    
    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            
            yield return new WaitForSeconds(1f);
            
            countdownTime--;
        }
        
        countdownDisplay.text = "GO!";
        
        
        yield return new WaitForSeconds(1f);
        GameController.Instance.StartRace();
        countdownDisplay.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

 }
