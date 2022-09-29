$(document).ready(function () {

    var searching = document.getElementById("SearchingWordsInBlogs");
    searching.addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            var UserId = $("#CurrentUserId").val();

            var General = $(
                "#flexCheckDefault").prop('checked');


            var Sports = $(
                "#flexCheckDefault4").prop('checked');
            var Gaming = $(
                "#flexCheckDefault5").prop('checked');
            var Hiking = $(
                "#flexCheckDefault5").prop('checked');

            var phrase = $(
                "#SearchingWordsInBlogs").val();
            console.log(phrase);
            var Formdata = new FormData();
            Formdata.append("General", General);
            Formdata.append("phrase", phrase);


            Formdata.append("Sports", Sports);
            Formdata.append("Gaming", Gaming);
            Formdata.append("Hiking", Hiking);
            Formdata.append("UserId", UserId);
            if (phrase.value != "") {
                $.ajax({
                    method: "Post",
                    url: "/Post/FilterPosts",
                    contentType: false,
                    processData: false,
                    data: Formdata,
                    success: function (data) {

                        data = JSON.parse(data);
                        if (data != "not found") {
                            var main = document.getElementById("Main_container");

                            DisplayResult(data, main);
                        } else {
                            var main = document.getElementById("Main_container");
                            var p = document.createElement("h3");
                            p.innerHTML = "No result found";
                            
                            var node = creatingPostNode(main);
                            main.append(p);
                            main.append(node);

                        }
                    }, error: function () {
                        alert("something went wrong try again");
                    }
                })
            }


        }
    });
    function creatingPostNode(main) {
        var copied = main.children[1].cloneNode(true);
        removeAllChildNodes(main);

        copied.setAttribute("class", "bg-white p-4 rounded shadow mt-3 d-none");
        return copied;
    }

    function DisplayResult(data, main) {

        var copied = main.children[1].cloneNode(true);
        copied.setAttribute("class", "bg-white p-4 rounded shadow mt-3");
        removeAllChildNodes(main);
        var count = 0;
        data.forEach(function (obj) {
            var copied1 = copied.cloneNode(true);

            var node = changeContents1(copied1, obj);
            count = count + 1;
            main.prepend(node);
        });
        if (count == 1) {
            var copied1 = copied.cloneNode(true);
            copied1.setAttribute("class", "bg-white p-4 rounded shadow mt-3 d-none");
            main.append(copied1);
            
        }
    }
    function changeContents1(copied, obj) {
        copied.children[0].setAttribute("value", obj["post"].PostId);
        copied.children[1].setAttribute("value", obj["post"].UserId);
        copied.children[2].children[0].children[0].setAttribute("src", "/" + obj["user"].ImageUrl);
        copied.children[2].children[0].children[1].children[0].innerHTML = obj["user"].FirstName + obj["user"].LastName;
        copied.children[2].children[0].children[1].children[1].innerHTML = obj["post"].PostDate;
        copied.children[3].children[0].children[0].innerHTML = obj["post"].PostMessage;
        if (obj["post"].ImageUrl == undefined) {

        } else {
            copied.children[3].children[0].children[1].setAttribute("src", "/" + obj["post"].ImageUrl);
            copied.children[3].children[0].children[1].setAttribute("class", "img-fluid rounded");
        }
        copied.children[3].children[1].children[0].children[1].innerHTML = obj["post"].NoOfLikes + " Likes";
        copied.children[3].children[1].children[1].children[0].children[0].children[0].children[0].innerHTML = obj["post"].NoOfComments + " Comments";
        copied.children[3].children[1].children[1].children[0].children[2].children[0].children[0].setAttribute("value", obj["post"].PostId);
        var here = copied.children[3].children[1].children[1].children[0];
        here.children[2].children[1].setAttribute("data-target", "#collapseExample_" + obj["post"].PostId);
        here.children[2].children[1].setAttribute("aria-controls", "collapseExample_" + obj["post"].PostId);
        here.children[2].children[1].children[0].setAttribute("value", obj["post"].PostId);
        here.children[3].setAttribute("id", "collapseExample_" + obj["post"].PostId);
        here = here.children[3];

        here = here.lastElementChild.lastElementChild;
        console.log(here);
        here.children[0].children[0].setAttribute("id", "CommentatorId_" + obj["post"].PostId)
        here.children[0].children[0].setAttribute("value", document.getElementById("CurrentUserId").value);
        here.children[0].children[1].setAttribute("id", "CommentatorPostId_" + obj["post"].PostId);
        here.children[0].children[1].setAttribute("value", obj["post"].PostId);
        var curUserImage = document.getElementById(
            "CurUserImageURL").src;
        here.children[0].children[2].setAttribute("src", curUserImage);
        here.children[1].setAttribute("id", "comment_Msg_" + obj["post"].PostId);
        here.children[2].setAttribute("id", "_" + obj["post"].PostId);

        return copied;
    }

    function removeAllChildNodes(parent) {
        while (parent.firstChild) {
            parent.removeChild(parent.firstChild);
        }
    }
});