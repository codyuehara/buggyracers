using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;

public abstract class ODE
{
    Vector3[] x0;
    float t0;
    
    public virtual Vector3[] func(Vector3[] x, float t)
    {
        Vector3[] v = new Vector3[3];
        return v;
    }
}
