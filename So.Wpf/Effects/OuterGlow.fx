/// <class>DirectionalBlurEffect</class>
/// <description>An effect that blurs in a single direction.</description>
sampler2D  Texture1Sampler : register(S0);
//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

/// <summary>The direction of the blur (in degrees).</summary>
/// <minValue>0</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>0</defaultValue>
float Angle : register(C0);

/// <summary>The scale of the blur (as a fraction of the input size).</summary>
/// <minValue>0</minValue>
/// <maxValue>0.01</maxValue>
/// <defaultValue>0.003</defaultValue>
float BlurAmount : register(C1);




float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 c = 0;
    float rad = Angle * 0.0174533f;
    float xOffset = cos(rad);
    float yOffset = sin(rad);

    for(int i=0; i<16; i++)
    {
        uv.x = uv.x - BlurAmount * xOffset;
        uv.y = uv.y - BlurAmount * yOffset;
        c += tex2D(Texture1Sampler, uv);
    }
    c /= 16;
    
    return c;
}

float4 PS_BlurHorizontal( float2 Tex : TEXCOORD0 ) : COLOR0
{
    float Color = 0.0f;

    Color += tex2D(sampler, float2(Tex.x - 3.0*blurSizeX, Tex.y)) * 0.09f;
    Color += tex2D(sampler, float2(Tex.x - 2.0*blurSizeX, Tex.y)) * 0.11f;
    Color += tex2D(sampler, float2(Tex.x - blurSizeX, Tex.y)) * 0.18f;
    Color += tex2D(sampler, Tex) * 0.24f;
    Color += tex2D(sampler, float2(Tex.x + blurSizeX, Tex.y)) * 0.18f;
    Color += tex2D(sampler, float2(Tex.x + 2.0*blurSizeX, Tex.y)) * 0.11f;
    Color += tex2D(sampler, float2(Tex.x + 3.0*blurSizeX, Tex.y)) * 0.09f;

    return Color;
}

float4 PS_BlurVertical( float2 Tex : TEXCOORD0 ) : COLOR0
{
    float Color = 0.0f;

    Color += tex2D(sampler, float2(Tex.x, Tex.y - 3.0*blurSizeY)) * 0.09f;
    Color += tex2D(sampler, float2(Tex.x, Tex.y - 2.0*blurSizeY)) * 0.11f;
    Color += tex2D(sampler, float2(Tex.x, Tex.y - blurSizeY)) * 0.18f;
    Color += tex2D(sampler, Tex) * 0.24f;
    Color += tex2D(sampler, float2(Tex.x, Tex.y + blurSizeY)) * 0.18f;
    Color += tex2D(sampler, float2(Tex.x, Tex.y + 2.0*blurSizeY)) * 0.11f;
    Color += tex2D(sampler, float2(Tex.x, Tex.y + 3.0*blurSizeY)) * 0.09f;

    return Color;
}
