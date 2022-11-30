import * as level from '/modules/levelSettings.js';
import { PlaySound } from "../modules/playSound.js";
import { TakeInventory, clickedInvItem } from './modules/inventory.js';
import { SetField } from './modules/setField.js';
import { SetPlatform } from './modules/platform.js';

const requestURL = 'http://25.32.13.14:5000'
let xhr = new XMLHttpRequest();
level.CreateLevel()
TakeInventory(document.querySelector(".item_list"), "laser")

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/get_action?device=SITE');
        
        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            console.log(info)
            if(info == 'GiveEscapeButton'){ TakeInventory(document.querySelector(".item_list"), "button") }
            if(info == 'GiveGreenButton'){ TakeInventory(document.querySelector(".item_list"), "green_button") }
            if(info == 'GiveBlueButton'){ TakeInventory(document.querySelector(".item_list"), "blue_button") }
            if(info == 'Water'){ ShowWater() }
            if(info == 'SequenceButton'){ ShowButton() }
            if(info == 'Block') { ShowPlatform() }
            if(info == 'GreenBlock') { GreenShowPlatform() }
            if(info == 'BlueBlock') { BlueShowPlatform() }
            if(info.includes('Ceil') || info.includes('Floor')){ SetField(info) }
            if(info == 'ActivatePlatform'){ SetPlatform() }
            if(info == 'CompleteLevel') { level.CompleteLevel() }
            if(info == 'Symbols') { TakeInventory(document.querySelector(".item_list"), "symbols") }
            if(info == 'GiveLaser') { TakeInventory(document.querySelector(".item_list"), "laser") }
        }
        xhr.send()
    })}, 1000)

export function GiveEscapeButton(x, y, z, platform='basic') {
    return new Promise((resolve, reject) => {
        if (clickedInvItem.className != undefined) {
            console.log(platform)
            console.log(clickedInvItem.querySelector("div").className)
            if(platform == 'basic' && clickedInvItem.querySelector("div").className != "button"){
                return
            }
            if(platform == "white_platform" && (clickedInvItem.querySelector("div").className != "green_button" && clickedInvItem.querySelector("div").className != "blue_button")){
                return
            }
            if(platform == "green_platform" && clickedInvItem.querySelector("div").className != "green_button"){
                return
            }
            if(platform == "blue_platform" && clickedInvItem.querySelector("div").className != "blue_button"){
                return
            }
            clickedInvItem.className = ''
            console.log(clickedInvItem.querySelector("div"))
            clickedInvItem.remove()
            PlaySound('../sounds/laser_send.mp3')
            xhr.open('POST', requestURL + '/action?action='+clickedInvItem.querySelector("div").className+':'+x+','+y+','+z+'&device=VR');
            console.log('POST', requestURL + '/action?action='+clickedInvItem.querySelector("div").className+':'+x+','+y+','+z+'&device=VR')
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
    let buttons = document.querySelectorAll(".black_button")
    buttons.forEach(element => {
        element.classList.add("buttonshow")
    });
}

function ShowPlatform(){
    let platform = document.querySelectorAll(".platform_button")
    platform.forEach(element => {
        element.classList.add("platform_button_show")
    })
}

function GreenShowPlatform(){
    let platform = document.querySelectorAll(".green_platform_button")
    platform.forEach(element => {
        element.classList.add("green_platform_button_show")
    })
}

function BlueShowPlatform(){
    let platform = document.querySelectorAll(".blue_platform_button")
    platform.forEach(element => {
        element.classList.add("blue_platform_button_show")
    })
}

