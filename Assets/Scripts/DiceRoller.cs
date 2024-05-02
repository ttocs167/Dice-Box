using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public GameObject dicePrefab;
    public DiceSpawner[] possibleSpawnPoints;
    
    public float initialForce;
    public float initialTorque;

    public void OnRollDice()
    {
        Roll();
    }
    
    private DiceSpawner SelectSpawner()
    {
        return possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)];
    }

    private void Roll()
    {
        var spawner = SelectSpawner();
        
        var newDie = Instantiate(dicePrefab,
            spawner.transform.position,
            spawner.transform.rotation).GetComponent<Die>();
        
        newDie.SetRandomColour();
        
        newDie.Roll(initialForce, initialTorque, spawner.spawnDirection);
    }
}
