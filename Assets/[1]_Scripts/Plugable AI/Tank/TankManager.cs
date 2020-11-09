using UnityEngine;

namespace SA.Tank
{
    public class TankManager : MonoBehaviour
    {
        [System.Serializable]
        struct EnemySlot
        {
            public GameObject tankEnemyPrefab;
            public Transform enemySpawnPoint;
        }
        

        [SerializeField] EnemySlot[] enemySlots = default;
        [SerializeField] public Transform[] wayPoints;


        // Start is called before the first frame update
        void Start()
        {
            if (wayPoints == null) 
            {
                Debug.Log("WayPoints list is empty!!!");
                return;
            }

            SpawnEnemy();            
        }


        void SpawnEnemy()
        {
            foreach(var es in enemySlots)
            {
                var prefab = es.tankEnemyPrefab;
                var enemy = Instantiate(es.tankEnemyPrefab, es.enemySpawnPoint.position, Quaternion.identity);
                //enemy.GetComponent<StateController>().SetupAI(true, wayPoints);
            }
        }
    }
}
