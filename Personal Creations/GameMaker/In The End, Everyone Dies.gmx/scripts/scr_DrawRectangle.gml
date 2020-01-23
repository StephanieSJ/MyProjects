///scr_DrawRectangle(Xpos1, Ypos1, Xpos2, Ypos2, Type)
var StartX = argument0;
var StartY = argument1;
var EndX = argument2;
var EndY = argument3;
var Type = argument4;

if (Type == 0) or (Type == 2)
{
    draw_set_colour(c_teal);
}
else
{
    draw_set_colour(c_silver);
}

draw_rectangle(StartX - 95, StartY - 50, EndX + 95, EndY - 5, false);
    
for (var i = 1; i < 12; i++)
{
    switch (i)
    {
        case 1:
        case 2:
        case 3:
        case 13:
        case 14:
        case 15:
        {
            draw_set_colour(c_dkgray);
            break;
        }
        
        case 4:
        case 5:
        case 6:
        case 10:
        case 11:
        case 12:
        {
            draw_set_colour(c_gray);
            break;
        }
        
        case 7:
        case 8:
        case 9:
        {
            draw_set_colour(c_ltgray);
            break;
        }
        
        default:
        {
            if (Type == 0) or (Type == 2)
            {
                draw_set_colour(c_teal);
            }
            else
            {
                draw_set_colour(c_silver);
            }
            break;
        }
    }
    
    draw_rectangle(StartX - 100 + i, StartY - 55 + i, EndX + 100 - i, EndY - i, true);
}
