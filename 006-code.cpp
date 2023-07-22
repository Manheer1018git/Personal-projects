#include "splashkit.h"
#include "player.h"
#include "power_up.h"
#include "lost_in_space.h"


using std::vector;
using namespace std;

// this is the function which has all the cases of different power ups
bitmap power_up_bitmap(power_up_kind kind)
{
    switch (kind)
    {
        case SHIELD:
        return bitmap_named("shield");
        break;
        case BULLET:
        return bitmap_named("bullet");
        break;
        case FUEL:
        return bitmap_named("fuel");
        break;
        case MUSCLE:
        return bitmap_named("muscle");
        break;
        case POWER:
        return bitmap_named("power");
        break;
        case COIN:
        return bitmap_named("coin");
        break;
        case CASH:
        return bitmap_named("cash");
        break;
        default:
        return bitmap_named("cash");
        
    }
}

// this is the function where we set the speed of the power ups and its location
power_up_data new_power_up(double x, double y)
{
    power_up_data result;

    result.kind = static_cast<power_up_kind> (rnd(6));
    result.power_up_sprite = create_sprite(power_up_bitmap(result.kind));

    sprite_set_x(result.power_up_sprite, x);
    sprite_set_y(result.power_up_sprite, y);

    sprite_set_dx(result.power_up_sprite, rnd() * 4 - 2);
    sprite_set_dy(result.power_up_sprite, rnd() * 4 - 2);

    return result;


}



// this is the procedure to draw the power ups
void draw_power_up(const power_up_data& power_up_to_draw)
{
    draw_sprite(power_up_to_draw.power_up_sprite);
}

// this is the procedure to update the power ups
void update_power_up(power_up_data& power_up_to_update)
{
    update_sprite(power_up_to_update.power_up_sprite);
}