///scr_DestroySurvivalMenu
if (instance_exists(obj_SurvivalMenu))
{
    with (obj_SurvivalMenu)
    {
        instance_destroy();
    }
}

if (instance_exists(obj_ScrollBar))
{
    with (obj_ScrollBar)
    {
        instance_destroy();
    }
}

if (instance_exists(obj_ScrollBarSlider))
{
    with (obj_ScrollBarSlider)
    {
        instance_destroy();
    }
}

if (instance_exists(obj_ScrollingMenuController))
{
    with (obj_ScrollingMenuController)
    {
        instance_destroy();
    }
}

if (instance_exists(obj_TimePlanner))
{
    with (obj_TimePlanner)
    {
        instance_destroy();
    }
}
