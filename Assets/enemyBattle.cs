using UnityEngine;
using TMPro;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] GameObject enemyHealthBar;
    TMP_Text healthText;
    float enemyMaxHealth = 10f;
    [SerializeField] float enemyCurrentHealth = 10f;
    [SerializeField] float damageDealt;
    partyBattle[] activeAllies;
    BattleManager battleManager;

    void Start()
    {
        enemyHealthBar = Instantiate(enemyHealthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        //^^creates the health text object. its not as scary as it looks
        healthText = enemyHealthBar.GetComponent<TMP_Text>();
        activeAllies = Object.FindObjectsByType<partyBattle>();
        battleManager = Object.FindAnyObjectByType<BattleManager>();
    }

    void Update()
    {
        healthText.text = (int)((enemyCurrentHealth/enemyMaxHealth)*100) + "%";
    }

    public void takeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth<=0)
        {
            Destroy(enemyHealthBar);
            Destroy(gameObject);
        }
    }

    public void playTurn()
    {
        activeAllies[UnityEngine.Random.Range(0,activeAllies.Length-1)].takeDamage(damageDealt);
        Debug.Log("Enemy Acted");
        battleManager.endTurn();
    }
}
