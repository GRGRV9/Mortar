using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    GameObject gameOverMenu;

    [SerializeField]
    GameObject joystick;

    [SerializeField]
    GameObject mainRestartButton;

    [SerializeField]
    GameObject mainQuitButton;

    [SerializeField]
    GameObject player;

    bool gameIsOver;

    private void Start()
    {
        playerCamera.GetComponent<Animator>().SetBool("GameOver", false);
        gameOverMenu.SetActive(false);
        joystick.SetActive(true);
        mainQuitButton.SetActive(true);
        mainRestartButton.SetActive(true);
        player.SetActive(true);
        gameIsOver = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        if (!gameIsOver)
        {
            player.GetComponent<PlayerController>().PlayerDying();
            gameOverMenu.SetActive(true);
            joystick.SetActive(false);
            mainQuitButton.SetActive(false);
            mainRestartButton.SetActive(false);
            playerCamera.GetComponent<Animator>().SetBool("GameOver", true);
            Debug.Log("GameOver");
            gameIsOver = true;
        }        
    }
}
