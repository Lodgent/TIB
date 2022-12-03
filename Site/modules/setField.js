export let field = ""
export function SetField(info){
    console.log("a")
    info = info.toLowerCase()
    document.getElementById("field").innerText = info
    field = info
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