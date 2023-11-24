using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    // Reference to the audio source you want to mute.
    public AudioSource audioSource;

    // Boolean to track whether the audio is currently muted.
    private bool isMuted = false;

    // Reference to the button component.
    private Button button;

    private void Start()
    {
        // Get the button component from the GameObject.
        button = GetComponent<Button>();

        // Attach the button click listener.
        button.onClick.AddListener(ToggleMute);
    }

    // Function to toggle between mute and unmute.
    private void ToggleMute()
    {
        // Toggle the mute state.
        isMuted = !isMuted;

        // Mute or unmute the audio based on the state.
        audioSource.mute = isMuted;
    }
}