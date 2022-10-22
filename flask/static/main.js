const requestURL = 'http://26.100.4.13:5000'
let level = 1
let levelH = 10
let levelW = 16

let firstUl = document.createElement('ul')
firstUl.className = "game"
for (let i = 0; i <levelW; i++) {
    let ull = document.createElement('ul')
    for (let j = 0; j < levelH; j++) {
        let li = document.createElement('li');
        li.onclick = function() { POSTRequest(j, 0, i) }
        ull.append(li)
    }
    firstUl.append(ull)
}
document.body.children[0].append(firstUl)

let invul = document.createElement('ul')
document.body.children[1].append(invul)


setInterval(function GETRequest() {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();
        /*let invli = document.createElement('li')
        let invtext = document.createElement('p')
        invtext.textContent = "im button"
        invli.append(invtext)
        invul.append(invli)*/

        xhr.open('POST', 'http://26.100.4.13:5000/get_action?device=SITE');

        xhr.onload = () => {
            var a = JSON.parse(xhr.response)
            if(JSON.stringify(a) == '{"action":"GiveButton"}'){
                let text = document.createElement('p')
                text.textContent = "im button"
                document.body.children[1].append(text)
            }
        }
        xhr.send()
    })}, 1000)

function POSTRequest(x, y, z) {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();

        xhr.open('POST', 'http://26.100.4.13:5000/action?action=SpawnEscapeButton:'+x+','+y+','+z+'&device=VR');
        xhr.send()
    })}