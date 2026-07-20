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
        activeAllies[0].playTurn();
    }

    public int checkTurn()
    {
        return currentTurn;
    }

    public void nextTurn()
    {
        //turn 0 = ally turns, turn 1 = enemy turns
        if(currentTurn==0)
        {
            if(currentActor < activeAllies.Length)
            {
                
                if(activeAllies[currentActor] != null)
                {
                    activeAllies[currentActor].playTurn();
                    currentActor += 1;
                }
            }else{
                currentTurn = 1;
                currentActor = 0;
                nextTurn();
            }
        }
        if(currentTurn==1 && activeEnemies.Length>=currentActor+1)
        {
            if(currentActor < activeEnemies.Length)
            {
                if(activeEnemies[currentActor] != null)
                {
                    activeEnemies[currentActor].playTurn();
                    currentActor += 1;
                }
            }else if(currentTurn == 1){
                currentTurn = 0;
                currentActor = 0;
                nextTurn();
            }
        }
    }

    void Update()
    {
        
    }
}
