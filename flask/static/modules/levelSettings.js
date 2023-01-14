import { GiveEscapeButton } from '../main.js'
import * as level1 from './../levels/level1.js'
import * as level2 from './../levels/level2.js'
import * as level0 from './../levels/level0.js'
let windowH = 935
let windowW = 1421.8
let levels = [level0, level1, level2]
let levelsData = [{
    'floor': level0.floor,
    'ceil' : level0.ceil}, {
      'floor': level1.floor, 
      'ceil' : level1.ceil}, {
        'floor1': level2.floor1, 
        'ceil1' : level2.ceil1,
        'floor2': level2.floor2, 
        'ceil2' : level2.ceil2,
        'floor3': level2.floor3,
        'ceil3' : level2.ceil3}]
export let number = 1
export let activeLevel = levels[number - 1]

export function CreateLevel(){
  for (let key in levelsData[number - 1]){
    if (key.includes('floor')){
      SetAllGameFields(key, levelsData[number - 1][key], 0)
    }
    else{
      SetAllGameFields(key, levelsData[number - 1][key], 10)
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
            li.classList.add("block")
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
          li.style.setProperty('--element-height', windowH / map.length + 'px')
          li.style.setProperty('--element-width', windowW / map[i].length + 'px')
          ull.append(li)
      }
      firstUl.append(ull)
  }
  document.querySelector(".game_field").append(firstUl)
}

export function CompleteLevel(){
  let fields = document.querySelectorAll(".game")
  let mover = document.querySelector(".mover")
  let inventoryItems = document.querySelectorAll(".item_list li")
  let horizontalArrows = document.querySelector('.horizontal')
  let verticalArrows = document.querySelector('.vertical')
  horizontalArrows.classList.add("hidden")
  verticalArrows.classList.add("hidden")
  mover.classList.add("hidden")
  fields.forEach(element => {
      element.remove()
  });
  inventoryItems.forEach(element => {
      element.remove()
  });
  number += 1
  activeLevel = levels[number - 1]
  CreateLevel()
}
