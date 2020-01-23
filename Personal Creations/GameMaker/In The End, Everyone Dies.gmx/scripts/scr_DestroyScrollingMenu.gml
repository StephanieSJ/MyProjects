///scr_DestroyScrollingMenu(InstanceNumber)
var Number = argument0;

if (instance_exists(obj_ScrollingMenuController))
{
    with (obj_ScrollingMenuController)
    {
        if (InstanceNumber == Number)
        {
            instance_destroy();
        }
    }
}

if (instance_exists(obj_ScrollBar))
{
    with (obj_ScrollBar)
    {
        if (InstanceNumber == Number)
        {
            instance_destroy();
        }
    }
}

if (instance_exists(obj_ScrollBarSlider))
{
    with (obj_ScrollBarSlider)
    {
        if (InstanceNumber == Number)
        {
            instance_destroy();
        }
    }
}
