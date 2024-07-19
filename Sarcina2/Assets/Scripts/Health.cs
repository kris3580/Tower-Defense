using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private GameObject healthBarPrefab;
    private GameObject healthBar;
    private Image healthBarForeground;
    private Canvas healthBarsCanvas;
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
        healthBarsCanvas = GameObject.Find("HealthBarsCanvas").GetComponent<Canvas>();
        

        healthBar = Instantiate(healthBarPrefab, healthBarsCanvas.transform);
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


    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.name.Contains("Enemy") && other.tag == "Arrow")
        {
            Debug.Log(gameObject.name);
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
                SceneManager.LoadScene("Game");
            }
        }
    }


    private void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarForeground.fillAmount = currentHealth / maxHealth;
    }


}


