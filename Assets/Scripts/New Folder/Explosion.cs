using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private GameObject m_ExplosionPrefab;//������ �� ������ ������
        [SerializeField] private int lifetime;

        private float timer;

        public void Init(Vector3 position)
        {
            m_ExplosionPrefab.transform.position = position;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > lifetime)
            {
                Destroy(gameObject);
            }
        }

    }
}
