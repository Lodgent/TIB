import { PlaySound } from "./playSound.js";

export let clickedInvItem = ''

export function TakeInventory(invul, element){
    PlaySound('../sounds/laser_send.mp3')
    let invli = document.createElement('li')
    invli.classList.add("not_active")
    invli.onclick = function() { ChangeActive(invli) }
    let invtext = document.createElement('div')
    if (element.includes('button')) { invtext.classList.add(element) }
    if (element == "symbols") { 
        invtext.classList.add("symbols") 
        invli.onclick = function() { 
            if (invtext.classList.contains("symbols_clicked")){
                invtext.classList.remove("symbols_clicked")
            }
            else{
                invtext.classList.add("symbols_clicked")
            } 
        }
    }
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