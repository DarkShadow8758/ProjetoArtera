using UnityEngine;

public class RayCastExample : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 20f, layerMask))
        {
            //Debug.Log("Hit Something!");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
        }
         else
        {
            //Debug.Log("Hit Nothing.");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
        }
    }
   
}
