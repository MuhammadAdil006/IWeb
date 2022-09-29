
//like button handling color
document.addEventListener('click', function (e) {
    var like = e.target;
    if (like.classList.contains('likeSection')) {
        console.log(like);
        //this will select the likes node upon which is clicked
        var likeSECTION = like.parentNode.parentNode.parentNode.parentNode.children[0].children[1];

      
        
        

        const child = like.children;
        console.log(child);
        //get post id and user id and send ajax to db also change the content of post likes
        var postId = child[0].value;
        var curUserId = $("#CurUserId").val();

        

        if (like.classList.contains('text-muted')) {
            LikeIncDec(likeSECTION, 1);
            for (let i = 0; i < child.length; i++) {
                child[i].classList.add("likeColor");
                child[i].classList.remove("text-muted");

            }

            addLikeToDB(postId, curUserId);
            like.classList.remove("text-muted");
            like.classList.add("likeColor");
        }
        else {

            for (let i = 0; i < child.length; i++) {
                child[i].classList.add("text-muted");
                child[i].classList.remove("likeColor");
            }
            LikeIncDec(likeSECTION, 2);
            //remove the like from db
            removelikeFromDb(postId, curUserId);
            like.classList.remove("likeColor");
            like.classList.add("text-muted");

        }
    }
    else if (like.classList.contains('Clike'))
    {
        like = like.parentNode;
        var likeSECTION = like.parentNode.parentNode.parentNode.parentNode.children[0].children[1];
        if (like.classList.contains('likeSection')) {
            console.log(like);
            const child = like.children;
            var postId = child[0].value;
            var curUserId = $("#CurUserId").val();
            if (like.classList.contains('text-muted')) {
                for (let i = 0; i < child.length; i++) {
                    child[i].classList.add("likeColor");
                    child[i].classList.remove("text-muted");
                }
                LikeIncDec(likeSECTION, 1);
                addLikeToDB(postId, curUserId);
                like.classList.remove("text-muted");
                like.classList.add("likeColor");
            }
            else {

                for (let i = 0; i < child.length; i++) {
                    child[i].classList.add("text-muted");
                    child[i].classList.remove("likeColor");
                }
                LikeIncDec(likeSECTION, 2);
                removelikeFromDb(postId, curUserId);
                like.classList.remove("likeColor");
                like.classList.add("text-muted");

            }
        }
    }
   
});
//this function will increment and decrement the value in likes
function LikeIncDec(likeSECTION, a) {

    var value = likeSECTION.innerHTML;
    var replaced = value.replace(/\D/g, '');
    if (a == 1) {
        replaced = parseInt(replaced) + 1;
    } else {
        replaced = parseInt(replaced) - 1;
    }
    likeSECTION.innerHTML = replaced + " " + "Likes";
        
}
//this function will remove record from db using ajax
function removelikeFromDb(postId, curUserId) {
    var formdata = new FormData();
    formdata.append("PostId", postId);
    formdata.append("UserId", curUserId);
    $.ajax({
        type: "POST",
        url: "/Post/removeLike",
        contentType: false,
        processData: false,
        data: formdata,

        success: function (data) {
            //document.getElementById("CreatepostForm").reset();
            //$("#closingPostModal").click();
            //$("#PostMessage").removeClass("d-none");
            //setTimeout(function () {
            //    $("#PostMessage").addClass('d-none');
            //}, 3000);
           

        },
        error: function () {
            //$("#closingPostModal").click();
            //$("#PostMessageNot").removeClass("d-none");
            //setTimeout(function () {
            //    $("#PostMessageNot").addClass('d-none');
            //}, 3000);
            alert("like remove error");

        }
    });
}
//this function will add record to db useing ajax
function addLikeToDB(postId, curUserId) {
    var formdata = new FormData();
    formdata.append("PostId", postId);
    formdata.append("UserId", curUserId);
    $.ajax({
        type: "POST",
        url: "/Post/AddLike",
        contentType: false,
        processData: false,
        data: formdata,

        success: function (data) {
           
            /*alert("like addded");*/

        },
        error: function () {
           
            alert("like error");

        }
    });

}
function hideLoadingDiv() {
    setTimeout(function () {
        document.getElementById('success-alerting').classList.add('hidden');
    }, 2000);
    //setTimeout(function () {
    //    document.getElementById('updateddb').classList.add('hidden');
    //}, 2000);
    const boxes = document.getElementsByClassName('a1');
    for (const box of boxes)
    {
        setTimeout(function () {
            box.classList.add('hidden');
        }, 2000);
    }
   

   

}

//create post function using ajax
$(document).ready(function () {
    console.log("document is loaded");
    var postMessageValidation = $("#validationForPostMessage");
    $('#CreatingPost').click(function () {
        
        var form_data = new FormData();
        //console.log(myFile);
        var files = document.getElementById("PostImage").files[0];


       
        //check if there is post message filed is empty or not
        form_data.append("UserId", $("#CurUserId").val());
        var pstmsg = $("#Postmsg").val();
        console.log(pstmsg);
        form_data.append("PostMessage", pstmsg);
        form_data.append("PostCategory", $("#PostCategory").val());
        form_data.append("image", files);
        console.log(form_data["PostMessage"]);

        if ( pstmsg.length != 0) {
            postMessageValidation.addClass("d-none");
            $.ajax({
                type: "POST",
                url: "/Post/CreatePost",
                contentType: false,
                processData: false,
                data: form_data,

                success: function (data) {
                    document.getElementById("CreatepostForm").reset();
                    $("#closingPostModal").click();
                    $("#PostMessage").removeClass("d-none");
                    setTimeout(function () {
                        $("#PostMessage").addClass('d-none');
                    }, 3000);

                },
                error: function () {
                    $("#closingPostModal").click();
                    $("#PostMessageNot").removeClass("d-none");
                    setTimeout(function () {
                        $("#PostMessageNot").addClass('d-none');
                    }, 3000);

                }
            });

        } else {
            postMessageValidation.removeClass("d-none");
        }
       
    });
})
