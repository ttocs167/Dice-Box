using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public LayerMask diceFaceLayer;
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
    
    private int? Evaluate()
    {
        // cast a ray down onto the centre of the dice from a position in world space 2m above the die
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out hit, 3, diceFaceLayer,
                QueryTriggerInteraction.Collide))
        {
            return hit.collider.GetComponent<DiceFace>().faceValue;
        }
        else return null;
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
