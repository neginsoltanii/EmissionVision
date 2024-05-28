![Screenshot_2024-05-26_at_18 10 22-removebg-preview](https://github.com/neginsoltanii/EmissionVision/assets/62497271/0acddba0-8d8a-4f9f-b7e0-d518723bc0f0)


# Emission Vision
## _What if knowing the past, can change the future?_

## Introduction
Emission Vision is a digital twin implemented in mixed reality and designed to provide an interactive and collaborative way of analyzing global CO2 emissions data and each country's contribution. Over the years, CO2 emissions have steadily climbed and continue to rise. Consequently, it is crucial for policymakers and environmental analysts to provide accurate projections based on data patterns. By integrating real-world environmental data into a 3D visualization, Emission Vision enables users to better explore, analyze, and compare the complex trends of CO2 emissions ratio around the world over time. Users can easily compare countries' contributions, gain insights from the data, and collaborate in real-time, allowing analysts to work together to address global environmental challenges.


## Design Process
Emission Vision is a project designed for the Design for Complex and Dynamic Contexts course at Stockholm University. Our goal was to create a collaborative and interactive tool that could transform environmental data into useful insights with the help of mixed reality.
### Brainstorming
In the initial brainstorming sessions, we focused on identifying areas that could be enhanced with digital twin and mixed reality technologies. We discussed the importance of analyzing CO2 emissions over the years and how this information could be useful in addressing environmental challenges. We explored how to incorporate the three main layers of a Digital Twin: Physical Entity, Virtual Entity, and Data. For the physical entity, we chose the phenomena of CO2 emissions on the planet Earth; hence, for the virtual entity, we decided on a virtual globe.
Initially, we aimed to use real data on the total amount of CO2 emissions per year for each country. However, we realized that using the CO2 emission ratio would be more insightful as it accounts for population differences, providing a clearer picture of each country's impact.
### Wireframes and Prototypes
<img width="760" alt="Screenshot 2024-05-26 at 19 50 56" src="https://github.com/neginsoltanii/EmissionVision/assets/62497271/a35ddc37-ab20-4ff8-bb31-42f675c87b74">
<img width="760" alt="Screenshot 2024-05-26 at 19 51 05" src="https://github.com/neginsoltanii/EmissionVision/assets/62497271/640fdf2e-d5fd-49b7-9694-e7199048115a">

### User Journey
Since our project is multi-user and supports collaboration, we decided on an approach where when one person interacts, the other user cannot interact with anything in the mixed-reality environment (with the virtual objects). As soon as one person stops interacting, the other user is allowed to interact. Here is one potential scenario for the users' journey in this app:
<img width="1037" alt="Screenshot 2024-05-26 at 19 57 39" src="https://github.com/neginsoltanii/EmissionVision/assets/62497271/05ec069a-7e31-462c-ab26-18df10b2ac66">

### Challenges and Solutions
One major challenge was integrating the data into Unity. Some values were in a format that Unity could not read directly. To address this, we conducted Exploratory Data Analysis (EDA) to identify and handle potential missing values and format inconsistencies before integrating the data into our project.


## System description
### Features

- Mixed-reality Setup, showcasing a virtual earth model in the physical space
- Real Data Integration with Unity, displaying CO2 emission ratios from countries worldwide across different years with color-coded countries on the globe
- Interactive Controls, using controllers to rotate the globe and explore data by year
- Real-time Collaboration using Photon Unity Networking (PUN), enabling analysts to collaborate and discuss data trends in real time.
- Compatibility with Meta Quest headsets

## Installation
This section outlines the steps to set up your environment for developing Android VR applications using [Unity](https://unity.com/) 2022.3.

### Step One: Setting Up Unity Hub
1. Download and install Unity Hub from the [Unity download page](https://unity.com/download).
2. Open Unity Hub after installation.

### Step Two: Installing Unity Editor and Required Modules
1. In Unity Hub, navigate to the 'Installs' tab and click on the 'Add' button to install a new version of the Unity Editor.
2. Select Unity Editor LTS version 2022.3.Xf1 or higher from the list of available versions. You can find the recommended versions on the Unity releases page.
3. During the installation setup, ensure to include the following modules:
Microsoft Visual Studio IDE (for code editing).
Android Build Support (libraries necessary for creating Android
4. Follow the instructions to complete the Unity Editor installation.

__Note:__ Since you are only working with a pre-built project and do not need to modify the code, you might not need to install Visual Studio. However, it is recommended that you install it if you need to troubleshoot or make adjustments to the code.

### Step Three: Downloading the Emission Vision Repository
1. Download the zip file from this [GitHub Repository](https://github.com/neginsoltanii/EmissionVision).
2. Extract the zip file to a desired location on your disk.
3. In Unity Hub, navigate to the Projects tab and click on the Add button.
5. Select the folder where you extracted the project and add it to Unity Hub.

### Building and Deploying the Project to Your Headset
__1. Connect the Headset:__
- Connect your VR headset to your computer using the appropriate cable.

__2. Open Build Settings:__
- In Unity, go to __File > Build Settings__.

__3. Switch to Android Platform:__
- In the __Build Settings__ window, if the platform is not already set to Android, select __Android__ from the list and click __Switch Platform__.

__4. Select Run Device:__
- In the __Build Settings__ window, find the __Run Device__ dropdown menu.
- Select your connected VR headset from the dropdown list.
- If your headset is not listed, click the __Refresh__ button and try again.

__5. Enter Keystore Passwords:__
- If prompted for a password during the build process, go to __File > Build Settings > Player Settings__.
- In the __Player Settings__ panel, navigate to __Player__ and scroll down to find __Publishing Settings__.
- Enter the password __123456__ in both the Project Keystore and Project Key fields.

__6. Build and Run:__
- Back in the __Build Settings__ window, click the __Build and Run__ button to start building the project.
- Unity will build the project and deploy it to your connected VR headset.

__7. Verify Deployment:__
- Once the build process is completed, verify that the application is running correctly on your VR headset.

By following these steps, you will be able to build and deploy your VR project to your headset directly from Unity. Make sure your headset is properly connected and recognized by Unity before starting the build process. 

## References
### Unity Assets
- [World Map Globe Edition 2]

### Documentations
- [Spatial Anchors Overview]
- [Shared Spatial Anchors Sample]

## Contributors
- [__Negin Soltani__](https://www.linkedin.com/in/negin-soltani-5764911b9/)
- [__Nicklas Bourelius__](https://www.linkedin.com/in/nicklas-bourelius-1362a9225/)
- [__Tianyu Bao__](https://www.linkedin.com/in/amberbao/)


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


[World Map Globe Edition 2]: <https://assetstore.unity.com/packages/tools/gui/world-map-globe-edition-2-150643>

[Spatial Anchors Overview]: <https://developer.oculus.com/documentation/unity/unity-spatial-anchors-overview/>

[Shared Spatial Anchors Sample]:
<https://developer.oculus.com/documentation/unity/unity-ssa-sf/>

![Sample Banner](./Media/colocated-block-toss.gif 'Unity SSA Sample')

# Unity-SharedSpatialAnchors

Unity-SharedSpatialAnchors was built to demonstrate how to use the Shared Spatial Anchors API, available in the Meta XR Core SDK for the Unity game engine.

The sample app showcases:
- Spatial Anchor Creation, Saving, Loading, and Sharing
- Scene Sharing
- Automatic Colocation
- Passthrough Avatars

This app uses Photon Unity Networking to share anchor data and support interaction with networked objects in a colocated space.

This codebase is available both as a reference and as a template for a project that utilizes shared spatial anchors. Unity-SharedSpatialAnchors is under the license found [here](LICENSE) unless otherwise specified.

## SETUP
You must follow these instructions first: https://developer.oculus.com/documentation/unity/unity-ssa-sf/

## Documentation

Sample App Architecture: https://developer.oculus.com/documentation/unity/unity-ssa-sf/

Scene Sharing: https://developer.oculus.com/documentation/unity/unity-shared-scene-sample/

Health & Safety: https://developer.oculus.com/resources/unity-ssa-hs-app/

See the [CONTRIBUTING](CONTRIBUTING.md) file for how to help out.
