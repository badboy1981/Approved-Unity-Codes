using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveOnTheSplinePath : MonoBehaviour
{
    public SplineContainer m_Target;
    public float ObjectPositionOnPath;
    public float Speed = 1;
    public Vector3 OffsetPosition;
    public Vector3 position;
    Quaternion rotation, axisRemapRotation;

    void Start() { }
    void Update()
    {
        ObjectPositionOnPath += 0.0005f * Speed;
        if (ObjectPositionOnPath > 1) { ObjectPositionOnPath = 0; }
        EvaluatePositionAndRotation();
    }
    private void LateUpdate()
    {
        transform.SetLocalPositionAndRotation(position, rotation);
        transform.Translate(OffsetPosition);
    }
    void EvaluatePositionAndRotation()
    {
        position = m_Target.EvaluatePosition(ObjectPositionOnPath);
        //rotation = Quaternion.identity;
        Vector3 forward = Vector3.forward;
        Vector3 up = Vector3.up;
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(forward, up));
        forward = Vector3.Normalize(m_Target.EvaluateTangent(ObjectPositionOnPath));
        up = m_Target.EvaluateUpVector(ObjectPositionOnPath);
        rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
    }
}