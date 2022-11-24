import { GiveMovePlatform } from '/main.js';

export function SetPlatform(){
    let platform = document.querySelector('.mover')
    platform.classList.remove('hidden')
    platform.querySelector('.arrow_button.left').onclick = function() { MoveLeft('Left') }
    platform.querySelector('.arrow_button.right').onclick = function() { MoveRight('Right') }
    let game_field = document.querySelector('.floor').children
    for (let index = 8; index < 10; index++) {
        for (let j = 0; j < 2; j++) {
            let element = game_field[index].childNodes[j]
            element.classList.add("platform")
        }
    }
}

let position = 9;
let timeout
let working = false
function MoveLeft(dir) {
    if(working) {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = document.querySelector('.floor').children
        for (let index = position; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.remove("platform")
            }
        }
        position--;
        for (let index = position - 1; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.add("platform")
            }
        }
        if(position == 6){
            clearInterval(timeout)
            working = false
            GiveMovePlatform('End', dir)
        }
    }, 1100)
    GiveMovePlatform('Start', dir)
}

function MoveRight(dir) {
    if(working) {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = document.querySelector('.floor').children
        for (let index = position - 1; index < position; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.remove("platform")
            }
        }
        position++;
        for (let index = position; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.add("platform")
            }
        }
        if(position == 9){
            clearInterval(timeout)
            working = false
            GiveMovePlatform('End', dir)
        }
    }, 1100)
    GiveMovePlatform('Start', dir)
}