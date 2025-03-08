using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    
    public LayerMask terrainLayer;
    public float groundDist;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //audio
    private EventInstance playerFootsteps;

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }


    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);

    }

    private void Start() {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
    }

    void Update() {
        MovePlayer();
    }

    public void MovePlayer() {       

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

        
        PLAYBACK_STATE playbackState;
        playerFootsteps.getPlaybackState(out playbackState);

        if (movement.Equals(Vector3.zero)) {

            if (playbackState.Equals(PLAYBACK_STATE.PLAYING))
            {
                playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        else {
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }

                
        }

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