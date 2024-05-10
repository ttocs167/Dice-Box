using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public struct DiceFace
{
    public Vector3 FaceNormal;
    public int FaceValue;
}

public class Die : MonoBehaviour
{
    public DiceFace[] faces;
    
    private Rigidbody _rb;
    private bool _hasComeToRest = false;
    private MeshRenderer _meshRenderer;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void CheckRoll()
    {
        if (!_hasComeToRest && IsResting())
        {
            _hasComeToRest = true;
            
            int? faceValue = Evaluate();
            if (faceValue.HasValue)
            {
                Debug.Log("The dice landed on " + faceValue.Value);
            }
            
            Destroy(this.gameObject, 5f);
        }
    }
    
    public void Roll(float force, float torque, Vector3 direction)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
        _rb.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
        
        InvokeRepeating(nameof(CheckRoll), 0.5f, 0.5f);
    }
    
    /// <summary>
    /// check the array of faces to find the normal vector which most closely aligns with Vector3.up
    /// </summary>
    /// <returns>The value of the face most aligned with Vector3.up</returns>
    private int? Evaluate()
    {
        var closestAngle = 180f;
        var bestFace = new DiceFace();
        foreach (DiceFace face in faces)
        {
            var localFaceNormal = transform.TransformDirection(face.FaceNormal);
            var angle = Vector3.Angle(localFaceNormal, Vector3.up);
            
            if (!(angle < closestAngle)) continue;
            closestAngle = angle;
            bestFace = face;
        }
        return bestFace.FaceValue;
    }

    private bool IsResting()
    {
        return _rb.velocity.magnitude < 0.01f && _rb.angularVelocity.magnitude < 0.01f;
    }

    public void SetRandomColour()
    {
        var newColour = Random.ColorHSV(0, 1, .4f, 1, .8f, 1);
        _meshRenderer.material.color = newColour;
    }
}
