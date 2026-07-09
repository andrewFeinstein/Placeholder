using UnityEngine;
using TMPro;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] TMP_Text healthText;
    float enemyMaxHealth = 10f;
    float enemyCurrentHealth = 10f;

    void Start()
    {
        enemyHealthBar = Instantiate(enemyHealthBar, transform.position + new Vector3(0,1.5f,0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
        healthText = enemyHealthBar.GetComponent<TMP_Text>();
    }

    void Update()
    {
        healthText.text = (int)((enemyCurrentHealth/enemyMaxHealth)*100) + "%";
        if(enemyCurrentHealth>0)
        {
            enemyCurrentHealth -= .01f;
        }else{
            Destroy(enemyHealthBar);
            Destroy(gameObject);
        }
    }
}
