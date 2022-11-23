import * as level1 from '/levels/level1.js';
import { PlaySound } from "../modules/playSound.js";
import { TakeEscapeButton, clickedInvItem } from './modules/takeEscapeButton.js';
import { SetField } from './modules/setField.js';
import { SetPlatform } from './modules/platform.js';

const requestURL = 'http://25.20.176.216:5000'
let xhr = new XMLHttpRequest();

let invul = document.createElement('ul')
invul.className = "item_list"
document.body.children[1].append(invul)

level1.SetAllGameFields("ceil", 10)
level1.SetAllGameFields("floor", 0)

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/get_action?device=SITE');

        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            console.log(info)
            if(info == 'GiveEscapeButton'){ TakeEscapeButton(invul) }
            if(info == 'Ceil' || info == 'Floor'){ SetField(info) }
            if(info == 'ActivatePlatform'){ SetPlatform() }
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