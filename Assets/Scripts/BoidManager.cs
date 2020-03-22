using System;
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

    public BoidGrid boidGrid;
    public ComputeShader boidComputeShader;
    
    public float awarenessRadius = 5;
    public float minDistance = 0.4f;
    public float alignmentRate = 0.01f;
    
    private Boid[] _boids;
    private BoidData[] _boidData;

    #endregion

    private void Awake(){
        //_boidGrid = new BoidGrid();
    }


    void Start(){
        _boids = FindObjectsOfType<Boid>();
    }

    void Update(){
        if (_boids == null || _boids.Length == 0) return;


        _boidData = new BoidData[_boids.Length];
        
        int kernelId = boidComputeShader.FindKernel("BoidCompute");
        
        boidComputeShader.SetInt("numBoids", _boidData.Length);
        boidComputeShader.SetFloat("awarenessRadius", awarenessRadius);
        boidComputeShader.SetFloat("minDistance", minDistance);
        boidComputeShader.SetFloat("alignmentRate", alignmentRate);
        
        for (int i = 0; i < _boidData.Length; i++) {
            _boidData[i].position = _boids[i].transform.position;
            _boidData[i].velocity = _boids[i].velocity;
        }
        
        ComputeBuffer boidDataBuffer = new ComputeBuffer(_boidData.Length, BoidData.Size);
        boidDataBuffer.SetData(_boidData);

        boidComputeShader.SetBuffer(kernelId, "boidData", boidDataBuffer);
        
        int threadGroups = Mathf.CeilToInt(_boids.Length / (float) ThreadGroupSize);
        boidComputeShader.Dispatch(kernelId, threadGroups, 1, 1);

        boidDataBuffer.GetData(_boidData);
        boidDataBuffer.Dispose();

        for (int i = 0; i < _boids.Length; i++) {
            _boids[i].velocity += _boidData[i].velocity;
            
            _boids[i].UpdateBoid();
        }
        
    }
}