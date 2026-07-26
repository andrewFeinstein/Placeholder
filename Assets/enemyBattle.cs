using UnityEngine;
using TMPro;
using System.Collections;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] GameObject enemyHealthBar;
    TMP_Text healthText;
    [SerializeField] float enemyMaxHealth = 10f;
    float enemyCurrentHealth;
    [SerializeField] float damageDealt;
    partyBattle[] activeAllies;
    BattleManager battleManager;

    void Start()
    {//sets various stuff when the enemy spawns
        enemyCurrentHealth = enemyMaxHealth;
        enemyHealthBar = Instantiate(enemyHealthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        //creates the health text object. its not as scary as it looks
        healthText = enemyHealthBar.GetComponent<TMP_Text>();
        activeAllies = Object.FindObjectsByType<partyBattle>();
        System.Array.Sort(activeAllies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        battleManager = Object.FindAnyObjectByType<BattleManager>();
    }

    void Update()
    {
        healthText.text = (int)((enemyCurrentHealth/enemyMaxHealth)*100) + "%";
        //updates health bar
    }

    public void takeDamage(float damage)
    {//called by an ally when attacking
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth<=0)
        {
            Destroy(enemyHealthBar);
            Destroy(gameObject);
        }
    }

    public void playTurn()
    {//called by BattleMananger to start turn
        activeAllies[UnityEngine.Random.Range(0,activeAllies.Length-1)].takeDamage(damageDealt);
        //attacks a random enemy
        Debug.Log("Enemy Acted");
        StartCoroutine(dramaticPause());
    }

    IEnumerator dramaticPause()
    {//pauses dramatically
        yield return new WaitForSeconds(1f);
        battleManager.endTurn();
    }

}
