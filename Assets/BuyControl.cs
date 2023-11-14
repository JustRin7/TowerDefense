using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_towerBuyPrefab;
        private List<TowerBuyControl> m_activeControl;
        private RectTransform m_RectTransform;//его нужно доставать через GetComponent


        //*-- События Unity
        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
        //*--*//


        private void MoveToBuildSite(BuildSite buildSite)
        {            
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

                m_RectTransform.anchoredPosition = position;

                m_activeControl = new List<TowerBuyControl>();
                foreach (var asset in buildSite.buildableTowers)
                {
                    if (asset.isAvailable())
                    {
                        var newControl = Instantiate(m_towerBuyPrefab, transform);
                        m_activeControl.Add(newControl);
                        newControl.SetTowerAssets(asset);
                    }
                }

                if(m_activeControl.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_activeControl.Count;
                    for (int i = 0; i < m_activeControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                        m_activeControl[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                }

            }
            else
            {
                if(m_activeControl != null)
                {
                    foreach (var control in m_activeControl) Destroy(control.gameObject);
                    m_activeControl.Clear();
                }                
                gameObject.SetActive(false);
            }
                
        }


    }
}
