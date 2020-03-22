using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {
    
    #region Compute Shader Structs

    private struct BoidData {
        public Vector3 position;
        public Vector3 velocity;

        public static int Size => sizeof(float) * 3 * 2;
    }

    #endregion

    #region Field Declarations

    private const int ThreadGroupSize = 1024;
    
    public ComputeShader boidComputeShader;
    
    public float awarenessRadius = 5;
    public float minDistance = 0.4f;
    public float alignmentRate = 0.01f;
    
    private Boid[] boids;
    private BoidData[] boidData;
    

    #endregion
    
    
    void Start(){
        //boids = FindObjectsOfType<Boid>();
    }

    void Update(){
        if (boids == null || boids.Length == 0) {
            boids = FindObjectsOfType<Boid>();
        }
        
        
        boidData = new BoidData[boids.Length];
        
        int kernelId = boidComputeShader.FindKernel("BoidCompute");
        
        boidComputeShader.SetInt("numBoids", boidData.Length);
        boidComputeShader.SetFloat("awarenessRadius", awarenessRadius);
        boidComputeShader.SetFloat("minDistance", minDistance);
        boidComputeShader.SetFloat("alignmentRate", alignmentRate);
        
        for (int i = 0; i < boidData.Length; i++) {
            boidData[i].position = boids[i].transform.position;
            boidData[i].velocity = boids[i].velocity;
        }
        
        ComputeBuffer boidDataBuffer = new ComputeBuffer(boidData.Length, BoidData.Size);
        boidDataBuffer.SetData(boidData);

        boidComputeShader.SetBuffer(kernelId, "boidData", boidDataBuffer);
        
        int threadGroups = Mathf.CeilToInt(boids.Length / (float) ThreadGroupSize);
        boidComputeShader.Dispatch(kernelId, threadGroups, 1, 1);

        boidDataBuffer.GetData(boidData);
        boidDataBuffer.Dispose();

        for (int i = 0; i < boids.Length; i++) {
            boids[i].velocity += boidData[i].velocity;
        }
        
    }
}