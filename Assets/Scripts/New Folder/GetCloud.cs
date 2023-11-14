using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class GetCloud : MonoBehaviour
    {
        [SerializeField] private GameObject spawnedCloud;

        public void CreateCloud()
        {
            var c = Instantiate(spawnedCloud);
            c.GetComponent<Explosion>().Init(new Vector3(-3, -3, 0));
        }


    }
}

