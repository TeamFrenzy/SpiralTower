// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CameraTestScripts/InputActionsB.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActionsB : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActionsB()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsB"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""2b412a17-8117-4f52-afdd-f8e171cfe936"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""Value"",
                    ""id"": ""3405c2ea-5a49-4ab6-b14f-9748d9ed1cd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""Value"",
                    ""id"": ""376a72d3-3b43-4551-8ee3-182f39e45d1e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryContact"",
                    ""type"": ""Value"",
                    ""id"": ""42d295aa-a592-4a3e-bb2e-66da273e5c14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryPosition"",
                    ""type"": ""Value"",
                    ""id"": ""1f91294d-dd0b-4f96-b3bf-fd8586291d11"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryTap"",
                    ""type"": ""Button"",
                    ""id"": ""cbbe5dd3-4854-47f2-90a6-4f63f3794d3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,SlowTap(duration=0.21)""
                },
                {
                    ""name"": ""PrimaryHold"",
                    ""type"": ""Value"",
                    ""id"": ""72d93290-6834-4aaa-904c-8554a4758854"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryTapTest"",
                    ""type"": ""Button"",
                    ""id"": ""f364661a-54b2-419f-b5af-01dc79739525"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,SlowTap(duration=0.201)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58d1460a-0a6a-45da-a97b-b9f97d972f97"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da6301a1-4038-42a1-b3a6-c4b035a166e6"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73bbbc01-4380-48ca-919f-ebe6461ca6ba"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba8eab22-8cb8-495f-a48e-c69c064113bb"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39dfdbc6-0790-489c-964a-39965430817b"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dce742dc-92f7-4cdf-816d-55ebb2ffb698"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""650190ba-0338-4baf-8f98-ec22f9ed579e"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTapTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_PrimaryContact = m_Touch.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Touch_PrimaryPosition = m_Touch.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_Touch_SecondaryContact = m_Touch.FindAction("SecondaryContact", throwIfNotFound: true);
        m_Touch_SecondaryPosition = m_Touch.FindAction("SecondaryPosition", throwIfNotFound: true);
        m_Touch_PrimaryTap = m_Touch.FindAction("PrimaryTap", throwIfNotFound: true);
        m_Touch_PrimaryHold = m_Touch.FindAction("PrimaryHold", throwIfNotFound: true);
        m_Touch_PrimaryTapTest = m_Touch.FindAction("PrimaryTapTest", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_PrimaryContact;
    private readonly InputAction m_Touch_PrimaryPosition;
    private readonly InputAction m_Touch_SecondaryContact;
    private readonly InputAction m_Touch_SecondaryPosition;
    private readonly InputAction m_Touch_PrimaryTap;
    private readonly InputAction m_Touch_PrimaryHold;
    private readonly InputAction m_Touch_PrimaryTapTest;
    public struct TouchActions
    {
        private @InputActionsB m_Wrapper;
        public TouchActions(@InputActionsB wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_Touch_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_Touch_PrimaryPosition;
        public InputAction @SecondaryContact => m_Wrapper.m_Touch_SecondaryContact;
        public InputAction @SecondaryPosition => m_Wrapper.m_Touch_SecondaryPosition;
        public InputAction @PrimaryTap => m_Wrapper.m_Touch_PrimaryTap;
        public InputAction @PrimaryHold => m_Wrapper.m_Touch_PrimaryHold;
        public InputAction @PrimaryTapTest => m_Wrapper.m_Touch_PrimaryTapTest;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @PrimaryContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContact;
                @PrimaryPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @SecondaryContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryContact;
                @SecondaryContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryContact;
                @SecondaryContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryContact;
                @SecondaryPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryPosition;
                @SecondaryPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryPosition;
                @SecondaryPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnSecondaryPosition;
                @PrimaryTap.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTap;
                @PrimaryTap.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTap;
                @PrimaryTap.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTap;
                @PrimaryHold.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryHold;
                @PrimaryHold.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryHold;
                @PrimaryHold.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryHold;
                @PrimaryTapTest.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTapTest;
                @PrimaryTapTest.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTapTest;
                @PrimaryTapTest.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTapTest;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryContact.started += instance.OnPrimaryContact;
                @PrimaryContact.performed += instance.OnPrimaryContact;
                @PrimaryContact.canceled += instance.OnPrimaryContact;
                @PrimaryPosition.started += instance.OnPrimaryPosition;
                @PrimaryPosition.performed += instance.OnPrimaryPosition;
                @PrimaryPosition.canceled += instance.OnPrimaryPosition;
                @SecondaryContact.started += instance.OnSecondaryContact;
                @SecondaryContact.performed += instance.OnSecondaryContact;
                @SecondaryContact.canceled += instance.OnSecondaryContact;
                @SecondaryPosition.started += instance.OnSecondaryPosition;
                @SecondaryPosition.performed += instance.OnSecondaryPosition;
                @SecondaryPosition.canceled += instance.OnSecondaryPosition;
                @PrimaryTap.started += instance.OnPrimaryTap;
                @PrimaryTap.performed += instance.OnPrimaryTap;
                @PrimaryTap.canceled += instance.OnPrimaryTap;
                @PrimaryHold.started += instance.OnPrimaryHold;
                @PrimaryHold.performed += instance.OnPrimaryHold;
                @PrimaryHold.canceled += instance.OnPrimaryHold;
                @PrimaryTapTest.started += instance.OnPrimaryTapTest;
                @PrimaryTapTest.performed += instance.OnPrimaryTapTest;
                @PrimaryTapTest.canceled += instance.OnPrimaryTapTest;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnSecondaryContact(InputAction.CallbackContext context);
        void OnSecondaryPosition(InputAction.CallbackContext context);
        void OnPrimaryTap(InputAction.CallbackContext context);
        void OnPrimaryHold(InputAction.CallbackContext context);
        void OnPrimaryTapTest(InputAction.CallbackContext context);
    }
}
