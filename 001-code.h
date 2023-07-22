#ifndef LOST_IN_SPACE
#define LOST_IN_SPACE

#include "splashkit.h"
#include <vector>
#include "player.h"
#include "power_up.h"

#include <string>
using std::vector;
using namespace std;

// The game data keeps track of all of the information related to the game.
struct game_data 
{
    player_data player;
    vector<power_up_data> power_ups;

};

// this creates our new version of the game.
game_data new_game();

// this is the procedure call to the formation of the entire scene of our game
void hud_display(player_data& player_data);

// this draws the game on our screens 
void draw_game(game_data& game);

// this is the procedure call to update the game
void update_game(game_data& game);

// this is the procedure call to add power ups
void add_power_up(game_data& game);

// this is the procedure call to update our power ups
void update_power_up(power_up_data& power_up_to_update);

// this is the procedure call to check if user touches any power ups
void check_collisions(game_data& game);

// this is the procedure call to apply a power up if there is a collision with good power ups
void apply_power_up(game_data& game, power_up_kind kind);

// this is the procedure call to remove a power up if there is a collision with bad power ups
void remove_power_up(game_data& game, int index);



#endif