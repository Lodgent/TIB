const requestURL = 'http://26.100.4.13:5000'
let level = 1
let levelH = 10
let levelW = 16

let clickedInvItem = ''
let activePosition = 'floor'

SetAllGameFields("ceil", 10)
SetAllGameFields("floor", 0)

let invul = document.createElement('ul')
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
            ull.append(li)
        }
        firstUl.append(ull)
    }
    document.body.children[0].append(firstUl)
}

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();

        xhr.open('POST', 'http://26.100.4.13:5000/get_action?device=SITE');

        xhr.onload = () => {
            let a = JSON.parse(xhr.response)
            let info = JSON.stringify(a).split(":")[1]
            info = info.slice(1, info.length - 2)
            if(info == 'GiveEscapeButton'){
                PlaySound('laser_send.mp3')
                let invli = document.createElement('li')
                invli.className = "not_active"
                invli.onclick = function() { ChangeActive(invli) }
                let invtext = document.createElement('p')
                invtext.textContent = "im button"
                invli.append(invtext)
                invul.append(invli)
            }
            if(info == 'Ceil' || info == 'Floor'){
                info = info.toLowerCase()
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
        }
        xhr.send()
    })}, 1000)

function POSTRequest(x, y, z) {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();
        if (clickedInvItem != '') {
            clickedInvItem.remove()
            clickedInvItem = ''
            PlaySound('laser_send.mp3')
            xhr.open('POST', 'http://26.100.4.13:5000/action?action=SpawnEscapeButton:'+x+','+y+','+z+'&device=VR');
            xhr.send()
        }
    })}

function ChangeActive(element){
    clickedInvItem.className = "not_active"
    clickedInvItem = element
    element.className = "active"
}