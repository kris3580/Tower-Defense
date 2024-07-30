using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingBase : MonoBehaviour
{
    // BASE FIELDS AND METHODS

    [HideInInspector] public int currentLevel;

    [HideInInspector] public int[] hpPerLevel;
    [HideInInspector] public int[] upgradePrices;

    [HideInInspector] public int[] profitPerLevel;
    [HideInInspector] public int[] damagePerLevel;

    [HideInInspector] public float attackSpeed = 1f;
    [HideInInspector] public int[] attackSpeedPerLevel;

    [HideInInspector] public BuildingType buildingType;

    [HideInInspector] public GameObject buildingModelHandle;
    public Health healthComponent;

    public ArrowShooting arrowShooting;




    public virtual void DefaultStatsSetup() { }
    public virtual void CurrentStatsSetup() { }
    public virtual void AlliesSetup() { }


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
        BuildingSetup();
    }


    private Vector3 taskLoadingCirclePositionOffset = new Vector3(-0.8f, -0.5f, 1.1f);
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !FlagPole.isFlagPoleTargeted)
        {
            if (!isBuildingAtMaxLevel()) { dottedSquareAreaIcon.enabled = true; }
            ShowBuildingInfoPopUp(true);

                if (isAllowedToBuild() && !isBuildingAtMaxLevel())
                {
                    taskLoadingCircle.transform.localPosition = buildingInfoPopUp.transform.position + taskLoadingCirclePositionOffset;
                    taskLoadingCircle.ShowTaskLoadingCircle(true);
                    taskLoadingCircle.isTimerActive = true;
                    isPlayerInRange = true;
                }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !FlagPole.isFlagPoleTargeted)
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
    private GameObject buildingShowcase;
    public bool isBuilt = false;
    private BuildingManager buildingManager;

    private void BuildingModelHandler()
    {


        if (isPlayerInRange && taskLoadingCircle.isTimeOut) 
        {

            buildingModelHandle.SetActive(true);
            buildingShowcase.SetActive(false);
            isBuilt = true;

                if (isAllowedToBuild() && !isBuildingAtMaxLevel())
                {
                    Store.currentYellowCoins -= upgradePrices[currentLevel];
                    CurrentStatsSetup();
                    currentLevel++;
                    SetBuildingInfo();
                    isPlayerInRange = false;

                    AlliesSetup();
                }
        }
    }

    bool isAllowedToBuild()
    {
        try
        {
            return (gameObject.name == "Castle" || upgradePrices[currentLevel] <= Store.currentYellowCoins && buildingManager.castle.currentLevel > currentLevel);
        }
        catch
        {
            return false;
        }
        
    }




    // TASK LOADING CIRCLE STUFF
    public TaskLoadingCircle taskLoadingCircle;
    bool isTaskCircleLoadingComponentLoaded = false;
    private GameManager gameManager;
    private GameObject taskLoadingCircleObject;
    bool isCircleCanceled = false;

    public void LoadTaskLoadingCircleComponent()
    {
        if (!isTaskCircleLoadingComponentLoaded & gameManager.wasSpawnPlacementSelected)
        {
            taskLoadingCircle = GameObject.Find("TaskLoadingCircle").GetComponent<TaskLoadingCircle>();
            taskLoadingCircleObject = GameObject.Find("TaskLoadingCircle");
            isTaskCircleLoadingComponentLoaded = true;
        }

    }




    // BUILDING POP UP INFO STUFF

    private Canvas canvas;
    private GameObject buildingInfoPrefab;
    private Vector3 buildingInfoPositionOffset = new Vector3(2.5f, 3, -1);
    private GameObject buildingInfoPopUp;

    private TextMeshProUGUI text_buildingCost;
    private TextMeshProUGUI text_buildingName;
    private TextMeshProUGUI text_buildingLevel;
    private TextMeshProUGUI text_upgradeInfo;
    private Image upgradeIcon;
    private Image coinIcon;
    private Image buildingInfoPopUpBackground; 
    private Sprite infoPopUpMaxLevelImage;
    private SpriteRenderer dottedSquareAreaIcon;


    private void BuildingSetup()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();

        

        buildingInfoPrefab = Resources.Load<GameObject>("BuildingInfoPopUp");
        buildingInfoPopUp = Instantiate(buildingInfoPrefab, canvas.transform);

        try { GetFarms(); } catch { }

        buildingInfoPopUpBackground = buildingInfoPopUp.transform.Find("Background").GetComponent<Image>();
        infoPopUpMaxLevelImage = Resources.Load<Sprite>("BuildingInfoPopUpBackgroundMaxLevel");

        coinIcon = buildingInfoPopUp.transform.Find("BuildingCost").transform.Find("Coin").GetComponent<Image>();
        dottedSquareAreaIcon = transform.Find("DottedSquareArea").GetComponent<SpriteRenderer>();
        dottedSquareAreaIcon.enabled = false;

        buildingInfoPopUp.SetActive(false);
        buildingModelHandle = transform.Find("BuildingModelHandle").gameObject;
        healthComponent = buildingModelHandle.GetComponent<Health>();

        try { arrowShooting = buildingModelHandle.GetComponent<ArrowShooting>(); } catch { }


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

        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();


    }


    private void ShowBuildingInfoPopUp(bool state)
    {
        if (state) SetBuildingInfo();
        buildingInfoPopUp.SetActive(state);
    }


    
    private bool isBuildingAtMaxLevel()
    {
        try
        {
            if (currentLevel >= upgradePrices.Length) return true;
        }
        catch
        {
            return false;
        }
        return false;
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

                if (currentLevel == 0)
                {
                    text_upgradeInfo.text = $" +{profitPerLevel[currentLevel]}";
                }

                else
                {
                    text_upgradeInfo.text = $" {profitPerLevel[currentLevel-1]} +{Math.Abs(profitPerLevel[currentLevel - 1] - profitPerLevel[currentLevel])}";
                }
                    
                
                
            }
        }
        else
        {
            if (isBuildingNonDescriptive())
            {
                text_upgradeInfo.text = $" {profitPerLevel[currentLevel-1]}";
            }
            else
            {
                buildingInfoPopUp.transform.Find("BuildingNameAndLevel").GetComponent<RectTransform>().localPosition = new Vector3(-10.24f, -19.39f, 0);
                text_upgradeInfo.text = "MAX";
                upgradeIcon.enabled = false;
            }
            text_buildingCost.text = "";
            buildingInfoPopUp.transform.Find("UpgradeDescription").gameObject.SetActive(true);
            coinIcon.enabled = false;
            buildingInfoPopUpBackground.sprite = infoPopUpMaxLevelImage;

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



    // farms for some reason

    public List<GameObject> farmsList = new List<GameObject>();
    public void GetFarms()
    {
        for (int i = 0; i < transform.Find("Farms").childCount; i++)
        {
            farmsList.Add(transform.Find("Farms").GetChild(i).gameObject);
            transform.Find("Farms").GetChild(i).gameObject.SetActive(false);
        }
    }



}


public enum BuildingType
{
    Castle, House, Tower, Wall, Mill, Barracks, Archery, Farm
}
