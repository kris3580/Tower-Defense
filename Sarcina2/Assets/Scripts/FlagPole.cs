using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public static bool isFlagPoleTargeted;

    [SerializeField] Vector3 taskLoadingCirclePositionOffset;
    public TaskLoadingCircle taskLoadingCircle;
    private GameObject taskLoadingCircleObject;
    private bool isPlayerInRange = false;
    private GameObject player;
    private bool isFlagPoleTaken;
    private Vector3 flagPoleOffset = new Vector3(0, 1, -0.8f);
    private GameObject joystick;
    private bool joystickStateWhenFlagPoleIsTaken;
    


    private void Update()
    {
        if (isPlayerInRange && taskLoadingCircle.isTimeOut)
        {
            isPlayerInRange = false;
            isFlagPoleTaken = true;
            joystickStateWhenFlagPoleIsTaken = joystick.activeSelf;
            StartCoroutine(CheckForPlayerStopping());
        }

        if (isFlagPoleTaken)
        {
            transform.position = player.transform.position + flagPoleOffset;
        }
    }


    private void Awake()
    {
        taskLoadingCircle = GameObject.Find("TaskLoadingCircle").GetComponent<TaskLoadingCircle>();
        taskLoadingCircleObject = GameObject.Find("TaskLoadingCircle");
        player = GameObject.Find("Player");
        joystick = GameObject.Find("Joystick").transform.Find("Background").gameObject;

    }

    private BuildingBase building;

    private void Start()
    {
        building = gameObject.transform.parent.transform.parent.GetComponent<BuildingBase>();
    }


    private IEnumerator CheckForPlayerStopping()
    {
        yield return new WaitForSeconds(1f);

    restart:
        yield return new WaitForSeconds(0.1f);
        if (!joystick.activeSelf)
        {
            isFlagPoleTaken = false;
            transform.position = player.transform.position + flagPoleOffset + new Vector3(0, -0.5f, 0);
            building.AlliesSetup();
            yield break;
        }


        goto restart;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = true;
            isFlagPoleTargeted = true;
            taskLoadingCircle.isTimerActive = true;
            taskLoadingCircle.transform.localPosition = transform.position + taskLoadingCirclePositionOffset;
            taskLoadingCircle.ShowTaskLoadingCircle(true);

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isFlagPoleTargeted = false;
            taskLoadingCircle.ShowTaskLoadingCircle(false);
            taskLoadingCircle.isTimerActive = false;
            isPlayerInRange = false;
        }
    }







}