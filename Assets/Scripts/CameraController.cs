using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Player reference
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Offset is camera position minus player position
        offset = transform.position - player.transform.position;
    }

    // Update After all computation is done every frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
