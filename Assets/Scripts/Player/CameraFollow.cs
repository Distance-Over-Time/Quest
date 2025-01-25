using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float camX = 0;
    public float camY = 1;
    public float camZ = -5;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(camX, camY, camZ);
    }
}
