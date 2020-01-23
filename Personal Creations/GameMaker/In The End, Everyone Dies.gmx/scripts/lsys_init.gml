#define lsys_init
// lsys_init(quality)
// Initializes the light system
// quality between 0 and 1 (highest quality)

global._lsys_lights = 0;
global._lsys_casters = 0;
global._lsys_quality = max(10/max(room_width, room_height), argument0);
global._lsys_surface = surface_create(room_width * global._lsys_quality, room_height * global._lsys_quality);


#define lsys_light_add
// lsys_light_add(x, y, radius, color);
// Defines a new point light and returns it's ID

var i;

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (global._lsys_light_deleted[i])
    {
        global._lsys_light_xpos[i] = argument0 * global._lsys_quality;
        global._lsys_light_ypos[i] = argument1 * global._lsys_quality;
        global._lsys_light_radius[i] = argument2 * global._lsys_quality;
        global._lsys_light_color[i] = argument3;
        global._lsys_light_sprite[i] = -1;
        global._lsys_light_rot[i] = 0;
        global._lsys_light_surface[i] = surface_create(argument2 * 2 * global._lsys_quality, argument2 * 2 * global._lsys_quality);
        global._lsys_light_deleted[i] = false;
        global._lsys_light_changed[i] = true;
        return i;
    }
}

global._lsys_light_xpos[global._lsys_lights] = argument0 * global._lsys_quality;
global._lsys_light_ypos[global._lsys_lights] = argument1 * global._lsys_quality;
global._lsys_light_radius[global._lsys_lights] = argument2 * global._lsys_quality;
global._lsys_light_color[global._lsys_lights] = argument3;
global._lsys_light_sprite[global._lsys_lights] = -1;
global._lsys_light_rot[global._lsys_lights] = 0;
global._lsys_light_surface[global._lsys_lights] = surface_create(argument2 * 2 * global._lsys_quality, argument2 * 2 * global._lsys_quality);
global._lsys_light_deleted[global._lsys_lights] = false;
global._lsys_light_changed[global._lsys_lights] = true;

global._lsys_lights += 1;

return global._lsys_lights - 1;


#define lsys_light_set_position
// lsys_light_set_position(ID, x, y);
// Changes the light's position

global._lsys_light_xpos[argument0] = argument1 * global._lsys_quality;
global._lsys_light_ypos[argument0] = argument2 * global._lsys_quality;
global._lsys_light_changed[argument0] = true;


#define lsys_light_set_radius
// lsys_light_set_radius(ID, radius);
// Changes the light's radius

global._lsys_light_radius[argument0] = argument1 * global._lsys_quality;
global._lsys_light_changed[argument0] = true;


#define lsys_light_set_rotation
// lsys_light_set_rotation(ID, angle);
// Changes the light's rotation

global._lsys_light_rot[argument0] = argument1;
global._lsys_light_changed[argument0] = true;


#define lsys_light_set_color
// lsys_light_set_color(ID, color);
// Changes the color of the light

global._lsys_light_color[argument0] = argument1;
global._lsys_light_changed[argument0] = true;


#define lsys_light_set_sprite
// lsys_light_set_sprite(ID, sprite);
// Changes the light's sprite

global._lsys_light_sprite[argument0] = argument1;
global._lsys_light_changed[argument0] = true;


#define lsys_light_delete
// lsys_light_delete(ID);
// Deactivates the light to be overwritten by new light

global._lsys_light_deleted[argument0] = true;
surface_free(global._lsys_light_surface[argument0]);
global._lsys_light_surface[argument0] = -1;


#define lsys_caster_add
// lsys_caster_add(x, y);
// Defines a new shadow caster and returns it's ID

var i;

for (i = 0; i < global._lsys_casters; i += 1)
{
    if (global._lsys_caster_deleted[i])
    {
        global._lsys_caster_points[i] = 0;
        global._lsys_caster_xpos[i] = (argument0 - 4) * global._lsys_quality;
        global._lsys_caster_ypos[i] = (argument1 - 4) * global._lsys_quality;
        global._lsys_caster_cx[i] = 0;
        global._lsys_caster_cy[i] = 0;
        global._lsys_caster_deleted[i] = false;
        return i;
    }
}

global._lsys_caster_points[global._lsys_casters] = 0;
global._lsys_caster_xpos[global._lsys_casters] = (argument0 - 4) * global._lsys_quality;
global._lsys_caster_ypos[global._lsys_casters] = (argument1 - 4) * global._lsys_quality;
global._lsys_caster_cx[global._lsys_casters] = 0;
global._lsys_caster_cy[global._lsys_casters] = 0;
global._lsys_caster_deleted[global._lsys_casters] = false;

global._lsys_casters += 1;

return global._lsys_casters - 1;


#define lsys_caster_add_point
// lsys_caster_add_point(ID, x, y);
// Adds a vertex to the shadow caster relative to it's position
// Returns number of points

var i;

global._lsys_caster_point_x[argument0, global._lsys_caster_points[argument0]] = (argument1 - 4) * global._lsys_quality;
global._lsys_caster_point_y[argument0, global._lsys_caster_points[argument0]] = (argument2 - 4) * global._lsys_quality;

global._lsys_caster_points[argument0] += 1;

global._lsys_caster_cx[argument0] = 0;
global._lsys_caster_cy[argument0] = 0;

for (i = 0; i < global._lsys_caster_points[argument0]; i += 1)
{
    global._lsys_caster_cx[argument0] += global._lsys_caster_point_x[argument0, i];
    global._lsys_caster_cy[argument0] += global._lsys_caster_point_y[argument0, i];
}

global._lsys_caster_cx[argument0] /= global._lsys_caster_points[argument0];
global._lsys_caster_cy[argument0] /= global._lsys_caster_points[argument0];

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (global._lsys_light_deleted[i])
        continue;
    if (lsys_caster_check_inside(argument0, i))
        global._lsys_light_changed[i] = true;
}

return global._lsys_caster_points[argument0];


#define lsys_caster_set_position
// lsys_caster_set_position(ID, x, y);
// Changes the casters position

var i;

global._lsys_caster_xpos[argument0] = (argument1 - 4) * global._lsys_quality;
global._lsys_caster_ypos[argument0] = (argument2 - 4) * global._lsys_quality;

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (global._lsys_light_deleted[i])
        continue;
    if (lsys_caster_check_inside(argument0, i))
        global._lsys_light_changed[i] = true;
}


#define lsys_caster_check_inside
// lsys_caster_check_inside(ID, light ID);

var i;

for (i = 0; i < global._lsys_caster_points[argument0]; i += 1)
{
    if (global._lsys_caster_xpos[argument0] + global._lsys_caster_point_x[argument0, i] >= global._lsys_light_xpos[argument1] - global._lsys_light_radius[argument1]
    && global._lsys_caster_ypos[argument0] + global._lsys_caster_point_y[argument0, i] >= global._lsys_light_ypos[argument1] - global._lsys_light_radius[argument1]
    && global._lsys_caster_xpos[argument0] + global._lsys_caster_point_x[argument0, i] <= global._lsys_light_xpos[argument1] + global._lsys_light_radius[argument1]
    && global._lsys_caster_ypos[argument0] + global._lsys_caster_point_y[argument0, i] <= global._lsys_light_ypos[argument1] + global._lsys_light_radius[argument1])
        return 1;
}

return 0;


#define lsys_caster_delete
// lsys_caster_delete(ID);
// Deactivates the caster to be overwritten by new caster

var i;

global._lsys_caster_deleted[argument0] = true;

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (global._lsys_light_deleted[i])
        continue;
    if (lsys_caster_check_inside(argument0, i))
        global._lsys_light_changed[i] = true;
}


#define lsys_update
// lsys_update(alpha)
// Updates the lightsystem with the given alpha
// Use in step event, only performs actions if something changed

var i, j, k, xp, yp, xs, ys, mul;

draw_set_color(c_black);

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (global._lsys_light_changed[i] == false || global._lsys_light_deleted[i])
        continue;
    
    surface_set_target(global._lsys_light_surface[i]);

    draw_clear(c_black);
    if (global._lsys_light_sprite[i] < 0)
    {
        draw_circle_color(global._lsys_light_radius[i], global._lsys_light_radius[i], global._lsys_light_radius[i], c_white, c_black, false);
    }
    else
    {
        xs = global._lsys_light_radius[i] / sprite_get_width(global._lsys_light_sprite[i]) * 2;
        ys = global._lsys_light_radius[i] / sprite_get_height(global._lsys_light_sprite[i]) * 2;
        draw_sprite_ext(global._lsys_light_sprite[i], 0, global._lsys_light_radius[i], global._lsys_light_radius[i], xs, ys, global._lsys_light_rot[i], c_white, 1);
    }

    xp =  -global._lsys_light_xpos[i] + global._lsys_light_radius[i];
    yp =  -global._lsys_light_ypos[i] + global._lsys_light_radius[i];

    for (j = 0; j < global._lsys_casters; j += 1)
    {
        if (!lsys_caster_check_inside(j, i))
        || (global._lsys_caster_deleted[j])
            continue;
        mul = 10;
        draw_primitive_begin(pr_trianglestrip);
        for (k = 0; k < global._lsys_caster_points[j]; k += 1)
        {
            draw_vertex(xp + global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, k], yp + global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, k]);
            draw_vertex(xp + global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, k] + lengthdir_x(global._lsys_light_radius[i] * mul, point_direction(global._lsys_light_xpos[i], global._lsys_light_ypos[i], global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, k], global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, k])),
            yp + global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, k] + lengthdir_y(global._lsys_light_radius[i] * mul, point_direction(global._lsys_light_xpos[i], global._lsys_light_ypos[i], global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, k], global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, k])));
        }
        draw_vertex(xp + global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, 0], yp + global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, 0]);
        draw_vertex(xp + global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, 0] + lengthdir_x(global._lsys_light_radius[i] * mul, point_direction(global._lsys_light_xpos[i], global._lsys_light_ypos[i], global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, 0], global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, 0])),
        yp + global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, 0] + lengthdir_y(global._lsys_light_radius[i] * mul, point_direction(global._lsys_light_xpos[i], global._lsys_light_ypos[i], global._lsys_caster_xpos[j] + global._lsys_caster_point_x[j, 0], global._lsys_caster_ypos[j] + global._lsys_caster_point_y[j, 0])));
        draw_primitive_end();
    } 
    global._lsys_light_changed[i] = false;
    surface_reset_target();
}

surface_set_target(global._lsys_surface);
draw_clear(c_black);
draw_set_blend_mode(bm_add);
for (i = 0; i < global._lsys_lights; i += 1)
{
    if (!global._lsys_light_deleted[i])
        draw_surface_ext(global._lsys_light_surface[i], global._lsys_light_xpos[i] - global._lsys_light_radius[i], global._lsys_light_ypos[i] - global._lsys_light_radius[i], 1, 1, 0, global._lsys_light_color[i], 1);
}
draw_set_blend_mode(bm_normal);
draw_set_alpha(1-argument0);
draw_rectangle_color(0, 0, room_width * global._lsys_quality, room_height * global._lsys_quality, c_white, c_white, c_white, c_white, false);
draw_set_alpha(1);

surface_reset_target();

global._lsys_changed = false;


#define lsys_draw
// lsys_draw()
// draws the final shadow surface in the view

draw_set_blend_mode_ext(bm_dest_color, bm_zero);
draw_surface_part_ext(global._lsys_surface, view_xview * global._lsys_quality, view_yview * global._lsys_quality, view_wview * global._lsys_quality, view_hview * global._lsys_quality, view_xview, view_yview, 1/global._lsys_quality, 1/global._lsys_quality, c_white, 1);
draw_set_blend_mode(bm_normal);


#define lsys_free
// lsys_free
// Frees all surfaces, lights and casters
// Use this on room change or game end

var i;

for (i = 0; i < global._lsys_lights; i += 1)
{
    surface_free(global._lsys_light_surface[i]);
}
surface_free(global._lsys_surface);

global._lsys_lights = 0;
global._lsys_casters = 0;


#define merge_walls
// merge_walls(obj)
// Verbindet alle nebeneinanderliegende Objekte obj miteinander.
var c;
repeat (2)
{
  with (argument0)
  {
    if (x-9>=0&&x+sprite_get_width(argument0.sprite_index)*image_xscale<=room_width&&object_index=argument0)
    {
     c=collision_point(x-9,y,argument0,false,true)
     if (c)
     {
         if (c.image_yscale=image_yscale&&c.object_index=argument0&&c.y=y)
         {
           x-=c.image_xscale*sprite_get_width(argument0.sprite_index);
           image_xscale+=c.image_xscale;
           with (c)
             instance_destroy();
         }
     }
     c=collision_point(x+sprite_get_width(argument0.sprite_index)*image_xscale,y,argument0,false,true)
     if (c)
     {
       if (c.image_yscale=image_yscale&&c.object_index=argument0&&c.y=y)
       {
         image_xscale+=c.image_xscale;
         with (c)
           instance_destroy();
       }
     }
    }
  }
  with (argument0)
  {
    var c;
    if (y-9>=0&&y+sprite_get_height(argument0.sprite_index)*image_yscale<=room_height&&object_index=argument0)
    {
     c=collision_point(x,y-9,argument0,false,true)
     if (c)
     {
       if (c.image_xscale=image_xscale&&c.object_index=argument0&&c.x=x)
       {
         y-=c.image_yscale*sprite_get_height(argument0.sprite_index);
         image_yscale+=c.image_yscale;
         with (c)
           instance_destroy();
       }
     }
     c=collision_point(x,y+sprite_get_height(argument0.sprite_index)*image_yscale,argument0,false,true)
     if (c)
     {
       if (c.image_xscale=image_xscale&&c.object_index=argument0&&c.x=x)
       {
         image_yscale+=c.image_yscale;
         with (c)
           instance_destroy();
       }
     }
    }
  }
}


#define vram_usage
var i, u;

u = surface_get_width(global._lsys_surface) * surface_get_height(global._lsys_surface) * 4;

for (i = 0; i < global._lsys_lights; i += 1)
{
    if (!global._lsys_light_deleted[i])
        u += surface_get_width(global._lsys_light_surface[i]) * surface_get_height(global._lsys_light_surface[i]) * 4;
}

return u;