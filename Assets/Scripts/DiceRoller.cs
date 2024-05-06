using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    public static DiceRoller Instance;
    
    public GameObject defaultDicePrefab;
    public DiceSpawner[] possibleSpawnPoints;
    
    public float initialForce;
    public float initialTorque;

    private Queue<DiceQueueItem> _diceQueue;
    private Coroutine _rollCoroutine;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    public void OnRollDice()
    {
        Roll(SelectSpawner(), defaultDicePrefab);
    }
    
    public void AddToQueueAndRoll(Queue<DiceQueueItem> queue)
    {
        _diceQueue = queue;
        _rollCoroutine = StartCoroutine(RollUntilQueueEmpty());
    }
    
    private DiceSpawner SelectSpawner()
    {
        return possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)];
    }

    private void Roll(DiceSpawner spawner, GameObject dicePrefab)
    {
        
        var newDie = Instantiate(dicePrefab,
            spawner.transform.position,
            spawner.transform.rotation).GetComponent<Die>();
        
        newDie.SetRandomColour();
        
        newDie.Roll(initialForce, initialTorque, spawner.spawnDirection);
    }
    
    private void RollBatch()
    {
        // get the maximum number of dice that can be spawned in this batch
        var maxIdx = Math.Min(possibleSpawnPoints.Length, _diceQueue.Count);
        
        // get a random list of integers to determine the order in which the spawners will be used
        var randomList = RandomUtils.GenerateRandomizedIntegerList(maxIdx);
        
        // spawn the dice in the order determined by the random list
        foreach (var idx in randomList)
        {
            var spawner = possibleSpawnPoints[idx];
            var dicePrefab = _diceQueue.Dequeue().DicePrefab;
            
            Roll(spawner, dicePrefab);
        }
    }
    
    private IEnumerator RollUntilQueueEmpty()
    {
        while (_diceQueue.Count > 0)
        {
            RollBatch();
            
            yield return new WaitForSeconds(.5f);
            
        }
    }
}
