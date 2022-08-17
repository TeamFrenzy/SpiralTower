// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/BattleScripts/TestInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TestInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestInput"",
    ""maps"": [
        {
            ""name"": ""TestPlayer"",
            ""id"": ""3c7381d8-717e-405e-9025-f563f61ee86b"",
            ""actions"": [
                {
                    ""name"": ""XUp"",
                    ""type"": ""Button"",
                    ""id"": ""ab97b512-fa00-4fba-a9d6-24837caecadb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""SlowTap""
                },
                {
                    ""name"": ""XDown"",
                    ""type"": ""Button"",
                    ""id"": ""4e8a74a6-5d74-41b3-b046-b26da8eae516"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZUp"",
                    ""type"": ""Button"",
                    ""id"": ""735421a0-6f73-49af-8b8a-a1bd51660e06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZDown"",
                    ""type"": ""Button"",
                    ""id"": ""cfd53f3a-0d3e-433f-9ed4-370d131fc7e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d2817637-6138-423e-97ed-943f29151a4d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a91a3fa8-7ee7-4e82-abe0-54c20c713b35"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc0da636-3145-4820-a7a1-135170c87748"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""SlowTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3cd3801-8475-4663-a862-45569fe4f03c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TestPlayer
        m_TestPlayer = asset.FindActionMap("TestPlayer", throwIfNotFound: true);
        m_TestPlayer_XUp = m_TestPlayer.FindAction("XUp", throwIfNotFound: true);
        m_TestPlayer_XDown = m_TestPlayer.FindAction("XDown", throwIfNotFound: true);
        m_TestPlayer_ZUp = m_TestPlayer.FindAction("ZUp", throwIfNotFound: true);
        m_TestPlayer_ZDown = m_TestPlayer.FindAction("ZDown", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // TestPlayer
    private readonly InputActionMap m_TestPlayer;
    private ITestPlayerActions m_TestPlayerActionsCallbackInterface;
    private readonly InputAction m_TestPlayer_XUp;
    private readonly InputAction m_TestPlayer_XDown;
    private readonly InputAction m_TestPlayer_ZUp;
    private readonly InputAction m_TestPlayer_ZDown;
    public struct TestPlayerActions
    {
        private @TestInput m_Wrapper;
        public TestPlayerActions(@TestInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @XUp => m_Wrapper.m_TestPlayer_XUp;
        public InputAction @XDown => m_Wrapper.m_TestPlayer_XDown;
        public InputAction @ZUp => m_Wrapper.m_TestPlayer_ZUp;
        public InputAction @ZDown => m_Wrapper.m_TestPlayer_ZDown;
        public InputActionMap Get() { return m_Wrapper.m_TestPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestPlayerActions set) { return set.Get(); }
        public void SetCallbacks(ITestPlayerActions instance)
        {
            if (m_Wrapper.m_TestPlayerActionsCallbackInterface != null)
            {
                @XUp.started -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXUp;
                @XUp.performed -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXUp;
                @XUp.canceled -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXUp;
                @XDown.started -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXDown;
                @XDown.performed -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXDown;
                @XDown.canceled -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnXDown;
                @ZUp.started -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZUp;
                @ZUp.performed -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZUp;
                @ZUp.canceled -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZUp;
                @ZDown.started -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZDown;
                @ZDown.performed -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZDown;
                @ZDown.canceled -= m_Wrapper.m_TestPlayerActionsCallbackInterface.OnZDown;
            }
            m_Wrapper.m_TestPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @XUp.started += instance.OnXUp;
                @XUp.performed += instance.OnXUp;
                @XUp.canceled += instance.OnXUp;
                @XDown.started += instance.OnXDown;
                @XDown.performed += instance.OnXDown;
                @XDown.canceled += instance.OnXDown;
                @ZUp.started += instance.OnZUp;
                @ZUp.performed += instance.OnZUp;
                @ZUp.canceled += instance.OnZUp;
                @ZDown.started += instance.OnZDown;
                @ZDown.performed += instance.OnZDown;
                @ZDown.canceled += instance.OnZDown;
            }
        }
    }
    public TestPlayerActions @TestPlayer => new TestPlayerActions(this);
    public interface ITestPlayerActions
    {
        void OnXUp(InputAction.CallbackContext context);
        void OnXDown(InputAction.CallbackContext context);
        void OnZUp(InputAction.CallbackContext context);
        void OnZDown(InputAction.CallbackContext context);
    }
}
