using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Text PressAToJoinPlayer1;
    [SerializeField]
    Text PressAToJoinPlayer2;
    [SerializeField]
    Text PressAToJoinPlayer3;
    [SerializeField]
    Text PressAToJoinPlayer4;
    
    [SerializeField]
    Text StartInText;

    bool PlayerOneIsReady = false;
    bool PlayerTwoIsReady = false;
    bool PlayerThreeIsReady = false;
    bool PlayerFourIsReady = false;
    int nbOfReadyPlayers = 0;

    float Countdown = 4;
    private bool PressedStart = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("J1Abutton") && !PlayerOneIsReady)
        {
            PlayerOneIsReady = true;
            PressAToJoinPlayer1.text = "Ready!";
            nbOfReadyPlayers += 1;
            print("1");
        }

        if (Input.GetButtonDown("J2Abutton") && !PlayerTwoIsReady)
        {
            PlayerTwoIsReady = true;
            PressAToJoinPlayer2.text = "Ready!";
            nbOfReadyPlayers += 1;
        }

        if (Input.GetButtonDown("J3Abutton") && !PlayerThreeIsReady)
        {
            PlayerThreeIsReady = true;
            PressAToJoinPlayer3.text = "Ready!";
            nbOfReadyPlayers += 1;
            print("3");
        }

        if (Input.GetButtonDown("J4Abutton") && !PlayerFourIsReady)
        {
            PlayerFourIsReady = true;
            PressAToJoinPlayer4.text = "Ready!";
            nbOfReadyPlayers += 1;
        }

        if (Input.GetButtonDown("StartAnyController"))
        {
            PressedStart = true;
        }



        if (nbOfReadyPlayers >= 2 && !PressedStart)
        {
            StartInText.text = "Press Start to launch the game!";
        }
        else if (PressedStart)
        {
            StartInText.text = "Start in " + (int)Countdown;
            Countdown -= Time.deltaTime;
        }
        else
        {
            Countdown = 4;
            StartInText.text = " Waiting for players";
        }

        if (Countdown < 1)
            SceneManager.LoadScene("MoveTest");

        

    }

    /*public void Quit()
    {
        Application.Quit();
    }*/

}
