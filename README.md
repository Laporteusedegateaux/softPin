![img](https://i.imgur.com/1SrS3BG.gif)
# A SoftBody3D Pinned Point addon for Godot 4.2+
This addon makes it easier to pin points on a `SoftBody3D` Node. The `SoftPin Node` uses Vertex Colors and separate the areas you want the `SoftBody3D` to simulate.
> [!WARNING]
> This addon was written in C# and will require Godot Engine - .NET build. A GDScript overhaul will come in the near future.

# Installation
1. Download the repository as a zip and extract it.
2. Copy the `addons` folder into the root folder of your project.
3. Go to Project > Project Settings > Plugins tab and enable SoftPin

![img](https://i.imgur.com/yX16MdL.png) 

4. Click on the `build project` button to compile C# scripts

![img](https://i.imgur.com/WRO9M6b.png)

5. You're ready to go! The `SoftPin Node` should be under Node

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
Add the `SoftPin Node` to the scene with the mesh you want to pin.

![img](https://i.imgur.com/rKPyuAQ.png)

| Property              |     Description |
| --------------------- | -------------   |
|` Soft Body`  | Used to assign the SoftBody3D you want to pin |
|`Bone Attachment  ` | Used to attach the pinned points to `BoneAttachment3D` when Pin Type: is set to Skelelton3D |
|`Pin Type  `  | Method of pinning, If `MeshInstance3D` is selected you only need a SoftBody. If `Skeleton3D` is selected you'll need to include a BoneAttachment with the SoftBody  |
|` Start Pinning  `     | Used to start the pinning process once ready|
|`Pinning Status `| Debug menu that tells you the status of the pinning or errors                       |

## Pinning MeshInstance3D
When pinning a `MeshInstance3D ` convert the instance into a SoftBody, Assign the `SoftBody3D`, Set the `Pin Type:` to `MeshInstance3D`, and switch `Start Pinning` to On. 

![img](https://i.imgur.com/C58M4oK.gif)

Once you're done you can delete the `SoftPin Node` from the scene or repeat the process with another `SoftBody3D`. Your days of wrestling with pins are finally over. 

## Pinning Skeleton3D
>[!WARNING]
>When using `PinType: Skeleton3D`, this is more ideal for things like hair, hanging cloth, and Accessories that only need one bone. Refer to [Current Gotachas]() for more info.

Before starting to pin your initial setup should look something like this.

![img](https://i.imgur.com/JVkBeV2.png)


Select the `MeshInstance3D` under `Skeleton3D` you want to use. Convert the `MeshInstance3D` into a `SoftBody3D`, Assign the `SoftBody3D`, Assign the `BoneAttachment`, Set the `Pin Type:` to `Skeleton3D`, and switch `Start Pinning` to On.

![img](https://i.imgur.com/cC1AoUB.gif)

Once finished go hit the play button to check out the SoftBody!

![img](https://i.imgur.com/KmEG24v.gif)

# Current Gotchas
> [!CAUTION]
> Below are limitations/issues caused by Godot, not the addon:

* Selecting the `SoftBody3D` Node After pinning causes editor lag. The denser the model the longer the lag. Keeping vert count under 4K per mesh helps. 
* On project start up, having a scene tab open with Nodes that have Spatial Attachments assigned in SoftBody3D prints this error: - _scene/3d/node_3d.cpp:345 - Condition "!is_inside_tree()" is true. Returning: Transform3D()_
* Unfortunately, When using `Jolt3D Physics` the quality of the SoftBody3D does not look as good as regular Godot Physics when simulating
* When you assign `Skeleton3D` In the SoftBody it breaks. Animations on the mesh do not deform properly.  
