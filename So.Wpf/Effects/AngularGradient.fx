// no input texture, the output is completely generated in code

/// <summary>The center of the gradient. </summary>
/// <minValue>0,0</minValue>
/// <maxValue>1,1</maxValue>
/// <defaultValue>.5,.5</defaultValue>
float2 centerPoint : register(C0);

/// <summary>The primary color of the gradient. </summary>
/// <defaultValue>Blue</defaultValue>
float4 primaryColor : register(C1);

/// <summary>The primary color of the gradient. </summary>
/// <defaultValue>Red</defaultValue>
float4 secondaryColor : register(C2);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float2 p = float2(centerPoint)-uv;
	float angle = (atan2(p.x, p.y) + 3.141596) / (2 * 3.141596);
	float4 color = lerp(float4(primaryColor.rgb, 1), float4(secondaryColor.rgb, 1), saturate(angle));
		return color;
}

