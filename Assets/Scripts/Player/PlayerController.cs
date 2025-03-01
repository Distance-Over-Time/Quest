using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    public LayerMask terrainLayer;
    public float groundDist;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {        
        // Snap player to ground
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer)) {
            if (hit.collider != null) {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        FlipSprite();
    }

    // Currently snaps back to facing left
    public void FlipSprite() {
        if (move.x > 0) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }

    public void FaceNPC() {
        // TODO: have player sprite face NPC when talking to one
    }
}
