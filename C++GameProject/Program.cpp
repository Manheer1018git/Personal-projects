#include "splashkit.h"
#include "player.h"
#include "power_up.h"
#include "lost_in_space.h"



using namespace std;

// we declare a location to string function for the location
string location_to_string(const player_data& player_data)
{
    point_2d location = center_point(player_data.player_sprite);
    int x = (int)location.x;
    int y = (int)location.y;
    return to_string(x) + ", " + to_string(y);
}

/**
 * Load the game images, sounds, etc.
 */
void load_resources()
{
    load_resource_bundle("game_bundle", "lost_in_space.txt");
}

/**
 * Entry point.
 * 
 * Manages the initialisation of data, the event loop, and quitting.
 */                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     //main body                                                                                                                                                                
int main()
{
    open_window("Lost In Space", 800, 800);
    load_resources();
    game_data game2;
    game2 = new_game();
    while ( not quit_requested() )
    {
        process_events();
        handle_input(game2.player);
        update_game(game2);
        draw_game(game2);
        refresh_screen(60);  
    }

    return 0;
}
