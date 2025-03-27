using UnityEngine;
public class PauseButton : MonoBehaviour
{
    public PauseMenu pauseMenu;

    public void OnPauseButtonClick()
    {
        pauseMenu.Pause();
    }
}