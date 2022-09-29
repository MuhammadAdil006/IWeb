$(document).ready(function () {
    document.addEventListener('click', function (e) {
        var obj = e.target;
        if (obj.classList.contains("DeletePost")) {
            DeletePost(obj);

        } else if (obj.classList.contains("EditPost")) {
            EditPost(obj);
        } else if (obj.classList.contains("PostEditbtn")) {
            EditPostToDb(obj);
        } else if (obj.classList.contains("ReportPost")) {
            ReportPostToDb(obj);
        }
    });
    function ReportPostToDb(obj) {
        var postId = obj.parentElement.parentElement.parentElement.parentElement.children[0].value;
        var UserId = obj.parentElement.parentElement.parentElement.parentElement.children[1].value;
        var CurrentUserId = document.getElementById("CurrentUserId").value;
        console.log(postId); console.log(UserId);
        var f = new FormData();
        var BlogsSection = obj.parentElement.parentElement.parentElement.parentElement.parentElement;
        var currentBlog = obj.parentElement.parentElement.parentElement.parentElement;

        //report blog to post
    }
    function EditPost(obj) {
        var postId = obj.parentElement.parentElement.parentElement.parentElement.children[0].value;
        var UserId = obj.parentElement.parentElement.parentElement.parentElement.children[1].value;
        var CurrentUserId = document.getElementById("CurrentUserId").value;
        console.log(postId); console.log(UserId);
        var f = new FormData();
        var BlogsSection = obj.parentElement.parentElement.parentElement.parentElement.parentElement;
        var currentBlog = obj.parentElement.parentElement.parentElement.parentElement;
        console.log(currentBlog);
        f.append("PostId", postId);
        if (UserId == CurrentUserId) {
            console.log("post editing");
            changeBlogNode(BlogsSection, currentBlog);
        }
        else {
            var alertForDeletion = document.createElement("div");
            alertForDeletion.innerHTML = "Sorry You can not edit this Post.";
            alertForDeletion.setAttribute("class", "alert alert-danger");
            alertForDeletion.setAttribute("role", "alert");
            currentBlog.prepend(alertForDeletion);
            setTimeout(function () {
                currentBlog.removeChild(alertForDeletion);
            }, 2000);
        }
    }



    //utility functions
    //this function will change the blog node to edit form
    function changeBlogNode(parent, CurrentBlog) {
        var PostNode = CurrentBlog.children[3];
        var msg = PostNode.children[0].children[0].innerHTML;
        var textArea = document.createElement("textarea");
        textArea.setAttribute("class", "form-control");
        textArea.setAttribute("rows", "3");
        textArea.innerHTML = msg;
        PostNode.children[0].replaceChild(textArea, PostNode.children[0].children[0]);
        if (PostNode.children[0].childNodes.length > 1) {
            //it also contains image

        } else {
            //it does not contains image

        }
        var EditPostBtn = document.createElement("button");
        EditPostBtn.setAttribute("type", "button");
        EditPostBtn.setAttribute("class", "btn btn-primary rounded-pill my-auto PostEditbtn");
        EditPostBtn.innerHTML = "Save Post";
        PostNode.insertBefore(EditPostBtn, PostNode.children[1]);

    }
    function DeletePost(obj) {
        var postId = obj.parentElement.parentElement.parentElement.parentElement.children[0].value;
        var UserId = obj.parentElement.parentElement.parentElement.parentElement.children[1].value;
        var CurrentUserId = document.getElementById("CurrentUserId").value;
        var f = new FormData();
        var BlogsSection = obj.parentElement.parentElement.parentElement.parentElement.parentElement;
        var currentBlog = obj.parentElement.parentElement.parentElement.parentElement;
        f.append("PostId", postId);
        if (UserId == CurrentUserId) {
            //can be deleted
            console.log("post deletion");
           
            //delete the post using ajax
            $.ajax({
                type: "Post",
                url: "/Post/DeletePost",
                contentType: false,
                processData: false,
                data: f,
                success: function (data) {
                    console.log(data);
                    if (data == true) {
                        //delete also at runtime

                        
                        console.log("postDeleted");
                        //create alert and then display also then delete after 2 seconds

                        var alertForDeletion = document.createElement("div");
                        alertForDeletion.innerHTML = "Your post has been deleted successfully.";
                        alertForDeletion.setAttribute("class", "alert alert-success");
                        alertForDeletion.setAttribute("role", "alert");
                        BlogsSection.replaceChild(alertForDeletion,currentBlog);
                       
                        setTimeout(function () {
                            BlogsSection.removeChild(alertForDeletion);
                        }, 2000);
                    } else {
                        console.log("postIsNotDeleted");
                        var alertForDeletion = document.createElement("div");
                        alertForDeletion.innerHTML = "Sorry,Your post is not deleted.";
                        alertForDeletion.setAttribute("class", "alert alert-danger");
                        alertForDeletion.setAttribute("role", "alert");
                        currentBlog.prepend(alertForDeletion);
                        setTimeout(function () {
                            currentBlog.removeChild(alertForDeletion);
                        }, 2000);
                    }
                    

                },
                error: function (e) {
                    alert(e);
                    alert("getting Post deletion error Reload the page and try again");

                }
            });
        } else {
            var alertForDeletion = document.createElement("div");
            alertForDeletion.innerHTML = "Sorry You can not delete this Post.";
            alertForDeletion.setAttribute("class", "alert alert-danger");
            alertForDeletion.setAttribute("role", "alert");
            currentBlog.prepend(alertForDeletion);
            setTimeout(function () {
                currentBlog.removeChild(alertForDeletion);
            }, 2000);
        }
    }
});