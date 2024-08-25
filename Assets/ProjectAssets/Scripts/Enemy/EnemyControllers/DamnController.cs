using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy.EnemyControllers
{
    public class DamnController : EnemyController
    {
        public DamnController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition) : base(enemyProvider, container, spawnEnemyPosition)
        {
        }

        public override EnemyType GetEnemyType()
        {
            return EnemyType.Damn;
        }
    }
}