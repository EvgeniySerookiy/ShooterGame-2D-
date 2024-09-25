using System;
using Zenject;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class PlayerMoveController : IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly GameInput _gameInput;
        private readonly PlayerWeaponController _playerWeaponController;
        
        public PlayerMoveController(Player player, GameInput gameInput, PlayerWeaponController playerWeaponController)
        {
            _player = player;
            _gameInput = gameInput;
            _playerWeaponController = playerWeaponController;
        }

        public void Initialize()
        {
            _gameInput.OnMove += _player.Move;
            _gameInput.OnShoot += _playerWeaponController.Fire;
        }

        public void Dispose()
        {
            _gameInput.OnMove -= _player.Move;
            _gameInput.OnShoot -= _playerWeaponController.Fire;
        }
    }
}