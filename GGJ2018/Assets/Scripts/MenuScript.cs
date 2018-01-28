using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Transform PressAToJoinPlayer1;
    [SerializeField]
	Transform PressAToJoinPlayer2;
    [SerializeField]
	Transform PressAToJoinPlayer3;
    [SerializeField]
	Transform PressAToJoinPlayer4;
    
    [SerializeField]
    Text StartInText;

    [FMODUnity.EventRef]
    private string threeTwoOne_sfx = "event:/GGJ_2018_3_2_1";
    [FMODUnity.EventRef]
    private string addPlayer_sfx = "event:/GGJ_2018_ADD_PLAYER";
	[FMODUnity.EventRef]
	private string pressStart_sfx = "event:/GGJ_2018_START";



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
			PressAToJoinPlayer1.GetChild(0).gameObject.SetActive(true);
			PressAToJoinPlayer1.GetChild(1).GetComponent<Text>().text = "Ready!";
            AddingPlayer();
			FMODUnity.RuntimeManager.PlayOneShot(addPlayer_sfx, Vector3.zero);
        }

        if (Input.GetButtonDown("J2Abutton") && !PlayerTwoIsReady)
        {
            PlayerTwoIsReady = true;
			PressAToJoinPlayer2.GetChild(0).gameObject.SetActive(true);
			PressAToJoinPlayer2.GetChild(1).GetComponent<Text>().text = "Ready!";
            AddingPlayer();
			FMODUnity.RuntimeManager.PlayOneShot(addPlayer_sfx, Vector3.zero);
        }

        if (Input.GetButtonDown("J3Abutton") && !PlayerThreeIsReady)
        {
            PlayerThreeIsReady = true;
			PressAToJoinPlayer3.GetChild(0).gameObject.SetActive(true);
			PressAToJoinPlayer3.GetChild(1).GetComponent<Text>().text = "Ready!";
            AddingPlayer();
			FMODUnity.RuntimeManager.PlayOneShot(addPlayer_sfx, Vector3.zero);
        }

        if (Input.GetButtonDown("J4Abutton") && !PlayerFourIsReady)
        {
            PlayerFourIsReady = true;
			PressAToJoinPlayer4.GetChild(0).gameObject.SetActive(true);
			PressAToJoinPlayer4.GetChild(1).GetComponent<Text>().text = "Ready!";
            AddingPlayer();
			FMODUnity.RuntimeManager.PlayOneShot(addPlayer_sfx, Vector3.zero);
        }

        if (Input.GetButtonDown("StartAnyController") && !PressedStart)
        {
            PressedStart = true;
			FMODUnity.RuntimeManager.PlayOneShot(pressStart_sfx, Vector3.zero);
         	FMODUnity.RuntimeManager.PlayOneShot(threeTwoOne_sfx, Vector3.zero);
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

    void AddingPlayer ()
    {
            nbOfReadyPlayers += 1;
            FMODUnity.RuntimeManager.PlayOneShot(addPlayer_sfx, Vector3.zero);
    }

    /*public void Quit()
    {
        Application.Quit();
    }*/

}
