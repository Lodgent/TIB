const requestURL = 'http://26.100.4.13:5000'
let level = 1
let levelH = 9
let levelW = 15
let div = document.createElement('div')
let firstUl = document.createElement('ul')
firstUl.className = "game"
for (let i = 0; i <levelW; i++) {
    let ull = document.createElement('ul')
    for (let j = 0; j < levelH; j++) {
        let li = document.createElement('li');
        li.onclick = function() { POSTRequest(i, j) }
        ull.append(li)
    }
    firstUl.append(ull)
}
div.append(firstUl)

document.body.append(div)

setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();

        xhr.open('POST', 'http://26.100.4.13:5000/get_action?device=SITE');

        xhr.onload = () => {
            var a = JSON.parse(xhr.response)
            if(JSON.stringify(a) != '{"action":"Something went wrong"}'){
              var audio = new Audio('VineBoom.mp3');
              audio.loop = false;
              audio.play()
            }
        }
        xhr.send()
    })}, 1000)

function POSTRequest(i, j) {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();

        xhr.open('POST', 'http://26.100.4.13:5000/action?action='+i+' '+j+'&device=VR');
        xhr.send()
    })}