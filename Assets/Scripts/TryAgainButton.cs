using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    private Button tryAgainButton;

    void Start()
    {
        // Get a reference to the Button component
        tryAgainButton = GetComponent<Button>();

        // Add a listener for the button click event
        tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
    }

    // Method to handle the button click event
    void OnTryAgainButtonClick()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
