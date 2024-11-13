## **Magic-Tap: A Kinematics-Driven Virtual Hand Selection Technique in AR/VR**

### Dependencies

Unity3D 2020.3.37f1c1

[MRTK 2.8](https://github.com/microsoft/MixedRealityToolkit-Unity)

### Run the project

To run the project, please download Unity Hub and install `Unity 2020.3.37f1c1`. The MRTK components required for the project have already been configured, and this project can be run directly. If you need to edit code, please download Visual Studio or other IDEs for C# development.

The demo scene is located in `Assets/Scenes/show`. First, connect your PC and VR headset using Quest Link, then locate the scene and click play in Unity to run it. After successfully entering the 3D scene, you will see two vertically aligned spheres in front of you. Interact with the lower sphere using your right index fingertip. If triggered successfully, the upper sphere will change color and there will be audio feedback.

### Parameter modification

If you want to modify the trigger parameters for Magic-Tap, please select the `AcceStimulate` Script attached to the `/show/showball/magic` GameObject in the demo scene. You can then adjust the parameters on this script to modify Magic-Tap's trigger parameters, where `Ab` represents `α` and `Vbc `represents `β`.