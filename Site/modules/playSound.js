export function PlaySound(fileName){
    var audio = new Audio(fileName);
    audio.loop = false;
    audio.play()
  }