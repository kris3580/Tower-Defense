using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinType coinType;
    List<Material> coinMaterials = new List<Material>();
    MeshRenderer currentMesh;

    private void Awake()
    {
        coinMaterials.Add(Resources.Load<Material>("Yellow"));
        coinMaterials.Add(Resources.Load<Material>("Red"));
        coinMaterials.Add(Resources.Load<Material>("Purple"));

        currentMesh = transform.Find("CoinModel").GetComponent<MeshRenderer>();
    }



    private void Start()
    {
        switch (coinType)
        {
            case CoinType.Yellow:
                currentMesh.material = coinMaterials[0];
                break;
            case CoinType.Red:
                currentMesh.material = coinMaterials[1];
                break;
            case CoinType.Purple:
                currentMesh.material = coinMaterials[2];
                break;
        }

        Invoke("GetCoin", 0.5f);

    }

    void GetCoin()
    {
        switch (coinType)
        {
            case CoinType.Yellow:
                Store.currentYellowCoins++;
                break;
            case CoinType.Red:
                // Store.currentRedCoins++;
                break;
            case CoinType.Purple:
                Store.currentPurpleCoins++;
                break;  
        }

        Destroy(gameObject);

    }


}
