using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class partyBattle : MonoBehaviour
{
    [SerializeField] float damageDealt = 1f;
    [SerializeField] float maxHealth;
    float currentHealth;
    enemyBattle[] activeEnemies;
    //an array that contains all enemies in the current battle. it has the scripts of these enemies, not the gameobjects
    int selectedEnemy = 0;
    //number current enemy selected by player. not the gameobject
    [SerializeField] GameObject enemySelector;
    [SerializeField] GameObject healthBar;
    TMP_Text healthText;
    //im using text right now because i don't know how to make an actual health bar
    void Start()
    {
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        System.Array.Sort(activeEnemies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y)); //sorts enemy list by y position
        enemySelector = Instantiate(enemySelector, activeEnemies[0].transform.position, Quaternion.identity);
        healthBar = Instantiate(healthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        healthText = healthBar.GetComponent<TMP_Text>(); //gets the text compentent of the healthbar
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthText.text = (int)((currentHealth/maxHealth)*100) + "%";
        //updates health percet
        if(Keyboard.current!=null && Keyboard.current.downArrowKey.wasPressedThisFrame && selectedEnemy+1<activeEnemies.Length)
        {//if down arrow pressed and there is an enemy below
            selectedEnemy += 1;
        }
        if(Keyboard.current!=null && Keyboard.current.upArrowKey.wasPressedThisFrame && selectedEnemy-1>=0)
        {//if up arrow pressed and there is an enemy above
            selectedEnemy -= 1;
        }
        if(activeEnemies[selectedEnemy] != null)
        {//if the selected enemy is not dead
            enemySelector.SetActive(true);
            enemySelector.transform.position = activeEnemies[selectedEnemy].transform.position;
            //show enemySelector
        }else{
            enemySelector.SetActive(false);//hide enemySelector
        }
        if(Keyboard.current!=null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {//if space pressed
            activeEnemies[selectedEnemy].takeDamage(damageDealt);//damage selected enemy
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
