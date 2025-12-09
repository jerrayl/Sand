shader_type canvas_item;

uniform float fill_amount : hint_range(0.0, 1.0) = 0.0;
uniform vec4 fill_color : source_color;
varying float v_norm_y;

void vertex() {
    float height = 400.0f;
    v_norm_y = VERTEX.y / height;
}

void fragment() {
    float mask = (v_norm_y <= fill_amount) ? 1.0 : 0.0;
    COLOR = mix(COLOR, fill_color, mask);
}
