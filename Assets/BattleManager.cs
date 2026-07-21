using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] int currentTurn;
    //0 = party turn
    //1 = enemy turn
    [SerializeField]enemyBattle[] activeEnemies;
    [SerializeField]partyBattle[] activeAllies;
    [SerializeField] int currentActor;
    

    void Start()
    {
        currentTurn = 0;
        currentActor = 0;
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        activeAllies = Object.FindObjectsByType<partyBattle>();
        //activeAllies[0].playTurn();
        startTurn();
    }

    public void endTurn()
    {
        currentActor += 1;
        if(currentTurn==0 && currentActor>=activeAllies.Length)
        {
            currentTurn = 1;
            currentActor = 0;
        }
        if(currentTurn==1 && currentActor>=activeEnemies.Length)
        {
            currentTurn = 0;
            currentActor = 0;
        }
        startTurn();
    }

    public void startTurn()
    {
        if(currentTurn == 0)
        {
            activeAllies[currentActor].playTurn();
        }
        if(currentTurn == 1)
        {
            activeEnemies[currentActor].playTurn();
        }
    }

    void Update()
    {
        
    }
}
