![img](https://i.imgur.com/1SrS3BG.gif)

# A SoftBody3D Pinned Point addon for Godot 4.2+
This addon makes it easier to pin points on a SoftBody3D Node. The softPin Node uses Vertex Colors and separate the areas you want the SoftBody3D to simulate.
> [!WARNING]
> This addon was written in C# and will require Godot Engine - .NET build. A GDScript overhaul will come in the near future.  
# Installation
1. Download the repository as a zip and extract it.
2. Copy the `addons` folder into the root folder of your project.
3. Go to Project > Project Settings > Plugins tab and enable SoftPin

![img](https://i.imgur.com/yX16MdL.png) 

4. Click on the `build project` button to compile C# scripts

![img](https://i.imgur.com/WRO9M6b.png)

5. You're ready to go!

# How to use

## MeshInstance3D

# Current Issues
The issues listed below are cause by Godot not the addon:
When opening your project for the first time, a tabbed scene using Spatial Attachments will print this error - _scene/3d/node_3d.cpp:345 - Condition "!is_inside_tree()" is true. Returning: Transform3D()_
