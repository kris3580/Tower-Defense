using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class TaskLoadingCircle : MonoBehaviour
{
    private GameObject taskLoadingCirclePrefab;
    public GameObject taskLoadingCircleObject;
    private Image circleIcon;
    private Canvas canvas;
    [SerializeField] Vector3 taskLoadingCirclePositionOffset;


    private void Awake()
    {
        taskLoadingCirclePrefab = Resources.Load<GameObject>("TaskLoadingCircle");
        canvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();

        taskLoadingCircleObject = Instantiate(taskLoadingCirclePrefab, canvas.transform);
        taskLoadingCircleObject.name = "PlayerTaskLoadingCircle";
        taskLoadingCircleObject.SetActive(false);
    }


    private void Update()
    {
        taskLoadingCircleObject.GetComponent<RectTransform>().position = transform.position + taskLoadingCirclePositionOffset;

        CircleTimer();

    }

    private void Start()
    {
        circleIcon = taskLoadingCircleObject.transform.Find("Circle").GetComponent<Image>();

    }


    // TIMER

    [SerializeField] float timeForActionCompletion = 2f;
    float timePassed = 0f;
    public bool isTimerActive = false;
    void CircleTimer()
    {
        Debug.Log(timePassed);

        if (timePassed >= timeForActionCompletion)
        {
            timePassed = 0f;
            taskLoadingCircleObject.SetActive(false);
        }


        if (isTimerActive)
        {
            timePassed += Time.deltaTime;

            circleIcon.fillAmount = timePassed / timeForActionCompletion;
        }
        else
        {
            timePassed = 0f;
        }
    }


    public void ShowTaskLoadingCircle(bool state)
    {
       taskLoadingCircleObject.SetActive(state);
    }




}
