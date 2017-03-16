R""(
#version 400 core
in vec3 vertexNormal;
in vec3 vertexPosition;
in vec3 vertexColor;
in vec2 textureCoords;

layout (location = 0) out vec3 positionBuffer;
layout (location = 1) out vec3 normalBuffer;
layout (location = 2) out vec3 colorBuffer;
layout (location = 3) out vec4 pointLightColorBuffer; 

uniform sampler2D perlinNoise;
uniform int objectType;

#define OBJECT_TYPE_NONE 0
#define OBJECT_TYPE_DEFAULT 1
#define OBJECT_TYPE_POINTLIGHT 2

void main()
{
  pointLightColorBuffer = vec4(0.f);
  colorBuffer = vec3(0.f);
  normalBuffer = vec3(0.f);
  switch(objectType)
  {
    case OBJECT_TYPE_POINTLIGHT:
    {
      pointLightColorBuffer = vec4(vertexColor, 1.f);
      normalBuffer = normalize(vertexNormal);
    } break;
    case OBJECT_TYPE_DEFAULT:
    {
      vec3 proceduralNormal = texture(perlinNoise,
                                        textureCoords.xy
                                          * (vertexPosition.x
                                          * vertexPosition.y * vertexPosition.z)).rgb;
      
      proceduralNormal = proceduralNormal * 2 - vec3(1);
      //a more appealing way of doing this is needed.
      normalBuffer = normalize(vertexNormal + proceduralNormal / 2);

      positionBuffer = vertexPosition;
      colorBuffer = vertexColor;
    } break;
  }
}
)"";
