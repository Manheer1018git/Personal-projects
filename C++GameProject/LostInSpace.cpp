#include "splashkit.h"
#include "lost_in_space.h"
#include "player.h"
#include "power_up.h"
#include <vector>


using std::vector;
using namespace std;


// this is the function where pass our game that we have created 
game_data new_game()
{
    game_data game2;
    game2.player = new_player();
    return game2;
}

// this is the procedure to generate a power up to the game
void add_power_up(game_data& game)
{
    if (rnd() < 0.02)
    {
        game.power_ups.push_back(new_power_up(400, 400));
    }
}

// this is the procedure where we form our entire game project scene
void hud_display(player_data& player_data)
{
    clear_screen(COLOR_BLACK);
  

    draw_text("SCORE: " + to_string(player_data.score), COLOR_WHITE, 0, 10, option_to_screen());
    draw_text("LOCATION: " + point_to_string(center_point(player_data.player_sprite)), COLOR_WHITE, 0, 20, option_to_screen());


    draw_text("SHIELD:", COLOR_WHITE, 0, 60, option_to_screen());
    draw_text("CASH:", COLOR_WHITE, 140, 60, option_to_screen());
    draw_text("COIN:", COLOR_WHITE, 140, 80, option_to_screen());
    draw_text("MUSCLE:", COLOR_WHITE, 0, 80, option_to_screen());

   
    fill_rectangle(COLOR_WHITE, 400, 400, 10, 10);

    load_bitmap("hud", "hud_scaled.png");
    draw_bitmap("hud", 10, 10, option_to_screen());

    load_bitmap("cash", "cash.png");
    if (player_data.score >= 500)
    {
        draw_bitmap("cash", 150, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.score >= 1000)
    {
        draw_bitmap("cash", 170, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));

    }
    if (player_data.score >= 1500)
    {
        draw_bitmap("cash", 190, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }

    load_bitmap("coin", "coin.png");
    if (player_data.score >= 100)
    {
        draw_bitmap("coin", 150, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.score >= 200)
    {
        draw_bitmap("coin", 170, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.score >= 300)
    {
        draw_bitmap("coin", 190, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }

    
    load_bitmap("muscle", "muscle.png");
    if (player_data.muscle_level >= 0.25)
    {

        draw_bitmap("muscle", 10, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.muscle_level >= 0.50)
    {
        draw_bitmap("muscle", 10, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("muscle", 30, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.muscle_level >= 0.75)
    {
        draw_bitmap("muscle", 10, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("muscle", 30, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("muscle", 50, 30, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }

    load_bitmap("empty_bar", "empty_bar_transparent.png");
    load_bitmap("full_bar", "green_bar_bubbles.png");

    draw_bitmap("empty_bar", 0, 85, option_to_screen());
    draw_bitmap("full_bar", 0, 85, option_part_bmp(0, 0, player_data.fuel_pct * bitmap_width("full_bar"), bitmap_height("full_bar"), option_to_screen()));

    load_bitmap("shield", "shield.png");
    
    if (player_data.shield_level >= 0.25)
    {
        draw_bitmap("shield", 30, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));

    }
    if (player_data.shield_level >= 0.50)
    {
        draw_bitmap("shield", 30, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("shield", 50, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }
    if (player_data.shield_level >= 0.75)
    {
        draw_bitmap("shield", 30, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("shield", 50, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
        draw_bitmap("shield", 70, 10, option_to_screen(option_scale_bmp(0.2, 0.2)));
    }

}

// this is the procedure where we draw the game scene on our screens
void draw_game(game_data& game)
{
    
    hud_display(game.player);
    draw_player(game.player);
    for (int i = 0; i < game.power_ups.size(); i++)
    {
        draw_power_up(game.power_ups[i]);
    }
}

// this is the procedure to apply power ups when the user collides with them
void apply_power_up(game_data& game, power_up_kind kind)
{
    play_sound_effect("bloop.wav");

    switch (kind)
    {
    case SHIELD:
        game.player.shield_level = game.player.shield_level + 0.25;
        if (game.player.shield_level >= 1.0)
        {
            game.player.shield_level = 1.0;
        }
        break;
    case FUEL:
        game.player.fuel_pct = game.player.fuel_pct + 0.25;
        if (game.player.fuel_pct >= 1.0)
        {
            game.player.fuel_pct = 1.0;
        }
        break;
    case MUSCLE:
        game.player.muscle_level = game.player.muscle_level + 0.25;
        if (game.player.muscle_level >= 1.0)
        {
            game.player.muscle_level = 1.0;
        }
        break;
    case BULLET:
        game.player.shield_level = game.player.shield_level - 0.25;
        break;
    case POWER:
        game.player.muscle_level = game.player.muscle_level - 0.25;
        game.player.score = game.player.score - 100;
        game.player.fuel_pct = game.player.fuel_pct - 0.25;
        if (game.player.muscle_level < 0)
        {
            game.player.muscle_level = 0;
        }
        if (game.player.fuel_pct < 0)
        {
            game.player.fuel_pct = 0;
        }
        if (game.player.score < 0)
        {
            game.player.score = 0;
        }
        break;
    case COIN:
        game.player.score = game.player.score + 100;
        break;
    default:
        break;
    }

}

// this is the procedure to check whether the user touches something in the game
void check_collisions(game_data& game)
{
    for (int i = game.power_ups.size() - 1; i >= 0; i--)
    {
        if (sprite_collision(game.player.player_sprite, game.power_ups[i].power_up_sprite))
        {
            apply_power_up(game, game.power_ups[i].kind);
            remove_power_up(game, i);
        }
    }
}

// this is the procedure to remove a power up when the user collides with a bad one such as a bullet
void remove_power_up(game_data& game, int index)
{
    if (index >= 0 and index < game.power_ups.size())
    {
        int last_idx;
        last_idx = game.power_ups.size() - 1;
        game.power_ups[index] = game.power_ups[last_idx];
        game.power_ups.pop_back();
    }
}

// this is the procedure where we update our game
void update_game(game_data& game)
{
    update_player(game.player);
    add_power_up(game);

    for (int i = game.power_ups.size() - 1; i >= 0; i--)
    {
        update_power_up(game.power_ups[i]);
    }
    check_collisions(game);
}
