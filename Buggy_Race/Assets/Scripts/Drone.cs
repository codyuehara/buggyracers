using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private bool movementEnabled {get; set;}
    public bool userControl;
    public float turnSpeed = 25;
    public float thrust = 50;
    public string name;
    CheckpointManager ctMgr;
    GameObject[] gates;
    
    void Start()
    {
        ctMgr = GetComponent<CheckpointManager>();
        gates = GameController.Instance.gates;
    }

    void Update()
    {
        if (movementEnabled)
        {
            if (userControl)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
        
                transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * thrust);
                transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
            }
            else
            {
                if (ctMgr.checkpoint + 1 < gates.Length-1)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, gates[ctMgr.checkpoint+1].transform.position, Time.fixedDeltaTime * thrust);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, gates[0].transform.position, Time.fixedDeltaTime * thrust);
                }

            } //end controller switch statement
        }
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }
    
    public void DisableMovement()
    {
        movementEnabled = false;
    }

}
