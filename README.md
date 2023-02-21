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

# It is important
After importing the meshes into Unity, you need to set Read/Write to true.

# Extracting Unity Files
1) Download an asset ripper. (I used AssetRipper found on Github)
2) In AssetRipper select all the .asset, .ress and any files that could relate to Assets and Meshes. (I used it on the Entire Game Folder)
3) Move all the Asset files from the exporter to a Unity Project.

# Modifying Existing Meshes
1) Download an asset extractor which converts the files to a format accepted by Blender. Such as .obj, .fbx etc.
(I attempted to use the Unity package FBX Exporter but couldnt get it to work, Dragon prompted me to AssetBundleExtractor which is a bit difficult to navigate for the meshes, however it still worked.)
2) Find the mesh you want to manipulate (Dont worry about the materials etc yet) and export it to a format accepted by Blender. Such as .obj, .fbx etc.
3) Import and manipulate in Blender.
4) Export as a .fbx file (It holds the information about multiple meshes)
5) Import the .fbx file into Unity and assign meshes etc.

# Build Unity Bundle

In Unity Editor:
1) You need to select the path to the game exe in the ThunderKit settings and click Import.  There may be errors in AssemblyCsharp if PML is installed.  If there is an error, delete pml dll from Pulsar/Packages/PulsarLostColony/PulsarModLoader.dll, or try importing the dll from a clean version of the game.
2) Select Pipe file in Assets and change path to your mods directory
3) Click Execute in Pipe file
4) 👍

Possible problems:
1) After restarting unity, errors may appear due to missing scripts.  Just delete the Pulsar/Packages/PulsarLostColony folder and click Import again in ThunderKit. And copy again PulsarExperiments.dll to Pulsar/Assets/
