export let field = ""
export function SetField(info){
    info = info.toLowerCase()
    if (info.includes("floor")){ 
        field = info.replace("floor", "пол ")
    }
    else{
        field = info.replace("ceil", "потолок ")
    }
    document.getElementById("field").innerText = field
    let a = Array.prototype.slice.call(document.querySelector(".game_field").children)
    a.forEach(element => {
        if (element.classList.contains(info)) {
            element.classList.remove("hidden")
        }
        else if(!element.classList.contains(info)){
            element.classList.add("hidden")
        }
    });
}