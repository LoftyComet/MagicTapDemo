<!--
 * @Author: LoftyComet 1277173875@qq。
 * @Date: 2025-03-10 12:24:56
 * @LastEditors: LoftyComet 1277173875@qq。
 * @LastEditTime: 2025-03-10 15:55:46
 * @FilePath: \MagicTapDemo\README.md
 * @Description: 
 * 
 * Copyright (c) 2025 by ${git_name_email}, All Rights Reserved. 
-->
## **Magic-Tap: A Kinematics-Driven Virtual Hand Selection Technique in AR/VR**

### Basic Information

**Project Name**: `Magic-Tap: A Kinematics-Driven Virtual Hand Selection Technique in AR/VR`

**Authors**: `Ruyang Yu`, `Yixuan Liu`, `Zijian Wu`, and `Tao Luo`

### Dependencies

**Programming Language**: [C#](https://learn.microsoft.com/zh-cn/dotnet/csharp/)

**Game Engine**: [Unity3D 2020.3.37f1c1](https://unity.cn/releases/lts/2020)

**IDE**: [Unity Hub](https://unity.cn/releases)

**Necessary SDK or Plugins**: [MRTK 2.8](https://github.com/microsoft/MixedRealityToolkit-Unity)

**Operating System**: Windows 10 and Above

**HMD**: Quest 2, Quest Pro or Quest 3

### Configure the Environment

Please download `Unity Hub` and install `Unity 2020.3.37f1c1`. Clone the project on GitHub and open it in Unity Hub using the corresponding Unity version. The MRTK components required for the project have already been configured, and this project can be run directly. If you need to edit code, please download `Visual Studio` or other IDEs for C# development. If using `Visual Studio` for development, please install the `"Game Development with Unity"` workload in the Visual Studio Installer.

### Run the project

The demo scene is located in `Assets/Scenes/show`. First, connect your PC and VR headset using a data cable. Launch `Quest Link` on your PC while simultaneously accessing the` Quest Link` interface in your VR headset. Then locate the scene and click the `Play` button in Unity to run it. Upon successfully entering the 3D scene, you will see two vertically aligned spheres in front of you. Interact with the lower sphere using your right index finger. If triggered successfully, the upper sphere will change color and provide audio feedback.

![demo](https://github.com/LoftyComet/MagicTapDemo/blob/master/static/demo.gif)

### Parameter modification

If you want to modify the trigger parameters for Magic-Tap, please select the `AcceStimulate` Script attached to the `/show/showball/magic` GameObject in the demo scene. You can then adjust the parameters on this script to modify Magic-Tap's trigger parameters. As in the paper, `alpha` represents the `acceleration` threshold parameter, and `beta` represents the `speed` threshold parameter.

### License
This software is available for free non-commercial and academic use only. Commercial use requires permission from the authors.