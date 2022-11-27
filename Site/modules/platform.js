import { GiveMovePlatform } from '/main.js';

export function SetPlatform(){
    let platform = document.querySelector('.mover')
    platform.classList.remove('hidden')
    platform.querySelector('.arrow_button.left').onclick = function() { Move('Left') }
    platform.querySelector('.arrow_button.right').onclick = function() { Move('Right') }
    let game_field = document.querySelector('.floor').children
    for (let index = 0; index < 2; index++) {
        for (let j = 8; j < 10; j++) {
            let element = game_field[index].childNodes[j]
            element.classList.add("platform")
        }
    }
}

let position = 8;
let timeout
let working = false
function Move(dir) {
    if(working || (position == 8 && dir == 'Right') || position == 5 && dir == 'Left') {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = document.querySelector('.floor').children
        for (let index = 0; index < 2; index++) {
            for(let j = position; j < position + 2; j++){
                let element = game_field[index].childNodes[j]
                element.classList.remove("platform")
            }
        }
        position = dir == 'Left' ? position - 1 : position + 1;
        for (let index = 0; index < 2; index++) {
            for(let j = position; j < position + 2; j++){
                let element = game_field[index].childNodes[j]
                element.classList.add("platform")
            }
        }
        if((position == 5 && dir == 'Left') || (position == 8 && dir == 'Right')){
            clearInterval(timeout)
            working = false
            GiveMovePlatform('End', dir)
        }
    }, 1100)
    GiveMovePlatform('Start', dir)
}