![img](https://i.imgur.com/1SrS3BG.gif)

# A SoftBody3D Pinned Point addon for Godot 4.2+
This addon makes it easier to pin points on a SoftBody3D Node. The `SoftPin Node` uses Vertex Colors and separate the areas you want the SoftBody3D to simulate.
> [!WARNING]
> This addon was written in C# and will require Godot Engine - .NET build. A GDScript overhaul will come in the near future.  

# Installation
1. Download the repository as a zip and extract it.
2. Copy the `addons` folder into the root folder of your project.
3. Go to Project > Project Settings > Plugins tab and enable SoftPin

![img](https://i.imgur.com/yX16MdL.png) 

4. Click on the `build project` button to compile C# scripts

![img](https://i.imgur.com/WRO9M6b.png)

5. You're ready to go! The SoftPin Node should be under Node

![img](https://i.imgur.com/VzXU6Fh.png)

# How to use
>[!NOTE]
>First you'll need to vertex paint your model. That will require Blender or another 3D software of choice.

## Preparing your model
1. Select the mesh you want to vertex paint. 
2. Fill the entire model white or black with NO transparency. Values should look like image below:  

![img](https://i.imgur.com/6F5A1mi.png)

3. Once the model is fully white or black, use any color and paint the area you want simulated. The non colored areas will be pinned.

![img](https://i.imgur.com/hAZesYI.png)

4. Export your model to godot.

## Using the SoftPin Node

This is the softPin Node menu:

![img](https://i.imgur.com/rKPyuAQ.png)

###

# Current Gotchas
> [!CAUTION]
> Below are limitations/issues caused by Godot, not the addon:

* On project start up, having a scene tab open with Nodes that have Spatial Attachments assigned in SoftBody3D prints this error: - _scene/3d/node_3d.cpp:345 - Condition "!is_inside_tree()" is true. Returning: Transform3D()_
* Selecting the SoftBody3D Node After pinning causes editor lag. The denser the model the longer the lag. 
* Unfortunately, When using Jolt3D Physics the quality of the SoftBody3D does not look as good as regular Godot Physics when simulating
* Skeleton3D breaks the SoftBody3D if assigned. 
