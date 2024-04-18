using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] gates;

    public static GameManager Instance { get; private set; } 

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
    } 


    // Start is called before the first frame update
    void Start()
    {
        gates = GameObject.FindGameObjectsWithTag("Gate");
        foreach (GameObject g in gates)
        {
            Debug.Log(g.transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
