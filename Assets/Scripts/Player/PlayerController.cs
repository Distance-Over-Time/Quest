using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

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

    private void Start() {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
    }

    void Update() {
        MovePlayer();
    }

    public void MovePlayer() {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
