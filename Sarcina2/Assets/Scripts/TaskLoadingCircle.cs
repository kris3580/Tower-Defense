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

    float timeForActionCompletion = 1.2f;
    float timePassed = 0f;
    public bool isTimerActive = false;

    public bool isTimeOut = false;
    void CircleTimer()
    {
        if (timePassed >= timeForActionCompletion)
        {
            timePassed = 0f;
            isTimeOut = true;
            ShowTaskLoadingCircle(false);
        }
        else isTimeOut = false;

        if (isTimerActive)
        {
            timePassed += Time.deltaTime;

            circleIcon.fillAmount = timePassed / timeForActionCompletion;
        }
        else
        {
            isTimerActive = false ;
            timePassed = 0f;
        }
    }


    public void ShowTaskLoadingCircle(bool state)
    {
        taskLoadingCircleObject.GetComponent<RectTransform>().transform.SetAsLastSibling();
        taskLoadingCircleObject.SetActive(state);
    }




}
