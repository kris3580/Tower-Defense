using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private GameObject healthBarPrefab;
    private GameObject healthBar;
    private Image healthBarForeground;
    private Canvas canvas;
    [SerializeField] Vector3 healthBarPositionOffset;


    public int maxHealth;
    public int currentHealth;

    // player

    private float delayDamageTimerCurrent;
    [SerializeField] float delayDamageTimerDefault;



    private void Awake()
    {
        currentHealth = maxHealth;

        healthBarPrefab = Resources.Load<GameObject>("HealthBar");
        canvas = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
        

        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        healthBarForeground = healthBar.transform.Find("Foreground").GetComponent<Image>();

        healthBar.name = $"{gameObject.name}HealthBar"; 

        if (gameObject.name.Contains("Enemy"))
        {
            healthBarForeground.color = Color.red;
        }
    }


    private void Update()
    {
        healthBar.GetComponent<RectTransform>().position = transform.position + healthBarPositionOffset;

        if (gameObject.name == "Player")
        {
            delayDamageTimerCurrent -= Time.deltaTime;
        }

        healthBar.SetActive(currentHealth != maxHealth);
        


    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.name.Contains("Enemy") && other.tag == "Arrow")
        {
            ApplyDamage();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && gameObject.name == "Player")
        {
            if ( delayDamageTimerCurrent <= 0)
            {
                delayDamageTimerCurrent = delayDamageTimerDefault;
                ApplyDamage();
            }
            
        }
    }

    private void ApplyDamage()
    {
        
        currentHealth--;
        UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {


            Destroy(gameObject);
            Destroy(healthBar);

            if (gameObject.name == "Player")
            {
                Store.ResetCoins();
                SceneManager.LoadScene("Game");
            }
            else if (gameObject.name.Contains("Boss"))
            {
                Debug.Log("boss");
                DropCoin(CoinType.Purple);
            }
            else if (gameObject.name.Contains("Enemy")) 
            {
                DropCoin(CoinType.Yellow);
            }
            
            
        }
    }


    private void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarForeground.fillAmount = currentHealth / maxHealth;
    }


    void DropCoin(CoinType coinType)
    {
        Coin coin = Instantiate(Resources.Load<GameObject>("Coin"), transform.position, Quaternion.identity).GetComponent<Coin>();
        coin.coinType = coinType;
    }




}


