using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {
    #region Compute Shader Structs

    private struct BoidData {
        public Vector3 position;
        public Vector3 velocity;

        public Vector3 perceivedCentre;
        public Vector3 perceivedVelocity;
        public Vector3 displacement;

        public static int Size => sizeof(float) * 3 * 5;
    }

    #endregion

    #region Field Declarations

    private const int ThreadGroupSize = 1024;

    public BoidSettings boidSettings;
    public BoidGrid boidGrid;
    public ComputeShader boidComputeShader;

    private Boid[] _boids;
    private BoidData[] _boidData;

    #endregion

    void Start(){
        _boids = FindObjectsOfType<Boid>();
        
        foreach (var boid in _boids) {
            boid.Initialize(boidSettings);
        }
        //boidGrid.AddAll(_boids);
    }

    void Update(){
        if (_boids == null || _boids.Length == 0) return;

        _boidData = new BoidData[_boids.Length];

        int kernelId = boidComputeShader.FindKernel("BoidCompute");

        boidComputeShader.SetInt("numBoids", _boidData.Length);
        boidComputeShader.SetFloat("awarenessRadius", boidSettings.awarenessRadius);
        boidComputeShader.SetFloat("minDistance", boidSettings.minDistance);
        boidComputeShader.SetFloat("alignmentRate", boidSettings.alignmentRate);

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
            /*
            _boids[i].velocity += _boidData[i].velocity;
            _boids[i].velocity = Vector3.ClampMagnitude(_boids[i].velocity, boidSettings.maxVelocity);
            */
            _boids[i].SetForces(_boidData[i].perceivedCentre, _boidData[i].perceivedVelocity,
                _boidData[i].displacement);

            //Vector3 oldPosition = _boids[i].transform.position;
            
            _boids[i].UpdateBoid();

            //boidGrid.Move(_boids[i], oldPosition);
        }
    }
}