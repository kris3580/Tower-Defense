using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingBase : MonoBehaviour
{
    // BASE FIELDS AND METHODS

    [HideInInspector] public int currentLevel;
    [HideInInspector] public int currentHP;

    [HideInInspector] public int[] hpPerLevel;
    [HideInInspector] public int[] upgradePrices;

    [HideInInspector] public int[] profitPerLevel;
    [HideInInspector] public int[] damagePerLevel;

    [HideInInspector] public float attackSpeed = 1f;
    [HideInInspector] public int[] attackSpeedPerLevel;

    [HideInInspector] public BuildingType buildingType;

    public virtual void DefaultStatsSetup() { }




    // UNITY RELATED METHODS

    private void Update()
    {
        buildingInfoPopUp.GetComponent<RectTransform>().position = transform.position + buildingInfoPositionOffset;
        LoadTaskLoadingCircleComponent();
        BuildingModelHandler();

        if (isBuildingAtMaxLevel() && !isCircleCanceled)
        {
            taskLoadingCircle.ShowTaskLoadingCircle(false);
            taskLoadingCircle.isTimerActive = false;
            isPlayerInRange = false;
            isCircleCanceled = true;
        }
    }


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BuildingInfoPopUpSetup();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dottedSquareAreaIcon.enabled = true;
            ShowBuildingInfoPopUp(true);

            if (!isBuildingAtMaxLevel())
            {
                if (upgradePrices[currentLevel] <= Store.currentYellowCoins)
                {
                    taskLoadingCircle.ShowTaskLoadingCircle(true);
                    taskLoadingCircle.isTimerActive = true;
                    isPlayerInRange = true;
                }
            }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dottedSquareAreaIcon.enabled = false;
            ShowBuildingInfoPopUp(false);
            taskLoadingCircle.ShowTaskLoadingCircle(false);
            taskLoadingCircle.isTimerActive = false;
            isPlayerInRange = false;
        } 
}


    //BUILDING SPAWNER

    private bool isPlayerInRange = false;
    private GameObject buildingModelHandle;
    private GameObject buildingShowcase;

    private void BuildingModelHandler()
    {


        if (isPlayerInRange && taskLoadingCircle.isTimeOut) 
        {

            buildingModelHandle.SetActive(true);
            buildingShowcase.SetActive(false);

            if (!isBuildingAtMaxLevel())
            {
                if (upgradePrices[currentLevel] <= Store.currentYellowCoins)
                {
                    Store.currentYellowCoins -= upgradePrices[currentLevel];
                    currentLevel++;
                    SetBuildingInfo();
                    isPlayerInRange = false;
                }
                

            }
        }
    }


    // TASK LOADING CIRCLE STUFF
    public TaskLoadingCircle taskLoadingCircle;
    bool isTaskCircleLoadingComponentLoaded = false;
    private GameManager gameManager;

    bool isCircleCanceled = false;

    public void LoadTaskLoadingCircleComponent()
    {
        if (!isTaskCircleLoadingComponentLoaded & gameManager.wasSpawnPlacementSelected)
        {
            taskLoadingCircle = GameObject.Find("Player").GetComponent<TaskLoadingCircle>();
            isTaskCircleLoadingComponentLoaded = true;
        }

    }




    // BUILDING POP UP INFO STUFF

    private Canvas canvas;
    private GameObject buildingInfoPrefab;
    private Vector3 buildingInfoPositionOffset = new Vector3(0, 3, 0);
    private GameObject buildingInfoPopUp;

    private TextMeshProUGUI text_buildingCost;
    private TextMeshProUGUI text_buildingName;
    private TextMeshProUGUI text_buildingLevel;
    private TextMeshProUGUI text_upgradeInfo;
    private Image upgradeIcon;
    private Image coinIcon;
    private SpriteRenderer dottedSquareAreaIcon;

    private void BuildingInfoPopUpSetup()
    {
        canvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
        buildingInfoPrefab = Resources.Load<GameObject>("BuildingInfoPopUp");
        buildingInfoPopUp = Instantiate(buildingInfoPrefab, canvas.transform);

        coinIcon = buildingInfoPopUp.transform.Find("BuildingCost").transform.Find("Coin").GetComponent<Image>();
        dottedSquareAreaIcon = transform.Find("DottedSquareArea").GetComponent<SpriteRenderer>();
        dottedSquareAreaIcon.enabled = false;

        buildingInfoPopUp.SetActive(false);
        buildingModelHandle = transform.Find("BuildingModelHandle").gameObject;
        buildingModelHandle.SetActive(false);
        buildingShowcase = transform.Find("BuildingTypeInfo").gameObject;
        

        text_buildingCost = buildingInfoPopUp.transform.Find("BuildingCost").transform.Find("CoinText").GetComponent<TextMeshProUGUI>();
        text_buildingName = buildingInfoPopUp.transform.Find("BuildingNameAndLevel").transform.Find("BuildingName").GetComponent<TextMeshProUGUI>();
        text_buildingLevel = buildingInfoPopUp.transform.Find("BuildingNameAndLevel").transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        text_upgradeInfo = buildingInfoPopUp.transform.Find("UpgradeDescription").transform.Find("PlusText").GetComponent<TextMeshProUGUI>();



        List<Sprite> upgradeIcons = new List<Sprite>
            {
            Resources.Load<Sprite>("CoinIcon"),
            Resources.Load<Sprite>("BattleIcon"),
            Resources.Load<Sprite>("BowIcon")
            };

        upgradeIcon = buildingInfoPopUp.transform.Find("UpgradeDescription").transform.Find("Icon").GetComponent<Image>();



        switch (buildingType)
        {
            case BuildingType.Barracks:
                upgradeIcon.sprite = upgradeIcons[1];
                break;
            case BuildingType.Archery:
                upgradeIcon.sprite = upgradeIcons[2];
                break;
            default:
                upgradeIcon.sprite = upgradeIcons[0];
                break;
        }

        if (!isBuildingNonDescriptive() && !isBuildingAtMaxLevel())
        {
            buildingInfoPopUp.transform.Find("UpgradeDescription").gameObject.SetActive(false);
            buildingInfoPopUp.transform.Find("BuildingNameAndLevel").GetComponent<RectTransform>().localPosition = new Vector3(122, -6.66f, 0);
        }


    }


    private void ShowBuildingInfoPopUp(bool state)
    {
        if (state) SetBuildingInfo();
        buildingInfoPopUp.SetActive(state);
    }


    
    private bool isBuildingAtMaxLevel()
    {
        if (currentLevel >= upgradePrices.Length) return true;
        else return false;
    }


    public void SetBuildingInfo()
    {

        if (currentLevel == 0)
        {
            text_buildingLevel.enabled = false;
        }
        else
        {
            text_buildingLevel.enabled = true;
        }

        text_buildingName.text = buildingType.ToString().ToUpper();
        text_buildingLevel.text = $"LVL {currentLevel}";

        if (!isBuildingAtMaxLevel())
        {
            text_buildingCost.text = upgradePrices[currentLevel].ToString();

            if (isBuildingNonDescriptive())
            {
                text_upgradeInfo.text = $"+ {profitPerLevel[currentLevel]}";
            }
        }
        else
        {
            buildingInfoPopUp.transform.Find("UpgradeDescription").gameObject.SetActive(true);
            buildingInfoPopUp.transform.Find("BuildingNameAndLevel").GetComponent<RectTransform>().localPosition = new Vector3(-10.24f, -19.39f, 0);
            text_buildingCost.text = "";
            text_upgradeInfo.text = "MAX";
            upgradeIcon.enabled = false;
            coinIcon.enabled = false;

        }

        

    }


    bool isBuildingNonDescriptive()
    {
        switch (buildingType)
        {
            case BuildingType.Castle: case BuildingType.House: case BuildingType.Mill: case BuildingType.Barracks: case BuildingType.Archery: return true;
            default: return false;
        }
    }


}


public enum BuildingType
{
    Castle, House, Tower, Wall, Mill, Barracks, Archery
}
