using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Parabox.CSG;


[RequireComponent(typeof(MeshCollider))]

public class UnionObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curPosition;
    public GameObject SpawnLocation;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        
    }

    private void OnMouseExit()
    {

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider>().isTrigger = false;
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            return;
        }
        Model result = CSG.Union(gameObject, other.gameObject);
        var composite = new GameObject();
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        composite.AddComponent<MeshCollider>();
        composite.AddComponent<Rigidbody>().isKinematic = true;

        composite.GetComponent<MeshCollider>().isTrigger = false;

        composite.transform.position = SpawnLocation.transform.position;
        composite.AddComponent<UnionObject>();

        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
        return;
    }

    */
}
