import * as level from '/modules/levelSettings.js';
import { PlaySound } from "../modules/playSound.js";
import { TakeInventory, clickedInvItem } from './modules/inventory.js';
import { SetField, field } from './modules/setField.js';
import { SetPlatform } from './modules/platform.js';
import { CreateLevel, number} from './modules/levelSettings.js';

const requestURL = 'http://26.100.4.13:5000'
let xhr = new XMLHttpRequest();
let code = ""
const applicantForm = document.getElementById('menu')
applicantForm.addEventListener('submit', handleFormSubmit)
function handleFormSubmit(event) {
    event.preventDefault()
    code = applicantForm.elements.code.value
    GiveCode(code)
}

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/get_action?device=SITE&code=' + code);
        console.log(code)
        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            console.log(info)
            if(info == 'GiveEscapeButton'){ TakeInventory(document.querySelector(".item_list"), "button") }
            if(info == 'GiveGreenButton'){ TakeInventory(document.querySelector(".item_list"), "green_button") }
            if(info == 'GiveBlueButton'){ TakeInventory(document.querySelector(".item_list"), "blue_button") }
            if(info == 'GiveCeilButton'){ TakeInventory(document.querySelector(".item_list"), "ceil_button") }
            if(info == 'GiveExampleButton'){ TakeInventory(document.querySelector(".item_list"), "example_button") }
            if(info == 'Water'){ ShowWater() }
            if(info == 'SequenceButton'){ ShowButton() }
            if(info == 'Block') { ShowPlatform() }
            if(info == 'GreenBlock') { GreenShowPlatform() }
            if(info == 'BlueBlock') { BlueShowPlatform() }
            if((info.includes('Ceil') || info.includes('Floor')) && !info.includes('Button')){ SetField(info) }
            if(info == 'ActivatePlatform'){ SetPlatform() }
            if(info == 'CompleteLevel0') { if(number == 1) {level.CompleteLevel()} }
            if(info == 'CompleteLevel') { level.CompleteLevel() }
            if(info == 'Symbols') { TakeInventory(document.querySelector(".item_list"), "symbols") }
            if(info == "Session find!") { CorrectCode() }
        }
        xhr.send()
    })}, 1000)

    setInterval(function GETRequest() {

        const followCursor = () => {
          const el = document.querySelector('.active')
      
          window.addEventListener('mousemove', e => {
            if(el == null) {return}
            el.style.left = e.pageX + 'px'
            el.style.top = e.pageY + 'px'
          })
        }
      
        followCursor()
      
      }, 100)

function CorrectCode(){
    StartGameServer(code)
    document.querySelector(".menu").classList.add("hidden")
    document.querySelector(".creators").classList.add("hidden")
    document.querySelector(".game_field").classList.remove("hidden")
    document.querySelector(".inv_field").classList.remove("hidden")
    document.body.classList.add("flex")
    let html = document.querySelector(".main_back")
    html.classList.remove("main_back")
    html.classList.add("game_back")
    CreateLevel()
}

export function GiveEscapeButton(x, y, z, platform='basic') {
    return new Promise((resolve, reject) => {
        if (clickedInvItem.className != undefined) {
            if(platform == 'basic' && clickedInvItem.querySelector("div").className != "button" && clickedInvItem.querySelector("div").className != "example_button" && clickedInvItem.querySelector('div').className != 'ceil_button'){
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
            if(platform == 'basic' && clickedInvItem.querySelector("div").className == "ceil_button" && !field.includes('потолок')){
                return
            }
            clickedInvItem.className = ''
            console.log(clickedInvItem.querySelector("div"))
            clickedInvItem.remove()
            PlaySound('../sounds/laser_send.mp3')
            if(clickedInvItem.querySelector("div").className == "ceil_button"){
                xhr.open('POST', requestURL + '/action?action='+clickedInvItem.querySelector("div").className+''+field[field.length - 1]+':'+x+','+y+','+z+'&device=VR&code=' + code); 
            }
            else{
                xhr.open('POST', requestURL + '/action?action='+clickedInvItem.querySelector("div").className+':'+x+','+y+','+z+'&device=VR&code=' + code);
            }
            xhr.send()
        }
    })}

function StartGameServer(code){
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/action?action=start_game&device=VR&code=' + code)
        xhr.send()
    })}

export function GiveMovePlatform(action, direction) {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/action?action=MovePlatform'+direction + action +'&device=VR&code=' + code)
        xhr.send()
    })}

export function GiveCode(code) {
    return new Promise((resolve, reject) => {
        xhr.open('POST', requestURL + '/find_session?code='+code)
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

