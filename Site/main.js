const requestURL = 'http://26.100.4.13:5000'
let level = 1
let levelH = 10
let levelW = 16
let windowH = 969
let xhr = new XMLHttpRequest();
let clickedInvItem = ''
let activePosition = 'floor'

SetAllGameFields("ceil", 10)
SetAllGameFields("floor", 0)

let invul = document.createElement('ul')
invul.className = "item_list"
document.body.children[1].append(invul)

function PlaySound(fileName){
    var audio = new Audio(fileName);
    audio.loop = false;
    audio.play()
}

function SetAllGameFields(position, shify){
    let firstUl = document.createElement('ul')
    firstUl.className = "game " + position + ' ' + "hidden"
    for (let i = 0; i <levelW; i++) {
        let ull = document.createElement('ul')
        for (let j = 0; j < levelH; j++) {
            let li = document.createElement('li');
            li.onclick = function() { POSTRequest(j, shify, i) }
            li.style.setProperty('--element-height', Math.floor(windowH / levelH) + 'px')
            ull.append(li)
        }
        firstUl.append(ull)
    }
    document.body.children[0].append(firstUl)
}

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        xhr.open('POST', 'http://26.100.4.13:5000/get_action?device=SITE');

        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            console.log(info)
            if(info == 'GiveEscapeButton'){
                PlaySound('laser_send.mp3')
                let invli = document.createElement('li')
                invli.className = "not_active"
                invli.onclick = function() { ChangeActive(invli) }
                let invtext = document.createElement('div')
                invli.append(invtext)
                invul.append(invli)
            }
            if(info == 'Ceil' || info == 'Floor'){
                info = info.toLowerCase()
                document.getElementById("field").innerText = info == 'floor' ? 'Пол' : 'Потолок'
                let a = Array.prototype.slice.call(document.body.children[0].children)
                a.forEach(element => {
                    if (element.classList.contains(info)) {
                        element.classList.remove("hidden")
                    }
                    else if(!element.classList.contains(info)){
                        element.classList.add("hidden")
                    }
                });
            }
            if(info == 'ActivatePlatform'){
                document.querySelector('.mover').classList.remove('hidden')
                let game_field = document.querySelector('.floor').children
                for (let index = 8; index < 10; index++) {
                    for (let j = 0; j < 2; j++) {
                        let element = game_field[index].childNodes[j]
                        element.classList.add("platform")
                    }
                }
            }
        }
        xhr.send()
    })}, 1000)

function POSTRequest(x, y, z) {
    return new Promise((resolve, reject) => {
        if (clickedInvItem != '') {
            clickedInvItem.remove()
            clickedInvItem = ''
            PlaySound('laser_send.mp3')
            xhr.open('POST', 'http://26.100.4.13:5000/action?action=SpawnEscapeButton:'+x+','+y+','+z+'&device=VR');
            xhr.send()
        }
    })}

function ChangeActive(element){
    if (element.className == "active"){
        clickedInvItem.className = "not_active"
        clickedInvItem = ""
    }
    else{
        clickedInvItem.className = "not_active"
        clickedInvItem = element
        element.className = "active"
    }
}


position = 9;
let timeout
let working = false
function MoveLeft(clr) {
    if(working) {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = document.querySelector('.floor').children
        for (let index = position; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.remove("platform")
            }
        }
        position--;
        for (let index = position - 1; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.add("platform")
            }
        }
        if(position == 6){
            clearInterval(timeout)
            working = false
            xhr.open('POST', 'http://26.100.4.13:5000/action?action=MovePlatform'+clr+'End&device=VR')
            xhr.send()
        }
    }, 800)
    xhr.open('POST', 'http://26.100.4.13:5000/action?action=MovePlatform'+clr+'Start&device=VR')
    xhr.send()
}

function MoveRight(clr) {
    if(working) {
        return
    }
    working = true
    timeout = setInterval(function() {
        let game_field = document.querySelector('.floor').children
        for (let index = position - 1; index < position; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.remove("platform")
            }
        }
        position++;
        for (let index = position; index < position + 1; index++) {
            for (let j = 0; j < 2; j++) {
                let element = game_field[index].childNodes[j]
                element.classList.add("platform")
            }
        }
        if(position == 9){
            clearInterval(timeout)
            working = false
            xhr.open('POST', 'http://26.100.4.13:5000/action?action=MovePlatform'+clr+'End&device=VR')
            xhr.send()
        }
    }, 800)
    xhr.open('POST', 'http://26.100.4.13:5000/action?action=MovePlatform'+clr+'Start&device=VR')
    xhr.send()
}