import { PlaySound } from "../modules/playSound.js";

export let clickedInvItem = ''

export function TakeEscapeButton(invul){
    PlaySound('laser_send.mp3')
    let invli = document.createElement('li')
    invli.className = "not_active"
    invli.onclick = function() { ChangeActive(invli) }
    let invtext = document.createElement('div')
    invli.append(invtext)
    invul.append(invli)
}

function ChangeActive(element){
    if (element.className == "active"){
        clickedInvItem.className = "not_active"
        clickedInvItem = ""
    }
    else{
        if (clickedInvItem.className != undefined) {
            clickedInvItem.className = "not_active"
        }
        clickedInvItem = element
        element.className = "active"
    }
}