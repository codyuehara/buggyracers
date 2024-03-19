using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;


public class DroneODE : ODE
{
    private float m;
    private Vector3 g = new Vector3(0,0,-9.81f);
    private Vector3 force;

    public void SetParameters(float m)
    {
        this.m = m;
    }
    
    public void SetForce(Vector3 f)
    {
        force = f;
    }

    public override Vector3[] func(Vector3[] x, float t)
    {
        //x[0] = pos vector
        //x[1] = vel vector
        //x[2] = ang vel vector
        
        Vector3[] x_dot = new Vector3[]{Vector3.zero, Vector3.zero, Vector3.zero};
        
        x_dot[0] = x[1];
        x_dot[1] = 1/m * force;
        x_dot[2] = Vector3.zero;
        //x_dot[2] = 
        
        return x_dot;
        
    }
}
