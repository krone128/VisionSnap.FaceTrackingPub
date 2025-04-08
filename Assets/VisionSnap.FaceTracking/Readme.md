## Face Tracking with Blendshapes
#### by VisionSnap AS 2025

Unity plugin enabling face tracking with blendshapes on Android

### Supported platforms
* Android
* iOS  - Coming soon!

### Features
* Auto camera controller
* Permission handling
* BlendshapeVisualizer component, ready to work with any rigged face mesh

### Blendshape Format
Plugin provides blendshapes in ARKit format. Compatible with face meshes rigged for ARKit blendshapes. 52 blendshapes with values in 0..1 range

### Building Project
#### Android build setup guide
1. Import this package into your Unity project
2. Open `Edit -> Project Settings -> Player -> Android -> Other Settings` 
* Set Minimum Target API Level to 29 or more
* Enable `Allow unsafe code` checkbox
3. If you don't have `mainTemplate.gradle` in your `Assets/Android/Plugins` folder
* Open `Edit -> Project Settings -> Player -> Android -> Publishing Settings` and enable `Custom Main Gradle Template`
4. Copy contents of `VisionSnap.FaceTracking/Plugins/Android/mainTemplate.gradle` into your `Assets/Plugins/Android/mainTemplate.gradle` file right before first `dependencies` block
5. Open `File -> BuildSettings` and select Android target platform. Press Build button

#### Building Demo Application
1. Go to `VisionSnap.FaceTracking/Examples/Scenes` and open `FaceBlendshapesDemo` scene
2. Open `File -> Build Settings`, add `FaceBlendshapesDemo` to Scene List as startup scene (first in list)
3. In `File -> Build Settings` Select Android target platform and press `Build`
