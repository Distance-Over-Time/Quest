using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    private void FixedUpdate()
    {
        UpdateSound();
    }

    public void MovePlayer() {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    // audio
    private EventInstance playerFootsteps;

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);
    }

    private void UpdateSound()
    {
        // start footsteps event if the player has an x velocity and is on the ground
        if (RenderBuffer.velocity.x !=0 && isGrounded)
        {
            // get the playback state
            PLAYBACK_STATE playerbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (PLAYBACK_STATE.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        // otherwise, stop the footsteps event
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
