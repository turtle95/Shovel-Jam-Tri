using UnityEngine;

/// <summary>
/// Pull everything within range.
/// </summary>
public class Gravity : MonoBehaviour
{
    [Tooltip("Distance from planet gravity stars to take effects.")]
    [SerializeField] private float _range;
    [Tooltip("Maximum gravity strength.")]
    [SerializeField] private float _maxStrength;
    [Tooltip("Minimum gravity strength within range.")]
    [SerializeField] private float _minStrength;
    
    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                Pull(col.gameObject);
            }
        }
    }

    /// <summary>
    /// Apply force to target.
    /// The closer target object is the stronger the gravity will be.
    /// </summary>
    /// <param name="obj">target</param>
    private void Pull(GameObject obj)
    {
        var objRigidbody = obj.GetComponent<Rigidbody>();
        if (objRigidbody == null)
            return;

        Vector3 direction = (transform.position - obj.transform.position).normalized;
        float distance = Vector3.Distance(transform.position, obj.transform.position);

        //Points: (0, max), (range, min)
        //actualStrength = slope * distance + maxStrength;
        float slope = (_minStrength - _maxStrength) / _range;
        float actualStrengh = Mathf.Clamp(slope * distance + _maxStrength, _minStrength, _maxStrength);
        
        objRigidbody.AddForce(objRigidbody.mass * direction * actualStrengh);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
#endif
}
