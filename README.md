# Graphics3D
3D graphics application made using a custom rendering pipeline.
## Application
The scene comprises
* moving car model and three stationary objects (sphere, cube, and torus);
* multiple light sources:
    * two stationary
        1. yellow light coming from the positive _x_ direction,
        2. white light coming from the positive _z_ direction,
    * one white spotlight moving along with the car - **headlights**;
* smooth transitions between day and night;
* distance fog.

The headlights can be adjusted:
* I - move up
* K - move down
* J - move left
* L - move right

There's also an option to make the car swing from one side to another (as if it is going down a bumpy road).

There are three cameras/views available:
* stationary - always targeted toward the center of the scene;
* tracking - doesn't change its position but follows the car;
* TPP (third-person perspective) - tracking the car from behind.

The user can choose between three different [shading models](https://en.wikipedia.org/wiki/Computer_graphics_lighting#Polygonal_shading) (flat, Phong, and Gouraud).

### Animation mode
In the animation mode, the car follows a lemniscate curve.

### Interactive mode
In the interactive mode, the user can change the car's movement:
* W - speed up
* S - slow down
* A - turn left
* D - turn right

## Rendering pipeline
The rendering pipeline is divided into two main phases:
1. vertex processing
2. rasterization.

In short, vertex processing phase is responsible for performing vertex transformations, i.e. model, view, projection, and viewport transforms.

During the rasterization stage, the color of each visible pixel is calculated. Vertex visibility is determined through two tests.
First we "cull the back-faces": faces whose normals align with the direction of observation are discarded.
Then, using z-buffering we make sure that the shapes correctly occlude each other.

Each rendering stage is executed in parallel across all the shape's faces.

## Demo
A short video showing the application in action can be found [here](https://youtu.be/k0YEHQFGgKU).
