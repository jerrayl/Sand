shader_type canvas_item;

uniform float fill_amount : hint_range(0.0, 1.0) = 0.0;
uniform vec4 fill_color : source_color;

void fragment() {
	float polygon_height = 150.0;
	float mask = (VERTEX.y <= fill_amount * polygon_height) ? 1.0 : 0.0;
    COLOR = mix(COLOR, fill_color, mask);
}