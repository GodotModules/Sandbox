# Sandbox
A repo created for testing mostly small scripts.

This is a sandbox, it's a testing playground, it's not meant to be perfect. Once the work is done here, it will be merged with [Godot Modules CSharp](https://github.com/GodotModules/GodotModulesCSharp).

## Scenarios

#### Inventory
https://user-images.githubusercontent.com/6277739/174914687-22dad5c7-6049-4fec-9bfa-99e47098fb2f.mp4

Vision
- This will be like an inventory from Minecraft. Will not be supporting items that take up multiple slots like from Resident Evil because I like there being a noticable gap between slots, it just looks nice imo.

Todo
- [ ] Stack items
- [ ] Display stack count next to cursor when picking up an item
- [ ] Right clicking a stack takes half the stack
- [ ] Player inventory and chest inventory
- [ ] Shift clicking a stack from chest brings that entire stack to the players inventory
- [ ] Holding shift click + drag moves all stacks dragged over to players inventory
- [ ] Inventory auto sort button(s)
- [ ] Holding right click + drag places one item per each slot dragged over
- [ ] Middle click takes all items from chest to players inventory
- [ ] Special inventory slots (main hand, head, body, leggings, boots, hands, shoulders, ring1, etc)
- [ ] Quick stack to nearby chests (would work for both 2D / 3D)
- [ ] Quick take from nearby chests to fill existing item stacks (would work for both 2D / 3D)
- [ ] Show preview of chest contents when hovering over it (would work for both 2D / 3D)

#### CameraShakeTest
https://user-images.githubusercontent.com/6277739/169621183-40ed573e-837f-4564-9a22-7da0c5edec02.mp4

#### DialogueText
https://user-images.githubusercontent.com/6277739/169625417-15e53a92-5ee4-4550-a8e4-8b4be507e79b.mp4

#### Hotkeys
![image](https://user-images.githubusercontent.com/6277739/174914954-3d6fb5fd-992c-452e-a4f6-2d6be237dd53.png)

(see the dev branch of [Godot Modules CSharp](https://github.com/GodotModules/GodotModulesCSharp) for a more up-to-date version of the hotkeys)

## Contributing
1. Fork this repo
2. Clone your fork with `git clone https://github.com/<USERNAME>/GodotModules` (replace `<USERNAME>` with your GitHub username) 
    - *If you get `'git' is not recognized as an internal or external command` then install [Git scm](https://git-scm.com/downloads)*
3. Extract the zip and open the folder in VSCode
4. Go to the source control tab
5. All the files you make changes to should appear here as well, you can stage the files you want to commit, give the commit a message and then push it to your fork
6. Once you have some commits on your fork, you can go [here](https://github.com/GodotModules/Sandbox/pulls) and open up a new pull request and request to merge your work with the main repo

> ⚠️ Before committing anything please talk to `valk#9904` in the [Godot Modules Discord Server](https://discord.gg/866cg8yfxZ) so we can mitigate potential merge conflicts.
