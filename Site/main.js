import * as level from '/modules/levelSettings.js';
import { PlaySound } from "../modules/playSound.js";
import { TakeEscapeButton, clickedInvItem } from './modules/takeEscapeButton.js';
import { SetField } from './modules/setField.js';
import { SetPlatform } from './modules/platform.js';

const requestURL = 'http://25.32.13.14:5000'
let xhr = new XMLHttpRequest();
level.CreateLevel()

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/get_action?device=SITE');

        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            console.log(info)
            if(info == 'GiveEscapeButton'){ TakeEscapeButton(document.querySelector(".item_list")) }
            if(info == 'Water'){ ShowWater() }
            if(info == 'Button'){ ShowButton() }
            if(info.includes('Ceil') || info.includes('Floor')){ SetField(info) }
            if(info == 'ActivatePlatform'){ SetPlatform() }
            if(info == 'CompleteLevel') { level.CompleteLevel() }
        }
        xhr.send()
    })}, 1000)

export function GiveEscapeButton(x, y, z) {
    return new Promise((resolve, reject) => {
        if (clickedInvItem.className != undefined) {
            clickedInvItem.className = ''
            clickedInvItem.remove()
            PlaySound('laser_send.mp3')
            xhr.open('POST', requestURL + '/action?action=SpawnEscapeButton:'+x+','+y+','+z+'&device=VR');
            xhr.send()
        }
    })}

export function GiveMovePlatform(action, direction) {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/action?action=MovePlatform'+direction + action +'&device=VR')
        xhr.send()
    })}

function ShowWater(){
    let waters = document.querySelectorAll(".water")
    waters.forEach(element => {
        element.classList.add("watershow")
    });
}

function ShowButton(){
    let buttons = document.querySelectorAll(".button")
    buttons.forEach(element => {
        element.classList.add("buttonshow")
    });
}