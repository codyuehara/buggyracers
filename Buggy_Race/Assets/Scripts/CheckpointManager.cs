using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public int checkpoint;
    private Rigidbody rb;
    private GameObject[] gates; 
    public Vector3 startPosition;
    public bool finishFlag; 
    private int currentLap;
    private Drone drone;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        currentLap = 0;
        checkpoint = -1;
        rb = GetComponent<Rigidbody>();
        gates = GameController.Instance.gates;
        drone = GetComponent<Drone>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("num gates: {0}", gates.Length);

        if (other.GetComponent<Gate>() != null)
        {
            //Debug.LogFormat("PASSED GATE: {0}", other.GetComponent<Gate>().id);
            //Debug.LogFormat("Current checkpoint: {0}", checkpoint);

            if (other.GetComponent<Gate>().id-1 == checkpoint) 
            {
                checkpoint++;
            }
            else if (other.GetComponent<Gate>().id == 0 && checkpoint == gates.Length-1)
            {
                if (currentLap < GameController.Instance.numLaps)
                {
                    checkpoint = 0;
                    currentLap++;
                    Debug.Log("Lap completed");
                }
                else
                {
                    //Destroy(this);      
                    finishFlag = true;     
                }
            }
            else
            {
                StartCoroutine(Respawn());
            }
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StartCoroutine(Respawn());
        }
        
    }
    
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
 	   ResetToCheckpoint();
 	   drone.DisableMovement();
 	   yield return new WaitForSeconds(1);
 	   drone.EnableMovement();
    }
    
    void ResetToCheckpoint()
    {
        if (checkpoint == -1)
        {
            //reset to starting position
            this.transform.position = startPosition;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            this.transform.position = gates[checkpoint].transform.position;
            this.transform.rotation = gates[checkpoint].transform.rotation;

        }

    }

}
