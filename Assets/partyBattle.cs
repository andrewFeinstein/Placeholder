using UnityEngine;
using UnityEngine.InputSystem;

public class partyBattle : MonoBehaviour
{
[SerializeField] float damageDealt;
[SerializeField] float characterHealth;
[SerializeField] enemyBattle[] activeEnemies;
[SerializeField] int selectedEnemy;
[SerializeField] GameObject enemySelector;
    void Start()
    {
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        System.Array.Sort(activeEnemies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        selectedEnemy = 0;
        enemySelector = Instantiate(enemySelector, activeEnemies[0].transform.position, Quaternion.identity);
    }

    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.downArrowKey.wasPressedThisFrame && selectedEnemy+1<activeEnemies.Length)
        {
            selectedEnemy += 1;
        }
        if(Keyboard.current != null && Keyboard.current.upArrowKey.wasPressedThisFrame && selectedEnemy-1>=0)
        {
            selectedEnemy -= 1;
        }
        if(activeEnemies[selectedEnemy] != null)
        {
            enemySelector.transform.position = activeEnemies[selectedEnemy].transform.position;
        }else{
            Destroy(enemySelector);
        }
    }
}
