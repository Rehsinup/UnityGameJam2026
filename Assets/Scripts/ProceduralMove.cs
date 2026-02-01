using UnityEngine;

public class ProceduralMove : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
        {
            Debug.Log("Object Destroyed");
            Destroy(gameObject);
        }
    }
}
