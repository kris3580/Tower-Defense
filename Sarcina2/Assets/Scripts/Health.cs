using System.Collections;
using System.Linq;
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

    public Animator animator;

    public int maxHealth;
    public int currentHealth;

    // player

    public float delayDamageTimerCurrent;
    public float delayDamageTimerDefault = 1f;



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

        if (gameObject.name.Contains("EnemyRanged"))
        {
            try { animator = transform.Find("EnemyHolder").transform.Find("Devil Bat").GetComponent<Animator>(); } catch { }
        }
        else if (gameObject.name.Contains("Enemy"))
        {
            try { animator = transform.Find("EnemyHolder").transform.Find("Goblin Minion").GetComponent<Animator>(); } catch { }
        }


    

    }


    private void Update()
    {
        try { healthBar.GetComponent<RectTransform>().position = transform.position + healthBarPositionOffset; } catch { }

        if (gameObject.name == "Player" || gameObject.name == "BuildingModelHandle" || gameObject.name.Contains("Ally") || gameObject.name.Contains("Enemy"))
        {
            delayDamageTimerCurrent -= Time.deltaTime;
        }

        try { healthBar.SetActive(currentHealth != maxHealth); } catch { }
        


    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("Enemy") && other.tag == "Enemy")
        {
            return;
        }
        if (gameObject.name.Contains("Ally") && other.name.Contains("Ally"))
        {
            return;
        }

        bool isArrowShotByEnemy = false;
        if (other.tag == "Arrow") isArrowShotByEnemy = other.gameObject.GetComponent<Arrow>().isArrowShotByEnemy;

        if (isArrowShotByEnemy && other.tag == "Arrow" && gameObject.name == "Player")
        {
            //Debug.Log("player shot by rangedEnemy");
            ApplyDamage(other.gameObject.GetComponent<Arrow>().damage);
        }
        if (isArrowShotByEnemy && other.tag == "Arrow" && gameObject.name == "BuildingModelHandle")
        {
            //Debug.Log("building shot by rangedEnemy");
            ApplyDamage(other.gameObject.GetComponent<Arrow>().damage);
        }
        if (!isArrowShotByEnemy && other.tag == "Arrow" && gameObject.name.Contains("Enemy"))
        {
            //Debug.Log("enemy shot by player");
            ApplyDamage(other.gameObject.GetComponent<Arrow>().damage);
        }
        if (isArrowShotByEnemy && other.tag == "Arrow" && gameObject.name.Contains("Ally"))
        {
            //Debug.Log("ally shot by enemy");
            ApplyDamage(other.gameObject.GetComponent<Arrow>().damage);
        }




    }

    private void OnTriggerStay(Collider other)
    {

        if (gameObject.name.Contains("Enemy") && other.tag == "Enemy")
        {
            return;
        }
        if (gameObject.name.Contains("Ally") && other.name.Contains("Ally"))
        {
            return;
        }

        if (other.tag == "Enemy" || other.tag == "Ally" && !gameObject.name.Contains("BuildingModelHandle") && !gameObject.name.Contains("Player"))
        {
            if (delayDamageTimerCurrent <= 0)
            {
                if (other.tag == "Enemy")
                {
                    try { other.gameObject.transform.GetComponentInParent<Health>().animator.SetBool("isAttacking", true); } catch { }
                }
                delayDamageTimerCurrent = delayDamageTimerDefault;
                ApplyDamage(other.gameObject.transform.parent.GetComponent<GameAI>().damage);
            }
            else
            {
                try { other.gameObject.transform.GetComponentInParent<Health>().animator.SetBool("isAttacking", false); } catch { }
            }

        }
    }



    private void ApplyDamage(int damage = 1)
    {
        
        currentHealth -= damage;
        UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {

            if (gameObject.name == "Player")
            {
                Destroy(gameObject);
                Destroy(healthBar);
                Store.ResetCoins();
                SceneManager.LoadScene("Game");
            }
            else if (gameObject.name.Contains("Boss"))
            {
                Destroy(gameObject);
                Destroy(healthBar);
                DropCoin(CoinType.Purple);
            }
            else if (gameObject.name.Contains("Enemy"))
            {

                if (!hasDied)
                {
                    hasDied = true;

                    try { animator.SetBool("isDying", true); } catch { };
                    Destroy(healthBar);
                    Invoke("DelayEnemyKillForAnimation", 1f);
                }


            }
            else if (gameObject.name == "BuildingModelHandle")
            {
                healthBar.SetActive(false);
                gameObject.SetActive(false);
            }
            else if (gameObject.name.Contains("Ally"))
            {
                Destroy(healthBar);
                Destroy(gameObject);
                
            }
        }
    }
    public bool hasDied = false;
    private void DelayEnemyKillForAnimation()
    {
        Destroy(gameObject);
        Destroy(healthBar);
        DropCoin(CoinType.Yellow);
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


