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
    }
    private void Start()
    {
        BuildingInfoPopUpSetup();

    }

    private void OnTriggerEnter(Collider other)
    {
        ShowBuildingInfoPopUp(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ShowBuildingInfoPopUp(false);
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


    private void BuildingInfoPopUpSetup()
    {
        canvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
        buildingInfoPrefab = Resources.Load<GameObject>("BuildingInfoPopUp");
        buildingInfoPopUp = Instantiate(buildingInfoPrefab, canvas.transform);
        buildingInfoPopUp.SetActive(false);

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

        if (!isBuildingNonProfit())
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

    public void SetBuildingInfo()
    {

        Debug.Log(currentLevel);
        text_buildingCost.text = upgradePrices[currentLevel].ToString();
        text_buildingName.text = buildingType.ToString().ToUpper();
        text_buildingLevel.text = $"LVL {currentLevel}";

        if (isBuildingNonProfit())
        {
            text_upgradeInfo.text = $"+ {profitPerLevel[currentLevel]}";
        }
        
        
            
        
        


        
    }


    bool isBuildingNonProfit()
    {
        switch (buildingType)
        {
            case BuildingType.Castle: case BuildingType.House: case BuildingType.Mill: return true;
            default: return false;
        }
    }

}





public enum BuildingType
{
    Castle, House, Tower, Wall, Mill, Barracks, Archery
}
