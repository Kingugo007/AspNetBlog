const nav = document.getElementsByClassName("nav")[0];
const paras = document.querySelectorAll(".short-text");
   paras.forEach(para => {
            var text = para.innerHTML;
            para.innerHTML = "";
            let words = text.split(" ");
            if (words.length >= 15) {
                for (let i = 0; i <= 15; i++) {
                    para.innerHTML += words[i] + " ";
                }
                para.innerHTML += ".....";
            }
           
        })

window.addEventListener("scroll", () => {
    if (window.pageYOffset > 60) {
        nav.classList.add("scrolled");
     
    } else {
        nav.classList.remove("scrolled");
        
    }
});