# fractal-primitives-unity

# Purpose
This project generates fractal tree and puts primitives on it. Inspired by [Catlike Coding Unity C# Tutorials](https://catlikecoding.com/unity/tutorials/constructing-a-fractal/).

The aim is to generate 3d training data from scratch, to increase performance in 3D deep learning, such as point cloud registration or feature matching.

Behavior has been tested with `Windows 10` with `Unity 2020.3.3f1`.

This project saves `.fbx` file, and processes them into watertight meshes using blender (blender part to be added)

# Usage
## Generate one fractal primitive
- clone the repository and open up with Unity
- open up the `GeneratroScene` scene
- start the scene, select `FractalGenerator` Object, and hit `Create`
- hit again, and different fractal will pop up

## Generate and save hundreds of fractal primitives
TODO : Add images for more comprehensive explanation.
- clone the repository and open up with Unity
- open up the `GeneratroScene` scene
- Inside `FractalRecorder` Object, edit `FractalGenerateAndSave` scripts' path to save and numver to save.

## Customization
- You can edit `FractalObject` prefab to change generation behavior
- Note that if you make depth deeper, increase `wait_time` in `FracatlGenerateAndSave`, since it'll take while to generate complete fractal tree and put primitives in.

# License
MIT License