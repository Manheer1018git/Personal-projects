#ifndef LOST_IN_SPACE_POWER_UP
#define LOST_IN_SPACE_POWER_UP

#include "splashkit.h"
#include <vector>

using namespace std;


#define PLAYER_SPEED 1.5
#define PLAYER_ROTATE_SPEED 3
#define SCREEN_BORDER 100

// Different options of power ups
enum power_up_kind
{
    SHIELD,
    BULLET,
    FUEL,
    MUSCLE,
    POWER,
    COIN,
    CASH
};

// The power data keeps track of all of the information related to the power ups.
struct power_up_data
{
    sprite power_up_sprite;
    power_up_kind kind;

};

/**
 * Creates a new power up in the with the location of x y.
 *
 * @returns     The new power up data
 */
power_up_data new_power_up(double x, double y);

/**
 * Draws the power up to the screen.
 *
 * @param power_up_to_draw    The power up to draw to the screen
 */
void draw_power_up(const power_up_data& power_up_to_draw);

/**
 * Read user input and update the power_up based on this interaction.
 *
 * @param power_up_to_update    The power up to update
 */
void update_power_up(power_up_data& power_up_to_update);



#endif 
