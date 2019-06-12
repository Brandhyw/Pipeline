# Pipeline
C# Simple Pipeline Library

It is created on the basis that a pipeline just contains a series of functions.
These functions will always be executed in order and with them a context will be passed.
The context is used for debugging purposes (timing and logging).

My intended use of this pipeline implementation is for procedural generation.
The thought is that you use a series of processes to alter the heightmap of the terrain.
