///scr_CheckGuiHover
global.GuiHover = false;
for (var i = 0; i < global.MenuTabs; i++)
{
    if (instance_exists(global.BuildMenus[i]))
    {
        var BuildMenuInstance = instance_nearest(0, 0, global.BuildMenus[i]);
        with (BuildMenuInstance)
        {
            if (Hover)
            {
                global.GuiHover = true;
                exit;
            }
        }
    }
}
