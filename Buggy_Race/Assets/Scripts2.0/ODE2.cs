using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;


public abstract class ODE2 <T> // T here would probably be a double/float
{
    T[] x; //initial state
    T t; //initial time
    
    public virtual float[] xdot(T[] x, float t)
    {
        return new float[1];
    }
    
}
