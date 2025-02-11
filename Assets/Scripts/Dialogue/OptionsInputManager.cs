using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsInputManager : MonoBehaviour
{
    private InputManager controls;

    public static OptionsInputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new InputManager();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    public Vector2 GetNavigationInput() => controls.Options.Navigate.ReadValue<Vector2>();
    public bool IsSubmitPressed() => controls.Options.Submit.WasPressedThisFrame();
}