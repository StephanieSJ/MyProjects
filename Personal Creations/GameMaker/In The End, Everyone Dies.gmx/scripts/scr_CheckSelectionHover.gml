///scr_CheckSelectionHover
global.SelectionHover = false;
for (var i = 0; i < instance_number(obj_ScrollingMenuController); i++)
{
    if (instance_find(obj_ScrollingMenuController, i).SelectionHover)
    {
        global.SelectionHover = true;
        break;
    }
}
