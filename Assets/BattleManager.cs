using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    int currentTurn = 0;
    //0 = party turn
    //1 = enemy turn
    enemyBattle[] activeEnemies;
    partyBattle[] activeAllies;
    int currentActor = 0; 
    //the index of the object currently acting. ex: if currentTurn==0 and currentActor==2, the thrid party member is acting
    [SerializeField] GameObject allyIndicator;
    //shows which ally is currently acting
    [SerializeField] GameObject resourcePanel;
    [SerializeField] GameObject attackPanel;
    //the panels that hold resources bars and attack buttons
    
    void Start()
    {
        activeEnemies = Object.FindObjectsByType<enemyBattle>();
        System.Array.Sort(activeEnemies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        //set and sort active enemies (highest to lowest y values)
        activeAllies = Object.FindObjectsByType<partyBattle>();
        System.Array.Sort(activeAllies, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        //set and sort active allies
        allyIndicator.SetActive(true);
        startTurn();
    }

    public void endTurn()
    {//called by allies/enemies at the end of their turns and prepares for startTurn()
        currentActor += 1;
        if(currentTurn==0 && currentActor>=activeAllies.Length)
        {//if all allies have acted
            currentTurn = 1;
            currentActor = 0;
            //switch to first enemy turn
            resourcePanel.SetActive(false);
            attackPanel.SetActive(false);
            //hide ui elements
        }
        if(currentTurn==1 && currentActor>=activeEnemies.Length)
        {//same but for enemy to ally
            currentTurn = 0;
            currentActor = 0;
            resourcePanel.SetActive(true);
            attackPanel.SetActive(true);
        }
        StartCoroutine(turnLag());
    }

    IEnumerator turnLag()
    {//this is needed to prevent 1 space press from activating 2 ally turns at once
    //this waits a given amount of time before starting the next turn
        yield return new WaitForSeconds(.1f);
        if(currentTurn==0)
        {//show or hide the ally indicator
            allyIndicator.SetActive(true);
        }else{
            allyIndicator.SetActive(false);
        }
        startTurn();
    }

    public void startTurn()
    {//starts the next actors turn
        if(currentTurn == 0)
        {
            allyIndicator.transform.position = activeAllies[currentActor].transform.position;
            activeAllies[currentActor].playTurn();
        }
        if(currentTurn == 1)
        {
            activeEnemies[currentActor].playTurn();
        }
    }


}
