document.addEventListener('click', function (e) {
   var  like = e.target;
    if (like.classList.contains('likeSection')) {
        console.log(like);
        const child = like.children;
        if (like.classList.contains('text-muted')) {
            for (let i = 0; i < child.length; i++) {
                child[i].classList.add("likeColor");
                child[i].classList.remove("text-muted");
            }


            like.classList.remove("text-muted");
            like.classList.add("likeColor");
        }
        else {

            for (let i = 0; i < child.length; i++) {
                child[i].classList.add("text-muted");
                child[i].classList.remove("likeColor");
            }


            like.classList.remove("likeColor");
            like.classList.add("text-muted");

        }
    }
    else if (like.classList.contains('Clike'))
    {
        like = like.parentNode;
        if (like.classList.contains('likeSection')) {
            console.log(like);
            const child = like.children;
            if (like.classList.contains('text-muted')) {
                for (let i = 0; i < child.length; i++) {
                    child[i].classList.add("likeColor");
                    child[i].classList.remove("text-muted");
                }


                like.classList.remove("text-muted");
                like.classList.add("likeColor");
            }
            else {

                for (let i = 0; i < child.length; i++) {
                    child[i].classList.add("text-muted");
                    child[i].classList.remove("likeColor");
                }


                like.classList.remove("likeColor");
                like.classList.add("text-muted");

            }
        }
    }
   
});

function hideLoadingDiv() {
    setTimeout(function () {
        document.getElementById('success-alerting').classList.add('hidden');
    }, 2000)
}