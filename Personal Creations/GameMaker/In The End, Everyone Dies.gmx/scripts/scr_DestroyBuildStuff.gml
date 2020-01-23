///scr_DestroyBuildStuff
with (obj_ObjectSelection)
{
    instance_destroy();
}

for (var i = 0; i < global.MenuTabs; i++)
{
    if (instance_exists(global.BuildMenus[i]))
    {
        with (global.BuildMenus[i])
        {
            instance_destroy();
        }
    }
}
