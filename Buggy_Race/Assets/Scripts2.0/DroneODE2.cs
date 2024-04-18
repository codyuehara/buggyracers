using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;
using System;


public class DroneODE2 : ODE2<float>
{
    //public float[] x_dot;
    private Vector<float> force;
    private Vector<float> torque;
    private float mass = 0.85f;
    private float l = 0.15f; //arm length
    private Matrix<float> drag;
    private Matrix<float> J;
    private Vector<float> g;
    private Vector<float> u;
    //private float ct = 0.022f;
    private float ct = 1f;
    private float w_max = 10;


    public DroneODE2(float m)
    {
      
        var M = Matrix<float>.Build;
        drag = M.DenseOfDiagonalArray(new[]{0.5f, 0.25f, 0});
        
        var M2 = Matrix<float>.Build;
        J = M2.DenseOfDiagonalArray(new[]{2.5f, 2.1f, 4.3f});
        
        mass = m;
        
        force = DenseVector.OfArray(new[]{0f,0f,0f});
        torque = DenseVector.OfArray(new[]{0f,0f,0f});
        g = DenseVector.OfArray(new[]{0f,0f,-9.81f});

    }
    
    public void AddThrusts(float f1, float f2, float f3, float f4)
    {
        force[2] = f1 + f2 + f3 + f4;
        torque[0] = l / (float)Math.Sqrt(2) * (f1 + f2 - f3 - f4);
        torque[1] = l / (float)Math.Sqrt(2) * (-f1 + f2 + f3 - f4);
        torque[2] = ct * (f1 - f2 + f3 - f4);
    }
    
    public Matrix<float> QuaternionToRotationMatrix(float x, float y, float z, float w)
    {
        Matrix<float> M = Matrix<float>.Build.Dense(3,3);
        M[0,0] = 1 - 2 * (y*y + z*z);
        M[0,1] = 2 * (x*y - w*z);
        M[0,2] = 2 * (w * y + x*z);
        M[1,0] = 2 * (x*y + w*z);
        M[1,1] = 1 - 2 * (x*x + z*z);
        M[1,2] = 2 * (y*z - w*x);
        M[2,0] = 2 * (x*z - w*y);
        M[2,1] = 2 * (w*x + y*z);
        M[2,2] = 1 - 2 * (x*x + y*y);
        return M;
    }
    
    public Matrix<float> SkewSymmetric(float x, float y, float z)
    {
        Matrix<float> M = Matrix<float>.Build.Dense(4,4);
        M[0,1] = -x;
        M[0,2] = -y;
        M[0,3] = -z;
        M[1,0] = x;
        M[1,2] = z;
        M[1,3] = -y;
        M[2,0] = y;
        M[2,1] = -z;
        M[2,3] = x;
        M[3,0] = z;
        M[3,1] = y;
        M[3,2] = -x;
        return M;
    }
    
    public Vector<float> Cross(Vector<float> left, Vector<float> right)
    {
        Vector<float> v = Vector<float>.Build.Dense(3,3);
        v[0] = left[1] * right[2] - left[2] * right[1];
        v[1] = -left[0] * right[2] + left[2] * right[0];
        v[2] = left[0] * right[1] - left[1] * right[0];
        return v;
    }
    
    public float Magnitude(float x, float y, float z)
    {
        return (float)Math.Sqrt(x*x + y*y + z*z);
    }

    // [px=0,py=1,pz=2,vx=3,vy=4,vz=5, qx=6,qy=7,qz=8,qw=9, wx=10, wy=11, wz=12]
    public override float[] xdot(float[] x, float t)
    {
        float[] x_dot = new float[x.Length];
        //Debug.LogFormat("qx: {0}, qy: {1}, qz: {2}, qw: {3}", x[6], x[7], x[8], x[9]);
        float L2Norm = (float)Math.Sqrt(x[6]*x[6] + x[7]*x[7] + x[8]*x[8] + x[9]*x[9]);
        
        Vector<float> q = DenseVector.OfArray(new[] {x[9], x[6], x[7], x[8]});
        q = q / L2Norm;
        //Debug.LogFormat("unit quaternion: {0}", (float)Math.Sqrt(q[0]*q[0] + q[1]*q[1] + q[2]*q[2] + q[3]*q[3]));
        //Debug.LogFormat("Quaternion: {0}", q);
        
        Matrix<float> Rb = QuaternionToRotationMatrix(q[1], q[2], q[3], q[0]).Transpose();
        
        Vector<float> pos = DenseVector.OfArray(new[]{x[0], x[1], x[2]});
        Vector<float> vel = DenseVector.OfArray(new[]{x[3], x[4], x[5]});
               
        Vector<float> angVel = DenseVector.OfArray(new[] {x[10], x[11], x[12]});

        Vector<float> acc = g + 1/mass * Rb * force - Rb * drag * Rb.Transpose() * vel;
        
        //velocity
        x_dot[0] = x[3];
        x_dot[1] = x[4];
        x_dot[2] = x[5];
        
        //acceleration
        x_dot[3] = acc[0];
        x_dot[4] = acc[1];
        x_dot[5] = acc[2];

        //calculation for qdot
        Matrix<float> angVelHat = SkewSymmetric(angVel[0], angVel[1], angVel[2]); 
        Vector<float> qdot = 0.5f * angVelHat * q;
        
 	  // quaternion
        x_dot[6] = qdot[1];
        x_dot[7] = qdot[2];
        x_dot[8] = qdot[3];
        x_dot[9] = qdot[0]; 
   
        //ang acceleration = J.Inverse(torque - w.Cross(J * w))
        Vector<float> angAcc = J.Inverse() * (torque - Cross(angVel, (J * angVel)));
        
        x_dot[10] = angAcc[0];
        x_dot[11] = angAcc[1];
        x_dot[12] = angAcc[2];
        
        return x_dot;
    }
}
