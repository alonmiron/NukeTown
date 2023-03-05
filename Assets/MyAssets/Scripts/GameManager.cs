using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI CurrentPlayerName;
    public TextMeshProUGUI CurrentPlayerTime;

    // Leader Board 1
    public TextMeshProUGUI FirstPlaceName;
    public TextMeshProUGUI FirstPlaceTime;
    public TextMeshProUGUI SecondPlaceName;
    public TextMeshProUGUI SecondPlaceTime;
    public TextMeshProUGUI ThirdPlaceName;
    public TextMeshProUGUI ThirdPlaceTime;
    public float firstplacetime;
    public float secondplacetime;
    public float thirdplacetime;

    // Leader Board 2
    public TextMeshProUGUI Room2_FirstPlaceName;
    public TextMeshProUGUI Room2_FirstPlaceTime;
    public TextMeshProUGUI Room2_SecondPlaceName;
    public TextMeshProUGUI Room2_SecondPlaceTime;
    public TextMeshProUGUI Room2_ThirdPlaceName;
    public TextMeshProUGUI Room2_ThirdPlaceTime;
    public float Room2_firstplacetime;
    public float Room2_secondplacetime;
    public float Room2_thirdplacetime;

    // Current Run stats
    public float CurrentRunScore;
    public int PlayerName;
    public float timeLimitForRoom = 900.00f;            // 900 second = 15 minutes
    private int RoomChoosen = 0;

    // Timer
    public float Timer = 0.0f;
    public bool RoomStarted;
    TimeSpan interval;

    // Sounds
    public AudioSource CityBackground;
    public AudioSource Sirens;
    public AudioSource Bang;
    public AudioSource RocketWhistle;
    public AudioSource RoomBackground;

    public GameObject Nuke;

    // Loading Screen
    public GameObject LoadingCanvas;
    private bool gameStarted = false;
    public GameObject city;

    // Room 1
    private bool buttonZeroClicked = false;
    private bool buttonOneClicked = false;
    private bool buttonTwoClicked = false;
    private bool doorHasBeenOpened = false;
    public Animator BottomDoorAnimator;
    public Animator UpperDoorAnimator;
    public GameObject BookDesk;
    private bool numOfBooksOnDeskIsBig = false;
    public Animator DrawerOneAnimator;
    public Animator DrawerTwoAnimator;
    public Animator DrawerThreeAnimator;
    public Animator DrawerFourAnimator;
    public GameObject EndingCanvas;
    private bool missionOneDone = false;
    private bool missionTwoDone = false;
    public GameObject CenterButtonOne;
    public GameObject CenterButtonTwo;
    public GameObject CenterButtonThree;
    public Material ButtonGreenMaterial;
    public Animator BookOneAnimator;
    public Animator BookTwoAnimator;
    public Animator BookThreeAnimator;

    // Game general
    private int currentSceneIndex;




    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            CityBackground.Play();
            CityBackground.volume = 0.4f;
        }
        else if(currentSceneIndex == 1 || currentSceneIndex == 2)
        {
            CityBackground.Play();
            CityBackground.volume = 0.01f;
            Sirens.volume = 0.01f;
        }

        LoadPlayerName();
        SetWatch();
        if (currentSceneIndex == 1)
        {
            LoadLeaderBoard_Room1();
        }
        if (currentSceneIndex == 2)
        {
            LoadLeaderBoard_Room2();
        }
        else
        {
            LoadLeaderBoard_Room1();
            LoadLeaderBoard_Room2();
        }

        // Just for testing watch
        StartRoom();

        //get script for collider in room 1
    //    GameObject collidingScript = BookDesk.GetComponent<CollidingScript>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            Bang.Play();
        }
        // Block of code for watch update
        if (RoomStarted == true)
        {
            if (Timer < timeLimitForRoom)
            {
                Timer += Time.deltaTime;
            }
            if (Timer < timeLimitForRoom)
            {
                CurrentPlayerTime.text = FloattoMin(Timer);
            }
        }

/*        if (Input.GetKeyDown(KeyCode.I))
        {
 //           BottomDoorAnimator.SetTrigger("Open");
 //           UpperDoorAnimator.SetTrigger("OpenUp");
            buttonZeroClicked = true;
            buttonOneClicked = true;
            buttonTwoClicked = true;
            openDoor();
        }*/

        // Check num of books on the table
        //   Debug.Log("num of books: " + BookDesk.GetComponent<CollidingScript>().numOfBooksOnDesk.ToString());

        // Playing room 
        if(currentSceneIndex == 1)
        {
            OpenDoorIfPossible();
            if (Input.GetKeyDown(KeyCode.I))
            {
                FinishMissionOne();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                FinishMissionTwo();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                FinishMissionThree();
            }
        }

        //  Debug.Log("numOfBooksOnDeskIsBig is evaluated to: ping ping   " + numOfBooksOnDeskIsBig.ToString());

    }

    void LoadPlayerName()
    {
        //For testing -  uncomment to reset player name (number)
        //PlayerPrefs.SetInt("PlayerName", 0);

        PlayerName = PlayerPrefs.GetInt("PlayerName", 0);
        PlayerName++;
        PlayerPrefs.SetInt("PlayerName", PlayerName);
    }

    void SetWatch()
    {
        CurrentPlayerName = GameObject.FindGameObjectWithTag("CurrentPlayerName").GetComponent<TextMeshProUGUI>();
        CurrentPlayerTime = GameObject.FindGameObjectWithTag("CurrentPlayerTimer").GetComponent<TextMeshProUGUI>();

        if (PlayerName < 10)
        {
            CurrentPlayerName.text = "#00" + PlayerName.ToString();
        }
        else if (PlayerName < 100)
        {
            CurrentPlayerName.text = "#0" + PlayerName.ToString();
        }
        else if (PlayerName < 1000)
        {
            CurrentPlayerName.text = "#" + PlayerName.ToString();
        }
        else
        {
            //reset players name in PlayerPrefs
            PlayerPrefs.SetInt("PlayerName", 1);
            CurrentPlayerName.text = "#001";

        }

        CurrentPlayerTime.text = Timer.ToString();
    }

    void LoadLeaderBoard_Room1()
    {
        FirstPlaceName = GameObject.FindGameObjectWithTag("FirstPlace").GetComponent<TextMeshProUGUI>();
        FirstPlaceTime = GameObject.FindGameObjectWithTag("FirstPlace-time").GetComponent<TextMeshProUGUI>();
        SecondPlaceName = GameObject.FindGameObjectWithTag("SecondPlace").GetComponent<TextMeshProUGUI>();
        SecondPlaceTime = GameObject.FindGameObjectWithTag("SecondPlace-time").GetComponent<TextMeshProUGUI>();
        ThirdPlaceName = GameObject.FindGameObjectWithTag("ThirdPlace").GetComponent<TextMeshProUGUI>();
        ThirdPlaceTime = GameObject.FindGameObjectWithTag("ThirdPlace-time").GetComponent<TextMeshProUGUI>();
    
        FirstPlaceName.text = PlayerPrefs.GetString("FirstPlace", "Unranked");
        firstplacetime = PlayerPrefs.GetFloat("FirstPlaceTime", timeLimitForRoom);
        FirstPlaceTime.text = FloattoMin(firstplacetime);

        SecondPlaceName.text = PlayerPrefs.GetString("SecondPlace", "Unranked");
        secondplacetime = PlayerPrefs.GetFloat("SecondPlaceTime", timeLimitForRoom);
        SecondPlaceTime.text = FloattoMin(secondplacetime);

        ThirdPlaceName.text = PlayerPrefs.GetString("ThirdPlace", "Unranked");
        thirdplacetime = PlayerPrefs.GetFloat("ThirdPlaceTime", timeLimitForRoom);
        ThirdPlaceTime.text = FloattoMin(thirdplacetime);

    }

    void LoadLeaderBoard_Room2()
    {

        Room2_FirstPlaceName = GameObject.FindGameObjectWithTag("Room2_FirstPlace").GetComponent<TextMeshProUGUI>();
        Room2_FirstPlaceTime = GameObject.FindGameObjectWithTag("Room2_FirsrtPlace-time").GetComponent<TextMeshProUGUI>();
        Room2_SecondPlaceName = GameObject.FindGameObjectWithTag("Room2_SecondPlace").GetComponent<TextMeshProUGUI>();
        Room2_SecondPlaceTime = GameObject.FindGameObjectWithTag("Room2_SecondPlace-time").GetComponent<TextMeshProUGUI>();
        Room2_ThirdPlaceName = GameObject.FindGameObjectWithTag("Room2_ThirdPlace").GetComponent<TextMeshProUGUI>();
        Room2_ThirdPlaceTime = GameObject.FindGameObjectWithTag("Room2_ThirdPlace-time").GetComponent<TextMeshProUGUI>();

        Room2_FirstPlaceName.text = PlayerPrefs.GetString("Room2_FirstPlace", "Unranked");
        Room2_firstplacetime = PlayerPrefs.GetFloat("Room2_FirstPlaceTime", timeLimitForRoom);
        Room2_FirstPlaceTime.text = FloattoMin(Room2_firstplacetime);

        Room2_SecondPlaceName.text = PlayerPrefs.GetString("Room2_SecondPlace", "Unranked");
        Room2_secondplacetime = PlayerPrefs.GetFloat("Room2_SecondPlaceTime", timeLimitForRoom);
        Room2_SecondPlaceTime.text = FloattoMin(Room2_secondplacetime);

        Room2_ThirdPlaceName.text = PlayerPrefs.GetString("Room2_ThirdPlace", "Unranked");
        Room2_thirdplacetime = PlayerPrefs.GetFloat("Room2_ThirdPlaceTime", timeLimitForRoom);
        Room2_ThirdPlaceTime.text = FloattoMin(Room2_thirdplacetime);

    }

    public void StartButton()
    {
        if (!gameStarted)
        {
            //StartCoroutine(DelayedBombExecution());
            RocketWhistle.Play();
            RocketWhistle.volume = 1;
            Sirens.Play();
            Sirens.volume = 1;
            Bang.PlayDelayed(5);
            Bang.volume = 1;

            Nuke.SetActive(true);
            gameStarted = true;
        }

    }

    IEnumerator DelayedBombExecution()
    {
        yield return new WaitForSeconds(5);
        Bang.Play();
        RocketWhistle.Play();
    }

    public void ExitButton()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    void StartRoom()
    {
        // Start Timer
        RoomStarted = true;

        //if (chooseroom 1){
        //    RoomChoosen = 1;
        //}
        //else
        //{
        //    RoomChoosen = 2;
        //}

        //Change Scene to one of the Rooms

    }

    void EndRoom()
    {
        CurrentRunScore = Timer;

        if (RoomChoosen == 1)
        {
            if (CurrentRunScore < firstplacetime)
            {
                PlayerPrefs.SetString("FirstPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("FirstPlaceTime", CurrentRunScore);
            }
            else if (CurrentRunScore < secondplacetime)
            {
                PlayerPrefs.SetString("SecondPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("SecondPlaceTime", CurrentRunScore);
            }
            else if (CurrentRunScore < thirdplacetime)
            {
                PlayerPrefs.SetString("ThirdPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("ThirdPlaceTime", CurrentRunScore);
            }
        }
        else if (RoomChoosen == 2)
        {
            if (CurrentRunScore < firstplacetime)
            {
                PlayerPrefs.SetString("Room2_FirstPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("Room2_FirstPlaceTime", CurrentRunScore);
            }
            else if (CurrentRunScore < secondplacetime)
            {
                PlayerPrefs.SetString("Room2_SecondPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("Room2_SecondPlaceTime", CurrentRunScore);
            }
            else if (CurrentRunScore < thirdplacetime)
            {
                PlayerPrefs.SetString("Room2_ThirdPlace", CurrentPlayerName.text);
                PlayerPrefs.SetFloat("Room2_ThirdPlaceTime", CurrentRunScore);
            }
        }
        

        // congratz message
        // play again?
    }

    public string FloattoMin(float time)
    {
        interval = TimeSpan.FromSeconds(time);
        return interval.ToString("mm") + ":" + interval.ToString("ss");
    }

    // Loading Rooms

    public void LoadRoomOne()
    {
        if (gameStarted)
        {
            LoadingCanvas.SetActive(true);
            city.SetActive(false);
            StartCoroutine(DelayedExecution(1));
        }
    }

    public void LoadRoomTwo()
    {
        if (gameStarted)
        {
            LoadingCanvas.SetActive(true);
            city.SetActive(false);
            StartCoroutine(DelayedExecution(2));
        }
    }

    // Code to execute after the delay of 4 seconds
    IEnumerator DelayedExecution(int sceneNumber)
    {
        yield return new WaitForSeconds(4);
        CityBackground.volume = 0.01f;
        SceneManager.LoadScene(sceneNumber);
    }

    // Room 1 Count Buttons clicked
    public void OnMissionOneButtonClicked0()
    {
        buttonZeroClicked = true;
        openDoor();
    }

    public void OnMissionOneButtonClicked1()
    {
        buttonOneClicked = true;
        openDoor();
    }

    public void OnMissionOneButtonClicked2()
    {
        buttonTwoClicked = true;
        openDoor();
    }
    private void openDoor()
    {
        if (buttonZeroClicked && buttonOneClicked && buttonTwoClicked)
        {
            missionOneDone = true;
        }
        if (buttonZeroClicked && buttonOneClicked && buttonTwoClicked && !doorHasBeenOpened && numOfBooksOnDeskIsBig)
        {
   //         Debug.Log("Got IN!");
            BottomDoorAnimator.SetTrigger("Open");
            UpperDoorAnimator.SetTrigger("OpenUp");
            doorHasBeenOpened = true;
            StartCoroutine(DelayedDoorDisabling());
        }
    }

    IEnumerator DelayedDoorDisabling()
    {
        yield return new WaitForSeconds(4);
        BottomDoorAnimator.enabled = false;
        UpperDoorAnimator.enabled = false;
    }

    public void OpenDrawerOne()
    {
        DrawerOneAnimator.SetTrigger("OpenDrawer");
        DisableDrawers();
    }

    public void OpenDrawerTwo()
    {
        DrawerTwoAnimator.SetTrigger("OpenDrawer");
        DisableDrawers();
    }
    
    public void OpenDrawerThree()
    {
        DrawerThreeAnimator.SetTrigger("OpenDrawer");
        DisableDrawers();
    }

    public void OpenDrawerFour()
    {
        DrawerFourAnimator.SetTrigger("OpenDrawer");
        DisableDrawers();
    }

    public void DisableDrawers()
    {
        StartCoroutine(DelayedDRawersDisabling());
    }

    IEnumerator DelayedDRawersDisabling()
    {
        yield return new WaitForSeconds(1);
/*        DrawerOneAnimator.enabled = false;*/
        DrawerTwoAnimator.enabled = false;
        DrawerThreeAnimator.enabled = false;
        DrawerFourAnimator.enabled = false;
    }

    public void EndButtonBad()
    {
        SceneManager.LoadScene(3);
    }

    public void EndButtonGood()
    {
        OpenDrawerOne();
    }

    public void OnKeyClicked()
    {
        EndingCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OpenDoorIfPossible()
    {
        if (BookDesk.GetComponent<CollidingScript>().numOfBooksOnDesk >= 3)
        {
            numOfBooksOnDeskIsBig = true;
            missionOneDone = true;
            missionTwoDone = true;
            openDoor();
            //     Debug.Log("numOfBooksOnDeskIsBig is evaluated to:  " + numOfBooksOnDeskIsBig.ToString());
        }
    }

    public void FinishMissionOne()
    {

        buttonZeroClicked = true;
        buttonOneClicked = true;
        buttonTwoClicked = true;
        ChangeMaterial(CenterButtonOne);
        buttonZeroClicked = true;
        ChangeMaterial(CenterButtonTwo);
        buttonOneClicked = true;
        ChangeMaterial(CenterButtonThree);
        buttonTwoClicked = true;
        openDoor();
    }

    public void FinishMissionTwo()
    {
        BookOneAnimator.SetTrigger("MoveBook1");
        BookTwoAnimator.SetTrigger("MoveBook2");
        BookThreeAnimator.SetTrigger("MoveBook3");
    }

    public void FinishMissionThree()
    {
        if(missionOneDone && missionTwoDone)
        {
            OpenDrawerOne();
            StartCoroutine(DelayedDRawersOpening());
        }
        
    }

    IEnumerator DelayedDRawersOpening()
    {
        yield return new WaitForSeconds(1);
        OnKeyClicked();
    }

    private void ChangeMaterial(GameObject gameObjectToChange)
    {
        Renderer renderer = gameObjectToChange.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = ButtonGreenMaterial;
        }
        else
        {
            Debug.LogWarning("Could not change material: GameObject has no Renderer component");
        }
    }
}
