using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy.EnemyControllers
{
    public class FlyerController : EnemyController
    {
        
        public FlyerController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition) : base(enemyProvider, container, spawnEnemyPosition)
        {
        }
        
        public override EnemyType GetEnemyType()
        {
            return EnemyType.Flyer;
        }
    }
}