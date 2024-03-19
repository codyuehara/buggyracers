using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

public class IntegratorRK4
{
    ODE eq;
    float h;
    
    public IntegratorRK4(ODE eq, float h)
    {
        this.eq = eq;
        this.h = h;
    }
    
    public void step(float t_k, Vector3[] x_k)
    {
        Vector3[] temp_k1 = new Vector3[3];
        Vector3[] temp_k2 = new Vector3[3];
        Vector3[] temp_k3 = new Vector3[3];
        Vector3[] temp_k4 = new Vector3[3];
    
        Vector3[] k1 = eq.func(x_k, t_k);
        for (int i = 0; i < 3; i++)
        {
            temp_k1[i] = x_k[i] + h * k1[i]/2;
        }
        Vector3[] k2 = eq.func(temp_k1, t_k + h/2);
        for (int i = 0; i < 3; i++)
        {
            temp_k2[i] = x_k[i] + h * k2[i]/2;
        }
        Vector3[] k3 = eq.func(temp_k2, t_k + h/2);
        for (int i = 0; i < 3; i++)
        {
            temp_k3[i] = x_k[i] + h * k3[i];
        }

        Vector3[] k4 = eq.func(temp_k3, t_k + h);
        
        Vector3[] temp = new Vector3[3];
        
        for (int i = 0; i < 3; i++)
        {
            x_k[i] = x_k[i] + h/6 *(k1[i] + 2*k2[i] + 2*k3[i] + k4[i]);       
        }

    }
       
    
}
