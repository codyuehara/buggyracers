using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public int id;
    private bool isFinishLine;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setFinishFlag()
    {
        isFinishLine = true;
    }
    
    /*
    void OnTriggerEnter(Collider other)
    {
    
        Debug.LogFormat("You have entered gate {0}", id);
        if (isFinishLine)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                other.gameObject.GetComponent<PlayerController>().checkpoint = 0;
                int lap = other.gameObject.GetComponent<PlayerController>().currentLap;
                if (lap == 2)
                {
                    Debug.Log("You have finished the race!");
                    Destroy(other.gameObject);
                }
                else 
                {
                    other.gameObject.GetComponent<PlayerController>().currentLap++;
                    Debug.LogFormat("Current lap is now: {0}", lap++);

                }
              
            }
        }
        else 
        {
            other.gameObject.GetComponent<PlayerController>().checkpoint++;
        }
        Debug.LogFormat("checkpoint is now: {0}", other.gameObject.GetComponent<PlayerController>().checkpoint);
        
    }
    */
    
    
}
