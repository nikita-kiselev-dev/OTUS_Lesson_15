using UnityEngine;

namespace Game.Scripts
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private RectTransform healthBarParent;
        [SerializeField] private GameObject healthPoint;
        
        public void UpdateHealthBar(int health)
        {
            ClearHealthBar();
            
            for (int i = 0; i < health; i++)
            {
                Instantiate(healthPoint, healthBarParent);
            }
        }

        public void ClearHealthBar()
        {
            foreach (Transform healthPoint in healthBarParent.transform)
            {
                Destroy(healthPoint.gameObject);
            }
        }
    }
}