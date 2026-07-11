using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class partyBattle : MonoBehaviour
{
    [SerializeField] float damageDealt = 1f;
    [SerializeField] float maxHealth;
    float currentHealth;
    enemyBattle[] activeEnemies;
    int selectedEnemy;
    [SerializeField] GameObject enemySelector;
    [SerializeField] GameObject healthBar;
    TMP_Text healthText;
    void Start()
    {
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        System.Array.Sort(activeEnemies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        selectedEnemy = 0;
        enemySelector = Instantiate(enemySelector, activeEnemies[0].transform.position, Quaternion.identity);
        healthBar = Instantiate(healthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        healthText = healthBar.GetComponent<TMP_Text>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthText.text = (int)((currentHealth/maxHealth)*100) + "%";

        if(Keyboard.current!=null && Keyboard.current.downArrowKey.wasPressedThisFrame && selectedEnemy+1<activeEnemies.Length)
        {
            selectedEnemy += 1;
        }
        if(Keyboard.current!=null && Keyboard.current.upArrowKey.wasPressedThisFrame && selectedEnemy-1>=0)
        {
            selectedEnemy -= 1;
        }
        if(activeEnemies[selectedEnemy] != null)
        {
            enemySelector.SetActive(true);
            enemySelector.transform.position = activeEnemies[selectedEnemy].transform.position;
        }else{
            enemySelector.SetActive(false);
        }
        if(Keyboard.current!=null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            activeEnemies[selectedEnemy].takeDamage(damageDealt);
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(enemySelector);
            Destroy(healthBar);
            Destroy(gameObject);
        }
    }
}
