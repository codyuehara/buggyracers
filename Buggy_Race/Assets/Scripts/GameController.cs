using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject[] gates;
    private GameObject[] drones;
    public string[] standings;
    public int numLaps = 2;
    private bool allFinished;
    public bool endRace;
    private int raceIdx;
    
    public static GameController Instance { get; private set; } 

    void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    
        raceIdx = 0;
        GetDrones();
        GetGates();  
    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFinished();
    }
    
    void CheckFinished()
    {
        allFinished = true;
        foreach (GameObject d in drones)
        {
            if (d.GetComponent<CheckpointManager>() != null && !d.GetComponent<CheckpointManager>().finishFlag)
            {
                allFinished = false;
            }
            else if (d.GetComponent<Drone>() != null && !standings.Contains(d.GetComponent<Drone>().name))
            {
                standings[raceIdx] = d.GetComponent<Drone>().name;
                raceIdx++;
            }
        }
        
        if (allFinished)
        {
            EndRace();
        }

    }
    
    public void StartRace()
    {
         foreach (GameObject d in drones)
         {
             if (d.GetComponent<Drone>() != null)
             {
                 d.GetComponent<Drone>().EnableMovement();
             }
         }
    }
    
    public void EndRace()
    {
        endRace = true;
    }
    
    void GetDrones()
    {
        drones = GameObject.FindGameObjectsWithTag("Drone");   
        standings = new string[drones.Length];
    }
    
    void GetGates()
    {
        gates = GameObject.FindGameObjectsWithTag("Gate");
        List<GameObject> sortedGates = gates.OrderBy(o=>o.transform.GetChild(0).gameObject.GetComponent<Gate>().id).ToList();
        for (int i = 0; i < sortedGates.Count; i++)
        {
            GameObject child = sortedGates[i].transform.GetChild(0).gameObject;
            Gate gate = child.GetComponent<Gate>();
 		 //set finish line flag
 		 //if (i == sortedGates.Count-1)
 		 if (i ==0)
 		 {
 		     gate.setFinishFlag();
 		 }
        }

    }
}
