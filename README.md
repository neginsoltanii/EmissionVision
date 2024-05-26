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
