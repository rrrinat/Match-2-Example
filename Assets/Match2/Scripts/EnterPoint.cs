using System;
using Match2.Scripts.Common.Configs;
using Match2.Scripts.Core.InputProcessor;
using UnityEngine;

namespace Match2.Scripts
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private ConfigsProvider configsProvider;
        
        private Root root;
        
        private void Start()
        {
            root = Root.CreateRoot(new RootContext
            {
                InputController = this.inputController,
                ConfigsProvider = this.configsProvider
            });
        }

        private void OnDestroy()
        {
            root.Dispose();
        }
    }
}
