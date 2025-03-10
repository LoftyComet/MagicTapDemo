# **Magic-Tap: A Kinematics-Driven Virtual Hand Selection Technique in AR/VR**

## Basic Information

**Project Name**: `Magic-Tap: A Kinematics-Driven Virtual Hand Selection Technique in AR/VR`

**Authors**: `Ruyang Yu`, `Yixuan Liu`, `Zijian Wu`, and `Tao Luo`

<br/>

## Dependencies

**Programming Language**: [C#](https://learn.microsoft.com/zh-cn/dotnet/csharp/)

**Game Engine**: [Unity3D 2020.3.37f1c1](https://unity.cn/releases/lts/2020)

**IDE**: [Unity Hub](https://unity.cn/releases)

**Necessary SDK or Plugins**: [MRTK 2.8](https://github.com/microsoft/MixedRealityToolkit-Unity)

**Operating System**: Windows 10 and Above

**HMD**: Quest 2, Quest Pro or Quest 3

<br/>

## Install Unity & Development Tools

1. Download **Unity Hub** and install **Unity 2020.3.37f1c1**.
2. Clone the project from **GitHub** and open it in **Unity Hub** using the correct Unity version.
3. The required **MRTK components** are already configured, so the project can be run directly.
4. If you need to modify the code:

   - Install **Visual Studio** or another **C#** development IDE.
   - If using **Visual Studio**, install the **"Game Development with Unity"** workload via the **Visual Studio Installer**.

<br/>

## Run the Project

#### **Setup Instructions**

1. Connect your **PC** and **VR headset** using a data cable.
2. Launch **Quest Link** on your PC and enter the **Quest Link interface** in your VR headset.
3. Open the scene in **Unity** and click the **Play** button to run it.

#### **Interaction Guide**

Once inside the 3D scene, you will see **two vertically aligned balls** in front of you.

- Use your **right index finger** to interact with the **blue ball** at the bottom.
- When successfully triggered:
  - The **color-change ball** above will change color.
  - The system will provide **audio feedback**.

> ⚠ **Note:** This program is interactive and relies on real-time tracking of virtual hand  movements to detect user intent. 
> A pre-scripted playback is not possible. The animation below demonstrates the interaction.

![demo](https://github.com/LoftyComet/MagicTapDemo/blob/master/static/demo.gif)

<br/>

## Modify Parameters

If you want to modify the configuration parameters for **Magic-Tap**, follow these steps:

1. Locate the **AcceStimulate Script** attached to the following GameObject in the demo scene:
   /show/showball/magic
2. Adjust the script parameters:

- **Alpha** → Acceleration threshold parameter.
- **Beta** → Speed threshold parameter.

<br/>

## License

This software is available for free non-commercial and academic use only.  
Commercial use requires explicit permission from the authors.