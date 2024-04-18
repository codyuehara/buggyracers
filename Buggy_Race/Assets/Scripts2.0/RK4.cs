using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

public class RK4
{
    ODE2<float> eq;
    float h;
    
    public RK4(ODE2<float> eq, float h)
    {
        this.eq = eq;
        this.h = h;
    }
    
    // need a list of vectors
    public void step(float[] x_k, float t_k)
    {
        float[] temp_k1 = new float[x_k.Length];
        float[] temp_k2 = new float[x_k.Length];
        float[] temp_k3 = new float[x_k.Length];
        float[] temp_k4 = new float[x_k.Length];
    
        float[] k1 = eq.xdot(x_k, t_k);
        for (int i = 0; i < x_k.Length; i++)
        {
            temp_k1[i] = x_k[i] + h * k1[i]/2;
        }
        float[] k2 = eq.xdot(temp_k1, t_k + h/2);
        for (int i = 0; i < x_k.Length; i++)
        {
            temp_k2[i] = x_k[i] + h * k2[i]/2;
        }
        float[] k3 = eq.xdot(temp_k2, t_k + h/2);
        for (int i = 0; i < x_k.Length; i++)
        {
            temp_k3[i] = x_k[i] + h * k3[i];
        }

        float[] k4 = eq.xdot(temp_k3, t_k + h);
        
        for (int i = 0; i < x_k.Length; i++)
        {
            x_k[i] = x_k[i] + h/6 *(k1[i] + 2*k2[i] + 2*k3[i] + k4[i]);       
        }

    }
       
    
}

