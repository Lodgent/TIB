export function SetField(info){
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