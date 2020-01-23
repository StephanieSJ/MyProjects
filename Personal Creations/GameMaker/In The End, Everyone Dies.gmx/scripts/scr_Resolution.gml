///scr_Resolution
var DisplayWidth = global.ResolutionWidth;
var DisplayHeight = global.ResolutionHeight;

display_set_gui_size(DisplayWidth, DisplayHeight);

window_set_size(DisplayWidth, DisplayHeight);

var BaseWidth = 960;
var BaseHeight = 540;

var AspectRatio = BaseWidth / BaseHeight;

if (DisplayWidth >= DisplayHeight)
{
    var Height = min(BaseHeight, DisplayHeight);
    var Width = Height * AspectRatio;
}

surface_resize(application_surface, Width, Height);
