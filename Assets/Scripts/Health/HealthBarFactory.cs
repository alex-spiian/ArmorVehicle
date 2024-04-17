using UnityEngine;

namespace Health
{
    public class HealthBarFactory
    {
        public HealthBarPositionController Create(HealthBarPositionController prefab, RectTransform root, Transform target)
        {
            var healthBar = Object.Instantiate(prefab, root);
            healthBar.Initialize(root, target);

            return healthBar;
        }
    }
}