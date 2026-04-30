using UnityEngine;
using UnityEngine.UI;
using Spyro.MainGame;

public class PauseButton : MonoBehaviour
{
    public PauseMenu pauseMenu;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            Color transparentColor = buttonImage.color;
            transparentColor.a = 0f;
            buttonImage.color = transparentColor;
        }
    }

    public void OnPauseButtonClick()
    {
        pauseMenu.Pause();
    }
}
