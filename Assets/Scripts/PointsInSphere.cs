using UnityEngine;

public class PointsInSphere : MonoBehaviour {
    public Transform parent;
    public GameObject pointPrefab;
    public int numPoints;

    
    private void OnValidate(){
        GeneratePoints();
    }
    

    private void GeneratePoints(){
        for (int i = 0; i < numPoints; i++) {
            float dist = Mathf.Sqrt(i / (numPoints - 1f));
            float angle = 2 * Mathf.PI * GoldenRatio() * i;

            float x = dist * Mathf.Cos(angle);
            float y = dist * Mathf.Sin(angle);

            PlotPoint(x, y);
        }
    }

    private void PlotPoint(float x, float y){
        var lossyScale = pointPrefab.transform.lossyScale;
        
        Vector3 position = new Vector3(x * lossyScale.x, 0, y * lossyScale.y);
        GameObject point = Instantiate(pointPrefab, position, Quaternion.identity);
        point.transform.parent = parent;
    }

    private static float GoldenRatio(){
        return (1 + Mathf.Sqrt(5f)) / 2f;
    }
}