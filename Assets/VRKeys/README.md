# VRKeys 2.0 - A touchable keyboard for VR in Unity

VRKeys is an open source keyboard interface for single-line text input in VR, made in Unity and [available for free in the Unity Asset Store](https://assetstore.unity.com/packages/tools/input-management/vrkeys-99222).

For the project to run on Oculus Quest 2, I modified the code to implement hand manipulation instead of controller rays during November 2022. 

### Features

* Works with Oculus Quest 2
* Hand clicking keyboard input
* Shift key for capitalization and special characters
* Special @ and .com keys
* Easy input validation, info, and confirmation messages
* Ability to enable/disable input while validating or submitting
* Alternate keyboard layouts for i18n (Dvorak & Azerty for French included)

### Try it out

There is a demo build [releases page](https://github.com/campfireunion/VRKeys/releases).

### Before intalling this package
* Install Unity, Unity Hub
* Install [MRTK2](https://www.microsoft.com/en-us/download/details.aspx?id=102778)
* Start a new Unity3D project with Unity Hub
* Set the project with [MRTK2](https://learn.microsoft.com/zh-cn/training/modules/learn-mrtk-tutorials/1-1-introduction)
* Follow [this guide](https://learn.microsoft.com/zh-cn/windows/mixed-reality/mrtk-unity/mrtk2/supported-devices/oculus-quest-mrtk?view=mrtkunity-2022-05) to set the deployment environment
* Install TextMeshPro in Package Manager if not installed earlier

### Manual installation

1. Download the code
2. Put the VRKeys folder under the `Assets` folder
3. Open `Assets/VRKeys/Scenes/MRTK-20221112-Keyboard.unity` to see a working example scene


### Code Logic

For `Assets/VRKeys/Scripts/*.cs`, the example scene is primarily related to these files:
`Keyboard.cs` controls the initialization and layouts of the keboard,
`Key.cs` is the base class of all keys which contains interfaces to be override by its derived class,
`BackspaceKey.cs`, `CancelKey.cs`, `ClearKey.cs`, `EnterKey.cs`, `SpaceKey.cs`, `ShiftKey.cs`, and `LetterKey.cs` are derived classes of `Key.cs`

In the `*Key.cs` file, the hand touching behavior is implemented through overriding the interface of `HandleTouchEnter()`. In `SetAcceMode.cs`, the `SetMode()` method binds the implemented `HandleTouchEnter()` method of different keys in the specific keyboard to either the `InEvent`, `HesEvent`, and `OutEvent` of the `AcceStimulate.cs` component. This `SetMode()` method is triggered in the `DoSetLanguage()` IEnumerator after the initialization of all keys's gameobject, in case that this method won't be effective if it's triggered before initializing all the keys.

To construct a new keyboard, follow these steps:
1. Copy one Keyboard game object / Use the `VRKeys01.prefab`
2. Change the placeholder message in the `keyboard` component in the VRKeys01 gameobject
3. Set the AcceStimulate mode in the `SetAcceMode` component in the VRKeys01 gameobject
4. Customize with the VRKeys01 gameobject's name and position, to ensure correct body-locked behavior, change simultaneously the additional position/rotation in the `Solver Handler` component in the Display System gameobject (you can put this component somewhere else if you don't want to use the a Display System to control all keyboards).

To change the outlook of the keyboard, you can bind different materials to each key's `MeshRenderer` component.

> Brought to you by [The Campfire Union](https://www.campfireunion.com/).
> Modified by Xinyi Zhou ([Cindy](https://github.com/CindyChow123))
