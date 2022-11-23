import { GiveEscapeButton } from '/main.js';
let level = 1
let levelH = 10
let levelW = 16
let windowH = 969

export function SetAllGameFields(position, shify){
  let firstUl = document.createElement('ul')
  firstUl.className = "game " + position + ' ' + "hidden"
  for (let i = 0; i <levelW; i++) {
      let ull = document.createElement('ul')
      for (let j = 0; j < levelH; j++) {
          let li = document.createElement('li');
          li.onclick = function() { GiveEscapeButton(j, shify, i) }
          li.style.setProperty('--element-height', Math.floor(windowH / levelH) + 'px')
          ull.append(li)
      }
      firstUl.append(ull)
  }
  document.body.children[0].append(firstUl)
}
