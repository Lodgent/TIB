import { GiveMovePlatform } from '../main.js';
import {activeLevel} from './levelSettings.js';

let position = 0
let max = 0
let min = 0

export function SetPlatform(){
    let platform = document.querySelector('.mover')
    platform.classList.remove('hidden')
    if(activeLevel.platformDir == "horizontal"){
        let arrows = platform.querySelector('.horizontal')
        arrows.classList.remove('hidden')
        platform.querySelector('.arrow_button.left').onclick = function() { Move('Left') }
        platform.querySelector('.arrow_button.right').onclick = function() { Move('Right') }
        position = activeLevel.platformStart;
        max = activeLevel.platformStart
        min = activeLevel.platformStart - activeLevel.platformDelta 
    }
    else{
        let arrows = platform.querySelector('.vertical')
        arrows.classList.remove('hidden')
        platform.querySelector('.arrow_button.up').onclick = function() { Move('Up') }
        platform.querySelector('.arrow_button.down').onclick = function() { Move('Down') }
        position = 0
        max = activeLevel.platformDelta
        min = 0
    }
    let game_field = document.querySelector('.' + activeLevel.platformPosition).children
    for (let index = activeLevel.y - 2; index < activeLevel.y; index++) {
        console.log(activeLevel.y)
        for (let j = activeLevel.platformStart; j < activeLevel.platformStart + 2; j++) {
            let element = game_field[index].childNodes[j]
            element.classList.add("platform")
        }
    }
}
let timeout
let working = false
function Move(dir) {
    if(working || (position == max && (dir == 'Right' || dir == 'Up')) || (position == min && (dir == 'Left' || dir == 'Down'))) {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = 0
        if(activeLevel.platformDir == "horizontal"){
            game_field = document.querySelector('.floor').children
            for (let index = 0; index < 2; index++) {
                for(let j = position; j < position + 2; j++){
                    let element = game_field[index].childNodes[j]
                    element.classList.remove("platform")
                }
            }
        }
        position = (dir == 'Left' || dir == 'Down') ? position - 1 : position + 1;
        console.log(position)
        if(activeLevel.platformDir == "horizontal"){
            for (let index = 0; index < 2; index++) {
                for(let j = position; j < position + 2; j++){
                    let element = game_field[index].childNodes[j]
                    element.classList.add("platform")
                }
            }
        }
        if((position == min && (dir == 'Left' || dir == 'Down')) || (position == max && (dir == 'Right' || dir == 'Up'))){
            clearInterval(timeout)
            working = false
            GiveMovePlatform('End', dir)
        }
    }, activeLevel.timeMovePlatform)
    GiveMovePlatform('Start', dir)
}