$(document).ready(function (){

    document.addEventListener("click", function (e) {
        e = e.target;
        if (e.classList.contains("ActiveDeactiveBtn")) {
            var parent = e.parentElement;
            var usrid = parent.children[0].value;
            var isActive = parent.children[1].value;
            console.log(isActive);
            var str = ActiveDeactiveUser(usrid, isActive, e.children[1]);
           
            
        }
    });
    function ActiveDeactiveUser(usrid, isActive,child) {
        var data = new FormData();
        data.append("UserId", usrid);
        data.append("isActive", isActive);
        console.log("in active funmciton");
        $.ajax({
            type: "POST",
            url: "/Admin/ActiveOrDeactiveUser",
            contentType: false,
            processData: false,
            data: data,
            success: function (dat) {
                if (isActive == 1) {
                    child.innerHTML = "Activate";
                    child.parentElement.parentElement.children[1].setAttribute("value", 2);
                }
                else {

                    child.innerHTML = "Deactivate";
                    child.parentElement.parentElement.children[1].setAttribute("value", 1);
                }
            },
            error: function () {
                alert("ajax error");
            }
        });
    }
});