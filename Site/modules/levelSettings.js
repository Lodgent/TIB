import { GiveEscapeButton } from '/main.js';
import * as level1 from '/levels/level1.js';
import * as level2 from '/levels/level2.js';
let windowH = 969
let windowW = 1416

let levels = [{
    'floor': level1.floor, 
    'ceil' : level1.ceil}, {
        'floor1': level2.floor1, 
        'ceil1' : level2.ceil1,
        'floor2': level2.floor2, 
        'ceil2' : level2.ceil2}]
let number = 2

export function CreateLevel(){
  for (let key in levels[number - 1]){
    if (key.includes('floor')){
      SetAllGameFields(key, levels[number - 1][key], 0)
    }
    else{
      SetAllGameFields(key, levels[number - 1][key], 10)
    }
  }
}

function SetAllGameFields(field, map, shify){
  let firstUl = document.createElement('ul')
  firstUl.className = "game " + field + ' ' + "hidden"
  for (let i = 0; i <map.length; i++) {
      let ull = document.createElement('ul')
      for (let j = 0; j < map[i].length; j++) {
          let li = document.createElement('li');
          if (map[i][j] == 'E'){
            li.onclick = function() { GiveEscapeButton(i, shify, j) }
          }
          else if(map[i][j] == 'D'){
            li.classList.add("water")
          }
          else if(map[i][j] == 'B'){
            li.classList.add("black_button")
          }
          else if(map[i][j] == 'P'){
            li.classList.add("platform_button")
            li.onclick = function() { GiveEscapeButton(i, shify, j, "white_platform") }
          }
          else if(map[i][j] == 'G'){
            li.classList.add("green_platform_button")
            li.onclick = function() { GiveEscapeButton(i, shify, j, "green_platform") }
          }
          else if(map[i][j] == 'b'){
            li.classList.add("blue_platform_button")
            li.onclick = function() { GiveEscapeButton(i, shify, j, "blue_platform") }
          }
          li.style.setProperty('--element-height', Math.floor(windowH / map.length) + 'px')
          li.style.setProperty('--element-width', Math.floor(windowW / map[i].length) + 'px')
          ull.append(li)
      }
      firstUl.append(ull)
  }
  document.body.children[0].append(firstUl)
}

export function CompleteLevel(){
  let fields = document.querySelectorAll(".game")
  let mover = document.querySelector(".mover")
  let inventoryItems = document.querySelectorAll(".item_list li")
  mover.classList.add("hidden")
  fields.forEach(element => {
      element.remove()
  });
  inventoryItems.forEach(element => {
      element.remove()
  });
  number += 1
  CreateLevel()
}
