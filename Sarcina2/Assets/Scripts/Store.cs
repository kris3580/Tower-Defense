using System.Collections;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{

    [SerializeField] GameObject joystick;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject startGameChoicesObject;
    [SerializeField] GameManager gameManager;

    public void GoToStoreButton()
    {
        joystick.SetActive(false);
        storePanel.SetActive(true);
        inGamePanel.SetActive(false);
        SetStartGameChoicesAvailability(false);
        Time.timeScale = 0;

    }

    public void ExitStoreButton()
    {
        joystick.SetActive(true);
        storePanel.SetActive(false);
        inGamePanel.SetActive(true);
        SetStartGameChoicesAvailability(true);
        Time.timeScale = 1;
    }


    // get children of startGameChoicesObject and set their box colliders to availability
    private void SetStartGameChoicesAvailability(bool availability)
    {
        if (startGameChoicesObject != null)
        {
            for (int i = 0; i < startGameChoicesObject.transform.childCount; i++)
            {
                startGameChoicesObject.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = availability;
            }
        }
    }





    public static int currentYellowCoins = 0;
    public static int currentRedCoins = 0;
    public static int currentPurpleCoins = 0;

    [SerializeField] TextMeshProUGUI text_InGamePanelCurrentYellowCoins;
    [SerializeField] TextMeshProUGUI text_StorePanelCurrentYellowCoins;
    [SerializeField] TextMeshProUGUI text_StoreCurrentRedCoins;
    [SerializeField] TextMeshProUGUI text_StoreCurrentPurpleCoins;



    private void Update()
    {
        UpdateCoinUI();
        RedCoinHandler();
    }


    // temporary
    void UpdateCoinUI()
    {
        text_InGamePanelCurrentYellowCoins.text = currentYellowCoins.ToString();
        text_StorePanelCurrentYellowCoins.text = currentYellowCoins.ToString();
        text_StoreCurrentRedCoins.text = currentRedCoins.ToString();
        text_StoreCurrentPurpleCoins.text = currentPurpleCoins.ToString();
    }

    public static void ResetCoins()
    {
        currentYellowCoins = 0;
    }


    private bool hasPurpleCoinHandlingBeenInitiated = false;

    void RedCoinHandler()
    {

        if (!hasPurpleCoinHandlingBeenInitiated && gameManager.wasSpawnPlacementSelected) 
        {

            hasPurpleCoinHandlingBeenInitiated = true;
            StartCoroutine(GiveRedCoin());
        }
    }

    IEnumerator GiveRedCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            currentRedCoins++;
        }
    }


}

public enum CoinType
{
    Yellow, Red, Purple
}