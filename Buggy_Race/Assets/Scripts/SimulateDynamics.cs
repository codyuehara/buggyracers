using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.IntegralTransforms;

public class SimulateDynamics : MonoBehaviour
{
    public GameObject cube;
    public Vector3[] states;
    private IntegratorRK4 integrator;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        Matrix<float> testmatrix = DenseMatrix.OfArray(new float[,] {
                                                                    {1,2,3,4},
                                                                    {5,6,7,8},
                                                                    {9,10,11,12}});
        Debug.Log("MATHNET.NUMERICS:   Matrix is " + testmatrix);
        
        DroneODE ode = new DroneODE();
        ode.SetParameters(10);
        ode.SetForce(new Vector3(0,0,1));
        
        Vector3 init_pos = new Vector3(-5,5,-5);
        Vector3 init_vel = new Vector3(0,0,0);
        Vector3 init_angVel = new Vector3(0,0,0);
        states = new Vector3[]{init_pos, init_vel, init_angVel};
        
        integrator = new IntegratorRK4(ode, 0.01f);
        
    }

    // Update is called once per frame
    void Update()
    {
        integrator.step(t, states);
        t += Time.fixedDeltaTime;
            Debug.Log("new set");
            for (int j = 0; j < 3; j++)
            {
                Debug.Log(states[j]);
            }
            cube.transform.position = states[0];

    }
}
