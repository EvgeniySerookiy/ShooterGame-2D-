using System;
using Zenject;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class PlayerMoveController : IInitializable, IDisposable
    {
        private readonly PlayerView _playerView;
        private readonly GameInput _gameInput;
        private readonly PlayerWeaponController _playerWeaponController;
        
        public PlayerMoveController(PlayerView playerView, GameInput gameInput, PlayerWeaponController playerWeaponController)
        {
            _playerView = playerView;
            _gameInput = gameInput;
            _playerWeaponController = playerWeaponController;
        }

        public void Initialize()
        {
            _gameInput.OnMove += _playerView.Move;
            _gameInput.OnShoot += _playerWeaponController.Fire;
        }

        public void Dispose()
        {
            _gameInput.OnMove -= _playerView.Move;
            _gameInput.OnShoot -= _playerWeaponController.Fire;
        }
    }
}