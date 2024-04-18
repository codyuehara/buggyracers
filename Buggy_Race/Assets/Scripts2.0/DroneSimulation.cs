using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;


public class DroneSimulation : MonoBehaviour
{
    public GameObject drone;
    float[] states;
    float time;
    RK4 rk4;
    DroneODE2 ode;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(drone.transform.rotation);
        time = 0;
         // [px=0,py=1,pz=2,vx=3,vy=4,vz=5, qx=6,qy=7,qz=8,qw=9, wx=10, wy=11, wz=12]
        states = new float[13];
        states[0] = drone.transform.position.z;
        states[1] = -drone.transform.position.x;
        states[2] = drone.transform.position.y;
        states[9] = 1;
         //define ODE
        ode = new DroneODE2(1);
        //define integrator(start_time, end_time, dt)
        rk4 = new RK4(ode, 0.001f);
        //set initial conditions
        ode.AddThrusts(10,0,0,10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rk4.step(states, time);
        //if (time > 5) ode.AddThrusts(0,0,0,0);
       
        // need to convert to unity left hand y-up
        Vector3 pos = new Vector3(-states[1], states[2], states[0]);
        Quaternion rot = new Quaternion(-states[7], states[8], states[6], states[9]);
        
        Debug.Log(rot);
        drone.transform.position = pos;
        drone.transform.rotation = rot;
        
        time += Time.fixedDeltaTime;
        Debug.LogFormat("Time: {0}", time);
    }
}
