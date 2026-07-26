using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

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
    GameObject battleManager;
    bool isActing = false;
    //set to true at the start of turn, activating the selection and attack logic
    
    void Start()
    {
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        System.Array.Sort(activeEnemies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y)); //sorts enemy list by y position
        enemySelector = Instantiate(enemySelector, activeEnemies[0].transform.position, Quaternion.identity);
        healthBar = Instantiate(healthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        healthText = healthBar.GetComponent<TMP_Text>(); //gets the text compentent of the healthbar
        currentHealth = maxHealth;
        battleManager = GameObject.FindWithTag("BattleManager");
    }

    void Update()
    {
        healthText.text = (int)((currentHealth/maxHealth)*100) + "%";
        if(isActing == true)
        {
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
                isActing = false;
                battleManager.GetComponent<BattleManager>().endTurn();
            }
        }else{
            enemySelector.SetActive(false);
        }
    }

    public void playTurn()
    {//called by BattleManager at start of turn
        isActing = true;
        Debug.Log(gameObject.name + " is acting");
    }

    public void takeDamage(float damage)
    {//called when enemy attacks this ally
        currentHealth -= damage;
        StartCoroutine(damageAnimation());
        if(currentHealth <= 0)
        {//if this ally is dead
            Destroy(enemySelector);
            Destroy(healthBar);
            Destroy(gameObject);
        }//destroy everything related to it
    }

    IEnumerator damageAnimation()
    {//ally flashes red when taking damage
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.5f);
        GetComponent<Renderer>().material.color = Color.white;
    }
}
