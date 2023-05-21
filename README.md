# PulsarExperiments

An example of the implementation of scary things. (Scary - because the code is scary, very scary)

# Examples
- Melee Weapon (with pseudo-api, 2 with primitive logic + 1 with special logic)
- Custom Gun (just with custom prefab, 1 item)
- Custom Item (code + prefab, Engineer Table)
- Space Drone (just code & prefab)
- Custom Faces (for robots only, special patch + code)

# Used

[ThunderKit](https://github.com/PassivePicasso/ThunderKit) - game dll import & asset build script
[PML](https://github.com/PULSAR-Modders/pulsar-mod-loader) - modding api

# It is important - FOR INTERIORS
After importing the meshes into Unity, you need to set Read/Write to true.
![image](https://user-images.githubusercontent.com/41182613/220408063-b53c5b1e-ea36-4c7c-8a3d-a5acefca33c9.png)


# Extracting Unity Files
1) Download an asset ripper. (I used AssetRipper found on Github)
2) In AssetRipper select all the .asset, .ress and any files that could relate to Assets and Meshes. (I used it on the Entire Game Folder)
3) Move all the Asset files from the exporter to a Unity Project if you want to browse through the entire game. For extracting GameObjects in general, you only need the folders:
  - Material
  - Mesh
  - Resources
  - Scripts (This one is temporary, just to get the script properties, need to manually recreate them with the Thunderkit script references)
  - Shader
  - Texture2D

# Modifying Existing GameObjects
1) Extract the unity files as shown above.
2) Open the Unity project with the extracted unity files in the assets.
3) Import the game scripts with ThunderKit.
4) Put the GameObjects you want to edit into the scene.
Findout how to disable Asset refreshing when deleting the scripts in assets :c
6) Delete the files within the `Scripts` folder. (Ensure you have a backup if you want to do multiple GameObjects)
7) Copy the scripts into new ThunderKit game scripts.
8) Save prefab and export like normal.

# Modifying Existing Meshes
1) Download an asset extractor which converts the files to a format accepted by Blender. Such as .obj, .fbx etc.
(I attempted to use the Unity package FBX Exporter but couldnt get it to work, Dragon prompted me to AssetBundleExtractor which is a bit difficult to navigate for the meshes, however it still worked.)
2) Find the mesh you want to manipulate (Dont worry about the materials etc yet) and export it to a format accepted by Blender. Such as .obj, .fbx etc.
3) Import and manipulate in Blender.
4) Export as a .fbx file (It holds the information about multiple materials)
5) Import the .fbx file into Unity and assign materials etc.

# Build Unity Bundle

In Unity Editor:
1) You need to select the path to the game exe in the ThunderKit settings and click Import.  There may be errors in AssemblyCsharp if PML is installed.  If there is an error, delete pml dll from Pulsar/Packages/PulsarLostColony/PulsarModLoader.dll, or try importing the dll from a clean version of the game.
2) Select Pipe file in Assets and change path to your mods directory
3) Click Execute in Pipe file
4) 👍

Possible problems:
1) After restarting unity, errors may appear due to missing scripts.  Just delete the Pulsar/Packages/PulsarLostColony folder and click Import again in ThunderKit. And copy again PulsarExperiments.dll to Pulsar/Assets/
