# AndriodDemo

Android game using unity

To run the Script in a new project, first, create a Basic(Built-in) scene and make the below Layer:

3: Ground

Now add the "ObjectCreationScript" to the Main Camera of the newly created scene play. All objects should be generated and work as expected.

The following touch gestures were implemented:

Tap - Selects and deselects objects.
Drag - moves object around if selected; else moves the camera around.
Pinch - scales the object if selected: or zooms in the camera if no object is selected.
Rotate - rotates a selected object or the camera if non is selected.
Two fingers drag - Rotates the camera in a fixed position to either up/down or left/right.
Press - If an object is selected and the finger held for a couple of seconds, the object starts shaking, or the camera starts shaking if no object is selected.
Reset Button - Resets the Scene.

Note: to get a green grass effect for the plane you need to have a Resources folder in the Assests directory with the Grass.mat file in it.
